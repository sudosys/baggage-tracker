import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { InternalDataService } from '../services/internal-data.service';
import { QrCodeService } from '../services/qr-code.service';

@Component({
    selector: 'app-scan-qr-code',
    templateUrl: './scan-qr-code.component.html',
    styleUrls: ['./scan-qr-code.component.css'],
})
export class ScanQrCodeComponent implements OnInit {
    qrCodeContent: string;
    scannerActive: boolean;
    scanStatus: string;
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
            this.scanStatus = 'Looking for QR code';
        }, 1000);
    }

    qrCodeScanSuccess(Ubc: string) {
        this.qrCodeService.sendQRCodeContent(Ubc, this.dataService.getPassangerHash()).subscribe((data: any) => {
            this.baggagePossession = data;
        });
        setTimeout(() => {
            this.dataService.setUbc(Ubc);
            this.dataService.setBaggagePossession(this.baggagePossession);
        }, 150);
        this.scannerActive = false;
        this.scanStatus = '';
        this.route.navigateByUrl('/after-scan');
    }
}
