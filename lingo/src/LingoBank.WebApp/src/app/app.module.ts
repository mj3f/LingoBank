import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { LanguagesModule } from './languages/languages.module';
import { LoginRegisterComponent } from './login-register/login-register.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HomeComponent } from './home/home.component';
import { NavbarModule } from './navbar/navbar.module';
import { SharedModule } from './shared/shared.module';

@NgModule({
	declarations: [
		AppComponent,
		LoginRegisterComponent,
		HomeComponent
	],
	imports: [
		BrowserModule,
		AppRoutingModule,
		LanguagesModule,
		HttpClientModule,
		ReactiveFormsModule,
		NavbarModule,
		SharedModule
	],
	bootstrap: [AppComponent]
})
export class AppModule { }
