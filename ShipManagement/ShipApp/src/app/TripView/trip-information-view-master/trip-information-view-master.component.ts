import { Component, inject, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { TripService } from '../../services/trip.service';
import { BoatService } from '../../services/boat.service';
import { Observable, Subscription } from 'rxjs';
import { Boat } from '../../Models/boat';
import { TripInformationViewComponent } from '../trip-information-view/trip-information-view.component';
import { TripExpenditureViewComponent } from '../trip-expenditure-view/trip-expenditure-view.component';
import { TripParticularsViewComponent } from '../trip-particulars-view/trip-particulars-view.component';

@Component({
  selector: 'app-trip-information-view-master',
  templateUrl: './trip-information-view-master.component.html',
  styleUrl: './trip-information-view-master.component.css'
})
export class TripInformationViewMasterComponent implements OnInit, OnDestroy {

  selectedTripId: number | undefined;
  SelectedBoatId: number | undefined;
  tripService = inject(TripService);
  boatservice = inject(BoatService)
  subscriptionList: Subscription[] = [];

 
  //boats: Boat[] = [];
  boats$!: Observable<Boat[]>;
  /**
   *
   */
  constructor() {

  }
  ngOnDestroy(): void {
    this.subscriptionList.forEach(sub => sub.unsubscribe());
  }
  ngOnInit(): void {
    this.boats$ = this.boatservice.boats$
  }

  getSelectedTripId(id: number) {
    this.selectedTripId = id;
  }

  GetTripInforamtionData(event: Event) {
    const val = (Number)((event.target as HTMLSelectElement).value);
    // this.tripInformationViewComponent.resetCache();
    // this.TripParticularsViewComponent.resetCache();
    // this.TripExpenditureViewComponent.resetCache();
    // this.SelectedBoatId =val;
    console.log('selected boatid for trip :' + val);
    this.SelectedBoatId =val;
    //this.tripService.updateSelectedValue(val);
  }


}
