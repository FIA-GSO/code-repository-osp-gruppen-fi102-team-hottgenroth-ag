import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, Input, inject } from '@angular/core';
import { IArticleData } from '../../models/IArticleData';
import { LogisticsStoreService } from '../../services/stores/logistics-store.service';
import { MatDividerModule } from '@angular/material/divider';
import { TruncatePipe } from '../../framework/TruncatePipe';
import { MatDialog } from '@angular/material/dialog';
import { ArticleDialogComponent } from '../article-dialog/article-dialog.component';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
import { AuthService } from '../../services/authentication/auth.service';
import { eRole } from '../../models/enum/eRole';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'article-list',
  standalone: true,
  imports: [CommonModule, MatDividerModule, TruncatePipe,
    MatExpansionModule, MatIconModule, MatButtonModule, MatListModule],
  templateUrl: './article-list.component.html',
  styleUrl: './article-list.component.scss'
})
export class ArticleListComponent{
  @Input() articles!: IArticleData[];

  private _logisticStore: LogisticsStoreService = inject(LogisticsStoreService);
  private _dialog: MatDialog = inject(MatDialog);
  private _auth: AuthService = inject(AuthService);
  private _cd: ChangeDetectorRef = inject(ChangeDetectorRef);
  private _snackbar: MatSnackBar = inject(MatSnackBar);

  constructor(){}

  public getSortedArticles()
  {
    return this.articles.sort((a, b) => a.position.toString().localeCompare(b.position.toString(), undefined, {numeric: true}))
  }

  public isArticleFirstPosition(article: IArticleData): boolean
  {
    var firstPositionList = this.getAllFirstPositions();
    if(firstPositionList.length > 0)
    {
      return !!firstPositionList.find(art => art.articleGuid === article.articleGuid);
    }
    return false;
  }

  public getAllArticlesFromSamePositions(article: IArticleData): IArticleData[]
  {
    let artpos: string = article.position.toString();
    let articles = this.getSortedArticles();
    return articles.filter(item => this.getPosNumber(item.position) === artpos && item.position.toString().includes("."));
  }

  private getPosNumber(pos: number): string
  {
    return pos.toString().substring(0, pos.toString().indexOf("."))
  }

  private hasPosNumberParent(pos: number): boolean
  {
    let posFirst = this.getPosNumber(pos);
    let allFirstPositions = this.getAllFirstPositions();
    let parent = allFirstPositions.find(art => art.position.toString() == posFirst)
    return !!parent;
  }

  public getAllFirstPositions(): IArticleData[]
  {
    return this.getSortedArticles().filter(item => !item.position.toString().includes("."))
  }

  public openArticleDialog(article: IArticleData)
  {
    const dialogRef = this._dialog.open(ArticleDialogComponent, {
      data: article
    })
    dialogRef.afterClosed().subscribe((result: IArticleData) => {
      if(!!result)
      {
        //we multiply by 1 to remove leading zeros and  1,0 zeros
        result.position = result.position * 1;
        if(this.isArticleFirstPosition(article) && this.getPosNumber(result.position) == article.position.toString())
        {
          alert("position cannot be update, because it wouldn´t have a parent")
          //der article ist die parent pos und soll auf eine unter pos geändert werden
          //das ist nicht möglich
          result.position = article.position;
        }
        else if(!this.hasPosNumberParent(result.position) && result.position.toString().includes("."))
        {
          alert("position cannot be update, because it wouldn´t have a parent")
          //wenn die position nummer z.b. 1.1 ist, es aber keine 1 gibt updaten wir die Nummer nicht
          result.position = article.position;
        }
        this._logisticStore.articleStore.update(result);
        this._cd.detectChanges();
      }
    })
  }

  public isReadOnly(): boolean
  {
    let role: string = this._auth.getUserRole();
    return role == eRole.user;
  }

  public openInfo(): void
  {
    this._snackbar.open("expiry date is expired!", undefined, {duration: 4000})
  } 

  public isDateExpired(pDate: string)
  {
    var expireDate = new Date(pDate); //dd-mm-YYYY
    var today = new Date();
    today.setHours(0,0,0,0);

    return expireDate <= today
  }

  public isStorekeeper(): boolean
  {
    return this._auth.getUserRole() == eRole.keeper || this._auth.getUserRole() == eRole.admin;
  }
}
