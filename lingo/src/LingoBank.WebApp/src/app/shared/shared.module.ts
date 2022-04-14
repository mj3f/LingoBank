import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ButtonComponent } from './components/button/button.component';
import { ModalComponent } from './components/modal/modal.component';
import { AutofocusDirective } from './directives/autofocus/autofocus.directive';

@NgModule({
	declarations: [
		ButtonComponent,
		ModalComponent,
		AutofocusDirective
	],
	imports: [
		CommonModule
	],
	exports: [
		ButtonComponent,
		ModalComponent,
		AutofocusDirective
	]
})
export class SharedModule { }
