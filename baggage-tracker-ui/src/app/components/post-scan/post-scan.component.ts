import { Component } from '@angular/core';
import { BaggageTrackingService } from '../../services/baggage-tracking-service/baggage-tracking.service';
import {
	BaggageStatus,
	QrCodeScanResult,
	UserRole
} from '../../../../open-api/bt-api.client';
import { UserService } from '../../services/user-service/user.service';
import { Router } from '@angular/router';

@Component({
	selector: 'app-post-scan',
	templateUrl: './post-scan.component.html',
	styleUrl: './post-scan.component.scss'
})
export class PostScanComponent {
	constructor(
		protected btService: BaggageTrackingService,
		protected router: Router
	) {}

	protected readonly QrCodeScanResult = QrCodeScanResult;
	protected readonly UserService = UserService;
	protected readonly UserRole = UserRole;
	protected readonly BaggageStatus = BaggageStatus;
	protected readonly Object = Object;

	protected selectedStatus = BaggageStatus.Undefined;

	protected setBaggageStatus(status: BaggageStatus) {
		this.btService.setBaggageStatus(status).subscribe();
	}
}
