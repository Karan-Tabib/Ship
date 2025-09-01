import { Component, inject, Input, OnChanges, OnDestroy, OnInit, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Boat } from '../../Models/boat';
import { Observable, Subscription } from 'rxjs';
import { BoatService } from '../../services/boat.service';
import { TripInformation } from '../../Models/trip-tripInformation';
import { TripService } from '../../services/trip.service';
import { TripData } from '../../Models/trip-data';

@Component({
  selector: 'app-trip-information-edit',
  templateUrl: './trip-information-edit.component.html',
  styleUrl: './trip-information-edit.component.css'
})
export class TripInformationEditComponent implements OnInit, OnDestroy, OnChanges {
  frm: FormGroup;
  subscriptionList: Subscription[] = [];
  boats$!: Observable<Boat[]>;
  boatService = inject(BoatService);
  @Input('ChildEditTripId') EditTripId!: number | undefined;
  tripService = inject(TripService);
  /**
   *
   */
  constructor(private fb: FormBuilder) {
    this.frm = this.fb.group({
      tripId: this.fb.control('', [Validators.required]),
      tripName: this.fb.control('', [Validators.required]),
      tripStartDate: this.fb.control('', [Validators.required]),
      tripEndDate: this.fb.control('', [Validators.required]),
      tripDescription: this.fb.control('', [Validators.required]),
      boatId: this.fb.control('', [Validators.required])
    })
  }
  ngOnChanges(changes: SimpleChanges): void {
    
  }
  ngOnDestroy(): void {
    this.subscriptionList.forEach(sub => sub.unsubscribe());
  }
  ngOnInit(): void {
    this.boats$ = this.boatService.boats$;

    // this.subscriptionList.push(
    //   this.boatService.getAllBoats().subscribe({
    //     next: (data: Boat[]) => { this.boats = data },
    //     error: (err: any) => { console.log('boat data not received!' + err) }
    //   })
    // );
  }

  AddTripInformation() {

  }
  resetForm() {
    this.frm.reset();
  }
  isValid(): boolean {
    return this.frm.valid;
  }

  getFormData() {
    return this.frm.value;
  }

  private createTripInfoData(tripInfoData: any): TripInformation {
    const tripinfo = new TripInformation();
    tripinfo.tripId = tripInfoData.tripId;
    tripinfo.boatId = tripInfoData.boatId;
    tripinfo.tripName = tripInfoData.tripName;
    tripinfo.tripStartDate = tripInfoData.tripStartDate;
    tripinfo.tripEndDate = tripInfoData.tripEndDate;
    tripinfo.tripDescription = tripInfoData.tripDescription;
    tripinfo.createdDate = new Date();
    tripinfo.updatedDate = new Date();
    return tripinfo;
  }
  getData(): TripInformation {
    return this.createTripInfoData(this.getFormData());
  }

  Edit(tripData: TripInformation) {
    console.log("Edit of trip Information hit!")
    this.frm.reset({
      tripId: tripData.tripId ?? '',
      tripName: tripData.tripName,
      tripStartDate: tripData.tripStartDate?.toString().split('T')[0],
      tripEndDate: tripData.tripEndDate?.toString().split('T')[0],
      tripDescription: tripData.tripDescription,
      boatId: tripData.boatId
    })
  }
}
