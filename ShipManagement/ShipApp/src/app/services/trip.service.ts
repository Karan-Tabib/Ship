import { inject, Injectable } from '@angular/core';
import { HttpservicesService } from './httpservices.service';
import { BehaviorSubject, Observable } from 'rxjs';
import { MethodType } from '../app.constants';
import { TripData } from '../Models/trip-data';

@Injectable({
  providedIn: 'root'
})
export class TripService {

  httpservice = inject(HttpservicesService);
  private selectedValue = new BehaviorSubject<number | null>(null);
  private selectedRow = new BehaviorSubject<number | null>(null);

  selectedValue$ = this.selectedValue.asObservable();
  selectedRow$ = this.selectedRow.asObservable();

  /**
   *
   */
  constructor() {

  }

  updateSelectedValue(boatid:number){
    this.selectedValue.next(boatid);
  }

  updateSelectedRow(tripId:number){
    this.selectedRow.next(tripId);
  }
  public getAllTripData(): Observable<any> {
    return this.httpservice.handleHtppRequest(MethodType.GET, "Trip/All")
  }
  public getAllTripInformationForBoat(boatId: number): Observable<any> {
    return this.httpservice.handleHtppRequest(MethodType.GET, "Trip/Get/Trip/Boat/" + boatId)
  }
  public getAllTripDataByID(tripId: number): Observable<any> {
    return this.httpservice.handleHtppRequest(MethodType.GET, "Trip/Get/TripDetails/" + tripId)
  }

  public AddTripRecord(rec: any): Observable<any> {
    console.log(JSON.stringify(rec));
    return this.httpservice.handleHtppRequest(MethodType.POST, "Trip/Create", JSON.stringify(rec));
  }

  public updateTripRecord(rec: any): Observable<any> {
    return this.httpservice.handleHtppRequest(MethodType.PUT, "Trip/Update", rec)
  }

  public DeleteTrip(tripId: Number): Observable<any> {
    return this.httpservice.handleHtppRequest(MethodType.DELETE, "Trip/Delete/" +tripId);
  }
  public GetTripParticularDataById(pid: number): Observable<any> {
    return this.httpservice.handleHtppRequest(MethodType.GET, "Trip/Particulars/" + pid);
  }

  public GetTripExpenditureDataById(pid: number): Observable<any> {
    return this.httpservice.handleHtppRequest(MethodType.GET, "Trip/Expenditures/" + pid);
  }

  public GetTripInformationByTripId(pid: number): Observable<any> {
    return this.httpservice.handleHtppRequest(MethodType.GET, "Trip/Get/TripDetails/" + pid);
  }

  public UpdatetripData(tripData: TripData): Observable<any> {
    return this.httpservice.handleHtppRequest(MethodType.PATCH, "Trip/Update" ,tripData);
  }
}
