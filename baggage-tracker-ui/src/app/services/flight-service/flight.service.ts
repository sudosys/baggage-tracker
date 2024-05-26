import { Injectable, signal, WritableSignal } from '@angular/core';
import { BaggageTrackerClient, FlightManifest } from '../../../../open-api/bt-api.client';
import { FileDownload } from '../../models/file-download.model';
import { tap } from 'rxjs';

@Injectable({
	providedIn: 'root'
})
export class FlightService {
	constructor(private btClient: BaggageTrackerClient) {}

	fileDownload: WritableSignal<FileDownload | undefined> = signal(undefined);

	async registerFlightManifest(manifests: File[]) {
		const promise = manifests.map(
			async (manifest) => await this.readManifestFile(manifest)
		);
		const deserializedManifests = await Promise.all(promise);
		return this.btClient.registerManifest(deserializedManifests).pipe(
			tap((fileResponse) => {
				const fileDownload = <FileDownload>{
					name: fileResponse.fileName,
					objectUrl: URL.createObjectURL(fileResponse.data)
				};

				this.fileDownload.set(fileDownload);
			})
		);
	}

	private async readManifestFile(file: File) {
		const fileContent = await file.text();

		return JSON.parse(fileContent) as FlightManifest;
	}

	getActiveFlights() {
		return this.btClient.activeFlight();
	}
}
