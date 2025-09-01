export class LoginResponse {
    username: string | undefined;
    token: string | undefined;

    /**
     *
     */
    constructor(username :string,token :string) {
        this.username =username;
        this.token =token;

    }
}