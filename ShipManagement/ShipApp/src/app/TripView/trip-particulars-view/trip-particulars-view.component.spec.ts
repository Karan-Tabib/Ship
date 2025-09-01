import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TripParticularsViewComponent } from './trip-particulars-view.component';

describe('TripParticularsViewComponent', () => {
  let component: TripParticularsViewComponent;
  let fixture: ComponentFixture<TripParticularsViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TripParticularsViewComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TripParticularsViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
