import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LanguagePhrasesListComponent } from './language-phrases-list.component';

describe('LanguagePhrasesListComponent', () => {
  let component: LanguagePhrasesListComponent;
  let fixture: ComponentFixture<LanguagePhrasesListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LanguagePhrasesListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LanguagePhrasesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
