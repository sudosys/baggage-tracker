import { apiURL } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  getUserData(username: string, flightnum: string): any {

    return this.http.get<any>(apiURL + "/User/GetUserData?Username=" + username + "&Flightnum=" + flightnum);

  }
}
