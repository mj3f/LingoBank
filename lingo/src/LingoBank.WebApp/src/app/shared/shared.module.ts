import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ButtonComponent } from './button/button.component';
import { AuthService } from './services/auth.service';
import { LanguageService } from './services/language.service';

@NgModule({
	declarations: [
		ButtonComponent
	],
	imports: [
		CommonModule
	],
	exports: [
		ButtonComponent
	],
	providers: [
		AuthService,
		LanguageService,
	],
})
export class SharedModule { }
