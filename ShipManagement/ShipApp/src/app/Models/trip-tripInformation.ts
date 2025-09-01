export class TripInformation {
    tripId: number | undefined;
    tripName: string | undefined;
    tripStartDate: Date | undefined;
    tripEndDate: Date | undefined;
    tripDescription: string | undefined;
    boatId: number | undefined;
    createdDate: Date | undefined;
    updatedDate: Date | undefined;
    /**
     *
     */
    constructor(tid?: number, tname?: string, tstartdate?: Date, tendtdate?: Date, tdesc?: string, bid?: number) 
    {
        this.tripId = tid ?? 0;
        this.tripName = tname ?? undefined
        this.tripStartDate = tstartdate ?? undefined;
        this.tripEndDate = tendtdate ?? undefined;
        this.tripDescription = tdesc ?? undefined;
        this.boatId = bid ?? 0;
    }
}
