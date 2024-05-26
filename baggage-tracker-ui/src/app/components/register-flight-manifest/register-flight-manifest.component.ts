import { Component, OnDestroy, ViewChild } from '@angular/core';
import { FileSelectEvent, FileUpload } from 'primeng/fileupload';
import { FlightService } from '../../services/flight-service/flight.service';
import { FileDownload } from '../../models/file-download.model';

@Component({
	selector: 'app-register-flight-manifest',
	templateUrl: './register-flight-manifest.component.html',
	styleUrl: './register-flight-manifest.component.scss'
})
export class RegisterFlightManifestComponent implements OnDestroy {
	constructor(private flightService: FlightService) {}

	@ViewChild('fileUploader') fileUploader: FileUpload;

	uploadedManifests: File[] = [];
	manifestFile: FileDownload | undefined;
	processInProgress = false;

	ngOnDestroy(): void {
		this.disposeObject();
	}

	onSelectManifest(e: FileSelectEvent) {
		this.uploadedManifests = e.currentFiles;
	}

	async onClickSendManifests() {
		this.processInProgress = true;

		const registrationResponses$ = await this.flightService.registerFlightManifest(
			this.uploadedManifests
		);

		registrationResponses$.subscribe((fileResponse) => {
			this.manifestFile = <FileDownload>{
				name: fileResponse.fileName,
				objectUrl: URL.createObjectURL(fileResponse.data)
			};
			this.processInProgress = false;
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

	disposeObject() {
		if (this.manifestFile) {
			URL.revokeObjectURL(this.manifestFile.objectUrl);
		}
		this.manifestFile = undefined;
	}
}
