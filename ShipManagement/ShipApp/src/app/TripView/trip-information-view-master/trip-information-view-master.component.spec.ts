import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TripInformationViewMasterComponent } from './trip-information-view-master.component';

describe('TripInformationViewMasterComponent', () => {
  let component: TripInformationViewMasterComponent;
  let fixture: ComponentFixture<TripInformationViewMasterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TripInformationViewMasterComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TripInformationViewMasterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
