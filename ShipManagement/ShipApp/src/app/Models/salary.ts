export class Salary {

    id?: number | undefined;
    forYear?: number | undefined;
    startDate?: Date | undefined;
    endDate?: Date | undefined;
    crewId?: number | undefined;
    totalSalary?: number|undefined;
    boatId ?:number|undefined;
    /**
     *
     */
    constructor();
    constructor(Id?:number,year?:number,sdate?:Date,eDate?:Date,Cid?:number,totalsal?:number,bid?:number) {
        this.id =Id ?? 0;
        this.forYear =this.forYear ?? undefined;
        this.startDate = sdate ?? undefined;
        this.endDate =eDate ?? undefined;
        this.crewId =Cid ?? 0;
        this.totalSalary =totalsal ??0;
        this.boatId =bid ??0;
    }
}