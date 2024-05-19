import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { routeGuard } from './guards/route-guard/route.guard';
import { HomeComponent } from './components/home/home.component';
import { QrCodeScanComponent } from './components/qr-code-scan/qr-code-scan.component';

const routes: Routes = [
	{ path: '', redirectTo: 'login', pathMatch: 'full' },
	{ path: 'login', component: LoginComponent },
	{ path: 'home', component: HomeComponent, canActivate: [routeGuard] },
	{ path: 'qr-code-scan', component: QrCodeScanComponent, canActivate: [routeGuard] }
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule]
})
export class AppRoutingModule {}
