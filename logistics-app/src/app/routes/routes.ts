import { Routes } from '@angular/router';
import { HomepageComponent } from '../pages/homepage/homepage.component';
import { LoginPageComponent } from '../pages/login-page/login-page.component';
import { ProjectPageComponent } from '../pages/project-page/project-page.component';
import { AuthGuard } from '../services/authentication/authguard';
import { RegisterPageComponent } from '../pages/register-page/register-page.component';
import { TransportBoxPageComponent } from '../pages/transport-box-page/transport-box-page.component';
import { UserManagementPageComponent } from '../pages/user-management-page/user-management-page.component';

export const routes: Routes = [
    {
      path: "",
      component: HomepageComponent,
      children: 
      [
        //Wenn keine Route angegeben wird, dann immer auf die Projektseite routen
        {
          path: '',  
          pathMatch: 'full',
          redirectTo: "/projects"
        },
        {
          path: "projects",
          component: ProjectPageComponent,
          canActivate: [AuthGuard]
        },
        {
          path: "transportbox",
          component: TransportBoxPageComponent,
          canActivate: [AuthGuard]
        },
        {
          path: "user",
          component: UserManagementPageComponent,
          //Checkt ob Token gültig ist
          canActivate: [AuthGuard]
        }
      ]
    },
    {
      path: "login",
      component: LoginPageComponent
    },
    {
      path: "register",
      component: RegisterPageComponent
    },
  ];