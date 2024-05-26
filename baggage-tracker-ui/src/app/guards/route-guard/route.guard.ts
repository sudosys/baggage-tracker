import { CanActivateFn, Router } from '@angular/router';
import { UserService } from '../../services/user-service/user.service';
import { inject } from '@angular/core';
import { Page } from '../../enums/page.enum';
import { UserRole, UserSlimDto } from '../../../../open-api/bt-api.client';

export const routeGuard: CanActivateFn = async (route) => {
	const token = window.localStorage.getItem(UserService.tokenKey);

	const serializedUser = window.localStorage.getItem(UserService.userKey);
	const user = JSON.parse(serializedUser ?? '') as UserSlimDto;

	const path = truncateSlash(route.url[0].path);
	const isAllowed = user.role && isPageAllowed(user.role, path);
	if (path == Page.Login || (token && isAllowed)) {
		return true;
	}

	await inject(Router).navigateByUrl(Page.Login);
	window.localStorage.clear();
	return false;
};

const passengerAllowedPaths: Page[] = [
	Page.Home,
	Page.QrCodeScan,
	Page.PostScan,
	Page.TrackBaggages,
	Page.Help
];

const personnelAllowedPaths: Page[] = [
	Page.Home,
	Page.QrCodeScan,
	Page.PostScan,
	Page.GenerateQrCode,
	Page.TrackBaggagesByFlight,
	Page.RegisterFlightManifest,
	Page.ActiveFlights
];

function isPageAllowed(userRole: UserRole, path: string): boolean {
	return (
		(userRole == UserRole.Passenger &&
			passengerAllowedPaths.includes(path as Page)) ||
		(userRole == UserRole.Personnel && personnelAllowedPaths.includes(path as Page))
	);
}

function truncateSlash(path: string) {
	return path.replace(/\//, '');
}
