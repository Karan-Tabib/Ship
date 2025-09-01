import { ChangeDetectorRef, Component, inject, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Boat } from '../Models/boat';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Crew } from '../Models/crew';
import { UtilityService } from '../services/utility.service';
import { Observable, Subscription } from 'rxjs';
import { BoatService } from '../services/boat.service';
import { CrewService } from '../services/crew.service';
import { SalarySummary } from '../Models/SalarySummary';
import { SalarySummaryService } from '../services/salary-summary.service';
import { ActivatedRoute } from '@angular/router';
import { Salary } from '../Models/salary';
import { SalaryService } from '../services/salary.service';

@Component({
  selector: 'app-view-running-salary-details',
  templateUrl: './view-running-salary-details.component.html',
  styleUrl: './view-running-salary-details.component.css'
})
export class ViewRunningSalaryDetailsComponent implements OnInit, OnChanges {

  SelectedBoatId: number | undefined;
  SelectedBoatId2: number | undefined;
  //boats: Boat[] = [];
  salariesSummaries: SalarySummary[] = [];
  salaries: Salary[] = [];
  crews: Crew[] = [];
  subscriptionList: Subscription[] = [];
  selectedBoat: number = -1;
  selectedBoat2: number = -1;
  selectedCrewId: number = -1;
  salarySummaryRec: SalarySummary | undefined;
  isEdit: boolean = false;
  boatService = inject(BoatService)
  crewService = inject(CrewService)
  utilityService = inject(UtilityService);
  salarySummerySrevice = inject(SalarySummaryService);
  salaryService = inject(SalaryService);
  isReadOnly: boolean = false;
  activatedRoute = inject(ActivatedRoute);
  queryParam: any;

  boats$!:Observable<Boat[]>;
  /**
   *
   */
  frm: FormGroup;
  Filterfrm: FormGroup;
  constructor(private fb: FormBuilder, private cdr: ChangeDetectorRef) {
    console.log("in Constructor!");
    this.frm = fb.group({
      CrewMembers: fb.control('', [Validators.required]),
      Amount: fb.control('', [Validators.required]),
      boatList: fb.control('', Validators.required),
      Id: fb.control(''),
      Description: fb.control('', [Validators.required]),
      receivedDate: fb.control('', [Validators.required]),
    });

    this.Filterfrm = fb.group({
      crewList2: fb.control('', [Validators.required]),
      BoatList2: fb.control('', Validators.required),
    })

    this.selectedCrewId = -1;
  }
  onCrewSelectionChanged() {
    this.selectedCrewId = this.frm.get('CrewMembers')?.value;
  }
  AddSalarySummary() {
    if (this.frm.valid) {
      this.salarySummaryRec = new SalarySummary();
      this.salarySummaryRec.crewId = this.frm.controls['CrewMembers'].value;
      this.salarySummaryRec.amountTaken = this.frm.controls['Amount'].value;
      this.salarySummaryRec.description = this.frm.controls['Description'].value;
      const now = new Date(); 
      this.salarySummaryRec.receivedDate = new Date(this.frm.controls['receivedDate'].value);
      this.salarySummaryRec.receivedDate.setHours(now.getHours(),now.getMinutes(),now.getSeconds(),now.getMilliseconds())
      this.salarySummaryRec.createdDate = now;
      this.salarySummaryRec.updatedDate = now;
      this.salarySummaryRec.SalaryId = this.salaries.find(rec => rec.crewId == this.selectedCrewId)?.id;
      this.subscriptionList.push(
        this.salarySummerySrevice.AddSalarySummaryRecord(this.salarySummaryRec).subscribe({
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
      this.salaryService.getAllSalaryInfo().subscribe({
        next: (data: Salary[]) => {
          this.salaries = data;
        },
        error: (error: any) => {
          alert('salary data not received!');
        }
      })
    );

    this.Filterfrm.controls['crewList2'].valueChanges.subscribe((newValue) => {
      if (newValue != undefined || newValue != null) {
        this.getSalarySummaryInfo(+newValue);
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
  Remove(rec: SalarySummary) {

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


  Edit(rec: SalarySummary) {
    this.frm.reset();
    this.ShowId();
    this.frm.controls['Id'].setValue(rec.salarySummaryId);
    this.frm.controls['CrewMembers'].setValue(rec.crewId);
    if (rec.crewId != undefined || rec.crewId != null) {
      this.frm.controls['boatList'].setValue(this.crews.find(item=>item.crewID ==rec.crewId)?.boatId);
    }
    this.frm.controls['Amount'].setValue(rec.amountTaken);
    this.frm.controls['Description'].setValue(rec.description);
    if (rec.receivedDate != undefined) {
      const formattedDate = new Date(rec.receivedDate).toISOString().split('T')[0];
      this.frm.controls['receivedDate'].setValue(formattedDate);
    }
    this.frm.updateValueAndValidity();
  }


  ResetForm() {
    this.frm.reset();
  }
  UpdateSalary() {
    if (this.frm.valid) {
      let newRecord = new SalarySummary();
      newRecord.salarySummaryId = this.frm.value.Id
      newRecord.crewId = this.frm.value.CrewMembers
      newRecord.amountTaken = this.frm.value.Amount
      newRecord.description = this.frm.value.Description
      newRecord.receivedDate = this.frm.value.receivedDate
      newRecord.updatedDate = new Date();

      this.subscriptionList.push(
        this.salarySummerySrevice.updateSalarySummaryRecord(newRecord).subscribe({
          next: (data: SalarySummary) => {
            console.log(data);
            this.ResetForm();
            if (newRecord.crewId !== undefined) {
              this.getSalarySummaryInfo(newRecord.crewId);
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
    this.getSalarySummaryInfo(+selectedCrew);
  }
  getSalarySummaryInfo(crewId: number) {
    console.log("in getSalarySummaryInfo");
    this.subscriptionList.push(
      this.salarySummerySrevice.getAllSalarySummaryInfo(crewId).subscribe({
        next: (data: SalarySummary[]) => {
          this.salariesSummaries = data;
        },
        error: (error: any) => {
          alert('record not received!' + error);
          console.log(error);
        }
      })
    );
  }
}
