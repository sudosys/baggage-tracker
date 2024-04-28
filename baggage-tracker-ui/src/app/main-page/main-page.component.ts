import { UserService } from '../services/user-service/user.service';
import { Component, OnInit } from '@angular/core';
import { InternalDataService } from '../services/internal-data-service/internal-data.service';
import { DemoUser } from '../models/demo-user.model';
import { User } from '../models/user.model';

@Component({
    selector: 'app-main-page',
    templateUrl: './main-page.component.html',
    styleUrls: ['./main-page.component.css'],
})
export class MainPageComponent implements OnInit {
    demoUsers: DemoUser[] = [
        { username: 'Begüm Özay', flightNumber: 'TK5091' },
        { username: 'Ahmed Taha Ergin', flightNumber: 'TK1864' },
        { username: 'Airport Staff' },
    ];

    userData: User[];
    isAdmin: boolean;

    constructor(
        private userService: UserService,
        private dataService: InternalDataService
    ) {}

    ngOnInit(): void {
        this.userSelection(['Ahmed Taha Ergin', 'TK1864']);
    }

    userSelection(user: any) {
        this.userData = [];
        this.userService.getUserData(user[0], user[1]).subscribe((data: any) => {
            this.userData = data.map((element: any) => {
                return {
                    id: element[0],
                    name: element[1],
                    surname: element[2],
                    passengerHash: element[3],
                    role: element[4],
                };
            });
        });
        setTimeout(() => {
            this.isAdmin = this.userData[0].role === '1';
            this.dataService.setUserData(this.userData);
            this.dataService.setIsAdmin(this.isAdmin);
            this.dataService.setPassengerHash(this.userData[0].passengerHash);
        }, 150);
    }
}
