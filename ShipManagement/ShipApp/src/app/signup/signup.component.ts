import { Component,inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import{AuthService} from '../services/auth.service';
import { User } from '../Models/user';
@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.css',
  providers:[AuthService]
})
export class SignupComponent {
  authService =inject(AuthService);
  userinfo:User|undefined;

  frm:FormGroup
  constructor(private fb:FormBuilder){
    this.frm =this.fb.group({
      firstname :this.fb.control('',[Validators.required]),
      middlename : this.fb.control('',[Validators.required]),
      lastname:this.fb.control('',[Validators.required]),
      email:this.fb.control('',[Validators.email]),
      password:this.fb.control('',[Validators.required]),
      ConfirmPassword :this.fb.control('',[Validators.required]),
      phone:this.fb.control('',[Validators.minLength(10),Validators.maxLength(10)]),
      address :this.fb.control('')
    });
  }
  saveUser(){
    if(this.frm.valid){
       this.userinfo=new User();
       
       this.userinfo.Firstname= this.frm.value.firstname;
       this.userinfo.Middlename = this.frm.value.middlename;
       this.userinfo.Lastname=this.frm.value.lastname,
       this.userinfo.Email=this.frm.value.email,
       this.userinfo.Password=this.frm.value.password,
       this.userinfo.ConfirmPassword = this.frm.value.ConfirmPassword,
       this.userinfo.Phone= this.frm.value.phone,
       this.userinfo.Address= this.frm.value.address;
       
       //console.log(this.frm.value.phone);
      
       console.log(this.userinfo);
      this.authService.addUser(this.userinfo);
    }
  }
}
