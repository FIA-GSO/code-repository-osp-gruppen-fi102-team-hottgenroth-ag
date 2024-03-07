import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, Input, inject } from '@angular/core';
import { IArticleData } from '../../models/IArticleData';
import { LogisticsStoreService } from '../../services/stores/logistics-store.service';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatSelectModule } from '@angular/material/select';
import { MatBadgeModule } from '@angular/material/badge';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { TruncatePipe } from '../../framework/TruncatePipe';
import { MatDialog } from '@angular/material/dialog';
import { ArticleDialogComponent } from '../article-dialog/article-dialog.component';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
import { AuthService } from '../../services/authentication/auth.service';
import { eRole } from '../../models/enum/eRole';

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

  constructor(){
    
  }

  public getSortedArticles()
  {
    return this.articles.sort((a, b) => a.position.toString().localeCompare(b.position.toString(), undefined, {numeric: true}))
  }

  public isArticleLastPosition(article: IArticleData): boolean
  {
    var samePositionList = this.getAllArticlesFromSamePositions(article);
    if(samePositionList[samePositionList.length - 1].articleGuid == article.articleGuid)
    {
      return true;
    }
    return false;
  }

  public isArticleFirstPosition(article: IArticleData): boolean
  {
    var samePositionList = this.getAllArticlesFromSamePositions(article);
    if(samePositionList.length > 0 && samePositionList[0].articleGuid == article.articleGuid)
    {
      return true;
    }
    return false;
  }

  public getAllArticlesFromSamePositions(article: IArticleData): IArticleData[]
  {
    return this.getSortedArticles().filter(item => item.position.toString()[0] === article.position.toString()[0])
  }

  public openArticleDialog(article: IArticleData)
  {
    const dialogRef = this._dialog.open(ArticleDialogComponent, {
      data: article
    })
    dialogRef.afterClosed().subscribe((result: IArticleData) => {
      console.log(result)
      if(!!result)
      {
        // this._logisticStore.articleStore.update(result.articleGuid)
      }
    })
  }

  public isReadOnly(): boolean
  {
    let role: string = this._auth.getUserRole();
    return role == eRole.user;
  }
}
