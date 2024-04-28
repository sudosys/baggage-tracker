import { Component, OnInit } from '@angular/core';
import { BaggageService } from '../services/baggage-service/baggage.service';
import { InternalDataService } from '../services/internal-data-service/internal-data.service';
import { Baggage } from '../models/baggage.model';

@Component({
    selector: 'app-track-baggage',
    templateUrl: './track-baggage.component.html',
    styleUrls: ['./track-baggage.component.css'],
})
export class TrackBaggageComponent implements OnInit {
    baggageList: Baggage[];

    constructor(
        private baggageService: BaggageService,
        private dataService: InternalDataService
    ) {}

    ngOnInit(): void {
        this.baggageList = [
            {
                flightNumber: 'TK5091',
                tagNumber: '0TK546617',
                status: 'Unloaded from the plane',
            },
            {
                flightNumber: 'TK5091',
                tagNumber: '0TK143394',
                status: 'In the lost office',
            },
        ];
    }
}
