import { CanActivateFn, Router } from '@angular/router';
import { UserService } from '../../services/user-service/user.service';
import { inject } from '@angular/core';

export const routeGuard: CanActivateFn = () => {
	const token = window.localStorage.getItem(UserService.tokenKey);
	const user = window.localStorage.getItem(UserService.userKey);
	const router = inject(Router);

	if (token && user) {
		return true;
	}

	router.navigateByUrl('login');
	return false;
};
