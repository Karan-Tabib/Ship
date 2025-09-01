import { Injectable, inject } from '@angular/core';
import { HttpservicesService } from './httpservices.service';
import { Observable } from 'rxjs';
import { MethodType } from '../app.constants';
import { Fish } from '../Models/fish';
@Injectable({
  providedIn: 'root'
})
export class FishService {

  httpservice = inject(HttpservicesService);
  private AllFish:Fish[]=[];

  constructor() 
  { 

  }

   public getAllFish(): Observable<any> {
      return this.httpservice.handleHtppRequest(MethodType.GET, "Fish/All")
    }
  
    public AddFishRecord(rec: Fish): Observable<any> {
      return this.httpservice.handleHtppRequest(MethodType.POST, "Fish/Create", rec);
    }
  
    public updateFishRecord(rec: Fish): Observable<any> {
      return this.httpservice.handleHtppRequest(MethodType.PUT, "Fish/Update", rec)
    }
  
    public DeleteFish(rec: Fish): Observable<any> {
      return this.httpservice.handleHtppRequest(MethodType.DELETE, "Fish/Delete/" + rec.fishId)
    }

    public SearchFish(rec: String): Observable<any> {
      return this.httpservice.handleHtppRequest(MethodType.GET, "Fish/Search/?query=" + rec);
    }

    public LoadFish(){
      this.getAllFish().subscribe({
        next:(data:Fish[])=>{
          this.AllFish =data
        },
        error:()=>{
          console.error('Fish Data not Received!');
        }
      })
    }

    public GetFish():Fish[]{
      return this.AllFish;
    }
}
