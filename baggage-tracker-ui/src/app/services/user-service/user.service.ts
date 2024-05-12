import { Injectable } from '@angular/core';
import {
	AuthenticationRequest,
	AuthenticationResponse,
	AuthenticationStatus,
	BaggageTrackerClient
} from '../../../../open-api/bt-api.client';
import { catchError, of, tap } from 'rxjs';
import { MessageService } from 'primeng/api';
import { Router } from '@angular/router';

@Injectable({
	providedIn: 'root'
})
export class UserService {
	constructor(
		private btClient: BaggageTrackerClient,
		private router: Router,
		private messageService: MessageService
	) {}

	private tokenKey = 'token';

	login(username: string, password: string) {
		const authRequest: AuthenticationRequest = {
			username: username,
			password: password
		} as AuthenticationRequest;

		return this.btClient.authenticate(authRequest).pipe(
			catchError((errorResponse: AuthenticationResponse) => {
				this.messageService.add({
					severity: 'error',
					summary: 'Authentication failed'
				});
				return of(errorResponse);
			}),
			tap((response) => {
				if (response.status == AuthenticationStatus.Success) {
					this.addTokenToLocalStorage(response.token!);
				}
			})
		);
	}

	async logout(): Promise<void> {
		window.localStorage.removeItem(this.tokenKey);
		await this.router.navigateByUrl('login');
	}

	private addTokenToLocalStorage(token: string): void {
		window.localStorage.setItem(this.tokenKey, token);
	}
}
