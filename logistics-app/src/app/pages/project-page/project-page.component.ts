import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { ProjectRouletteComponent } from '../../features/project-roulette/project-roulette.component';
import { MatCardModule } from '@angular/material/card';
import {MatButtonToggleModule} from '@angular/material/button-toggle';
import { MatIconModule } from '@angular/material/icon';
import { ProjectStoreService } from '../../services/stores/project-store.service';
import { IProjectData } from '../../models/IProjectData';
import { LogisticsStoreService } from '../../services/stores/logistics-store.service';

@Component({
  selector: 'app-project-page',
  standalone: true,
  imports: [CommonModule, ProjectRouletteComponent, MatCardModule, MatButtonToggleModule, MatIconModule],
  templateUrl: './project-page.component.html',
  styleUrl: './project-page.component.scss'
})
export class ProjectPageComponent {
  public sortedBy: 'date' | 'alphabet' = 'date';

  private _logisticsStore: LogisticsStoreService = inject(LogisticsStoreService);

  public get projects(): IProjectData[]
  {
    //Wir holen alle Projekte und geben Sie in das Projektroulette
    return this._logisticsStore.projectStore.getItems();
  }

  constructor(){
  }

  //Die Sortierung der Projekte wird verändert
  public toggleSorted(pSorting: 'date' | 'alphabet')
  {
    this.sortedBy = pSorting;
  }
}
