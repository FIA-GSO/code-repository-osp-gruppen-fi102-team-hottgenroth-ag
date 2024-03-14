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

  public openInfo()
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
}
