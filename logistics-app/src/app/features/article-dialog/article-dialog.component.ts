import { CommonModule } from '@angular/common';
import { Component, Inject, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
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
import { LogisticsStoreService } from '../../services/stores/logistics-store.service';
import { LoadingSpinnerService } from '../../services/loading-spinner.service';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'article-dialog',
  standalone: true,
  imports: [CommonModule, MatDialogModule, MatFormFieldModule, MatInputModule,
     MatButtonModule, MatSelectModule, DatePickerComponent, 
     ReactiveFormsModule, MatCheckboxModule, MatIconModule],
  templateUrl: './article-dialog.component.html',
  styleUrl: './article-dialog.component.scss'
})
export class ArticleDialogComponent {
  //Die article FormGroup wird reserviert
  public article: FormGroup;

  //Je nach Rolle hat der User unterschiedliche Status die er setzen kann
  public get states(): eArticleState[]
  {
    if(this._auth.getUserRole() === eRole.keeper)
    {
      return [
        eArticleState.available,
        eArticleState.unavailable,
        eArticleState.expired,
        eArticleState.defect,
        eArticleState.none
      ]
    }
    if(this._auth.getUserRole() === eRole.admin)
    {
      return [
        eArticleState.defect,
        eArticleState.consumed, eArticleState.discarded, 
        eArticleState.donated, eArticleState.lost, 
        eArticleState.received, eArticleState.none,
        eArticleState.available,
        eArticleState.unavailable, eArticleState.expired,
      ]
    }
    return [
      eArticleState.defect,
      eArticleState.consumed, eArticleState.discarded, 
      eArticleState.donated, eArticleState.lost, 
      eArticleState.received, eArticleState.none
    ]
  }

  //Hier werden alle Service injected und so hier nutzbar gemacht
  private _auth: AuthService = inject(AuthService);
  private _formBuilder: FormBuilder = inject(FormBuilder);
  private _logisticsStore: LogisticsStoreService = inject(LogisticsStoreService);
  private _spinner: LoadingSpinnerService = inject(LoadingSpinnerService);
  private _dialogRef: MatDialogRef<ArticleDialogComponent> = inject(MatDialogRef<ArticleDialogComponent>)
  
  //Der ausgewählte Artikel wird übergeben
  constructor(@Inject(MAT_DIALOG_DATA) article: IArticleData){
    //Wenn ein Ablaufdatum existiert, gehen wir Sicher das es ein Dateobjekt ist
    if(article.expiryDate) article.expiryDate = new Date(article.expiryDate);

    //Wir instanziieren die ArticleFormGroup mit ihren benötigten FormConntrolls und den
    //Validatoren
    this.article = this._formBuilder.group({
      ...article,
      ...{quantity: [article.quantity, Validators.min(0)]},
      ...{articleName: [article.articleName]},
      ...{position: [article.position, Validators.min(1)]}
      });
  }

  //Wir setzen das ausgewählte Datum und erstellen daraus ein Dateobjekt
  public setDate(pDate: string)
  {
    if(!!this.article.value && !!this.article.value.expiryDate)
    {
      this.article.value.expiryDate = new Date(pDate);
    }
  }

  //Wenn die Checkbox gesetzt wird, löschen oder setzen wir das ExpiryDate 
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

  //Wir löschen das Artikel aus der Box und schließen den Dialog
  //Wichtig dabei, wir löschen nur die Zuweisung auf die Box, nicht das zugrunde
  //liegende Artikel
  public deleteArticleFromBox(): void
  {
    this._spinner.show("deleting article...", new Promise<void>(async(resolve, reject) => {
      if(!!this.article.value)
      {
        await this._logisticsStore.articleStore.delete((this.article.value as IArticleData).articleBoxAssignment);
        this._dialogRef.close(undefined);
        resolve();
      }
      else reject();
    }))
  }


  //Wir Prüfen ob der eingeloggte User eine der von uns definierten Rollen hat
  public isAuthorized(): boolean
  {
    let role: string = this._auth.getUserRole();

    if(role == eRole.admin || role == eRole.keeper)
    {
      return true;
    }
    return false;
  }
}
