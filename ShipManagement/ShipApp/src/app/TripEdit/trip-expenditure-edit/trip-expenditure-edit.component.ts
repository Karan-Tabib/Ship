import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Fish } from '../../Models/fish';
import { Subscription } from 'rxjs';
import { TripExpenditure } from '../../Models/trip-expenditure';

@Component({
  selector: 'app-trip-expenditure-edit',
  templateUrl: './trip-expenditure-edit.component.html',
  styleUrl: './trip-expenditure-edit.component.css'
})

export class TripExpenditureEditComponent implements OnInit, OnDestroy {

  frmExp: FormGroup;
  subscriptionList: Subscription[] = [];
  fishList: Fish[] = [
   
  ]; // Mock fish list

  constructor(private fb: FormBuilder,private cd:ChangeDetectorRef) {
    this.frmExp = this.fb.group({
      tripExpenditureDetails: this.fb.array([]) // Initializing FormArray
    });
  }

  ngOnInit() {
    this.addRow(); // Add initial row to make sure form controls appear
  }

  get tripExpenditureDetails(): FormArray {
    return this.frmExp.get('tripExpenditureDetails') as FormArray;
  }

  addRow() {
    const tripFormGroup = this.fb.group({
      tripExpenditureId: ['', Validators.required],
      tripId: ['', Validators.required],
      tripDate: ['', Validators.required],
      reason: ['', Validators.required],
      amount: ['', Validators.required], 
    });

    this.tripExpenditureDetails.push(tripFormGroup);
    this.cd.detectChanges();
    console.log('Form Value:', this.frmExp.value); // Debugging
  }

  removeRow(index: number) {
    if (this.tripExpenditureDetails.length > 1) {
      this.tripExpenditureDetails.removeAt(index);
    }
  }

  AddTripExpenditures() {
    if (this.frmExp.valid) {
      console.log('Final Form Data:', this.frmExp.value);
    }
  }

  ngOnDestroy() {
    this.subscriptionList.forEach(sub => sub.unsubscribe()); // Unsubscribing to avoid memory leaks
  }

  isValid():boolean{
    return this.frmExp.valid;
  }

  getFormData(){
    return this.tripExpenditureDetails.value;
  }
   getData():TripExpenditure[]{
      return this.createTripExpenditureData();
    }

     
  createTripExpenditureData(): TripExpenditure[] {

    const tripInfoArray = Array.isArray(this.tripExpenditureDetails.value) ? this.tripExpenditureDetails.value : Object.values(this.tripExpenditureDetails.value);
    console.log("tripInfoArray:", tripInfoArray);
    console.log("Type of tripInfoArray:", typeof tripInfoArray);
    console.log("Is Array?", Array.isArray(tripInfoArray));
    console.log("Instance of Array?", tripInfoArray instanceof Array);
    console.log("Length:", Array.isArray(tripInfoArray) ? tripInfoArray.length : "Not an array");
    if (!Array.isArray(tripInfoArray)) {
      console.error("Expected an array but got:", this.tripExpenditureDetails.value);
      return [];
    }

    const tripExpenditures: TripExpenditure[] = tripInfoArray.map(data => {
      const tripExpenditure = new TripExpenditure();
      tripExpenditure.tripExpenditureId = data.tripExpenditureId;
      tripExpenditure.tripId = data.tripId;
      tripExpenditure.tripDate = new Date(data.tripDate);
      tripExpenditure.reason = data.reason;
      tripExpenditure.amount = data.amount;
      tripExpenditure.createdDate = new Date();
      tripExpenditure.updatedDate = new Date();
      return tripExpenditure;
    });
    return tripExpenditures;
  }
  resetForm(){
    this.frmExp.reset();
  }

  Edit(expenditures :TripExpenditure []|undefined){
      if(!expenditures) return;

      this.frmExp.reset();

      for (let rowcount = 0; rowcount < expenditures.length; rowcount++) {
        if(rowcount< this.tripExpenditureDetails.length )
          continue;
        else
        this.addRow();
      }

    this.tripExpenditureDetails.patchValue(
      expenditures.map(exp=>({
        tripExpenditureId :exp.tripExpenditureId,
        tripId :exp.tripId,
        tripDate :exp.tripDate?.toString().split('T')[0],
        reason :exp.reason,
        amount :exp.amount,
      }))
    );
  }
}
