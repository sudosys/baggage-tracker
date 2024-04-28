import { apiURL } from '../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
    providedIn: 'root',
})
export class QrCodeService {
    constructor(private http: HttpClient) {}

    generateQRCode(flightnum: string): any {
        return this.http.get<any>(apiURL + '/QRCodeGenerator/GenerateQR?Flightnum=' + flightnum);
    }

    sendQRCodeContent(Ubc: string, passengerHash: string): any {
        return this.http.get(apiURL + '/Baggage/CheckBaggagePossession?Ubc=' + Ubc + '&PassengerHash=' + passengerHash);
    }
}
