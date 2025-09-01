export class TripExpenditure {
    tripExpenditureId: number | undefined;
    tripId: number | undefined;
    tripDate: Date | undefined;
    reason: string | undefined;
    amount: number | undefined;
    createdDate: Date | undefined;
    updatedDate: Date | undefined;
    boatId: number | undefined;

    /**
     *
     */
    constructor(tpid?: number,
        tid?: number,
        tdate?: Date,
        reasons?: string,
        amt?: number,
        cdate?: Date,
        udate?: Date,
        bid?: number) 
    {
        this.tripExpenditureId = tpid ?? 0;
        this.reason = reasons ?? undefined;
        this.tripId = tid ?? 0;
        this.tripDate = tdate ?? undefined
        this.amount = amt ?? 0;
        this.createdDate = cdate ?? undefined;
        this.updatedDate = udate ?? undefined;
        this.boatId = bid ?? 0;
    }
}
