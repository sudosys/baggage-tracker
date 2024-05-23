import { Component } from '@angular/core';
import { BaggageTrackingService } from '../../services/baggage-tracking-service/baggage-tracking.service';
import { User } from '../../../../open-api/bt-api.client';

@Component({
	selector: 'app-track-baggages-by-flight',
	templateUrl: './track-baggages-by-flight.component.html',
	styleUrl: './track-baggages-by-flight.component.scss'
})
export class TrackBaggagesByFlightComponent {
	constructor(private btService: BaggageTrackingService) {}

	flightNumber: string;
	returnedInformation = false;

	userBaggageInformation: User[];

	onClickFetchFlightData() {
		if (!this.flightNumber) return;
		this.btService.trackBaggagesByFlight(this.flightNumber).subscribe((response) => {
			this.userBaggageInformation = response;
			this.returnedInformation = true;
		});
	}

	protected toTitleCase = (input: string) => input.toTitleCase();
}
