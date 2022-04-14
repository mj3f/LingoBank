import { Component, Input } from '@angular/core';

@Component({
	selector: 'app-navbar-link',
	templateUrl: './navbar-link.component.html'
})
export class NavbarLinkComponent {

	@Input()
	public title: string;

	@Input()
	public link: string[];

}
