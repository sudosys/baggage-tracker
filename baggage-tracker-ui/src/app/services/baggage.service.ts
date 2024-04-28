import { apiURL } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class BaggageService {

  headers: any;

  constructor(private http: HttpClient) { 
  }

  checkBaggageStatus(passangerHash: string): any {

    return this.http.get<any>(apiURL + "/Baggage/CheckBaggageStatus?PassangerHash=" + passangerHash);

  }

  setBaggageStatus(ubc: string, status: string) {

    return this.http.get(apiURL + "/Baggage/SetBaggageStatus?Ubc=" + ubc + "&Status=" + status);

  }

}
