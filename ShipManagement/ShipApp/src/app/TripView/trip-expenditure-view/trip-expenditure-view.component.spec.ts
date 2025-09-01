import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TripExpenditureViewComponent } from './trip-expenditure-view.component';

describe('TripExpenditureViewComponent', () => {
  let component: TripExpenditureViewComponent;
  let fixture: ComponentFixture<TripExpenditureViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TripExpenditureViewComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TripExpenditureViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
