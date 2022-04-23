import { RouterModule, Routes } from '@angular/router';
import { SettingsComponent } from './components/settings/settings.component';
import { NgModule } from '@angular/core';
import { AuthGuard } from '../shared/guards/auth/auth.guard';

const routes: Routes = [
	{ path: '', component: SettingsComponent, canActivate: [AuthGuard] }
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule]
})
export class SettingsRoutingModule { }
