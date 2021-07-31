import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SurveyAdminListComponent } from './survey-admin-list.component';

describe('SurveyAdminListComponent', () => {
  let component: SurveyAdminListComponent;
  let fixture: ComponentFixture<SurveyAdminListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SurveyAdminListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SurveyAdminListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
