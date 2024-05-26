import { Component, HostListener, OnInit } from '@angular/core';
import { UserService } from '../../services/user-service/user.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthenticationRequest } from '../../../../open-api/bt-api.client';
import { FormOf } from '../../models/form-of-t.model';

@Component({
	selector: 'app-login',
	templateUrl: './login.component.html',
	styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit {
	constructor(
		private userService: UserService,
		private formBuilder: FormBuilder
	) {}

	loginForm: FormGroup<FormOf<AuthenticationRequest>>;

	inProgress = false;

	protected readonly Object = Object;

	ngOnInit(): void {
		this.createForm();
	}

	@HostListener('document:keyup.enter', ['$event'])
	onClickLogin(): void {
		this.triggerValidation();

		if (this.loginForm.invalid) {
			return;
		}

		this.inProgress = true;
		this.userService
			.login(this.loginForm.value.username!, this.loginForm.value.password!)
			.subscribe({
				error: () => (this.inProgress = false),
				next: () => (this.inProgress = false)
			});
	}

	private triggerValidation(): void {
		for (const control in this.loginForm.controls) {
			(<any>this.loginForm.controls)[control].markAsDirty();
		}
	}

	private createForm(): void {
		this.loginForm = this.formBuilder.group(<FormOf<AuthenticationRequest>>{
			username: new FormControl('', [Validators.required]),
			password: new FormControl('', [Validators.required])
		});
	}
}
