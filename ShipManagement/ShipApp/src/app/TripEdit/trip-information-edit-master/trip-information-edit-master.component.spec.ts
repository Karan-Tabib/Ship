import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TripInformationEditMasterComponent } from './trip-information-edit-master.component';

describe('TripInformationEditMasterComponent', () => {
  let component: TripInformationEditMasterComponent;
  let fixture: ComponentFixture<TripInformationEditMasterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TripInformationEditMasterComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TripInformationEditMasterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
