export class TripParticulars {
    tripParticularId: number | undefined;
    tripId :number|undefined;
    tripDate: Date | undefined;
    fishId: number | undefined;
    ratePerKg :number |undefined;
    totalWeight:number|undefined;
    amount :number |undefined;
    createdDate: Date | undefined;
    updatedDate: Date | undefined;
    boatId: number | undefined;
  

    /**
     *
     */
    constructor(tpid?: number, 
                tname?:string,
                tid?: number, 
                tdate?: Date, 
                fid?: number, 
                rate?: number, 
                wt?: number,
                amt?: number,
                cdate?: Date, 
                udate?: Date, 
                bid?: number) 
    {
        this.tripParticularId = tpid ?? 0;
        //this.tripName =tname ?? undefined;
        this.tripId = tid ?? 0;
        this.tripDate = tdate ?? undefined
        this.fishId = fid ?? 0;
        this.ratePerKg = rate ?? 0;
        this.totalWeight = wt ?? 0;
        this.amount = amt ?? 0;
        this.createdDate = cdate ?? undefined;
        this.updatedDate = udate ?? undefined;
        this.boatId = bid ?? 0;
    }
}