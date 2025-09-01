import { ChangeDetectorRef, Component, EventEmitter, inject, Input, OnChanges, OnDestroy, OnInit, Output, SimpleChanges } from '@angular/core';
import { TripInformation } from '../../Models/trip-tripInformation';
import { Boat, } from '../../Models/boat';
import { find, map, Observable, Subscription } from 'rxjs'
import { TripService } from '../../services/trip.service';
import { BoatService } from '../../services/boat.service';
import { ActivatedRoute, Router } from '@angular/router';
@Component({
  selector: 'app-trip-information-view',
  templateUrl: './trip-information-view.component.html',
  styleUrl: './trip-information-view.component.css'
})
export class TripInformationViewComponent implements OnInit, OnDestroy, OnChanges {

  tripInformations: TripInformation[] = [];
  subscriptionList: Subscription[] = [];
  tripService = inject(TripService);
  boatService = inject(BoatService);
  boats$!: Observable<Boat[]>;
  @Input() boatId: number | undefined;
  selectedTripId: number | undefined;
  @Output()
  sendSelectedtripIdEvent = new EventEmitter<number>();

  
  constructor(private cdr:ChangeDetectorRef,private router:Router) {

  }
  ngOnInit(): void {
    this.tripService.selectedValue$.subscribe(value => {
      console.log('received boat Id from parent trip :' + value)
      this.boatId = value ?? -1;
    })

    this.boats$ = this.boatService.boats$;
  }

  ngOnChanges(changes: SimpleChanges): void {
    console.log('received boat Id from trip info table :' + this.boatId)
    if (this.boatId && this.boatId != -1) {
      this.subscriptionList.push(
        this.tripService.getAllTripInformationForBoat(this.boatId).subscribe({
          next: (data: TripInformation[]) => {
            this.onRowSelect(-1);
            this.tripInformations = data;
          },
          error: (err: any) => {
            console.error('Data not fetch!');
          }
        })
      );
    } else {
      this.onRowSelect(-1);
      this.tripInformations = [];
    }
  }



  ngOnDestroy(): void {
  }


  getBoatName(bid: number | undefined): Observable<string | undefined> {
    return this.boatService.boats$.pipe(
      map(boats => boats.find(rec => rec.boatId == bid)?.boatName)
    );
  }
  onRowSelect(tripId: number | undefined): void {
    console.log("Row selected:", tripId);
    if (tripId !== null || tripId !== undefined) {
      this.sendSelectedtripIdEvent.emit(tripId);
      this.tripService.updateSelectedRow(tripId ?? -1);
      this.selectedTripId = this.selectedTripId === tripId ? undefined : tripId;
      console.log("Selected Trip ID:", this.selectedTripId);
      this.cdr.detectChanges();
    }
  }

  Edit(event: Event, tripId: number): void {
    event.stopPropagation(); // Prevents triggering row click
    console.log("Edit clicked for:", tripId);
    this.router.navigate(['TripEdit'],{
      queryParams:{
        tripId:tripId
      }
    })
  }

  Remove(event: Event, tripId: number,boatId:number): void {
    event.stopPropagation(); // Prevents triggering row click
    console.log("Delete clicked for:", tripId);
    this.tripService.DeleteTrip(tripId).subscribe({
      next:(data:any)=>{
        console.log("Record Delete");
        this.boatId =boatId;
      },
      error:()=>{}
    })
  }

  public resetCache() {
    this.tripInformations = [];
  }

}
