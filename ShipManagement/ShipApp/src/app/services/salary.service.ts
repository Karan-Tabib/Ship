import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { UtilityService } from './utility.service';
import { Observable } from 'rxjs';
import { Salary } from '../Models/salary';
import { APP_CONSTANTS, MethodType } from '../app.constants';
import { HttpservicesService } from './httpservices.service';

@Injectable({
  providedIn: 'root'
})
export class SalaryService {
  httpservice = inject(HttpservicesService)
  salaries: Salary[] = [];
  years: number[] = [];
  constructor() { }

  public getAllSalaryInfo(): Observable<any> {
    return this.httpservice.handleHtppRequest(MethodType.GET,"Salary/All")
  }

  public AddSalaryRecord(rec: Salary): Observable<any> {
    return this.httpservice.handleHtppRequest(MethodType.POST,"Salary/Create", rec)

  }
  public updateSalaryRecord(rec: Salary): Observable<any> {
    return this.httpservice.handleHtppRequest(MethodType.PUT,"Salary/Update", rec)
  }

  public DeleteSalary(rec: Salary): Observable<any> {
    return this.httpservice.handleHtppRequest(MethodType.DELETE, "Salary/Delete/" + rec.id)
  }
  GetAllSalaries(): Salary[] {
    return this.salaries;
  }
  setSalaries(pay: Salary[]) {
    this.salaries = pay;
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
