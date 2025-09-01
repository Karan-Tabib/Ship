import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TripInformationViewComponent } from './trip-information-view.component';

describe('TripInformationViewComponent', () => {
  let component: TripInformationViewComponent;
  let fixture: ComponentFixture<TripInformationViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TripInformationViewComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TripInformationViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
