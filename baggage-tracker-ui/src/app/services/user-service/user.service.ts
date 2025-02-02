import { Injectable } from '@angular/core';
import {
	AuthenticationRequest,
	AuthenticationStatus,
	BaggageTrackerClient,
	UserSlimDto
} from '../../../../open-api/bt-api.client';
import { tap } from 'rxjs';
import { Router } from '@angular/router';
import { Page } from '../../enums/page.enum';

@Injectable({
	providedIn: 'root'
})
export class UserService {
	constructor(
		private btClient: BaggageTrackerClient,
		private router: Router
	) {}

	static readonly tokenKey = 'token';
	static readonly userKey = 'user';

	static userInfo: UserSlimDto | undefined = UserService.getUserInfo();

	login(username: string, password: string) {
		const authRequest: AuthenticationRequest = {
			username: username,
			password: password
		} as AuthenticationRequest;

		return this.btClient.authenticate(authRequest).pipe(
			tap(async (response) => {
				if (response.status == AuthenticationStatus.Success) {
					this.attachUserToContext(response.token!, response.user!);
					await this.router.navigateByUrl(Page.Home);
					UserService.userInfo = response.user!;
				} else {
					throw new Error(response.status);
				}
			})
		);
	}

	async logout(): Promise<void> {
		window.localStorage.removeItem(UserService.tokenKey);
		window.localStorage.removeItem(UserService.userKey);
		UserService.userInfo = undefined;
		await this.router.navigateByUrl(Page.Login);
	}

	private attachUserToContext(token: string, user: UserSlimDto): void {
		window.localStorage.setItem(UserService.tokenKey, token);
		window.localStorage.setItem(UserService.userKey, JSON.stringify(user));
	}

	private static getUserInfo(): UserSlimDto | undefined {
		const user = window.localStorage.getItem(UserService.userKey);
		return user ? JSON.parse(user) : undefined;
	}
}
