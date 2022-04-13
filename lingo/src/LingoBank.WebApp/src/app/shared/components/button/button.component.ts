import { Component, EventEmitter, Output } from '@angular/core';

@Component({
	selector: 'app-button',
	templateUrl: './button.component.html',
})
export class ButtonComponent { // TODO: Start using this.
	@Output()
	public onClick: EventEmitter<any> = new EventEmitter<any>();

	handleClick(event): void {
		this.onClick.emit(event);
	}

}
