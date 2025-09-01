import { Component, inject, Input, OnChanges, OnDestroy, OnInit, SimpleChanges } from '@angular/core';
import { Subscription } from 'rxjs';
import { TripParticulars } from '../../Models/trip-particulars';
import { TripService } from '../../services/trip.service';
import { FishService } from '../../services/fish.service';

@Component({
  selector: 'app-trip-particulars-view',
  templateUrl: './trip-particulars-view.component.html',
  styleUrl: './trip-particulars-view.component.css'
})
export class TripParticularsViewComponent implements OnInit, OnDestroy, OnChanges {
  tripParticulars: TripParticulars[] = [];
  subscriptionList: Subscription[] = [];
  tripservice = inject(TripService);
  fishService =inject(FishService);

  @Input()
  tripId: number | undefined;

  constructor() {

  }
  ngOnChanges(changes: SimpleChanges): void {
    if(this.tripId && this.tripId ==-1){
      this.tripParticulars =[];
    }

    if (this.tripId && this.tripId > 0) {
      this.subscriptionList.push(
        this.tripservice.GetTripParticularDataById(this.tripId).subscribe({
          next: (data: TripParticulars[]) => { this.tripParticulars = data; },
          error: () => {
            console.error('trip particular data not fetch!');
          }
        })
      )
    }
  }
  ngOnDestroy(): void {

  }
  ngOnInit(): void {

  }

  Remove() {

  }
  Edit() {

  }
  public getFishName(fid:number|undefined){
    if(fid !=undefined){
      return this.fishService.GetFish().find(rec=>rec.fishId ==fid)?.fishName ?? '';
    }
    return '';
  }
  public resetCache(){
    this.tripParticulars=[];
  }
}
