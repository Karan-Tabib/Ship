import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { Boat } from '../Models/boat';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { UtilityService } from '../services/utility.service';
import { Crew } from '../Models/crew';
import { map, Observable, Subscription } from 'rxjs';
import { HttpservicesService } from '../services/httpservices.service';
import { CrewService } from '../services/crew.service';
import { BoatService } from '../services/boat.service';

@Component({
  selector: 'app-crew',
  templateUrl: './crew.component.html',
  styleUrl: './crew.component.css'
})
export class CrewComponent implements OnInit, OnDestroy {

  boats: Boat[] = [];
  frm: FormGroup;
  crews: Crew[] = [];
  newRecord: Crew | undefined;
  SelectedBoatId: number = -1;
  subscrptionList: Subscription[] = [];
  crewService = inject(CrewService);
  boatService = inject(BoatService);
  boats$! : Observable<Boat[]>;
  /**
   *
   */
  constructor(private fb: FormBuilder) {
    this.frm = this.fb.group({
      crewId: this.fb.control(''),
      firstname: this.fb.control('', [Validators.required]),
      middlename: this.fb.control('', [Validators.required]),
      lastname: this.fb.control('', [Validators.required]),
      boatlist: this.fb.control('', [Validators.required]),
      gender: this.fb.control('', [Validators.required]),
      dateofbirth: this.fb.control('', [Validators.required]),
      mobileNumber: this.fb.control('', [Validators.required]),
      address: this.fb.control('', [Validators.required]),
      adharNumber: this.fb.control('', [Validators.required]),
    })
  }
  ngOnDestroy(): void {
    this.subscrptionList.forEach(sub => sub.unsubscribe());
  }
  ngOnInit(): void {
    this.frm.reset();
    //this.getBoats();
    this.boats$ = this.boatService.boats$;
    this.GetCrewMemberList();
  }

  public GetCrewMemberList() {
    this.subscrptionList.push(
      this.crewService.getAllCrewMembers().subscribe({
        next: (data: Crew[]) => {
          this.crews = data;
        },
        error: () => {

        }
      })
    );
  }
  public AddCrewMember() {
    if (this.frm.valid) {
      this.newRecord = new Crew();
      this.newRecord.firstname = this.frm.controls['firstname'].value;
      this.newRecord.middlename = this.frm.controls['middlename'].value;
      this.newRecord.lastname = this.frm.controls['lastname'].value;
      this.newRecord.address = this.frm.controls['address'].value;
      this.newRecord.gender = this.frm.controls['gender'].value;
      this.newRecord.adharNumber = this.frm.controls['adharNumber'].value;
      this.newRecord.boatId = this.frm.controls['boatlist'].value;
      this.newRecord.mobileNumber = this.frm.controls['mobileNumber'].value;
      this.newRecord.dob = this.frm.controls['dateofbirth'].value;

      this.subscrptionList.push(
        this.crewService.AddCrewRecord(this.newRecord).subscribe({
          next: (data: any) => {
            this.ResetForm();
            this.GetAllCrewMember();
          },
          error: () => {

          }
        })
      );
    }
  }
  public getBoats() {
    this.subscrptionList.push(
      this.boatService.getAllBoats().subscribe({
        next: (data: Boat[]) => {
          this.boats = data;
        },
        error: () => {

        }
      })
    );
  }

  public Edit(crew: Crew) {
    this.frm.reset();
    this.frm.controls['crewId'].setValue(crew.crewID);
    this.frm.controls['firstname'].setValue(crew.firstname);
    this.frm.controls['middlename'].setValue(crew.middlename);
    this.frm.controls['lastname'].setValue(crew.lastname);
    this.frm.controls['address'].setValue(crew.address);
    this.frm.controls['gender'].setValue(crew.gender);
    this.frm.controls['adharNumber'].setValue(crew.adharNumber);
    this.frm.controls['boatlist'].setValue(crew.boatId);
    this.frm.controls['mobileNumber'].setValue(crew.mobileNumber);
    if (crew.dob != undefined) {
      const formattedDate = crew.dob.toString().split('T')[0];
      this.frm.controls['dateofbirth'].setValue(formattedDate);
    }
    this.frm.updateValueAndValidity();
  }

  public Remove(crew: Crew) {

    this.subscrptionList.push(
      this.crewService.DeleteCrew(crew).subscribe({
        next: (data: any) => {
          alert("Record Deleted SuccessFully");
        },
        error: (error: any) => {
          alert("Record Not Deleted");
        }
      })
    );
  }

  public update() {
    if (this.frm.valid) {
      let newRecord = new Crew();
      newRecord.crewID = this.frm.value.crewId
      newRecord.firstname = this.frm.value.firstname
      newRecord.middlename = this.frm.value.middlename
      newRecord.lastname = this.frm.value.lastname
      newRecord.address = this.frm.value.address
      newRecord.gender = this.frm.value.gender
      newRecord.adharNumber = this.frm.value.adharNumber
      newRecord.boatId = this.frm.value.boatlist
      newRecord.mobileNumber = this.frm.value.mobileNumber
      newRecord.dob = this.frm.value.dateofbirth;

      this.subscrptionList.push(
        this.crewService.updateCrewRecord(newRecord).subscribe({
          next: (data: any) => {
            this.ResetForm();
            this.GetAllCrewMember();
          },
          error: () => {

          }
        })
      );
    }
  }
  GetAllCrewMember() {
    this.GetCrewMemberList();
  }
  getBoatName(Id: string | undefined): Observable<string | undefined> {
    //console.log(Id)
    return this.boats$.pipe(
      map(boat => boat.find(value => value.boatId == Id)?.boatName)  
      );
  }
  ResetForm() {
    this.frm.reset();
  }

}
