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

@NgModule({
  declarations: [
      AppComponent,
      LoginRegisterComponent,
      NavbarComponent
  ],
  imports: [
      BrowserModule,
      AppRoutingModule,
      LanguagesModule,
      HttpClientModule,
      ReactiveFormsModule
  ],
  providers: [
      AuthService,
      LanguageService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
