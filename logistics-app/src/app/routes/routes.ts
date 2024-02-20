import { Routes } from '@angular/router';
import { HomepageComponent } from '../pages/homepage/homepage.component';
import { LoginPageComponent } from '../pages/login-page/login-page.component';
import { ProjectPageComponent } from '../pages/project-page/project-page.component';
import { AuthGuard } from '../services/authentication/authguard';

export const routes: Routes = [
    {
      path: "",
      component: HomepageComponent,
      children: 
      [
        {
          path: '',  
          pathMatch: 'full',
          redirectTo: "/projects"
        },
        {
          path: "projects",
          component: ProjectPageComponent,
          canActivate: [AuthGuard]
        }
      ]
    },
    {
      path: "login",
      component: LoginPageComponent
    },
  ];