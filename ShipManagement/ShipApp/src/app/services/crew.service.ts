import { Injectable, inject } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { Crew } from '../Models/crew';
import { HttpservicesService } from './httpservices.service';
import { APP_CONSTANTS, MethodType } from '../app.constants';

@Injectable({
  providedIn: 'root'
})
export class CrewService {

  httpservice = inject(HttpservicesService);
  subscriptionList: Subscription[] = [];
  crewmembers: Crew[] = [];

  constructor() { }

  // writing fresh methods to fetch data 

  public getAllCrewMembers(): Observable<any> {
    return this.httpservice.handleHtppRequest(MethodType.GET, "Crew/All")
  }

  public AddCrewRecord(rec: Crew): Observable<any> {
    return this.httpservice.handleHtppRequest(MethodType.POST, "Crew/Create", rec);
  }

  public updateCrewRecord(rec: Crew): Observable<any> {
    return this.httpservice.handleHtppRequest(MethodType.PUT, "Crew/Update", rec)
  }

  public DeleteCrew(rec: Crew): Observable<any> {
    return this.httpservice.handleHtppRequest(MethodType.DELETE, "Crew/Delete/" + rec.boatId)
  }
  getCrewMembers(): Crew[] {
    return this.crewmembers;
  }

  setCrewmembers(members: Crew[]) {
    this.crewmembers = members;
  }

}
