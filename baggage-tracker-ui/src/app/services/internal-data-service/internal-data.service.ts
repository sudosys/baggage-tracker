import { Injectable } from '@angular/core';
import { User } from '../../models/user.model';

@Injectable({
    providedIn: 'root',
})
export class InternalDataService {
    _userData: User[];

    setUserData(userData: User[]) {
        this._userData = userData;
    }

    getUserData() {
        return this._userData;
    }

    ////////////////

    _isAdmin: boolean;

    getIsAdmin() {
        return this._isAdmin;
    }

    setIsAdmin(isAdmin: boolean) {
        this._isAdmin = isAdmin;
    }

    ////////////////

    _passengerHash: string;

    getPassengerHash() {
        return this._passengerHash;
    }

    setPassengerHash(passengerHash: string) {
        this._passengerHash = passengerHash;
    }

    ////////////////

    _ubc: string;

    getUbc() {
        return this._ubc;
    }

    setUbc(ubc: string) {
        this._ubc = ubc;
    }

    ////////////////

    _baggagePossession: boolean;

    getBaggagePossession() {
        return this._baggagePossession;
    }

    setBaggagePossession(baggagePossesion: boolean) {
        this._baggagePossession = baggagePossesion;
    }

    constructor() {}
}
