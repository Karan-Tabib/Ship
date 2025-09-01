
export class LeaveSummary {

    leaveSummaryId?: number | undefined;
    crewId?: number | undefined;
    startDate?: Date | undefined;
    endDate?: Date | undefined;
    createdDate?: Date | undefined;
    updatedDate?: Date | undefined;
    noDaysOff?: number|undefined;
    description :string|undefined;
    leaveId:number|undefined;
    /**
     *
     */
    constructor();
    constructor(Id?:number,year?:number,sdate?:Date,uDate?:Date,sDate?:Date,eDate?:Date,Cid?:number,sal?:number,bid?:number,desc?:string,lid?:number ){
        this.leaveSummaryId =Id ?? 0;
        this.startDate =sDate ?? undefined;
        this.endDate =eDate ?? undefined;
        this.createdDate = sdate ?? undefined;
        this.updatedDate =uDate ?? undefined;
        this.crewId =Cid ?? 0;
        this.noDaysOff =sal ??0;
        this.description =desc;
        this.leaveId =lid ??0;
    }
}