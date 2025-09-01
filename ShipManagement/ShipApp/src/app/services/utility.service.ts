import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable, OnInit } from '@angular/core';
import { LoginResponse } from '../Models/LoginResponse';
import { Observable } from 'rxjs';
import { APP_CONSTANTS } from '../app.constants';
import { Crew } from '../Models/crew';
@Injectable({
  providedIn: 'root'
})
export class UtilityService implements OnInit {
  http = inject(HttpClient);
  static CurrentUser: LoginResponse;

  constructor() {
  }

  ngOnInit(): void {

  }

  static SetCurrentUser(user: LoginResponse) {
    this.CurrentUser = user;
  }

  getFullName(member: Crew | undefined): string | undefined {
    if (member == undefined || member == null)
      return "";

    return member.firstname + " " + member.middlename + " " + member.lastname;
  }
}
