import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Client } from '../client';

@Component({
  selector: 'app-clients-queue',
  templateUrl: './clients-queue.component.html'
})
export class ClientsQueueComponent {
  
  public clientsInLine: Client[] = [];
  public clientsInService: Client[] = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    // need to use next time VS net 6.0 + Angular boilerplate!!
    console.log(baseUrl); //https://localhost:44484/
    baseUrl = 'https://localhost:44350/api/';


    http.get<GetClientsResponse>(baseUrl + 'clients/').subscribe(result => {
      console.log('clients response', result);
      this.clientsInLine = result.clientsInLine;
      this.clientsInService = result.clientsInProcess;
    }, error => console.error(error));
  }

  public serveNext() {
    console.log('serve next');
  }

}

interface GetClientsResponse {
  clientsInLine: Client[];
  clientsInProcess: Client[];
}

