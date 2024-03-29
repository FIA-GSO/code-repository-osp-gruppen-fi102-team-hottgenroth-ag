import { Injectable } from '@angular/core';
import { ProjectStoreService } from './project-store.service';
import { TransportboxStoreService } from './transportbox-store.service';
import { ArticleStoreService } from './article-store.service';
import { IProjectData } from '../../models/IProjectData';
import { ITransportBoxData } from '../../models/ITransportBoxData';

@Injectable({ providedIn: 'root' })
export class LogisticsStoreService
{
  private _initialized: boolean = false;
  public get initialized(): boolean
  {
    return this._initialized;
  }

  private _projectStore!: ProjectStoreService;
  private _transportboxStore!: TransportboxStoreService;
  private _articleStore!: ArticleStoreService;


  public get projectStore(): ProjectStoreService
  {
    return this._projectStore;
  }

  public get transportboxStore(): TransportboxStoreService
  {
    return this._transportboxStore;
  }

  public get articleStore(): ArticleStoreService
  {
    return this._articleStore;
  }

  constructor(
    private projectStoreService: ProjectStoreService,
    private transportboxStoreService: TransportboxStoreService,
    private articleStoreService: ArticleStoreService)
  {
    this._projectStore = this.projectStoreService;
    this._transportboxStore = this.transportboxStoreService;
    this._articleStore = this.articleStoreService;
  }

  public async loadProject(projectId: string): Promise<IProjectData | undefined>
  {
    if(!this._initialized)
    {
      return undefined;
    }

    this._articleStore.clear();
    this._transportboxStore.clear();
    
    var prj: IProjectData | undefined = await this._projectStore.loadProject(projectId);
    if(!!prj)
    {
      await this._transportboxStore.loadIntitalData(prj.projectGuid);
      return prj;
    }

    return undefined;
  }

  public async loadArticleForBox(boxGuid: string): Promise<void>
  {
    await this._articleStore.loadIntitalData(boxGuid);
  }

  public async loadIntitalData(): Promise<void>
  {
    await this._projectStore.loadIntitalData();

    this._initialized = true;
  }

  public clear(): void
  {
    this._projectStore.clear();
    this._transportboxStore.clear();
    this._articleStore.clear();
    
    this._initialized = false;
  }
}
