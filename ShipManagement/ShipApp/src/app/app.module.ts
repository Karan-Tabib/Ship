import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { MenuComponent } from './menu/menu.component';
import { HomeComponent } from './home/home.component';
import { SignupComponent } from './signup/signup.component';
import { FooterComponent } from './footer/footer.component';
import { BoatComponent } from './boat/boat.component';
import { CrewComponent } from './crew/crew.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthInterceptor } from './Interceptors/auth-interceptor.service';
import { BoatCardComponent } from './boat-card/boat-card.component';
import { SalaryComponent } from './salary/salary.component';
import { RouterModule } from '@angular/router';
import { ViewRunningSalaryDetailsComponent } from './view-running-salary-details/view-running-salary-details.component';
import { FilterByBoatPipe } from './Pipes/filter-by-boat.pipe';
import { CommonModule } from '@angular/common';
import { LeavesComponent } from './leaves/leaves.component';
import { CurrentLeavesComponent } from './current-leaves/current-leaves.component';
import { FishComponent } from './fish/fish.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { MatInputModule } from '@angular/material/input';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { AutoCompleteComponent } from './auto-complete/auto-complete.component';
import { FilterFishPipe } from './Pipes/filter-fish.pipe';
import { TripInformationViewComponent } from './TripView/trip-information-view/trip-information-view.component';
import { TripParticularsViewComponent } from './TripView/trip-particulars-view/trip-particulars-view.component';
import { TripExpenditureViewComponent } from './TripView/trip-expenditure-view/trip-expenditure-view.component';
import { TripInformationViewMasterComponent } from './TripView/trip-information-view-master/trip-information-view-master.component';
import { TripInformationEditMasterComponent } from './TripEdit/trip-information-edit-master/trip-information-edit-master.component';
import { TripInformationEditComponent } from './TripEdit/trip-information-edit/trip-information-edit.component';
import { TripExpenditureEditComponent } from './TripEdit/trip-expenditure-edit/trip-expenditure-edit.component';
import { TripParticularsEditComponent } from './TripEdit/trip-particulars-edit/trip-particulars-edit.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    MenuComponent,
    HomeComponent,
    SignupComponent,
    FooterComponent,
    BoatComponent,
    CrewComponent,
    BoatCardComponent,
    SalaryComponent,
    FilterByBoatPipe,
    ViewRunningSalaryDetailsComponent,
    LeavesComponent,
    CurrentLeavesComponent,
    FishComponent,
    AutoCompleteComponent,
    FilterFishPipe,
    // TripInformationComponent,
    TripInformationViewComponent,
    TripParticularsViewComponent,
    TripExpenditureViewComponent,
    TripInformationViewMasterComponent,
    TripInformationEditMasterComponent,
    TripInformationEditComponent,
    TripExpenditureEditComponent,
    TripParticularsEditComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    CommonModule,
    MatInputModule,
    MatAutocompleteModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    provideAnimationsAsync()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
