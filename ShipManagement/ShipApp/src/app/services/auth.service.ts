import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable, OnDestroy } from '@angular/core';
import { User } from '../Models/user';
import { UtilityService } from './utility.service';
import { Login } from '../Models/Login';
import { LoginResponse } from '../Models/LoginResponse';
import { Router } from '@angular/router';
import { HttpservicesService } from './httpservices.service';
import { APP_CONSTANTS, MethodType } from '../app.constants';
import { Observable, Subscription } from 'rxjs';
import { HttpOptionsService } from './http-options.service';
@Injectable({
  providedIn: 'root'
})
export class AuthService implements OnDestroy {

  httpservice = inject(HttpservicesService);
  httpClient = inject(HttpClient);
  USERID: string | undefined;
  router = inject(Router)
  subscriptionList: Subscription[] = [];
  httpoptionservices = inject(HttpOptionsService);

  constructor() { }
  ngOnDestroy(): void {
    this.subscriptionList.forEach(sub => sub.unsubscribe());
  }

  getUserID(): string | undefined {
    if (this.USERID == undefined) {
      const token = localStorage.getItem('token'); // Assuming you stored the token in local storage
      if (token) {
        // Decode the token
        const decodedToken: any = jwtDecode(token);
        this.USERID = decodedToken.userID; // Adjust the key as per your token structure
      }
    }
    return this.USERID; // No token found
  }

  addUser(userToInsert: User) {
    let header = new HttpHeaders({ 'Content-Type': 'application/json' });

    this.subscriptionList.push(
      this.httpservice.handleHtppRequest(MethodType.POST, "User/Create", userToInsert, this.GetOptionsForLogin()).subscribe({
        next: (data: any) => {
          console.log("{{data | json}}");
        },
        error: (error: any) => {
          alert("User can not created :" + error)
        }
      })
    );
  }

  // Method to logout the user
  logout() {
    // Clear the token from local storage
    localStorage.removeItem('token');

    // Optionally clear any other user-related data
    localStorage.removeItem('userData');

    // Redirect to the login page
    this.router.navigate(['/login']);
  }

  // Helper method to check if the user is logged in
  isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }


  // myheaders =
  // {
  //   params: { key: 'value' },
  //   headers: { 'Custom-Header': 'example' },
  //   observe: 'response', // Observe the full HTTP response
  //   responseType: 'json', // Response type
  // };
  Login(userToInsert: Login) {

    this.subscriptionList.push(
      this.httpservice.handleHtppRequest(MethodType.POST, "Auth/Login", userToInsert, this.GetOptionsForLogin()).subscribe({
        next: (data: any) => {
          UtilityService.SetCurrentUser(data);
          localStorage.setItem("token", data.token);
          //alert("Login Successfull!!")
          this.router.navigate(['home']);
        },
        error: (error: any) => {
          alert("login failed:" + error.message);
        }
      })
    );
  }

  GetOptionsForLogin(): any {
    return {
      headers: this.getLoginHeaders(),
    };
  }
  

  public getLoginHeaders(): HttpHeaders {
    return new HttpHeaders({
      'Content-Type': 'application/json',
      accept: 'application/json',
    });
  }

}

function jwtDecode(token: string): any {
  throw new Error('Function not implemented.');
}



