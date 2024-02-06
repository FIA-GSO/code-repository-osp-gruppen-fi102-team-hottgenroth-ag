import { Routes } from '@angular/router';
import { HomepageComponent } from '../pages/homepage/homepage.component';
import { LoginPageComponent } from '../pages/login-page/login-page.component';

export const routes: Routes = [
    {
      path: "",
      component: HomepageComponent
    },
    {
      path: "Login",
      component: LoginPageComponent
    },
  ];