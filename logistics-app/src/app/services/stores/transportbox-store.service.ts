import { Injectable } from '@angular/core';
import { BaseStoreService } from './base/base-store.service';
import { environment } from '../../../environments/environment';
import { RequestService } from '../request/request.service';
import { ITransportBoxData } from '../../models/ITransportBoxData';

@Injectable({ providedIn: 'root' })
export class TransportboxStoreService extends BaseStoreService<ITransportBoxData>
{
  private readonly _serviceURL = environment.serviceURL + environment.transportboxServicePath;

  constructor(private request: RequestService) {
    super();
  }

  public async loadIntitalData(projectId: string): Promise<void> 
  {
    try
    {
      if(this.getItems().length > 0) 
      {
        this.clear();
      }

      var items: ITransportBoxData[] = await this.request.get(this._serviceURL + "/all/" + projectId);
      this.setItems(items);
  
      this.initialized = true;
    }
    catch(ex: any){
      console.log(ex);
    }
  }

  public async delete(itemID: string): Promise<boolean> {
    try {
      var item: ITransportBoxData | undefined = this.getById(itemID);

      if (!!item) {
        await this.request.delete(this._serviceURL + "/" + item.boxGuid);
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
      var item: ITransportBoxData | undefined = this.getById(itemID);

      if (!!item) {
        await this.request.put(this._serviceURL, item);
        return true;
      }
    }
    catch (reason: any) {
      console.log(reason);
    }

    return false;
  }

  /** Create a new window in db and save it to store */
  public async create(item: ITransportBoxData): Promise<ITransportBoxData | undefined> {
    try {
      var data: ITransportBoxData | undefined = await this.request.post(this._serviceURL, item);

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

  public override getById(id: string): ITransportBoxData | undefined
  {
    return this.getItems().find(p => p.boxGuid === id);
  }
}

