
import { AfterContentInit, Component, inject, OnDestroy, OnInit } from '@angular/core';
import { Boat } from '../Models/boat';
import { Crew } from '../Models/crew';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BoatService } from '../services/boat.service';
import { Observable, Subscription } from 'rxjs';
import { CrewService } from '../services/crew.service';
import { Router } from '@angular/router';
import { Leaves } from '../Models/Leaves';
import { LeavesService } from '../services/leaves.service';
@Component({
  selector: 'app-leaves',
  templateUrl: './leaves.component.html',
  styleUrl: './leaves.component.css'
})
export class LeavesComponent  implements OnInit, OnDestroy, AfterContentInit {
  //boats: Boat[] = [];
  crewMembers: Crew[] = [];
  frm: FormGroup;

  boatService = inject(BoatService);
  isEdit: boolean = false;
  subscriptionList: Subscription[] = [];
  LeavesDetails: Leaves[] = [];
  Filtered: Leaves[] = [];
  leaveRec: Leaves | undefined;
  //service Injection
  crewService = inject(CrewService);
  leaveService = inject(LeavesService);

  years: number[] = [];
  SelectedBoatId: number = -1;
  router =inject(Router);

  boats$!:Observable<Boat[]>;
  /**
   *
   */
  constructor(private fb: FormBuilder) {
    this.frm = this.fb.group({
      //BoatList: fb.control('', [Validators.required]),
      CrewMembers: fb.control('', [Validators.required]),
      TotalLeaves: fb.control('', [Validators.required]),
      ForYear: fb.control('', [Validators.required]),
      endDate: fb.control('', [Validators.required]),
      startDate: fb.control('', [Validators.required]),
      Id: fb.control('')
    });

    this.HIdeId();
    this.subscriptionList.push(
      this.leaveService.getYearsData().subscribe({
        next: (data: number[]) => {
          //this.years = data;
          this.leaveService.Setyears(data);
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


    this.years = this.leaveService.getyears();
    this.GetAllSalaryInfo();
  }
  AddLeave() {
    if (this.frm.valid) {
      this.leaveRec = new Leaves()
      this.leaveRec.crewId = this.frm.controls['CrewMembers']?.value;
      this.leaveRec.forYear = this.frm.controls['ForYear']?.value;
      this.leaveRec.startDate = this.frm.controls['startDate']?.value;
      this.leaveRec.endDate = this.frm.controls['endDate']?.value;
      this.leaveRec.totalLeaves = this.frm.controls['TotalLeaves']?.value;

      this.subscriptionList.push(
        this.leaveService.AddLeaveRecord(this.leaveRec).subscribe({
          next: (data: any) => {
            alert('record added!')
          },
          error: () => {

          }
        })
      );
      this.ResetForm();
      this.GetAllSalaryInfo();
    } else {
      this.findInvalidControls();
    }
  }
  UpdateLeave() {
    if (this.frm.valid) {
      let newRecord = new Leaves();
      newRecord.leaveId = this.frm.value.Id
      newRecord.crewId = this.frm.value.CrewMembers
      newRecord.forYear = this.frm.value.ForYear
      newRecord.totalLeaves = this.frm.value.TotalLeaves
      newRecord.startDate = this.frm.value.startDate
      newRecord.endDate = this.frm.value.endDate
      this.subscriptionList.push(
        this.leaveService.updateLeaveRecord(newRecord).subscribe({
          next: (data: Leaves) => {
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
  Remove(rec: Leaves) {
    this.subscriptionList.push(
      this.leaveService.DeleteLeave(rec).subscribe({
        next: (data: any) => {
          alert('record deleted!')
          this.GetAllSalaryInfo();
        },
        error: () => {

        }
      })
    );
  }
  Edit(leave: Leaves) {
    this.frm.reset();
    this.ShowId();
    this.years = this.leaveService.getyears();
    this.frm.controls['Id'].setValue(leave.leaveId);
    this.frm.controls['CrewMembers'].setValue(leave.crewId);
    this.frm.controls['TotalLeaves'].setValue(leave.totalLeaves);
    this.frm.controls['ForYear'].setValue(leave.forYear);
    if (leave.startDate != undefined) {
      const formattedDate = new Date(leave.startDate).toISOString().split('T')[0];
      this.frm.controls['startDate'].setValue(formattedDate);
    }
    if (leave.endDate != undefined) {
      const formattedDate = new Date(leave.endDate).toISOString().split('T')[0];
      this.frm.controls['endDate'].setValue(formattedDate);
    }
    this.frm.updateValueAndValidity();
    //    new Date(sal.startDate).toISOString().substring(0, 10)
  }

  getCrewMemberName(id: number | undefined): string | undefined {
    if (id === undefined || id < 0) {
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
      this.leaveService.getAllLeaveInfo().subscribe({
        next: (data: Leaves[]) => {
          this.LeavesDetails = data;
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

  View(sal :Leaves){
    const queryParams = { crewid: sal.crewId, boatid:sal.leaveId };
    this.router.navigate(['ViewSalary'],{queryParams});
  }
}
