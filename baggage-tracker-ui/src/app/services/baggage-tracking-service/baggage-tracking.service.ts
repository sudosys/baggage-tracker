import { Injectable, signal, WritableSignal } from '@angular/core';
import {
	BaggageStatus,
	BaggageTrackerClient,
	QrCodeScanResponse
} from '../../../../open-api/bt-api.client';
import { tap } from 'rxjs';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { FileDownload } from '../../models/file-download.model';
import { Page } from '../../enums/page.enum';

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
	fileDownload: WritableSignal<FileDownload | undefined> = signal(undefined);

	scanQrCode(qrCodeData: string) {
		return this.btClient.qrCodeScan(qrCodeData).pipe(
			tap(async (result: QrCodeScanResponse) => {
				this.qrCodeScanResult.set(result);
				await this.router.navigateByUrl(Page.PostScan);
			})
		);
	}

	setBaggageStatus(status: BaggageStatus) {
		return this.btClient
			.baggageStatusPOST(this.qrCodeScanResult()?.baggage?.baggageId, status)
			.pipe(
				tap(async () => {
					this.messageService.add({
						severity: 'success',
						summary: 'Status updated',
						detail: 'Status updated successfully.'
					});
					await this.router.navigateByUrl(Page.Home);
				})
			);
	}

	getBaggageQrCodes(flightNumber: string) {
		return this.btClient.baggageQrCode(flightNumber).pipe(
			tap((response) => {
				const file: FileDownload = {
					name: response.fileName ?? '',
					objectUrl: URL.createObjectURL(response.data)
				};
				this.fileDownload.set(file);
				this.messageService.add({
					severity: 'success',
					summary: 'Download ready',
					detail: 'QR codes are ready to download.'
				});
			})
		);
	}

	trackBaggages(userId: number | undefined) {
		return this.btClient.baggageStatusGET(userId);
	}

	trackBaggagesByFlight(flightNumber: string) {
		return this.btClient.baggageTracking(flightNumber);
	}
}
