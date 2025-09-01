import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewRunningSalaryDetailsComponent } from './view-running-salary-details.component';

describe('ViewRunningSalaryDetailsComponent', () => {
  let component: ViewRunningSalaryDetailsComponent;
  let fixture: ComponentFixture<ViewRunningSalaryDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ViewRunningSalaryDetailsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ViewRunningSalaryDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
