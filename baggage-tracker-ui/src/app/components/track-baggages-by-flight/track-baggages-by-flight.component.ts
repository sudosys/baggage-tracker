import { Component, HostListener } from '@angular/core';
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
	inProgress = false;

	userBaggageInformation: User[] = [];

	@HostListener('document:keyup.enter', ['$event'])
	onClickFetchFlightData() {
		if (!this.flightNumber) return;

		this.inProgress = true;

		this.btService.trackBaggagesByFlight(this.flightNumber).subscribe((response) => {
			this.userBaggageInformation = response;
			this.inProgress = false;
		});
	}

	protected toTitleCase = (input: string) => input.toTitleCase();
}
