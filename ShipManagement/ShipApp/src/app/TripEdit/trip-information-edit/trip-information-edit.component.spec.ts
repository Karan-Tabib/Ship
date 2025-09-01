import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TripInformationEditComponent } from './trip-information-edit.component';

describe('TripInformationEditComponent', () => {
  let component: TripInformationEditComponent;
  let fixture: ComponentFixture<TripInformationEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TripInformationEditComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TripInformationEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
