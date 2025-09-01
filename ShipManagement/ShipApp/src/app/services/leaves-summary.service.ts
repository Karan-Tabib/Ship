import { inject, Injectable } from '@angular/core';
import { HttpservicesService } from './httpservices.service';
import { HttpOptionsService } from './http-options.service';
import { Observable } from 'rxjs';
import { MethodType } from '../app.constants';
import { LeaveSummary } from '../Models/LeavesSummary';
import { HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class LeavesSummaryService {
 httpservice = inject(HttpservicesService);
  httpOptionService = inject(HttpOptionsService);
  constructor() { }
  crewId: number = -1;

  public getAllLeaveSummaryInfo(crewId: number): Observable<any> {
    // generate params heres
    this.setCrewId(crewId);
    return this.httpservice.handleHtppRequest(MethodType.GET, "LeaveSummary/All?crewId=" + crewId.toString(), null, this.GetOptionsForGetAllSummary());
  }

  public AddLeaveSummaryRecord(rec: LeaveSummary): Observable<any> {
    return this.httpservice.handleHtppRequest(MethodType.POST, "LeaveSummary/Create", rec);
  }

  public updateLeaveSummaryRecord(rec: LeaveSummary): Observable<any> {
    return this.httpservice.handleHtppRequest(MethodType.PUT, "LeaveSummary/Update", rec);
  }

  public DeleteLeaveSummary(rec: LeaveSummary): Observable<any> {
    return this.httpservice.handleHtppRequest(MethodType.DELETE, "LeaveSummary/Delete/" + rec.leaveSummaryId);
  }

  setCrewId(id: number) {
    this.crewId = id;
  }
  ResetCrewId(id: number) {
    this.crewId = -1;
  }
  getCrewId(): number {
    return this.crewId;
  }
  getParams(): HttpParams {
    let params = new HttpParams();
    const id = this.getCrewId();
    if (id !== null && id !== undefined) {
      params = params.set('crewId', id.toString());
    }
    return params;
  }


  GetOptionsForGetAllSummary(): any {
    const options = {
     // params: this.getParams(),
      headers: this.httpOptionService.getHeaders(),
    }

    return this.httpOptionService.generateHttpOptions(options);;
  }

}
