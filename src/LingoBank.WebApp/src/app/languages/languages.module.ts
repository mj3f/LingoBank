import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LanguageListComponent } from './language-list/language-list.component';
import { LanguageViewComponent } from './language-view/language-view.component';


@NgModule({
  declarations: [LanguageListComponent, LanguageViewComponent],
  imports: [
    CommonModule
  ],
  exports: [
    LanguageListComponent
  ]
})
export class LanguagesModule { }
