import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { HeaderComponent } from './components/common/header/header.component';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { API_BASE_URL } from '../../open-api/bt-api.client';
import { environment } from '../environments/environment.development';
import {
	HttpClientModule,
	provideHttpClient,
	withInterceptors
} from '@angular/common/http';
import { PasswordModule } from 'primeng/password';
import { ButtonModule } from 'primeng/button';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { QrCodeScanComponent } from './components/qr-code-scan/qr-code-scan.component';
import { ZXingScannerModule } from '@zxing/ngx-scanner';
import { httpInterceptor } from './services/http-interceptor/http.interceptor';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { PostScanComponent } from './components/post-scan/post-scan.component';
import { NgOptimizedImage } from '@angular/common';
import { DropdownModule } from 'primeng/dropdown';
import { GenerateQrCodeComponent } from './components/generate-qr-code/generate-qr-code.component';
import { TrackBaggagesByFlightComponent } from './components/track-baggages-by-flight/track-baggages-by-flight.component';
import { AccordionModule } from 'primeng/accordion';
import { TableModule } from 'primeng/table';
import { TrackBaggagesComponent } from './components/track-baggages/track-baggages.component';
import { HelpComponent } from './components/help/help.component';
import { RegisterFlightManifestComponent } from './components/register-flight-manifest/register-flight-manifest.component';
import { FileUploadModule } from 'primeng/fileupload';
import { ActiveFlightsComponent } from './components/active-flights/active-flights.component';

@NgModule({
	declarations: [
		AppComponent,
		LoginComponent,
		HeaderComponent,
		QrCodeScanComponent,
		PostScanComponent,
		GenerateQrCodeComponent,
		TrackBaggagesByFlightComponent,
		TrackBaggagesComponent,
		HelpComponent,
		RegisterFlightManifestComponent,
		ActiveFlightsComponent
	],
	imports: [
		BrowserModule,
		AppRoutingModule,
		InputTextModule,
		PasswordModule,
		FormsModule,
		ButtonModule,
		ToastModule,
		HttpClientModule,
		BrowserAnimationsModule,
		ReactiveFormsModule,
		ZXingScannerModule,
		ProgressSpinnerModule,
		NgOptimizedImage,
		DropdownModule,
		AccordionModule,
		TableModule,
		FileUploadModule
	],
	providers: [
		{ provide: API_BASE_URL, useValue: environment.apiBaseUrl },
		provideHttpClient(withInterceptors([httpInterceptor])),
		MessageService
	],
	bootstrap: [AppComponent]
})
export class AppModule {}
