import { Component, OnInit } from '@angular/core';
import { BaggageService } from './../services/baggage.service';
import { InternalDataService } from './../services/internal-data.service';

interface Baggage {
  flightNumber: string,
  tagnumber: string,
  status: string
}

@Component({
  selector: 'app-track-baggage',
  templateUrl: './track-baggage.component.html',
  styleUrls: ['./track-baggage.component.css']
})
export class TrackBaggageComponent implements OnInit {

  baggages: Baggage[];

  constructor(private baggageService: BaggageService, private dataService: InternalDataService) { }

  ngOnInit(): void {
    this.baggageService.checkBaggageStatus(this.dataService.getUserData()[0].passangerHash).subscribe((data:any) => {
      this.baggages = (data.map((element: any) => {
        return {
          flightNumber: element[0],
          tagnumber: element[1],
          status: element[2],
        }
      }))
    });
  }

}
