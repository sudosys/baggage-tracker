import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { UserService } from '../user-service/user.service';
import { inject } from '@angular/core';
import { MessageService } from 'primeng/api';
import { catchError, throwError } from 'rxjs';

export const httpInterceptor: HttpInterceptorFn = (req, next) => {
	const messageService = inject(MessageService);

	const token = localStorage.getItem(UserService.tokenKey);

	const reqWithToken = token
		? req.clone({ setHeaders: { Authorization: `Bearer ${token}` } })
		: req;

	return next(reqWithToken).pipe(
		catchError((error: HttpErrorResponse) => {
			messageService.add({
				severity: 'error',
				summary: `Error Response: ${error.status}`,
				detail: error.message
			});

			return throwError(() => error);
		})
	);
};
