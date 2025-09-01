import { inject, Injectable } from '@angular/core';
import { Leaves } from '../Models/Leaves';
import { HttpservicesService } from './httpservices.service';
import { Observable } from 'rxjs';
import { MethodType } from '../app.constants';

@Injectable({
  providedIn: 'root'
})
export class LeavesService {
  httpservice = inject(HttpservicesService)
  Leaves: Leaves[] = [];
  years: number[] = [];
  constructor() { }

  public getAllLeaveInfo(): Observable<any> {
    return this.httpservice.handleHtppRequest(MethodType.GET, "Leave/All")
  }

  public AddLeaveRecord(rec: Leaves): Observable<any> {
    return this.httpservice.handleHtppRequest(MethodType.POST, "Leave/Create", rec)

  }
  public updateLeaveRecord(rec: Leaves): Observable<any> {
    return this.httpservice.handleHtppRequest(MethodType.PUT, "Leave/Update", rec)
  }

  public DeleteLeave(rec: Leaves): Observable<any> {
    return this.httpservice.handleHtppRequest(MethodType.DELETE, "Leave/Delete/" + rec.leaveId)
  }
  public getYearsData(): Observable<any> {
    return this.httpservice.handleHtppRequest(MethodType.GET, "StaticData/Years");
  }

  public getyears(): number[] {
    return this.years;
  }

  public Setyears(years: number[]) {
    this.years = years;
  }
}
