import { HttpClient } from '@angular/common/http';
import { Client } from './client';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})

export class ClientService {
 
  baseUrl = 'https://localhost:44350/api/';
  constructor(
    private http: HttpClient
  ) {}

  addClient(name: string) {
    var url = this.baseUrl + 'clients';
    console.warn('add client ',url, name);
    this.http.post(url, name);
  }
 }
