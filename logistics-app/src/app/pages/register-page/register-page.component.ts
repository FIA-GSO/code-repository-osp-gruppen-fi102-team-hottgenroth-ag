import { Component, ViewChild, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDialog } from '@angular/material/dialog';
import { NewPasswordFormComponent } from '../../features/register-password-form/new-password-form.component';
import { ILoginData } from '../../models/ILoginData';
import { AuthService } from '../../services/authentication/auth.service';
import { MatIconModule } from '@angular/material/icon';
import { SharedDialogComponent } from '../../framework/shared-dialog/shared-dialog.component';
import { LoadingSpinnerService } from '../../services/loading-spinner.service';

@Component({
  selector: 'app-register-page',
  standalone: true,
  imports: [CommonModule, NewPasswordFormComponent,MatCardModule, MatIconModule,
    MatInputModule, MatButtonModule, FormsModule, MatFormFieldModule, ReactiveFormsModule],
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.scss']
})
export class RegisterPageComponent {
  @ViewChild("newPasswordForm", {static: false}) set passwordFormComponent(pContent: NewPasswordFormComponent)
  {
    if(!!pContent)
    {
      this.passwordForm = pContent.passwordForm;
    }
  }
  
  private _dialog: MatDialog = inject(MatDialog);
  private _router: Router = inject(Router);
  private _registerService: AuthService = inject(AuthService);
  private _spinner: LoadingSpinnerService = inject(LoadingSpinnerService);

  private _loginData: ILoginData = this.createLoginData();
  
  private _dialogSubscription!: Subscription;

  public emailFormControl: FormControl;
  public passwordForm: FormGroup | undefined;

  public get mail(): string
  {
    return this._loginData.userEmail;
  }

  public set mail(pMail: string)
  {
    this._loginData.userEmail = pMail;
  }

  public get password(): string
  {
    return this._loginData.password;
  }

  public set password(pPassword: string)
  {
    this._loginData.password = pPassword;
  }

  constructor()
  {
    this.emailFormControl = new FormControl('', [Validators.required, Validators.email]);   
  }

  public async register()
  {
    this._spinner.show("Please wait...", new Promise<void>(async(resolve, reject) => {
      try
      {
        await this._registerService.register(this._loginData)
      
        resolve();
        
        const dialogRef = this._dialog.open(SharedDialogComponent,
        {
          data: {
            icon: 'info',
            title: "Success",
            text: "The registration was successfull! You can log in now!",
            okButtonText: "Close"
          }
        });
  
        if(!!this._dialogSubscription)
        {
          this._dialogSubscription.unsubscribe();
        }
  
        this._dialogSubscription = dialogRef.afterClosed().subscribe(() => {
          this._router.navigate(["/login"]);
        });
      }
      catch(err: any)
      {
        console.log(err);
        reject();
      } 
    }))
  }

  private createLoginData(): ILoginData
  {
    return {
      userEmail: "",
      password: "",
    }
  }
}
