import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'user-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.scss'
})
export class UserListComponent {

}
