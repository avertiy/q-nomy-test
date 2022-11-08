import { Component, Input } from '@angular/core';
import { Client } from '../client';
//import { Breakpoints } from '@angular/cdk/layout';

@Component({
  selector: 'app-clients-list',
  templateUrl: './clients-list.component.html',
  styleUrls: ['./clients-list.component.css']
})
export class ClientsListComponent {
  @Input() clients: Client[] | undefined;
 
}
