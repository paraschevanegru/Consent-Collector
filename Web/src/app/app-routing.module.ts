import { HelpComponent } from './help/help.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './authentication/login/login.component';
import { RegisterComponent } from './authentication/register/register.component';
import { AuthenticationComponent } from './authentication/authentication.component';
import { ProfileComponent } from './profile/profile.component';
import { AdminComponent } from './admin/admin.component';
import { ForgotPasswordComponent } from './authentication/forgot-password/forgot-password.component';


const routes: Routes = [
  { path: '', redirectTo: '/authentication/login', pathMatch: 'full' },
  { path: 'profile', redirectTo: '/authentication/login', pathMatch: 'full' },
  {
    path: 'authentication',
    component: AuthenticationComponent,
    children: [
      { path: '', redirectTo: '/authentication/login', pathMatch: 'full' },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'forgot_password', component: ForgotPasswordComponent}
    ]
  },
  { path: 'profile/:id', component: ProfileComponent },
  { path: 'admin/:id', component: AdminComponent },
  { path: 'help', component: HelpComponent }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
