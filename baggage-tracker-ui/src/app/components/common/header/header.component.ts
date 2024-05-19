import { Component } from '@angular/core';
import { UserService } from '../../../services/user-service/user.service';
import { Router } from '@angular/router';

@Component({
	selector: 'app-header',
	templateUrl: './header.component.html',
	styleUrl: './header.component.scss'
})
export class HeaderComponent {
	constructor(
		private userService: UserService,
		protected router: Router
	) {}

	protected readonly UserService = UserService;
	protected readonly history = history;

	protected async onClickLogout(): Promise<void> {
		await this.userService.logout();
	}
}
