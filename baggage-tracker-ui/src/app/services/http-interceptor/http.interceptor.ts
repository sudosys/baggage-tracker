import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { UserService } from '../user-service/user.service';
import { inject } from '@angular/core';
import { MessageService } from 'primeng/api';
import { catchError, throwError } from 'rxjs';
import { PlainResponse } from '../../../../open-api/bt-api.client';

export const httpInterceptor: HttpInterceptorFn = (req, next) => {
	const messageService = inject(MessageService);

	const token = localStorage.getItem(UserService.tokenKey);

	const reqWithToken = token
		? req.clone({ setHeaders: { Authorization: `Bearer ${token}` } })
		: req;

	return next(reqWithToken).pipe(
		catchError((response: HttpErrorResponse) => {
			const error = response.error as Blob;
			blobToPlainResponse(error).then((plainResponse) => {
				messageService.add({
					severity: 'error',
					summary: `Error Response: ${response.status}`,
					detail: plainResponse.response
				});
			});

			return throwError(() => error);
		})
	);
};

async function blobToPlainResponse(blob: Blob): Promise<PlainResponse> {
	const jsonStr = await blob.text();
	return JSON.parse(jsonStr) as PlainResponse;
}
