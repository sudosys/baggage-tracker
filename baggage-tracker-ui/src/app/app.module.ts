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

@NgModule({
	declarations: [
		AppComponent,
		LoginComponent,
		HeaderComponent,
		QrCodeScanComponent,
		PostScanComponent
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
		DropdownModule
	],
	providers: [
		{ provide: API_BASE_URL, useValue: environment.apiBaseUrl },
		provideHttpClient(withInterceptors([httpInterceptor])),
		MessageService
	],
	bootstrap: [AppComponent]
})
export class AppModule {}
