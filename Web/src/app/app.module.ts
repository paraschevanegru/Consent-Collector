import { HttpClientJsonpModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthenticationComponent } from './authentication/authentication.component';
import { LoginComponent } from './authentication/login/login.component';
import { RegisterComponent } from './authentication/register/register.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ProfileComponent } from './profile/profile.component';
import { MatIconModule } from '@angular/material/icon';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SurveyComponent } from './profile/survey/survey.component';
import { SurveyListComponent } from './profile/survey/survey-list/survey-list.component';
import { SurveyDetailsComponent } from './profile/survey/survey-list/survey-details/survey-details.component';
import { AdminComponent } from './admin/admin.component';
import { AddFormComponent } from './admin/add-form/add-form.component';
import { OptionalPipe } from './admin/optional.pipe';
import { AddAdminComponent } from './admin/add-admin/add-admin.component';
import { SurveyAdminListComponent } from './admin/survey-admin-list/survey-admin-list.component';
import { EditSurveyComponent } from './admin/edit-survey/edit-survey.component';
import { DatePipe } from '@angular/common';
import { NotificationsComponent } from './profile/notifications/notifications.component';
import { HelpComponent } from './help/help.component';
import { ForgotPasswordComponent } from './authentication/forgot-password/forgot-password.component';
@NgModule({
  declarations: [
    AppComponent,
    AuthenticationComponent,
    LoginComponent,
    RegisterComponent,
    ProfileComponent,
    SurveyComponent,
    SurveyListComponent,
    SurveyDetailsComponent,
    AdminComponent,
    AddFormComponent,
    OptionalPipe,
    AddAdminComponent,
    SurveyAdminListComponent,
    EditSurveyComponent,
    NotificationsComponent,
    HelpComponent,
    ForgotPasswordComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientJsonpModule,
    HttpClientModule,
    ReactiveFormsModule,
    MatIconModule,
    BrowserAnimationsModule
  ],
  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
