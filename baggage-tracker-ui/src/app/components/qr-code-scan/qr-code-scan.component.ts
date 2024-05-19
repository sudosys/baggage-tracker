import { Component } from '@angular/core';
import { BaggageTrackingService } from '../../services/baggage-tracking-service/baggage-tracking.service';
import { UserService } from '../../services/user-service/user.service';
@Component({
	selector: 'app-qr-code-scan',
	templateUrl: './qr-code-scan.component.html',
	styleUrl: './qr-code-scan.component.scss'
})
export class QrCodeScanComponent {
	constructor(private baggageTrackingService: BaggageTrackingService) {}

	scanEnabled = false;
	scanSuccessful = false;
	scanComplete = false;
	noCameraAvailable = false;

	protected onCameraFound() {
		this.scanEnabled = true;
	}

	protected onCameraNotFound() {
		this.noCameraAvailable = true;
	}

	protected onScanComplete(qrCodeData: string | undefined) {
		if (!qrCodeData) return;

		this.scanSuccessful = true;
		this.scanComplete = true;

		this.baggageTrackingService.scanQrCode(qrCodeData).subscribe();
	}

	protected onScanFailed() {
		this.scanSuccessful = true;
	}

	protected readonly UserService = UserService;
}
