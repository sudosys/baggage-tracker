import { Component, OnInit } from '@angular/core';
import { QrCodeService } from '../services/qr-code.service';


@Component({
  selector: 'app-generate-qr-code',
  templateUrl: './generate-qr-code.component.html',
  styleUrls: ['./generate-qr-code.component.css']
})
export class GenerateQrCodeComponent implements OnInit {

  flightNumber: string;

  constructor(private qrCodeService: QrCodeService) { }

  ngOnInit(): void {
  }

  generateOnClick() {
    
    if (this.flightNumber === undefined) { return; };

    this.qrCodeService.generateQRCode(this.flightNumber).subscribe();
    
  }

}
