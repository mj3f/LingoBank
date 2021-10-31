import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LanguageListComponent } from './language-list/language-list.component';
import { LanguageViewComponent } from './language-view/language-view.component';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [LanguageListComponent, LanguageViewComponent],
  imports: [
    CommonModule,
    SharedModule
  ],
  exports: [
    LanguageListComponent
  ]
})
export class LanguagesModule { }
