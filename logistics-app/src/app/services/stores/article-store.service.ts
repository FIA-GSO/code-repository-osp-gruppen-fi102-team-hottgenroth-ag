import { Injectable } from '@angular/core';
import { BaseStoreService } from './base/base-store.service';
import { environment } from '../../../environments/environment';
import { RequestService } from '../request/request.service';
import { IArticleData } from '../../models/IArticleData';

@Injectable({ providedIn: 'root' })
export class ArticleStoreService extends BaseStoreService<IArticleData>
{
  private readonly _serviceURL = environment.serviceURL + environment.articleServicePath;

  constructor(private request: RequestService) {
    super();
  }

  public async loadIntitalData(boxId: string): Promise<void> 
  {
    try
    {
      var items: IArticleData[] = await this.request.get(this._serviceURL + "/all/" + boxId);

      if(this.getItems().length > 0)
      {
        items.forEach((art: IArticleData) => {
          if(!!!this.getById(art.articleGuid))
          {
            this.addItem(art);
          }
        });
      }
      else
      {
        this.setItems(items);
      }
  
      this.initialized = true;
    }
    catch(err: any)
    {
      console.log(err);
    }
  }

  public async delete(itemID: string): Promise<boolean> {
    try {
      var item: IArticleData | undefined = this.getById(itemID);

      if (!!item) {
        await this.request.delete(this._serviceURL + "/" + item.articleGuid);
        this.removeItem(item);

        return true;
      }
    }
    catch (reason: any) {
      console.log(reason);
    }

    return false;
  }

  /** Update an item in db */
  public async update(itemID: string): Promise<boolean> {
    try {
      var item: IArticleData | undefined = this.getById(itemID);

      if (!!item) {
        await this.request.put(this._serviceURL + "/" + item.articleGuid, item);
        return true;
      }
    }
    catch (reason: any) {
      console.log(reason);
    }

    return false;
  }

  /** Create a new window in db and save it to store */
  public async create(item: IArticleData): Promise<any | undefined> {
    try {
      var data: IArticleData | undefined = await this.request.post(this._serviceURL, item);

      if (!!data) {
        this.addItem(data);
      }

      return data;
    }
    catch (reason: any) {
      console.log(reason);
    }

    return undefined;
  }

  public override getById(id: string): IArticleData | undefined
  {
    return this.getItems().find(p => p.articleGuid === id);
  }

  public getArticlesForBox(boxId: string): IArticleData[]
  {
    return this.getItems().filter(p => p.boxGuid === boxId);
  }
}

