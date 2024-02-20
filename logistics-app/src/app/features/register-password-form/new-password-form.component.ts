import { Component, EventEmitter, Input, Output, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatInputModule } from '@angular/material/input';
import { AbstractControlOptions, FormBuilder, FormControl, FormGroup, FormGroupDirective, FormsModule, NgForm, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ErrorStateMatcher } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

export class PasswordStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const invalidCtrl = !!(control && control.invalid && control?.parent?.dirty);
    const invalidParent = !!(control && control.parent && control.parent.invalid && control.parent.dirty);

    return (invalidCtrl || invalidParent);
  }
}

@Component({
  selector: 'new-password-form',
  standalone: true,
  imports: [CommonModule, MatInputModule, FormsModule, MatFormFieldModule, ReactiveFormsModule, MatButtonModule, MatIconModule],
  templateUrl: './new-password-form.component.html',
  styleUrls: ['./new-password-form.component.scss']
})
export class NewPasswordFormComponent {
  @Output() passwordChange:EventEmitter<string> = new EventEmitter<string>();

  private _password:string = "";
  @Input() set password(value:string)
  {
    this._password = value;
    this.passwordChange.emit(value);
  }

  get password():string
  {
    return this._password;
  }
  
  private _formBuilder: FormBuilder = inject(FormBuilder);
  
  public passwordForm: FormGroup;

  public matcher = new PasswordStateMatcher();
  public confirmPassword: string = "";
  public passwordVisible: boolean = false;
  public passwordVisibleConfirm: boolean = false;

  constructor()
  {
    this.passwordForm = this._formBuilder.group(
      {
      passwordFormControl: ['', Validators.required], passwordConfirmFormControl: ['', Validators.required]
      }, {validator: this.checkPasswords} as AbstractControlOptions
    );
  }

  private checkPasswords(group: FormGroup) { // here we have the 'passwords' group
    let pass = group.controls["passwordFormControl"]?.value;
    let confirmPass = group.controls["passwordConfirmFormControl"]?.value;

    return pass === confirmPass ? null : { notSame: true }
  }

}
