import { Component, Input } from '@angular/core';
import { Client } from '../client';

@Component({
  selector: 'app-clients-list',
  templateUrl: './clients-list.component.html'
})
export class ClientsListComponent {
  @Input() clients: Client[] | undefined;
 
}
