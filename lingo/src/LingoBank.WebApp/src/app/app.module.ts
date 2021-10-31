import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { LanguagesModule } from './languages/languages.module';
import { LanguageService } from './shared/services/language.service';
import { LoginRegisterComponent } from './login-register/login-register.component';
import { ReactiveFormsModule } from '@angular/forms';
import { AuthService } from './shared/services/auth.service';
import { NavbarComponent } from './navbar/navbar.component';
import { NavbarLinkComponent } from './navbar/navbar-link/navbar-link.component';
import { HomeComponent } from './home/home.component';
import { NavbarModule } from './navbar/navbar.module';

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
      NavbarModule
  ],
  providers: [
      AuthService,
      LanguageService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
