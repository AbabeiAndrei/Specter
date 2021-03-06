import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { ScrollingModule } from '@angular/cdk/scrolling';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { JwtInterceptor } from '../services/interceptors/jwt.interceptor';
import { ErrorInterceptor } from '../services/interceptors/error.interceptor';
import { AuthGuard } from '../services/auth.guard';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { TimesheetComponent } from './timesheet/timesheet.component';
import { TimesheetTableComponent } from './timesheet-table/timesheet-table.component';
import { LoginComponent } from './user-management/login/login.component';
import { RegisterComponent } from './user-management/register/register.component';
import { ForgotPasswordComponent } from './user-management/forgot-password/forgot-password.component';
import { LogoutComponent } from './user-management/logout/logout.component';
import { ReportsComponent } from './reports/reports.component';
import { ReportsFilterComponents } from './reports/reports-filter/reports-filter.component';
import { ReportsTableComponent } from './reports/reports-table/reports-table.component';
import { ProfileComponent } from './user-management/profile/profile.component';

import { TimesheetEditDialog } from './timesheet/timesheet-edit-dialog/timesheet-edit-dialog.component';
import { AdvancedFilterDialog } from './reports/reports-filter/advanced-filter-dialog.component';
import { InfoboxDialog } from './misc/infobox/infobox-dialog.component';

import { NgHttpLoaderModule } from 'ng-http-loader';

import {
  MatDatepickerModule,
  MatInputModule,
  MatNativeDateModule,
  MatSelectModule,
  MatButtonModule,
  MatButtonToggleModule,
  MatCheckboxModule,
  MatMenuModule,
  MatTableModule,
  MatIconModule,
  MatDialogModule,
  MatSnackBarModule,
  MatSlideToggleModule,
  MatExpansionModule,
  DateAdapter
} from '@angular/material';
import { SpecterDateAdapter } from 'src/utils/SpecterDateAdapter';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavMenuComponent,
    TimesheetComponent,
    ReportsComponent,
    TimesheetTableComponent,
    ReportsFilterComponents,
    ReportsTableComponent,
    LoginComponent,
    RegisterComponent,
    ForgotPasswordComponent,
    LogoutComponent,
    ProfileComponent,
    TimesheetEditDialog,
    AdvancedFilterDialog,
    InfoboxDialog
  ],
  exports: [
    DragDropModule,
    ScrollingModule,
    BrowserAnimationsModule,
    BrowserModule,
    FormsModule,
    MatDatepickerModule,
    MatInputModule,
    MatSelectModule,
    MatNativeDateModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatCheckboxModule,
    MatMenuModule,
    MatTableModule,
    MatIconModule,
    MatDialogModule,
    MatSnackBarModule,
    MatSlideToggleModule,
    MatExpansionModule,
    TimesheetTableComponent,
    ReportsFilterComponents,
    ReportsTableComponent,
    TimesheetEditDialog,
    AdvancedFilterDialog,
    InfoboxDialog
  ],
  imports: [
    HttpClientModule,
    DragDropModule,
    ScrollingModule,
    BrowserAnimationsModule,
    BrowserModule,
    FormsModule,
    MatDatepickerModule,
    MatInputModule,
    MatSelectModule,
    MatNativeDateModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatCheckboxModule,
    MatMenuModule,
    MatTableModule,
    MatIconModule,
    MatDialogModule,
    MatSnackBarModule,
    MatSlideToggleModule,
    MatExpansionModule,
    NgHttpLoaderModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' , canActivate: [AuthGuard]},
      { path: 'timesheet', component: TimesheetComponent, canActivate: [AuthGuard] },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'forgot', component: ForgotPasswordComponent },
      { path: 'logout', component: LogoutComponent},
      { path: 'profile', component: ProfileComponent},
      { path: 'reports', component: ReportsComponent, canActivate: [AuthGuard]},
      { path: '**', redirectTo: '' }
    ])
  ],
  entryComponents: [
    TimesheetEditDialog,
    AdvancedFilterDialog,
    InfoboxDialog
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: DateAdapter, useClass: SpecterDateAdapter},
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
