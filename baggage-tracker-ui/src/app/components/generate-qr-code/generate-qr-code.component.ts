import { Component, HostListener, OnDestroy } from '@angular/core';
import { BaggageTrackingService } from '../../services/baggage-tracking-service/baggage-tracking.service';

@Component({
	selector: 'app-generate-qr-code',
	templateUrl: './generate-qr-code.component.html',
	styleUrl: './generate-qr-code.component.scss'
})
export class GenerateQrCodeComponent implements OnDestroy {
	constructor(protected btService: BaggageTrackingService) {}

	protected flightNumber: string;
	inProgress = false;

	ngOnDestroy(): void {
		this.disposeFileDownload();
	}

	@HostListener('document:keyup.enter', ['$event'])
	onClickGenerate() {
		if (!this.flightNumber) return;
		this.inProgress = true;
		this.btService
			.getBaggageQrCodes(this.flightNumber)
			.subscribe(() => (this.inProgress = false));
	}

	private disposeFileDownload() {
		const file = this.btService.fileDownload();
		if (file) {
			URL.revokeObjectURL(file.objectUrl);
			this.btService.fileDownload.set(undefined);
		}
	}
}
