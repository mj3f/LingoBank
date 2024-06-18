import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { LanguagesModule } from './languages/languages.module';
import { LoginRegisterComponent } from './login-register/login-register.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HomeComponent } from './home/home.component';
import { NavbarModule } from './navbar/navbar.module';
import { SharedModule } from './shared/shared.module';

@NgModule({ declarations: [
        AppComponent,
        LoginRegisterComponent,
        HomeComponent
    ],
    bootstrap: [AppComponent], imports: [BrowserModule,
        AppRoutingModule,
        LanguagesModule,
        ReactiveFormsModule,
        NavbarModule,
        SharedModule], providers: [provideHttpClient(withInterceptorsFromDi())] })
export class AppModule { }
