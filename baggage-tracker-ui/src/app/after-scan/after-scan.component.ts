import { BaggageService } from './../services/baggage.service';
import { Component, OnInit } from '@angular/core';
import { InternalDataService } from '../services/internal-data.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-after-scan',
    templateUrl: './after-scan.component.html',
    styleUrls: ['./after-scan.component.css'],
})
export class AfterScanComponent implements OnInit {
    baggageBelongsToYou: string =
        "Looks like you retrieved your baggage!\nYou can mark it as 'I received my baggage' down below.";
    baggageNotYours: string = "This baggage doesn't belong to you!";
    staffSelectAction: string = 'Please select the appropriate action\naccording to the situation';

    baggagePossesion: boolean;
    isAdmin: boolean;

    constructor(
        private dataService: InternalDataService,
        private router: Router,
        private baggageService: BaggageService
    ) {}

    ngOnInit(): void {
        setTimeout(() => {
            this.baggagePossesion = this.dataService.getBaggagePossession();
            this.isAdmin = this.dataService.getIsAdmin();
        }, 150);
    }

    navigateToMainMenu() {
        this.router.navigateByUrl('/');
    }

    markBaggageAsInThePlane() {
        this.baggageService.setBaggageStatus(this.dataService.getUbc(), '0').subscribe();
        this.navigateToMainMenu();
    }

    markBaggageAsReceived() {
        this.baggageService.setBaggageStatus(this.dataService.getUbc(), '1').subscribe();
        this.navigateToMainMenu();
    }

    markBaggageAsInTheLostOffice() {
        this.baggageService.setBaggageStatus(this.dataService.getUbc(), '2').subscribe();
        this.navigateToMainMenu();
    }

    markBaggageAsWaitingForTransfer() {
        this.baggageService.setBaggageStatus(this.dataService.getUbc(), '3').subscribe();
        this.navigateToMainMenu();
    }

    markBaggageAsUnloaded() {
        this.baggageService.setBaggageStatus(this.dataService.getUbc(), '4').subscribe();
        this.navigateToMainMenu();
    }
}
