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

	ngOnInit(): void {
		this.btService.trackBaggages(UserService.userInfo?.id).subscribe((response) => {
			this.baggageInfo = response;
		});
	}

	protected toTitleCase = (input: string) => input.toTitleCase();
}
