import { Injectable, signal } from '@angular/core';
import {
	BaggageTrackerClient,
	QrCodeScanResult
} from '../../../../open-api/bt-api.client';
import { tap } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
	providedIn: 'root'
})
export class BaggageTrackingService {
	constructor(
		private btClient: BaggageTrackerClient,
		private router: Router
	) {}

	qrCodeScanResult = signal(QrCodeScanResult.UnknownError);

	scanQrCode(qrCodeData: string) {
		return this.btClient.qrCodeScan(qrCodeData).pipe(
			tap(async (result: QrCodeScanResult) => {
				this.qrCodeScanResult.set(result);
				await this.router.navigateByUrl('post-scan');
			})
		);
	}
}
