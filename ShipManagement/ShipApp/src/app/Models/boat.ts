export class Boat {
    boatName?: string | undefined;
    boatId?: string | undefined;
    boatType?: string | undefined;
    userId ?: number |undefined;
    /**
     *
     */
    constructor();
    constructor(userid: string, username: string, pass: string);

    constructor(boatid?: string, boatname?: string, boatType?: string,id?:number) {
        this.boatId = boatid ?? undefined;
        this.boatName = boatname ?? undefined;
        this.boatType = boatType ?? undefined;
        this.userId = id??undefined;
    }
}
