import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { HeaderComponent } from './components/common/header/header.component';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule } from '@angular/forms';

@NgModule({
	declarations: [AppComponent, LoginComponent, HeaderComponent],
	imports: [BrowserModule, AppRoutingModule, InputTextModule, FormsModule],
	providers: [],
	bootstrap: [AppComponent]
})
export class AppModule {}
