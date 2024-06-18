import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  Input,
  Output,
} from "@angular/core";

@Component({
  selector: "app-button",
  templateUrl: "./button.component.html",
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ButtonComponent {
  // TODO: Start using this.

  @Input() public title: string = "";

  @Output()
  public onClick: EventEmitter<any> = new EventEmitter<any>();

  handleClick(event): void {
    this.onClick.emit(event);
  }
}
