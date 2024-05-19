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
import { HttpClientModule } from '@angular/common/http';
import { PasswordModule } from 'primeng/password';
import { ButtonModule } from 'primeng/button';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { QrCodeScanComponent } from './components/qr-code-scan/qr-code-scan.component';
import { ZXingScannerModule } from '@zxing/ngx-scanner';

@NgModule({
	declarations: [AppComponent, LoginComponent, HeaderComponent, QrCodeScanComponent],
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
		ZXingScannerModule
	],
	providers: [
		{ provide: API_BASE_URL, useValue: environment.apiBaseUrl },
		MessageService
	],
	bootstrap: [AppComponent]
})
export class AppModule {}
