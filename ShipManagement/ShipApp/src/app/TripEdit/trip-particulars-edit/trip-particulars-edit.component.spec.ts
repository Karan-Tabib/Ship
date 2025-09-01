import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TripParticularsEditComponent } from './trip-particulars-edit.component';

describe('TripParticularsEditComponent', () => {
  let component: TripParticularsEditComponent;
  let fixture: ComponentFixture<TripParticularsEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TripParticularsEditComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TripParticularsEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
