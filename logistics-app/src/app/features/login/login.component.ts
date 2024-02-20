import { Component, inject } from '@angular/core';
import {MatIconModule} from '@angular/material/icon';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { AuthService } from '../../services/authentication/auth.service';
import { ILoginData } from '../../models/ILoginData';
import { HttpErrorResponse } from '@angular/common/http';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, MatFormFieldModule, MatInputModule, MatIconModule, 
    MatButtonModule, MatCardModule, RouterModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  private _loginService: AuthService = inject(AuthService);

  public hidePassword: boolean = true;
  public userName: string = "";
  public userPassword: string = "";

  constructor()
  {

  }

  public async login(): Promise<void>
  {
    if(!!this.userName && this.userName != "" || !!this.userPassword && this.userPassword != "")
    {
      let user: ILoginData = {
        userEmail: this.userName,
        password: this.userPassword
      }
      try
      {
        await this._loginService.login(user);
      }
      catch(err: any)
      {
        console.log(err)
      }
    }
  }
}
