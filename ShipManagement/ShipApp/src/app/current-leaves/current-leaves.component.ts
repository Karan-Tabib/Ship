
import { ChangeDetectorRef, Component, inject, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Boat } from '../Models/boat';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Crew } from '../Models/crew';
import { UtilityService } from '../services/utility.service';
import { Observable, Subscription } from 'rxjs';
import { BoatService } from '../services/boat.service';
import { CrewService } from '../services/crew.service';
import { ActivatedRoute } from '@angular/router';
import { LeaveSummary } from '../Models/LeavesSummary';
import { LeavesSummaryService } from '../services/leaves-summary.service';
import { Leaves } from '../Models/Leaves';
import { LeavesService } from '../services/leaves.service';

@Component({
  selector: 'app-current-leaves',
  templateUrl: './current-leaves.component.html',
  styleUrl: './current-leaves.component.css'
})
export class CurrentLeavesComponent implements OnInit, OnChanges {

  SelectedBoatId: number | undefined;
  SelectedBoatId2: number | undefined;
 // boats: Boat[] = [];
  leaves: Leaves[] = [];
  leavesSummaries: LeaveSummary[] = [];
  crews: Crew[] = [];
  subscriptionList: Subscription[] = [];
  selectedBoat: number = -1;
  selectedBoat2: number = -1;
  leaveSummaryRec: LeaveSummary | undefined;
  isEdit: boolean = false;
  boatService = inject(BoatService)
  crewService = inject(CrewService)
  utilityService = inject(UtilityService);
  leaveSummaryService = inject(LeavesSummaryService);
  leaveService = inject(LeavesService);
  isReadOnly: boolean = false;
  activatedRoute = inject(ActivatedRoute);
  queryParam: any;
  selectedCrewId: number = -1;

  boats$! :Observable<Boat[]>;
  /**
   *
   */
  frm: FormGroup;
  Filterfrm: FormGroup;
  constructor(private fb: FormBuilder, private cdr: ChangeDetectorRef) {
    console.log("in Constructor!");
    this.frm = fb.group({
      Id: fb.control(''),
      boatList: fb.control('', Validators.required),
      CrewMembers: fb.control('', [Validators.required]),
      NoDaysOff: fb.control('', [Validators.required]),
      startDate: fb.control('', [Validators.required]),
      endDate: fb.control('', [Validators.required]),
      Description: fb.control('', [Validators.required]),
    });

    this.Filterfrm = fb.group({
      crewList2: fb.control('', [Validators.required]),
      BoatList2: fb.control('', Validators.required),
    })


  }

  AddLeaveSummary() {
    if (this.frm.valid) {
      this.leaveSummaryRec = new LeaveSummary();
      this.leaveSummaryRec.crewId = this.frm.controls['CrewMembers'].value;
      this.leaveSummaryRec.noDaysOff = this.frm.controls['NoDaysOff'].value;
      this.leaveSummaryRec.description = this.frm.controls['Description'].value;
      this.leaveSummaryRec.startDate = this.frm.controls['startDate'].value;
      this.leaveSummaryRec.endDate = this.frm.controls['endDate'].value;
      this.leaveSummaryRec.createdDate = new Date();
      this.leaveSummaryRec.updatedDate = new Date();
      this.leaveSummaryRec.leaveId = this.leaves.find(rec => rec.crewId == this.selectedCrewId)?.leaveId;
      this.subscriptionList.push(
        this.leaveSummaryService.AddLeaveSummaryRecord(this.leaveSummaryRec).subscribe({
          next: (data: any) => {
            alert('data added successfully.');
            this.ResetForm();
          },
          error: () => {
            alert('data not submitted.');
          }
        })
      );

    } else {
      alert('Fill the forms Details!')
    }
  }
  ngOnChanges(changes: SimpleChanges): void {
    console.log("in ngOnChanges!");
    console.log('selected boat:' + this.selectedBoat);
  }
  ngOnInit(): void {
    console.log("in ngOnInit!");
   
    this.boats$ = this.boatService.boats$;
    this.subscriptionList.push(
      this.crewService.getAllCrewMembers().subscribe({
        next: (data: Crew[]) => {
          this.crews = data;
        },
        error: () => {

        }
      })
    );

  this.subscriptionList.push(
    this.leaveService.getAllLeaveInfo().subscribe({
      next:(data:Leaves[])=>{
        this.leaves =data;
      },
      error:()=>{
        alert('Leaves record not found!');
      }
    })
    );

    this.Filterfrm.controls['crewList2'].valueChanges.subscribe((newValue) => {
      if (newValue != undefined || newValue != null) {
        this.getLeaveSummaryInfo(+newValue);
      }
    })
    this.activatedRoute.queryParams.subscribe(params => {
      this.queryParam = params;
      //console.log(this.queryParam.crewid + " " + this.queryParam.boatid);
      if (params['boatid']) {
        this.Filterfrm.controls['BoatList2'].setValue(+params['boatid']);
        //console.log('BoatList2 updated to:', this.Filterfrm.controls['BoatList2'].value);
      }
      if (params['crewid']) {
        this.Filterfrm.controls['crewList2'].setValue(+params['crewid'], { emitEvent: true });
        this.cdr.detectChanges();
        //this.onCrewMember2Changed({ target: { value: this.queryParam.crewId } }); 
      }
    })


  }
  onCrewSelectionChanged() {
    this.selectedCrewId = this.frm.get('CrewMembers')?.value;
  }
  HIdeId() {
    var idCtrl = this.frm.get('Id');
    idCtrl?.clearValidators();
    idCtrl?.disable();
    idCtrl?.updateValueAndValidity();
    this.isEdit = false;
    this.isReadOnly = !this.isReadOnly;
  }

  ShowId() {
    var idCtrl = this.frm.get('Id');
    idCtrl?.clearValidators();
    idCtrl?.addValidators([Validators.required]);
    idCtrl?.enable();
    idCtrl?.updateValueAndValidity();
    this.isEdit = true;
    this.isReadOnly = !this.isReadOnly;
  }
  Remove(rec: LeaveSummary) {

  }
  getCrewMemberName(id: number | undefined): string | undefined {
    if (id === undefined || id < 0) {
      // Handle the case where id is invalid
      //console.warn('Invalid ID or ID out of bounds');
      return undefined;
    }
    const crewmember = this.crews.find(member => member.crewID == id);
    if (crewmember == undefined) {
      return "";
    }
    return this.utilityService.getFullName(crewmember);
  }


  Edit(rec: LeaveSummary) {
    this.frm.reset();
    this.ShowId();
    this.frm.controls['Id'].setValue(rec.leaveSummaryId);
    this.frm.controls['CrewMembers'].setValue(rec.crewId);
    if (rec.crewId != undefined || rec.crewId != null) {
      this.frm.controls['boatList'].setValue(this.crews[rec.crewId]?.boatId);
    }
    this.frm.controls['NoDaysOff'].setValue(rec.noDaysOff);
    this.frm.controls['Description'].setValue(rec.description);
    if (rec.startDate != undefined) {
      const formattedDate = new Date(rec.startDate).toISOString().split('T')[0];
      this.frm.controls['startDate'].setValue(formattedDate);
    }
    if (rec.endDate != undefined) {
      const formattedDate = new Date(rec.endDate).toISOString().split('T')[0];
      this.frm.controls['endDate'].setValue(formattedDate);
    }
    this.frm.updateValueAndValidity();
  }


  ResetForm() {
    this.frm.reset();
  }
  UpdateLeaveSummary() {
    if (this.frm.valid) {
      let newRecord = new LeaveSummary();
      newRecord.leaveSummaryId = this.frm.value.Id
      newRecord.crewId = this.frm.value.CrewMembers
      newRecord.noDaysOff = this.frm.value.NoDaysOff
      newRecord.description = this.frm.value.Description
      newRecord.startDate = this.frm.value.startDate
      newRecord.endDate = this.frm.value.endDate
      newRecord.updatedDate = new Date();

      this.subscriptionList.push(
        this.leaveSummaryService.updateLeaveSummaryRecord(newRecord).subscribe({
          next: (data: LeaveSummary) => {
            console.log(data);
            this.ResetForm();
            if (newRecord.crewId !== undefined) {
              this.getLeaveSummaryInfo(newRecord.crewId);
            }
            this.HIdeId();
          },
          error: () => {

          }
        })
      );
    }
  }
  isVisible() {
    return this.isEdit;
  }
  onBoatSelectionChange(event: Event) {
    console.log("in onBoatSelectionChange");
    const selectedValue = (event.target as HTMLSelectElement).value
    this.selectedBoat = (Number)(selectedValue);
  }

  onBoat2SelectionChange(event: Event) {
    console.log("in onBoat2SelectionChange");
    const selectedval = (event.target as HTMLSelectElement).value;
    this.selectedBoat2 = (Number)(selectedval);
  }

  onCrewMember2Changed(event: Event) {
    console.log("in onCrewMember2Changed");
    const selectedCrew = (event.target as HTMLSelectElement).value;
    this.getLeaveSummaryInfo(+selectedCrew);
  }
  getLeaveSummaryInfo(crewId: number) {
    console.log("in getLeaveSummaryInfo");
    this.subscriptionList.push(
      this.leaveSummaryService.getAllLeaveSummaryInfo(crewId).subscribe({
        next: (data: LeaveSummary[]) => {
          this.leavesSummaries = data;
        },
        error: (error: any) => {
          alert('record not received!' + error);
          console.log(error);
        }
      })
    );
  }
}
