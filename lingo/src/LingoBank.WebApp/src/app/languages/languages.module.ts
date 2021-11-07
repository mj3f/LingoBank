import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LanguageListComponent } from './language-list/language-list.component';
import { LanguageViewComponent } from './language-view/language-view.component';
import { SharedModule } from '../shared/shared.module';
import { LanguageCardComponent } from './language-list/language-card/language-card.component';


@NgModule({
  declarations: [LanguageListComponent, LanguageViewComponent, LanguageCardComponent],
  imports: [
    CommonModule,
    SharedModule
  ],
  exports: [
    LanguageListComponent
  ]
})
export class LanguagesModule { }
