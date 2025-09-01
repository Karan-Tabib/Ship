import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { Boat } from '../Models/boat';
import { BoatService } from '../services/boat.service';
import { ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable, Subscription } from 'rxjs';

@Component({
  selector: 'app-boat',
  templateUrl: './boat.component.html',
  styleUrl: './boat.component.css',
})
export class BoatComponent implements OnInit, OnDestroy {

  //boats: Boat[] = [];
  boatService = inject(BoatService);
  boatrecord: Boat | undefined;
  frm: FormGroup;
  subcriptionList: Subscription[] = [];

  boats$!: Observable<Boat[]>;
  /**
   *
   */
  constructor(private cdr: ChangeDetectorRef, private fb: FormBuilder) {
    this.frm = this.fb.group({
      boatId: this.fb.control(''),
      boatname: this.fb.control('', [Validators.required]),
      boatType: this.fb.control('', [Validators.required])
    })
  }
  ngOnDestroy(): void {
    this.subcriptionList.forEach(sub => sub.unsubscribe());
  }
  ngOnInit(): void {
    // this.boats = this.boatService.GetBoats();
    this.boats$ = this.boatService.boats$;
    this.boatService.loadBoats();
  }


  public Add() {
    if (this.frm.valid) {
      this.boatrecord = new Boat();
      this.boatrecord.boatName = this.frm.value.boatname
      this.boatrecord.boatType = this.frm.value.boatType

      this.subcriptionList.push(
        this.boatService.AddBoatRecord(this.boatrecord).subscribe({
          next: (data: any) => {
            alert('record added !');
            this.resetForm();
          },
          error: () => {
            console.error('Failed to add boat')
          }
        })
      );
    }
  }
  public resetForm() {
    this.frm.reset();
  }
  public update() {
    if (this.frm.valid) {
      this.boatrecord = new Boat();
      this.boatrecord.boatId = this.frm.value.boatId;
      this.boatrecord.boatName = this.frm.value.boatname;
      this.boatrecord.boatType = this.frm.value.boatType;

      this.subcriptionList.push(
        this.boatService.updateBoatRecord(this.boatrecord).subscribe({
          next: () => {
            alert("Record Updated successfully.");
            this.frm.reset();
          },
          error: () => {
            console.error('Failed to update boat')
          }
        })
      );
    }
  }
  public Edit(boat: Boat) {
    this.frm.reset();
    // var editBoat = this.boats[boatId];
    // this.frm.controls['boatId'].setValue(boat.boatId);
    // this.frm.controls['boatname'].setValue(boat.boatName);
    // this.frm.controls['boatType'].setValue(boat.boatType);

    this.frm.patchValue({
      boatId: boat.boatId,
      boatname: boat.boatName,
      boatType: boat.boatType
    });
  }

  public Remove(boat: Boat) {
    this.boatService.DeleteBoat(boat).subscribe({
      next: () => {
        console.log('Record Deleted');
      },
      error: () => {
        console.log('Record Not Deleted');
      }
    })
  }

}
