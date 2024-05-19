import { Component, OnInit } from '@angular/core';
import { BaggageTrackingService } from '../../services/baggage-tracking-service/baggage-tracking.service';
import {
	BaggageStatus,
	BaggageTrackerClient,
	QrCodeScanResult,
	UserRole
} from '../../../../open-api/bt-api.client';
import { UserService } from '../../services/user-service/user.service';
import { Router } from '@angular/router';
import { DropdownOption } from '../../models/dropdown-option.model';

@Component({
	selector: 'app-post-scan',
	templateUrl: './post-scan.component.html',
	styleUrl: './post-scan.component.scss'
})
export class PostScanComponent implements OnInit {
	constructor(
		protected btService: BaggageTrackingService,
		protected btClient: BaggageTrackerClient,
		protected router: Router
	) {}

	protected readonly QrCodeScanResult = QrCodeScanResult;
	protected readonly UserService = UserService;
	protected readonly UserRole = UserRole;
	protected readonly BaggageStatus = BaggageStatus;
	protected readonly String = String;

	protected selectedStatus = BaggageStatus.Undefined;
	protected statusOptions: DropdownOption[] = [];

	ngOnInit(): void {
		this.prepareAllowedStatusValues();
	}

	protected setBaggageStatus(status: BaggageStatus) {
		this.btService.setBaggageStatus(status).subscribe();
	}

	private prepareAllowedStatusValues() {
		if (UserService.userInfo?.role == UserRole.Passenger) {
			this.btClient
				.passengerAllowedStatuses()
				.subscribe((r) => this.mapStatusesToOptions(r.response));
		} else if (UserService.userInfo?.role == UserRole.Personnel) {
			this.btClient
				.personnelAllowedStatuses()
				.subscribe((r) => this.mapStatusesToOptions(r.response));
		}
	}

	private mapStatusesToOptions(statuses: BaggageStatus[]) {
		this.statusOptions = statuses.map((o: BaggageStatus) => {
			return <DropdownOption>{
				displayName: o.toTitleCase(),
				value: o
			};
		});
	}

	protected toTitleCase = (input: string) => input.toTitleCase();
}
