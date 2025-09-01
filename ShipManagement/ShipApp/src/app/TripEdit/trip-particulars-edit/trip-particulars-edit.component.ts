import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Fish } from '../../Models/fish';
import { Subscription } from 'rxjs';
import { FishService } from '../../services/fish.service';
import { TripParticulars } from '../../Models/trip-particulars';
import { TripExpenditure } from '../../Models/trip-expenditure';

@Component({
  selector: 'app-trip-particulars-edit',
  templateUrl: './trip-particulars-edit.component.html',
  styleUrl: './trip-particulars-edit.component.css'
})
export class TripParticularsEditComponent implements OnInit {
  frm: FormGroup;
  fishList: Fish[] = [];
  fishService = inject(FishService);
  subscriptionList: Subscription[] = [];

  constructor(private fb: FormBuilder) {
    this.frm = this.fb.group({
      tripDetails: this.fb.array([]) // Initializing FormArray
    });

    this.subscriptionList.push(
      this.fishService.getAllFish().subscribe({
        next: (data: Fish[]) => {
          this.fishList = data;
        },
        error: () => {
          console.log('fish data not received');
        }
      })
    );
  }

  ngOnInit() {
    // Add one initial row
    this.addRow();

    this.tripDetails.controls.forEach((control, index) => {
      this.SubscribeToChanges(control, index);
    })
  }
  SubscribeToChanges(tripFormGroup: AbstractControl, index: number) {
    const formGroup = tripFormGroup as FormGroup;
    formGroup.get('ratePerKg')?.valueChanges?.subscribe(value => {
      this.calculateTotalAmount();
    })

    formGroup.get('totalWeight')?.valueChanges.subscribe(value => {
      this.calculateTotalAmount();
    })
  }
  get tripDetails(): FormArray {
    return this.frm.get('tripDetails') as FormArray;
  }

  addRow() {
    const tripFormGroup = this.fb.group({
      tripParticularId: ['', Validators.required],
      tripId: ['', Validators.required],
      tripDate: ['', Validators.required],
      ratePerKg: ['', Validators.required],
      totalWeight: ['', Validators.required],
      amount: ['', Validators.required],
      fishId: ['', Validators.required]
    });

    this.tripDetails.push(tripFormGroup);

    this.SubscribeToChanges(tripFormGroup, this.tripDetails.length - 1);
  }

  removeRow(index: number) {
    if (this.tripDetails.length > 1) {
      this.tripDetails.removeAt(index);
    }
  }

  AddTripParticulars() {
    if (this.frm.valid) {
      console.log(this.frm.value);
    }
  }
  calculateTotalAmount() {
    for (let index = 0; index < this.tripDetails.length; index++) {
      const tripFormGroup = this.tripDetails.at(index) as FormGroup;
      const rate = tripFormGroup.get('ratePerKg')?.value;
      const wgt = tripFormGroup.get('totalWeight')?.value;
      if (!isNaN(rate) && rate != 0 && !isNaN(wgt) && wgt != 0) {
        const totalamt = rate * wgt;
        tripFormGroup.get('amount')?.setValue(totalamt);
      } else {
        tripFormGroup.get('amount')?.setValue('');
      }
    }
  }
  isValid(): boolean {
    return this.tripDetails.valid;
  }

  getFormData() {
    return this.tripDetails.value;
  }

  getData(): TripParticulars[] {
    return this.createTripParticularData();
  }
  createTripParticularData(): TripParticulars[] {
    const tripInfoArray = this.tripDetails.value || [];
    console.log("tripInfoArray:", tripInfoArray);
    console.log("Type of tripInfoArray:", typeof tripInfoArray);
    console.log("Is Array?", Array.isArray(tripInfoArray));
    console.log("Instance of Array?", tripInfoArray instanceof Array);
    console.log("Length:", Array.isArray(tripInfoArray) ? tripInfoArray.length : "Not an array");

    if (!Array.isArray(tripInfoArray)) {
      console.error("Expected an array but got:", this.tripDetails);
      return [];
    }
    const tripParticulars: TripParticulars[] = tripInfoArray.map(data => {
      const tripparticular = new TripParticulars();
      tripparticular.tripParticularId = data.tripParticularId;
      tripparticular.tripId = data.tripId;
      tripparticular.tripDate = new Date(data.tripDate);
      tripparticular.fishId =data.fishId;
      tripparticular.ratePerKg = data.ratePerKg;
      tripparticular.totalWeight = data.totalWeight;
      tripparticular.amount = data.amount;
      tripparticular.createdDate = new Date();
      tripparticular.updatedDate = new Date();
      return tripparticular;
    }
    );
    return tripParticulars;
  }
  resetForm(){
    this.frm.reset();
  }

  Edit(particulars :TripParticulars[]|undefined){
    if (!particulars) return;

    this.frm.reset(); // Reset the form before populating new data
  
    // Ensure enough rows exist in the form
    for (let rowcount = 0; rowcount < particulars.length; rowcount++) {
      if (rowcount < this.tripDetails.length) continue;
      else this.addRow();
    }
  
    this.tripDetails.patchValue(
      particulars.map(p => ({
        tripParticularId: p.tripParticularId,
        tripId: p.tripId,
        tripDate: p.tripDate?.toString().split('T')[0], // Format date to yyyy-mm-dd
        fishId: p.fishId,
        ratePerKg: p.ratePerKg,
        totalWeight: p.totalWeight,
        amount: p.amount
      }))
    );
  }
}