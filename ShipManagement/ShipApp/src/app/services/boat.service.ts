import { inject, Injectable } from '@angular/core';
import { BehaviorSubject, catchError, Observable, of, tap } from 'rxjs';
import { HttpservicesService } from './httpservices.service';
import { Boat } from '../Models/boat';
import { MethodType } from '../app.constants';

@Injectable({
  providedIn: 'root'
})
export class BoatService {

  httpservice = inject(HttpservicesService);
  private AllBoats: Boat[] = [];

  private boatSubject = new BehaviorSubject<Boat[]>([]);
  boats$ = this.boatSubject.asObservable();

  constructor() { }

  // writing fresh methods to fetch data 

  public getAllBoats():Observable<Boat[]> {
    return this.httpservice.handleHtppRequest(MethodType.GET, "Boat/All");
  }

  public addBoatRecord(rec: Boat): Observable<any> {
    return this.httpservice.handleHtppRequest(MethodType.POST, "Boat/Create", rec);
  }

  public updateBoatRecord(rec: Boat): Observable<any> {
    return this.httpservice.handleHtppRequest(MethodType.PUT, "Boat/Update", rec)
  }

  public deleteBoat(rec: Boat): Observable<any> {
    return this.httpservice.handleHtppRequest(MethodType.DELETE, "Boat/Delete/" + rec.boatId)
  }
  public loadBoats(): void {
    this.getAllBoats().pipe(
      tap(data => this.boatSubject.next(data)), 
      catchError(error => {
        console.error('Failed to fetch boat data', error);
        this.boatSubject.next([]); // Fallback value
        return of([]); 
      })
    ).subscribe();
  }
  

  getBoats(): Observable<Boat[]> {
    return this.boats$; // Components will subscribe to this
  }

  public DeleteBoat(boat: Boat) {
    return this.httpservice.handleHtppRequest(MethodType.DELETE, `Boat/Delete/${boat.boatId}`).pipe(
      tap(() => {
        const updatedBoats = this.boatSubject.value.filter(b => b.boatId !== boat.boatId);
        this.boatSubject.next(updatedBoats);
      })
    )
  }

  
  public AddBoatRecord(newBoat: Boat) {
    return this.httpservice.handleHtppRequest(MethodType.POST, "Boat/Create", newBoat).pipe(
      tap((addedBoat: Boat) => {
        const updatedBoats = [...this.boatSubject.value, addedBoat];
        this.boatSubject.next(updatedBoats);
      })
    );
  }
  
}
