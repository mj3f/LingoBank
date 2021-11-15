import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ButtonComponent } from './button/button.component';
import { AuthService } from './services/auth.service';
import { LanguageService } from './services/language.service';
import { ModalComponent } from './components/modal/modal.component';
import { UserService } from './services/user.service';
import { CurrentUserService } from './services/current-user.service';

@NgModule({
	declarations: [
		ButtonComponent,
		ModalComponent
	],
	imports: [
		CommonModule
	],
	exports: [
		ButtonComponent,
		ModalComponent
	],
	providers: [
		AuthService,
		LanguageService,
		UserService,
		CurrentUserService
	],
})
export class SharedModule { }
