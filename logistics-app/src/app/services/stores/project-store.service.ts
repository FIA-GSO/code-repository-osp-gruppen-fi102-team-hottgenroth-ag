import { Injectable } from '@angular/core';
import { BaseStoreService } from './base/base-store.service';
import { environment } from '../../../environments/environment';
import { IProjectData } from '../../models/IProjectData';
import { RequestService } from '../request/request.service';

@Injectable({ providedIn: 'root' })
export class ProjectStoreService extends BaseStoreService<IProjectData>
{
  private readonly _serviceURL = environment.serviceURL + environment.projectServicePath;

  private loadedProjectIdentifier: string | undefined;

  constructor(private request: RequestService) {
    super();
  }

  public async loadIntitalData(): Promise<void> 
  {

    if (this.getItems().length > 0) 
    {
      this.clear();
    }

    var items: IProjectData[] = await this.request.get(this._serviceURL);
    this.setItems(items);

    this.initialized = true;
  }

  public async delete(itemID: string): Promise<boolean> {
    try {
      var item: IProjectData | undefined = this.getById(itemID);

      if (!!item) {
        await this.request.delete(this._serviceURL + "/" + item.projectGuid);
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
      var item: IProjectData | undefined = this.getById(itemID);

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
  public async create(item: IProjectData): Promise<IProjectData | undefined> {
    try {
      var data: IProjectData | undefined = await this.request.post(this._serviceURL, item);

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

  /** Load a project */
  public async loadProject(projectId: string): Promise<IProjectData | undefined> 
  {
    try 
    {
      if (!!!this.getById(projectId)) 
      {
        // Project does not exist in store so it cannot be loaded
        return undefined;
      }

      var project: IProjectData = await this.request.get(this._serviceURL + "/" + projectId);
      this.loadedProjectIdentifier = project.projectGuid;
      return this.getById(project.projectGuid);
    }
    catch (reason: any) {
      console.log(reason);
    }

    return undefined
  }

  public getLoadedProject(): IProjectData | undefined 
  {
    if (!!this.loadedProjectIdentifier) 
    {
      return this.getItems().find(item => item.projectGuid === this.loadedProjectIdentifier);
    }
    return undefined;
  }

  public override getById(id: string): IProjectData | undefined
  {
    return this.getItems().find(p => p.projectGuid === id);
  }
}

