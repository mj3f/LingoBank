import { ChangeDetectionStrategy, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
	selector: 'app-modal',
	templateUrl: './modal.component.html',
	changeDetection: ChangeDetectionStrategy.OnPush
})
export class ModalComponent implements OnInit {

	@Input()
	public title: string;

	@Input()
	public onlyShowDismissButton = false;

	@Output()
	public okButtonClicked: EventEmitter<undefined> = new EventEmitter<undefined>();

	@Output()
	public cancelOrDismissButtonClicked: EventEmitter<undefined> = new EventEmitter<undefined>();

	@Input()
	public show: boolean;

	ngOnInit(): void {
		// this.show = true;
	}

	onOkClick(): void {
		// this.show = false;
		this.okButtonClicked.emit();
	}

	onCancelClick(): void {
		// this.show = false;
		this.cancelOrDismissButtonClicked.emit();
	}

}
