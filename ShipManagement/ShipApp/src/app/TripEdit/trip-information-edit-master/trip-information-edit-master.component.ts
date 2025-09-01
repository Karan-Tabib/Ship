import { AfterViewInit, Component, inject, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { TripInformationEditComponent } from '../trip-information-edit/trip-information-edit.component';
import { TripParticularsEditComponent } from '../trip-particulars-edit/trip-particulars-edit.component';
import { TripExpenditureEditComponent } from '../trip-expenditure-edit/trip-expenditure-edit.component';
import { TripService } from '../../services/trip.service';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { TripData } from '../../Models/trip-data';

@Component({
  selector: 'app-trip-information-edit-master',
  templateUrl: './trip-information-edit-master.component.html',
  styleUrl: './trip-information-edit-master.component.css'
})
export class TripInformationEditMasterComponent implements AfterViewInit, OnInit {

  parentTripForm: FormGroup;
  tripService = inject(TripService);
  subscriptionList: Subscription[] = [];
  EditTripId: number | undefined;
  IsUpdateFlag: boolean = false;
  router = inject(Router);

  @ViewChild('tripInformationEditComponent') tripInformationEditComponent!: TripInformationEditComponent;
  @ViewChild('tripParticularsEditComponent') tripParticularsEditComponent!: TripParticularsEditComponent;
  @ViewChild('tripExpenditureEditComponent') tripExpenditureEditComponent!: TripExpenditureEditComponent;
  /**
   *
   */
  constructor(private fb: FormBuilder, private activatedRoute: ActivatedRoute) {
    this.parentTripForm = this.fb.group({
      tripInfoForm: this.fb.array([]),
      tripParticularForm: this.fb.array([]),
      tripExpForm: this.fb.array([])
    })
  }
  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(data => {
      this.EditTripId = data['tripId'];
      console.log("EDIT Trip ID set:" + this.EditTripId);
      console.log(this.EditTripId);
      if (this.EditTripId != null && this.EditTripId != undefined) {
        this.IsUpdateFlag = true;
        this.tripService.GetTripInformationByTripId(this.EditTripId).subscribe({
          next: (data: TripData) => {
            this.Edit(data);
          },
          error: () => {
            console.error('trip data not received!');
          }
        })
      }
    })
  }
  ngAfterViewInit(): void {

  }

  isAllValid(): boolean {
    return this.tripExpenditureEditComponent?.isValid()
    // && this.tripInformationEditComponent?.isValid()
    //  && this.tripParticularsEditComponent?.isValid() 
    //   
  }
  handleAddandUpdateForm() {
    const tripData = this.GetAllData();
    if (this.IsUpdateFlag) {
      this.UpdateRecords(tripData);
    } else {
      this.onSubmit(tripData);
    }
  }

  GetAllData() {
    const tripInfoData = this.tripInformationEditComponent.getData();
    const tripParticularData = this.tripParticularsEditComponent.getData();
    const tripExpenditureData = this.tripExpenditureEditComponent.getData();

    const tripData = {
      tripInfo: tripInfoData,
      tripParticulars: tripParticularData,
      tripExpenditures: tripExpenditureData
    };
    console.log(tripData);
    return tripData;
  }

  onSubmit(tripData: TripData) {
    this.subscriptionList.push(
      this.tripService.AddTripRecord(tripData).subscribe({
        next: (data: any) => {
          alert('Trip data successfully');
          this.ResetAllForm()
          this.IsUpdateFlag = false;
        },
        error: (err: any) => {
          console.error("Trip data not added" + err);
        }
      })
    );
  }




  UpdateRecords(tripData: TripData) {
    this.tripService.UpdatetripData(tripData).subscribe({
      next: (data: boolean) => {
        console.log("record Updated!");
        this.ResetAllForm();
        this.IsUpdateFlag = false;
        this.router.navigate(['Trip']);
      },
      error: () => {
        console.error('Record not Updated');
      }
    })
  }

  Edit(TripData: TripData) {
    this.tripInformationEditComponent?.Edit(TripData.tripInfo);
    this.tripParticularsEditComponent?.Edit(TripData.tripParticulars);
    this.tripExpenditureEditComponent?.Edit(TripData.tripExpenditures);
  }
  ResetAllForm() {
    this.tripInformationEditComponent?.resetForm();
    this.tripParticularsEditComponent?.resetForm();
    this.tripExpenditureEditComponent?.resetForm();
  }
}
