import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './navbar.component';
import { NavbarLinkComponent } from './navbar-link/navbar-link.component';
import { AppRoutingModule } from '../app-routing.module';



@NgModule({
	declarations: [
		NavbarComponent,
		NavbarLinkComponent
	],
	imports: [
		CommonModule,
		AppRoutingModule
	],
	exports: [
		NavbarComponent
	]
})
export class NavbarModule { }
