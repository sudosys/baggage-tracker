import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { routeGuard } from './guards/route-guard/route.guard';
import { HomeComponent } from './components/home/home.component';
import { QrCodeScanComponent } from './components/qr-code-scan/qr-code-scan.component';
import { PostScanComponent } from './components/post-scan/post-scan.component';
import { GenerateQrCodeComponent } from './components/generate-qr-code/generate-qr-code.component';
import { TrackBaggagesByFlightComponent } from './components/track-baggages-by-flight/track-baggages-by-flight.component';
import { TrackBaggagesComponent } from './components/track-baggages/track-baggages.component';
import { HelpComponent } from './components/help/help.component';
import { RegisterFlightManifestComponent } from './components/register-flight-manifest/register-flight-manifest.component';
import { ActiveFlightsComponent } from './components/active-flights/active-flights.component';
import { Page } from './enums/page.enum';

const routes: Routes = [
	{ path: '', redirectTo: 'login', pathMatch: 'full' },
	{ path: Page.Login, component: LoginComponent },
	{
		path: Page.Home,
		component: HomeComponent,
		canActivate: [routeGuard]
	},
	{
		path: Page.QrCodeScan,
		component: QrCodeScanComponent,
		canActivate: [routeGuard]
	},
	{
		path: Page.PostScan,
		component: PostScanComponent,
		canActivate: [routeGuard]
	},
	{
		path: Page.GenerateQrCode,
		component: GenerateQrCodeComponent,
		canActivate: [routeGuard]
	},
	{
		path: Page.TrackBaggages,
		component: TrackBaggagesComponent,
		canActivate: [routeGuard]
	},
	{
		path: Page.TrackBaggagesByFlight,
		component: TrackBaggagesByFlightComponent,
		canActivate: [routeGuard]
	},
	{
		path: Page.Help,
		component: HelpComponent,
		canActivate: [routeGuard]
	},
	{
		path: Page.RegisterFlightManifest,
		component: RegisterFlightManifestComponent,
		canActivate: [routeGuard]
	},
	{
		path: Page.ActiveFlights,
		component: ActiveFlightsComponent,
		canActivate: [routeGuard]
	}
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule]
})
export class AppRoutingModule {}
