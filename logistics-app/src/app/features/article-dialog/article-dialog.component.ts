import { CommonModule } from '@angular/common';
import { Component, Inject, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { IArticleData } from '../../models/IArticleData';
import { MatSelectModule } from '@angular/material/select';
import { eArticleState } from '../../models/enum/eArticleState';
import { DatePickerComponent } from '../date-picker/date-picker.component';
import { eRole } from '../../models/enum/eRole';
import { AuthService } from '../../services/authentication/auth.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatCheckboxModule } from '@angular/material/checkbox';

@Component({
  selector: 'article-dialog',
  standalone: true,
  imports: [CommonModule, MatDialogModule, MatFormFieldModule, MatInputModule,
     MatButtonModule, MatSelectModule, DatePickerComponent, ReactiveFormsModule, MatCheckboxModule],
  templateUrl: './article-dialog.component.html',
  styleUrl: './article-dialog.component.scss'
})
export class ArticleDialogComponent {
  public article: FormGroup;

  public get states(): eArticleState[]
  {
    return [
      eArticleState.defect,
      eArticleState.consumed, eArticleState.discarded, 
      eArticleState.donated, eArticleState.lost, 
      eArticleState.received, eArticleState.none
    ]
  }

  private _auth: AuthService = inject(AuthService);
  private _formBuilder: FormBuilder = inject(FormBuilder);

  constructor(@Inject(MAT_DIALOG_DATA) article: IArticleData){
    if(article.expiryDate) article.expiryDate = new Date(article.expiryDate);
    this.article = this._formBuilder.group({
      ...article,
      ...{quantity: [article.quantity, Validators.min(0)]},
      ...{articleName: [article.articleName]},
      ...{position: [article.position]}
      });
  }

  public setDate(pDate: string)
  {
    if(!!this.article.value && !!this.article.value.expiryDate)
    {
      this.article.value.expiryDate = new Date(pDate);
    }
  }

  public setHasExpiryDate(checked: boolean)
  {
    if(checked && !!this.article.value)
    {
      (this.article.value as IArticleData).expiryDate = new Date(); 
    }
    else
    {
      (this.article.value as IArticleData).expiryDate = undefined;
    }
  }

  public isAuthorized(): boolean
  {
    let role: string = this._auth.getUserRole();

    if(role == eRole.admin || role == eRole.keeper || role == eRole.leader)
    {
      return true;
    }
    return false;
  }
}
