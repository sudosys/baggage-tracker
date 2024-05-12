import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { HeaderComponent } from './components/common/header/header.component';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule } from '@angular/forms';
import { API_BASE_URL } from '../../open-api/bt-api.client';
import { environment } from '../environments/environment.development';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
	declarations: [AppComponent, LoginComponent, HeaderComponent],
	imports: [
		BrowserModule,
		AppRoutingModule,
		InputTextModule,
		FormsModule,
		HttpClientModule
	],
	providers: [{ provide: API_BASE_URL, useValue: environment.apiBaseUrl }],
	bootstrap: [AppComponent]
})
export class AppModule {}
