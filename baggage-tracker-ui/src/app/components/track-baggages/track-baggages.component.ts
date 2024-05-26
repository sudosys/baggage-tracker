import { Component, OnInit } from '@angular/core';
import { BaggageInfoResponse } from '../../../../open-api/bt-api.client';
import { BaggageTrackingService } from '../../services/baggage-tracking-service/baggage-tracking.service';
import { UserService } from '../../services/user-service/user.service';

@Component({
	selector: 'app-track-baggages',
	templateUrl: './track-baggages.component.html',
	styleUrl: './track-baggages.component.scss'
})
export class TrackBaggagesComponent implements OnInit {
	constructor(private btService: BaggageTrackingService) {}

	baggageInfo: BaggageInfoResponse | undefined;
	inProgress: boolean = false;

	ngOnInit(): void {
		this.fetchBaggageInformation();
	}

	private fetchBaggageInformation() {
		this.inProgress = true;
		this.btService.trackBaggages(UserService.userInfo?.id).subscribe({
			error: () => (this.inProgress = false),
			next: (response) => {
				this.baggageInfo = response;
				this.inProgress = false;
			}
		});
	}

	protected toTitleCase = (input: string) => input.toTitleCase();
}
