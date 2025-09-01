import { inject, Injectable } from '@angular/core';
import { APP_CONSTANTS, MethodType } from '../app.constants';
import { Observable } from 'rxjs';
import { HttpservicesService } from './httpservices.service';
import { SalarySummary } from '../Models/SalarySummary';
import { HttpHeaders, HttpParams } from '@angular/common/http';
import { HttpOptionsService } from './http-options.service';
@Injectable({
  providedIn: 'root'
})
export class SalarySummaryService {
  httpservice = inject(HttpservicesService);
  httpOptionService = inject(HttpOptionsService);
  constructor() { }
  crewId: number = -1;

  public getAllSalarySummaryInfo(crewId: number): Observable<any> {
    // generate params heres
    this.setCrewId(crewId);
    return this.httpservice.handleHtppRequest(MethodType.GET, "SalarySummary/All?crewId=" + crewId.toString(), null, this.GetOptionsForGetAllSummary());
  }

  public AddSalarySummaryRecord(rec: SalarySummary): Observable<any> {
    return this.httpservice.handleHtppRequest(MethodType.POST, "SalarySummary/Create", rec);
  }

  public updateSalarySummaryRecord(rec: SalarySummary): Observable<any> {
    return this.httpservice.handleHtppRequest(MethodType.PUT, "SalarySummary/Update", rec);
  }

  public DeleteSalarySummary(rec: SalarySummary): Observable<any> {
    return this.httpservice.handleHtppRequest(MethodType.DELETE, "SalarySummary/Delete/" + rec.salarySummaryId);
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
