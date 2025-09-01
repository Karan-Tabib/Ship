import { Component, inject, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Fish } from '../Models/fish';
import { debounceTime, of, Subscription, switchMap } from 'rxjs';
import { FishService } from '../services/fish.service';

@Component({
  selector: 'app-fish',
  templateUrl: './fish.component.html',
  styleUrl: './fish.component.css'
})
export class FishComponent implements OnInit, OnDestroy {

  frm: FormGroup;
  fishes: Fish[] = [];
  subscriptionList: Subscription[] = [];
  fishRec: Fish | undefined;
  fishService = inject(FishService);
  filteredOptions: string[] = [];
  searchText:string|undefined;
  frmSearch:FormGroup;

  constructor(private fb: FormBuilder) {
    this.frm = this.fb.group({
      fishName: this.fb.control('', [Validators.required]),
      fishId: this.fb.control('', [Validators.required])
    })

    this.frmSearch =this.fb.group({
      searchControl:this.fb.control('')
    })
  }
  ngOnInit(): void {
    this.getAllFish();
    
    this.frmSearch.controls['searchControl'].valueChanges.pipe(
      debounceTime(300), // Wait 300ms after user stops typing
      switchMap(value => this.searchFosh(value ?? ''))
    ).subscribe(
      {
      next:(results:any) => { this.filteredOptions = results;  console.log(" receiving data for search")},
      error:(error:any)=>{ console.log("Error receiving data for search")}
    })
  }

  searchFosh(query: string) {
    if (!query.trim()) {
      return of([]); // Return empty if search is empty
    }
    
    return this.fishService.SearchFish(query);
  }
  getAllFish() {
    this.subscriptionList.push(
      this.fishService.getAllFish().subscribe({
        next:(data:Fish[])=>{
          this.fishes =data;
        },
        error:()=>{
          console.log('fish data not received!')
        }
      })
    );
  }

  ngOnDestroy() {
    this.fishRec =undefined;
    this.subscriptionList.forEach(sub => sub.unsubscribe());
  }
  Add() {
    if (this.frm.valid) {
      this.fishRec = new Fish();
      this.fishRec.fishName = this.frm.controls['fishName'].value;

      this.subscriptionList.push(
        this.fishService.AddFishRecord(this.fishRec).subscribe({
          next: (data: Fish[]) => {
            this.fishes = data;
            this.getAllFish();
          },
          error: () => {
            console.log('fish data not added!');
          }
        })
      );
    }
  }
  update() {
    this.fishRec = new Fish();
    this.fishRec.fishId = this.frm.controls['fishId'].value;
    this.fishRec.fishName = this.frm.controls['fishName'].value;

    this.subscriptionList.push(
      this.fishService.updateFishRecord(this.fishRec).subscribe({
        next:(data:any)=>{
          console.log('Fish record updated!')
          this.getAllFish();
        },
        error:()=>{
          console.log('Fish record not updated!')
        }
      })
    );

  }
  resetForm() {
    this.frm.reset();
  }
  Edit(fish: Fish) {
    this,this.frm.reset();
    this.frm.get('fishId')?.setValue(fish.fishId);
    this.frm.get('fishName')?.setValue(fish.fishName);
    this.frm.updateValueAndValidity();
  }
  Remove(fish: Fish) {
    this.subscriptionList.push(
      this.fishService.DeleteFish(fish).subscribe({
        next:(data:any)=>{
          console.log('Fish record deleted!')
          this.getAllFish();
        },
        error:()=>{
          console.log('Fish record not updated!')
        }
      })
    );
  }
  Search(){
    this.searchText = this.frmSearch.controls['searchControl'].value;
  }

}
