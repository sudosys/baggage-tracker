import { UserService } from './../services/user.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { InternalDataService } from './../services/internal-data.service';

interface DemoUserEntry {
  username: string,
  flightnumber: string
}

export interface User {
  id: string,
  name: string,
  surname: string,
  passangerHash: string,
  role: string
}

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css']
})
export class MainPageComponent implements OnInit {

  username: string;
  demoUsers: DemoUserEntry[] = [ {username: "Zeynep Dilara Kenar", flightnumber: "TK5091"},
                                 {username: "Ahmet Taha Ergin", flightnumber: "TK1864"}, 
                                 {username: "Airport Staff", flightnumber: "NA"} ];

  userData: User[];
  isAdmin: boolean;

  constructor(private router: Router, private userService: UserService, private dataService: InternalDataService) {}

  ngOnInit(): void {
    this.userSelection(["Zeynep Dilara Kenar", "TK5091"])
  }

  navigateScanQrCode() {
    this.router.navigateByUrl('/scan-qr-code');
  }

  navigateGenerateQrCode() {
    this.router.navigateByUrl('/generate-qr-code');
  }

  navigateTrackBaggage() {
    this.router.navigateByUrl('/track-baggage');
  }
  
  navigateGetHelp() {
    this.router.navigateByUrl('/get-help');
  }

  userSelection(user: any) {
    this.userData = [];
    this.userService.getUserData(user[0], user[1]).subscribe((data: any) => {
        this.userData = (data.map((element: any) => {
          return {
              id: element[0],
              name: element[1],
              surname: element[2],
              passangerHash: element[3],
              role: element[4],
            }
        }))
    })
    setTimeout(() => {
      this.isAdmin = this.userData[0].role === '1' ? true : false;
      this.dataService.setUserData(this.userData);
      this.dataService.setIsAdmin(this.isAdmin);
      this.dataService.setPassangerHash(this.userData[0].passangerHash);
    }, 150);
  }

}
