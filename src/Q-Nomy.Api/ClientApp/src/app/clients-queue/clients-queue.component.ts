import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ClientService } from '../client.service';
import { Client } from '../client';

@Component({
  selector: 'app-clients-queue',
  templateUrl: './clients-queue.component.html',
  styleUrls: ['./clients-queue.component.css']
})
export class ClientsQueueComponent {
  
  public clientsInLine: Client[] = [];
  public clientsInService: Client[] = [];

  constructor(private clientService: ClientService) {
    clientService.fetchData().subscribe(result => {
        this.clientsInLine = result.clientsInLine;
        this.clientsInService = result.clientsInProcess;
    });
  }

  public serveNext() {
    this.clientService.serveNext().subscribe(result => {
      this.clientsInLine = result.clientsInLine;
      this.clientsInService = result.clientsInProcess;
    });
  }

  public onAdd(name:string) {
    this.clientService.addClient(name).subscribe(result => {
      this.clientsInLine = result.clientsInLine;
      this.clientsInService = result.clientsInProcess;
    });
  }
}


