
export class Leaves {

    leaveId?: number | undefined;
    forYear?: number | undefined;
    startDate?: Date | undefined;
    endDate?: Date | undefined;
    crewId?: number | undefined;
    totalLeaves?: number | undefined;
    boatId ?:number|undefined;

    /**
     *
     */
    constructor();
    constructor(Id?: number, year?: number, sdate?: Date, eDate?: Date, Cid?: number, totalleave?: number, bid?: number) {
        this.leaveId = Id ?? 0;
        this.forYear = year?? undefined;
        this.startDate = sdate ?? undefined;
        this.endDate = eDate ?? undefined;
        this.crewId = Cid ?? 0;
        this.totalLeaves = totalleave ?? 0;
        this.boatId =bid ??0;
    }
}
