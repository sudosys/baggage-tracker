import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainPageComponent } from './main-page/main-page.component';
import { ScanQrCodeComponent } from './scan-qr-code/scan-qr-code.component';
import { AfterScanComponent } from './after-scan/after-scan.component';
import { TrackBaggageComponent } from './track-baggage/track-baggage.component';
import { GenerateQrCodeComponent } from './generate-qr-code/generate-qr-code.component';
import { GetHelpComponent } from './get-help/get-help.component';

const routes: Routes = [
    { path: '', component: MainPageComponent },
    { path: 'scan-qr-code', component: ScanQrCodeComponent },
    { path: 'after-scan', component: AfterScanComponent },
    { path: 'track-baggage', component: TrackBaggageComponent },
    { path: 'generate-qr-code', component: GenerateQrCodeComponent },
    { path: 'get-help', component: GetHelpComponent },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class AppRoutingModule {}
