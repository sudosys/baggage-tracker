import { Component } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { UserService } from '../../services/user-service/user.service';
import { UserRole } from '../../../../open-api/bt-api.client';
import { RippleModule } from 'primeng/ripple';

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
	protected readonly UserService = UserService;
	protected readonly UserRole = UserRole;
}
