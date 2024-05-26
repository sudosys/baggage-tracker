import { Component, OnInit } from '@angular/core';
import { FlightService } from '../../services/flight-service/flight.service';
import { ActiveFlight } from '../../../../open-api/bt-api.client';

@Component({
	selector: 'app-active-flights',
	templateUrl: './active-flights.component.html',
	styleUrl: './active-flights.component.scss'
})
export class ActiveFlightsComponent implements OnInit {
	constructor(private flightService: FlightService) {}

	activeFlights: ActiveFlight[] = [];
	inProgress = false;

	ngOnInit(): void {
		this.inProgress = true;
		this.flightService.getActiveFlights().subscribe({
			error: () => (this.inProgress = false),
			next: (response) => {
				this.activeFlights = response.activeFlights ?? [];
				this.inProgress = false;
			}
		});
	}
}
