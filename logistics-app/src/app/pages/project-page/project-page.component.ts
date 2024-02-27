import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { ProjectRouletteComponent } from '../../features/project-roulette/project-roulette.component';
import { MatCardModule } from '@angular/material/card';
import {MatButtonToggleModule} from '@angular/material/button-toggle';
import { MatIconModule } from '@angular/material/icon';
import { ProjectStoreService } from '../../services/stores/project-store.service';
import { IProjectData } from '../../models/IProjectData';

@Component({
  selector: 'app-project-page',
  standalone: true,
  imports: [CommonModule, ProjectRouletteComponent, MatCardModule, MatButtonToggleModule, MatIconModule],
  templateUrl: './project-page.component.html',
  styleUrl: './project-page.component.scss'
})
export class ProjectPageComponent {
  public sortedBy: 'date' | 'alphabet' = 'date';

  private _prjStore: ProjectStoreService = inject(ProjectStoreService);

  public get projects(): IProjectData[]
  {
    return this._prjStore.getItems();
  }

  constructor(){
  }
  
  public toggleSorted(pSorting: 'date' | 'alphabet')
  {
    this.sortedBy = pSorting;
  }
}
