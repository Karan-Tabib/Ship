export class User {
    Firstname?: string | undefined;
    Middlename?: string | undefined;
    Lastname?: string | undefined;
    Email?: string | undefined;
    Password?: string | undefined;
    ConfirmPassword?: string | undefined;
    Phone?: string | undefined;
    Address? : string |undefined;
    /**
     *
     */
    constructor();
    constructor(email: string, username: string, pass: string, phone: string,);

    constructor(email?: string, firstname?: string,middlename?: string, lastname?: string, 
                    pass?: string, phone?:string,confirmPass?:string,address?:string) {
        this.Firstname = firstname ?? undefined;
        this.Middlename = middlename ?? undefined;
        this.Lastname = lastname ?? undefined;
        this.Email = email ?? undefined;
        this.Password = pass ?? undefined;
        this.Phone = phone ?? undefined;
        this.ConfirmPassword = confirmPass ?? undefined;
        this.Address = address ?? undefined;
    }
}
