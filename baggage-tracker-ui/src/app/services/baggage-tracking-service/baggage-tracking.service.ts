import { Injectable, signal, WritableSignal } from '@angular/core';
import {
	BaggageStatus,
	BaggageTrackerClient,
	QrCodeScanResponse
} from '../../../../open-api/bt-api.client';
import { catchError, of, tap } from 'rxjs';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';

@Injectable({
	providedIn: 'root'
})
export class BaggageTrackingService {
	constructor(
		private btClient: BaggageTrackerClient,
		private router: Router,
		private messageService: MessageService
	) {}

	qrCodeScanResult: WritableSignal<QrCodeScanResponse | undefined> = signal(undefined);

	scanQrCode(qrCodeData: string) {
		return this.btClient.qrCodeScan(qrCodeData).pipe(
			tap(async (result: QrCodeScanResponse) => {
				this.qrCodeScanResult.set(result);
				await this.router.navigateByUrl('post-scan');
			})
		);
	}

	setBaggageStatus(status: BaggageStatus) {
		return this.btClient
			.baggageStatus(this.qrCodeScanResult()?.baggage?.baggageId, status)
			.pipe(
				catchError((error: Error) => {
					this.messageService.add({
						severity: 'error',
						summary: 'Error',
						detail: error.message
					});
					return of();
				}),
				tap(async () => {
					this.messageService.add({
						severity: 'success',
						summary: 'Status updated',
						detail: 'Status updated successfully.'
					});
					await this.router.navigateByUrl('home');
				})
			);
	}
}
