import { Injectable } from '@angular/core';
import { BaseStoreService } from './base/base-store.service';
import { environment } from '../../../environments/environment';
import { RequestService } from '../request/request.service';

@Injectable({ providedIn: 'root' })
export class TransportboxStoreService extends BaseStoreService<any>
{
  private readonly _serviceURL = environment.serviceURL + environment.transportboxServicePath;

  constructor(private request: RequestService) {
    super();
  }

  public async loadIntitalData(projectId: string): Promise<void> 
  {
    if (this.getItems().length > 0) 
    {
      this.clear();
    }

    var items: any[] = await this.request.get(this._serviceURL + "/" + projectId);
    this.setItems(items);

    this.initialized = true;
  }

  public async delete(itemID: string): Promise<boolean> {
    try {
      var item: any | undefined = this.getById(itemID);

      if (!!item) {
        await this.request.delete(this._serviceURL + "/" + item.id);
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
      var item: any | undefined = this.getById(itemID);

      if (!!item) {
        await this.request.put(this._serviceURL + "/" + item.id, item);
        return true;
      }
    }
    catch (reason: any) {
      console.log(reason);
    }

    return false;
  }

  /** Create a new window in db and save it to store */
  public async create(item: any): Promise<any | undefined> {
    try {
      var data: any | undefined = await this.request.post(this._serviceURL, item);

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
}

