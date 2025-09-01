import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, RequiredValidator, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Login } from '../Models/Login';
import { AuthService } from '../services/auth.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  router = inject(Router);
  http = inject(HttpClient);
  login : Login |undefined;
  authService = inject(AuthService);
  /**
   *
   */
  loginForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.loginForm = this.fb.group({
      username: this.fb.control('', Validators.required),
      password: this.fb.control('', Validators.required)
    });

  }

  onLogin() {
    console.log("Login Button hit");
      if(this.loginForm.valid){
        this.login = new Login();
        this.login.Username = this.loginForm.value.username;
        this.login.Password = this.loginForm.value.password;
        this.authService.Login(this.login);
      }
      else{
        console.log("OOPS!!")
      }
  }

  redirectToSignUp() {
    this.router.navigate(['signup']);
  }
}
