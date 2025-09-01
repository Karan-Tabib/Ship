export class Crew {

    crewID?: number | undefined;
    //crewType?: string | undefined;
    firstname?: string | undefined;
    middlename?: string | undefined;
    lastname?: string | undefined;
    boatId?: number | undefined;
    gender?: string | undefined;
    dob?: Date | undefined;
    mobileNumber?: number | undefined;
    address?: string | undefined;
    adharNumber?: number | undefined;
    /**
     *
     */
    constructor();
    constructor(
        crewID?: number,
        //crewType?: string | undefined;
        firstname?: string,
        middlename?: string,
        lastname?: string,
        boatId?: number,
        gender?: string,
        dateofbirth?: Date,
        mobileNumber?: number,
        address?: string,
        adharNumber?: number
    );

    constructor(
        Id?: number,
        //crewType?: string | undefined;
        fname?: string,
        mname?: string,
        lname?: string,
        bno?: number,
        gen?: string,
        dob?: Date,
        moNumber?: number,
        add?: string,
        adharNum?: number
    ) {
        this.crewID = Id ?? 0;
        //this.//crewType?: string | undefined;
        this.firstname = fname ?? undefined;
        this.middlename = mname ?? undefined;
        this.lastname = lname ?? undefined;
        this.boatId = bno ?? 0;
        this.gender = gen ?? undefined;
        this.dob = dob ?? undefined;
        this.mobileNumber = moNumber ?? undefined;
        this.address = add ?? undefined;
        this.adharNumber = adharNum ?? undefined;
    }
}
