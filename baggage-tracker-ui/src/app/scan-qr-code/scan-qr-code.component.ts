import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { InternalDataService } from '../services/internal-data-service/internal-data.service';
import { QrCodeService } from '../services/qr-code-service/qr-code.service';

@Component({
    selector: 'app-scan-qr-code',
    templateUrl: './scan-qr-code.component.html',
    styleUrls: ['./scan-qr-code.component.css'],
})
export class ScanQrCodeComponent implements OnInit {
    scannerActive: boolean;
    scanStatusText: string;
    baggagePossession: boolean;

    constructor(
        private route: Router,
        private dataService: InternalDataService,
        private qrCodeService: QrCodeService
    ) {}

    ngOnInit(): void {
        this.scannerActive = true;
        this.baggagePossession = false;
        setTimeout(() => {
            this.scanStatusText = 'Looking for QR code...';
        }, 1000);
    }

    async onQrCodeScanSuccess(Ubc: string) {
        this.qrCodeService.sendQRCodeContent(Ubc, this.dataService.getPassengerHash()).subscribe((data: any) => {
            this.dataService.setBaggagePossession(data);
        });
        setTimeout(() => {
            this.dataService.setUbc(Ubc);
        }, 150);
        this.scannerActive = false;
        this.scanStatusText = '';
        await this.route.navigateByUrl('/after-scan');
    }
}
