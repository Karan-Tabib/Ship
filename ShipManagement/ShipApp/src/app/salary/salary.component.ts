import { AfterContentInit, Component, inject, OnDestroy, OnInit } from '@angular/core';
import { Boat } from '../Models/boat';
import { Crew } from '../Models/crew';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { BoatService } from '../services/boat.service';
import { Observable, Subscription } from 'rxjs';
import { CrewService } from '../services/crew.service';
import { Salary } from '../Models/salary';
import { SalaryService } from '../services/salary.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-salary',
  templateUrl: './salary.component.html',
  styleUrl: './salary.component.css'
})
export class SalaryComponent implements OnInit, OnDestroy, AfterContentInit {
  // boats: Boat[] = [];
  crewMembers: Crew[] = [];
  http = inject(HttpClient)
  frm: FormGroup;

  boatService = inject(BoatService);
  isEdit: boolean = false;
  subscriptionList: Subscription[] = [];
  salaries: Salary[] = [];
  Filtered: Salary[] = [];
  salrec: Salary | undefined;
  //service Injection
  crewService = inject(CrewService);
  salaryService = inject(SalaryService);

  years: number[] = [];
  SelectedBoatId: number = -1;
  router =inject(Router);

  boats$! :Observable<Boat[]>;
  /**
   *
   */
  constructor(private fb: FormBuilder) {
    this.frm = this.fb.group({
      //BoatList: fb.control('', [Validators.required]),
      CrewMembers: fb.control('', [Validators.required]),
      TotalSalary: fb.control('', [Validators.required]),
      startDate: fb.control('', [Validators.required]),
      endDate: fb.control('', [Validators.required]),
      startYear: fb.control('', [Validators.required]),
      Id: fb.control('')
    });

    this.HIdeId();
    this.subscriptionList.push(
      this.salaryService.getYearsData().subscribe({
        next: (data: number[]) => {
          //this.years = data;
          this.salaryService.Setyears(data);
        },
        error: () => {

        }
      })
    );
  }
  ngAfterContentInit(): void {
    // this.years = this.utilityService.getyears();
  }

  HIdeId() {
    var idCtrl = this.frm.get('Id');
    idCtrl?.clearValidators();
    idCtrl?.disable();
    idCtrl?.updateValueAndValidity();
    this.isEdit = false;
  }

  ShowId() {
    var idCtrl = this.frm.get('Id');
    idCtrl?.clearValidators();
    idCtrl?.addValidators([Validators.required]);
    idCtrl?.enable();
    idCtrl?.updateValueAndValidity();
    this.isEdit = true;
  }

  isVisible(): boolean {
    return this.isEdit;
  }
  ngOnDestroy(): void {
    this.subscriptionList.forEach(sub => sub.unsubscribe());
  }
  ngOnInit(): void {
    
    this.boats$ = this.boatService.boats$;
    
    this.subscriptionList.push(
      this.crewService.getAllCrewMembers().subscribe({
        next: (data: Crew[]) => {
          this.crewMembers = data;
        },
        error: () => {

        }
      })
    );


    this.years = this.salaryService.getyears();
    this.GetAllSalaryInfo();
  }
  AddSalary() {
    if (this.frm.valid) {
      this.salrec = new Salary()
      this.salrec.crewId = this.frm.controls['CrewMembers']?.value;
      this.salrec.forYear = this.frm.controls['startYear']?.value;
      this.salrec.startDate = this.frm.controls['startDate']?.value;
      this.salrec.endDate = this.frm.controls['endDate']?.value;
      this.salrec.totalSalary = this.frm.controls['TotalSalary']?.value;

      this.subscriptionList.push(
        this.salaryService.AddSalaryRecord(this.salrec).subscribe({
          next: (data: any) => {
            alert('record added!')
          },
          error: () => {
            console.log('record not added')
          }
        })
      );
      this.ResetForm();
      this.GetAllSalaryInfo();
    } else {
      this.findInvalidControls();
    }
  }
  UpdateSalary() {
    if (this.frm.valid) {
      let newRecord = new Salary();
      newRecord.id = this.frm.value.Id
      newRecord.crewId = this.frm.value.CrewMembers
      newRecord.forYear = this.frm.value.startYear
      newRecord.totalSalary = this.frm.value.TotalSalary
      newRecord.startDate = this.frm.value.startDate
      newRecord.endDate = this.frm.value.endDate
      this.subscriptionList.push(
        this.salaryService.updateSalaryRecord(newRecord).subscribe({
          next: (data: Salary) => {
            console.log(data);
            this.ResetForm();
            this.GetAllSalaryInfo();
            this.HIdeId();
          },
          error: () => {

          }
        })
      );
    }
  }
  ResetForm() {
    this.frm.reset();
  }
  Remove(rec: Salary) {
    this.subscriptionList.push(
      this.salaryService.DeleteSalary(rec).subscribe({
        next: (data: any) => {
          alert('record deleted!')
          this.GetAllSalaryInfo();
        },
        error: () => {

        }
      })
    );
  }
  Edit(sal: Salary) {
    this.frm.reset();
    this.ShowId();
    this.years = this.salaryService.getyears();
    this.frm.controls['Id'].setValue(sal.id);
    this.frm.controls['CrewMembers'].setValue(sal.crewId);
    this.frm.controls['TotalSalary'].setValue(sal.totalSalary);
    this.frm.controls['startYear'].setValue(sal.forYear);
    if (sal.startDate != undefined) {
      const formattedDate = new Date(sal.startDate).toISOString().split('T')[0];
      this.frm.controls['startDate'].setValue(formattedDate);
    }
    if (sal.endDate != undefined) {
      const formattedDate = new Date(sal.endDate).toISOString().split('T')[0];
      this.frm.controls['endDate'].setValue(formattedDate);
    }
    this.frm.updateValueAndValidity();
    //    new Date(sal.startDate).toISOString().substring(0, 10)
  }

  getCrewMemberName(id: number | undefined): string | undefined {
    if (id === undefined || id < 0) {
      // Handle the case where id is invalid
      //console.warn('Invalid ID or ID out of bounds');
      return undefined;
    }
    const crewmember = this.crewMembers.find(member => member.crewID == id);
    if (crewmember == undefined) {
      return "";
    }
    return this.getFullName(crewmember);
  }

  getFullName(member: Crew | undefined): string | undefined {
    if (member == undefined || member == null)
      return "";

    return member.firstname + " " + member.middlename + " " + member.lastname;
  }
  GetAllSalaryInfo() {
    this.subscriptionList.push(
      this.salaryService.getAllSalaryInfo().subscribe({
        next: (data: Salary[]) => {
          this.salaries = data;
          this.salaryService.setSalaries(data);
          //alert("Salary Records!")
        },
        error: () => {
          //alert(" Error Salary Records!")
        }
      })
    );
  }

  findInvalidControls() {
    const invalidControls = [];
    const controls = this.frm.controls;

    for (const name in controls) {
      if (controls[name].invalid) {
        invalidControls.push(name);
      }
    }
    console.log('Invalid controls:', invalidControls);
  }
  onBoatSelectionChange(event: Event) {
    const selectedvalue = (event.target as HTMLSelectElement).value;
    this.SelectedBoatId = Number(selectedvalue);
    console.log('selected boat id :' + this.SelectedBoatId);
  }

  View(sal :Salary){
    const queryParams = { crewid: sal.crewId, boatid:sal.boatId };
    this.router.navigate(['SalaryParticulars'],{queryParams});
  }
}
