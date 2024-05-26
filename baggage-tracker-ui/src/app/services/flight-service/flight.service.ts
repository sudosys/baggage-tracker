import { Injectable } from '@angular/core';
import { BaggageTrackerClient, FlightManifest } from '../../../../open-api/bt-api.client';

@Injectable({
	providedIn: 'root'
})
export class FlightService {
	constructor(private btClient: BaggageTrackerClient) {}

	async registerFlightManifest(manifests: File[]) {
		const promise = manifests.map(
			async (manifest) => await this.readManifestFile(manifest)
		);
		const deserializedManifests = await Promise.all(promise);
		return this.btClient.registerManifest(deserializedManifests);
	}

	async readManifestFile(file: File) {
		const fileContent = await file.text();

		return JSON.parse(fileContent) as FlightManifest;
	}
}
