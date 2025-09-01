import { Component, inject, Input, OnChanges, OnDestroy, OnInit, SimpleChanges } from '@angular/core';
import { Subscription } from 'rxjs';
import { TripExpenditure } from '../../Models/trip-expenditure';
import { TripService } from '../../services/trip.service';
import { BoatService } from '../../services/boat.service';

@Component({
  selector: 'app-trip-expenditure-view',
  templateUrl: './trip-expenditure-view.component.html',
  styleUrl: './trip-expenditure-view.component.css'
})
export class TripExpenditureViewComponent implements OnInit, OnDestroy, OnChanges {
  tripExpenditures: TripExpenditure[] = [];
  subscriptionList: Subscription[] = [];
  tripservice = inject(TripService);
  boatservice = inject(BoatService);
  @Input()
  tripId: number | undefined;

  constructor() {

  }

  ngOnChanges(changes: SimpleChanges): void {
    if (this.tripId && this.tripId == -1) {
      this.tripExpenditures = [];
    }
    if (this.tripId && this.tripId > 0) {
      this.subscriptionList.push(
        this.tripservice.GetTripExpenditureDataById(this.tripId).subscribe({
          next: (data: TripExpenditure[]) => { this.tripExpenditures = data; },
          error: () => {
            console.error('trip tripExpenditures data not fetch!');
          }
        })
      )
    } else {
      this.tripExpenditures = [];
    }
  }
  ngOnDestroy(): void {
    this.subscriptionList.forEach(sub => sub.unsubscribe());
  }
  ngOnInit(): void {

  }

  Remove() {

  }
  Edit() {

  }

  getBoatName(bid: number | undefined) {
    //this.boatservice.GetBoats().find(rec => rec.boatId == bid)?.boatName ?? '';
  }
  public resetCache(){
    this.tripExpenditures=[];
  }
}