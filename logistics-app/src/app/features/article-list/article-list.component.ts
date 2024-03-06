import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, Input, inject } from '@angular/core';
import { IArticleData } from '../../models/IArticleData';
import { LogisticsStoreService } from '../../services/stores/logistics-store.service';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';

@Component({
  selector: 'article-list',
  standalone: true,
  imports: [CommonModule, MatDividerModule],
  templateUrl: './article-list.component.html',
  styleUrl: './article-list.component.scss'
})
export class ArticleListComponent implements AfterViewInit{
  @Input() articles!: IArticleData[];

  private _logisticStore: LogisticsStoreService = inject(LogisticsStoreService);

  constructor(){
    
  }

  ngAfterViewInit(): void {
    console.log(this._logisticStore.articleStore.getItems())
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

  private getAllArticlesFromSamePositions(article: IArticleData): IArticleData[]
  {
    return this.getSortedArticles().filter(item => item.position.toString()[0] === article.position.toString()[0])
  }
}
