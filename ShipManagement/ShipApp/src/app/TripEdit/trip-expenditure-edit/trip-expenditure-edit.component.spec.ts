import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TripExpenditureEditComponent } from './trip-expenditure-edit.component';

describe('TripExpenditureEditComponent', () => {
  let component: TripExpenditureEditComponent;
  let fixture: ComponentFixture<TripExpenditureEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TripExpenditureEditComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TripExpenditureEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
