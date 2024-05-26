import { Component } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { UserService } from '../../services/user-service/user.service';
import { UserRole } from '../../../../open-api/bt-api.client';
import { RippleModule } from 'primeng/ripple';
import { Router } from '@angular/router';

@Component({
	selector: 'app-home',
	standalone: true,
	imports: [
		ButtonModule,
		FormsModule,
		InputTextModule,
		PasswordModule,
		ReactiveFormsModule,
		RippleModule
	],
	templateUrl: './home.component.html',
	styleUrl: './home.component.scss'
})
export class HomeComponent {
	constructor(private router: Router) {}

	protected readonly UserService = UserService;
	protected readonly UserRole = UserRole;

	async onClickQrCodeScan() {
		await this.router.navigateByUrl('qr-code-scan');
	}

	async onClickQrCodeGenerate() {
		await this.router.navigateByUrl('generate-qr-code');
	}

	async onClickTrackBaggages() {
		await this.router.navigateByUrl('track-baggages');
	}

	async onClickTrackBaggagesByFlight() {
		await this.router.navigateByUrl('track-baggages-by-flight');
	}

	async onClickHelp() {
		await this.router.navigateByUrl('help');
	}

	async onClickRegisterFlightManifest() {
		await this.router.navigateByUrl('register-flight-manifest');
	}

	async onClickViewActiveFlights() {
		await this.router.navigateByUrl('active-flights');
	}
}
