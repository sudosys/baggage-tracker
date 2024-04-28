import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MainPageComponent } from './main-page/main-page.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule }from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormsModule } from '@angular/forms';
import { ScanQrCodeComponent } from './scan-qr-code/scan-qr-code.component';
import { ZXingScannerModule } from '@zxing/ngx-scanner';
import { HeaderComponent } from './header/header.component';
import { GoBackComponent } from './go-back/go-back.component';
import { MatDialogModule } from '@angular/material/dialog';
import { AfterScanComponent } from './after-scan/after-scan.component';
import { SubheaderComponent } from './subheader/subheader.component';
import { HttpClientModule } from '@angular/common/http';
import { GenerateQrCodeComponent } from './generate-qr-code/generate-qr-code.component';
import { TrackBaggageComponent } from './track-baggage/track-baggage.component';
import { GetHelpComponent } from './get-help/get-help.component';

const appRoutes: Routes = [
  {path: "", component: MainPageComponent},
  {path: "scan-qr-code", component: ScanQrCodeComponent},
  {path: "after-scan", component: AfterScanComponent},
  {path: "track-baggage", component: TrackBaggageComponent},
  {path: "generate-qr-code", component: GenerateQrCodeComponent},
  {path: "get-help", component: GetHelpComponent},
]

@NgModule({
  declarations: [
    AppComponent,
    MainPageComponent,
    ScanQrCodeComponent,
    HeaderComponent,
    GoBackComponent,
    AfterScanComponent,
    SubheaderComponent,
    GenerateQrCodeComponent,
    TrackBaggageComponent,
    GetHelpComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    MatDividerModule,
    MatSelectModule,
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    ZXingScannerModule,
    MatDialogModule,
    HttpClientModule,
    RouterModule.forRoot(appRoutes, {enableTracing: true})
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
