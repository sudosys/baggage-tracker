import { Component } from '@angular/core';
import { UserService } from '../../../services/user-service/user.service';

@Component({
	selector: 'app-header',
	templateUrl: './header.component.html',
	styleUrl: './header.component.scss'
})
export class HeaderComponent {
	constructor(private userService: UserService) {}

	protected readonly UserService = UserService;

	protected async onClickLogout(): Promise<void> {
		await this.userService.logout();
	}
}
