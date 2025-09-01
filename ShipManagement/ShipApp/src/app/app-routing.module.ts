import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { SignupComponent } from './signup/signup.component';
import { BoatComponent } from './boat/boat.component';
import { CrewComponent } from './crew/crew.component';
import { AuthGuard } from './Guards/auth.guard';
import { SalaryComponent } from './salary/salary.component';
import { ViewRunningSalaryDetailsComponent } from './view-running-salary-details/view-running-salary-details.component';
import { LeavesComponent } from './leaves/leaves.component';
import { CurrentLeavesComponent } from './current-leaves/current-leaves.component';
import { FishComponent } from './fish/fish.component';
import { AutoCompleteComponent } from './auto-complete/auto-complete.component';
import { TripInformationViewMasterComponent } from './TripView/trip-information-view-master/trip-information-view-master.component';
import { TripInformationEditMasterComponent } from './TripEdit/trip-information-edit-master/trip-information-edit-master.component';

const routes: Routes = [
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'signup', component: SignupComponent },
  { path: 'ship', component: BoatComponent, canActivate: [AuthGuard] },
  { path: 'crew', component: CrewComponent, canActivate: [AuthGuard] },
  { path: 'Salary', component: SalaryComponent, canActivate: [AuthGuard] },
  { path: 'SalaryParticulars', component: ViewRunningSalaryDetailsComponent, canActivate: [AuthGuard] },
  { path: 'Leaves', component: LeavesComponent, canActivate: [AuthGuard] },
  { path: 'LeaveParticulars', component: CurrentLeavesComponent, canActivate: [AuthGuard] },
  { path: 'Fish', component: FishComponent, canActivate: [AuthGuard] },
  { path: 'AutoCompelete', component: AutoCompleteComponent, title: 'Auto Compelete Demo' },
  { path: 'Trip', component: TripInformationViewMasterComponent, title: 'Trip Information' },
  { path: 'TripEdit', component: TripInformationEditMasterComponent, title: 'Trip Alter' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
