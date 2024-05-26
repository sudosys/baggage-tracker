import { Component, OnDestroy, ViewChild } from '@angular/core';
import { FileSelectEvent, FileUpload } from 'primeng/fileupload';
import { FlightService } from '../../services/flight-service/flight.service';

@Component({
	selector: 'app-register-flight-manifest',
	templateUrl: './register-flight-manifest.component.html',
	styleUrl: './register-flight-manifest.component.scss'
})
export class RegisterFlightManifestComponent implements OnDestroy {
	constructor(protected flightService: FlightService) {}

	@ViewChild('fileUploader') fileUploader: FileUpload;

	uploadedManifests: File[] = [];
	inProgress = false;

	ngOnDestroy(): void {
		this.disposeObject();
	}

	onSelectManifest(e: FileSelectEvent) {
		this.uploadedManifests = e.currentFiles;
	}

	async onClickSendManifests() {
		this.inProgress = true;

		const registrationResponses$ = await this.flightService.registerFlightManifest(
			this.uploadedManifests
		);

		registrationResponses$.subscribe({
			error: () => this.onRegistrationFail(),
			next: () => (this.inProgress = false)
		});
	}

	onClickRemoveFile(index: number) {
		this.fileUploader.removeUploadedFile(index);
		this.uploadedManifests.splice(index, 1);
	}

	onClickCancel() {
		this.fileUploader.clear();
		this.uploadedManifests = [];
	}

	onRegistrationFail() {
		this.inProgress = false;
		this.uploadedManifests = [];
	}

	disposeObject() {
		const fileDownload = this.flightService.fileDownload();
		if (fileDownload) {
			URL.revokeObjectURL(fileDownload.objectUrl);
		}
		this.flightService.fileDownload.set(undefined);
	}
}
