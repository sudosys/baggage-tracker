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

		this.userService
			.login(this.loginForm.value.username!, this.loginForm.value.password!)
			.subscribe();
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
