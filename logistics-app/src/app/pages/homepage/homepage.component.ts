import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { TransportBoxListComponent } from '../../features/transport-box-list/transport-box-list.component';
import { LoginComponent } from '../../features/login/login.component';

@Component({
  selector: 'app-homepage',
  standalone: true,
  imports: [CommonModule, TransportBoxListComponent, LoginComponent],
  templateUrl: './homepage.component.html',
  styleUrl: './homepage.component.scss'
})
export class HomepageComponent {

}
