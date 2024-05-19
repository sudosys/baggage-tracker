import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { routeGuard } from './guards/route-guard/route.guard';
import { HomeComponent } from './components/home/home.component';
import { QrCodeScanComponent } from './components/qr-code-scan/qr-code-scan.component';
import { PostScanComponent } from './components/post-scan/post-scan.component';
import { GenerateQrCodeComponent } from './components/generate-qr-code/generate-qr-code.component';
import { TrackBaggagesByFlightComponent } from './components/track-baggages-by-flight/track-baggages-by-flight.component';

const routes: Routes = [
	{ path: '', redirectTo: 'login', pathMatch: 'full' },
	{ path: 'login', component: LoginComponent },
	{ path: 'home', component: HomeComponent, canActivate: [routeGuard] },
	{ path: 'qr-code-scan', component: QrCodeScanComponent, canActivate: [routeGuard] },
	{ path: 'post-scan', component: PostScanComponent, canActivate: [routeGuard] },
	{
		path: 'generate-qr-code',
		component: GenerateQrCodeComponent,
		canActivate: [routeGuard]
	},
	{
		path: 'track-baggages-by-flight',
		component: TrackBaggagesByFlightComponent,
		canActivate: [routeGuard]
	}
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule]
})
export class AppRoutingModule {}
