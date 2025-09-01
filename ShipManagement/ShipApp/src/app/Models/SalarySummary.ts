
export class SalarySummary {

    salarySummaryId?: number | undefined;
    crewId?: number | undefined;
    receivedDate?: Date | undefined;
    createdDate?: Date | undefined;
    updatedDate?: Date | undefined;
    amountTaken?: number|undefined;
    description :string|undefined;
    SalaryId :number|undefined;
    /**
     *
     */
    constructor();
    constructor(Id?:number,year?:number,sdate?:Date,uDate?:Date,rDate?:Date,Cid?:number,sal?:number,bid?:number,desc?:string,sid?:number) {
        this.salarySummaryId =Id ?? 0;
        this.receivedDate =rDate ?? undefined;
        this.createdDate = sdate ?? undefined;
        this.updatedDate =uDate ?? undefined;
        this.crewId =Cid ?? 0;
        this.amountTaken =sal ??0;
        this.description =desc;
        this.SalaryId= sid ??0;
    }
}