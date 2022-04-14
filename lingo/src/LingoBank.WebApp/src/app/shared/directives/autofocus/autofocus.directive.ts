import { AfterContentInit, Directive, ElementRef, Renderer2 } from '@angular/core';

@Directive({
	selector: '[appAutofocus]'
})
export class AutofocusDirective implements AfterContentInit {

	constructor(
		private renderer: Renderer2,
		private elementRef: ElementRef) { }

	ngAfterContentInit(): void {
		setTimeout(() => {
			this.renderer.addClass(this.elementRef.nativeElement, 'focus:ring-blue-600');
			this.elementRef.nativeElement.focus();
		}, 200);
	}
}
