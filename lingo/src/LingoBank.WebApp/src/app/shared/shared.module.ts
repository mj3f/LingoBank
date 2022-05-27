import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ButtonComponent } from './components/button/button.component';
import { ModalComponent } from './components/modal/modal.component';
import { AutofocusDirective } from './directives/autofocus/autofocus.directive';
import { SpinnerComponent } from './components/spinner/spinner.component';
import { WithLoadingPipe } from './pipes/with-loading/with-loading.pipe';

@NgModule({
	declarations: [
		ButtonComponent,
		ModalComponent,
		AutofocusDirective,
		SpinnerComponent,
		WithLoadingPipe
	],
	imports: [
		CommonModule
	],
	exports: [
		ButtonComponent,
		ModalComponent,
		AutofocusDirective,
		SpinnerComponent
	]
})
export class SharedModule { }
