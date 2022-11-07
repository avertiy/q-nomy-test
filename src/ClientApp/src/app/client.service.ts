import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Client } from './client';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})

export class ClientService {
  private baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string)
  {
    // need to use next time VS net 6.0 + Angular boilerplate!!
    console.log(baseUrl); //https://localhost:44484/
    this.baseUrl = 'https://localhost:44350/api/';
  }

  public fetchData(): Observable<GetClientsResponse> {
    const url = this.baseUrl + 'clients';
    return this.http.get<GetClientsResponse>(url).pipe(
      tap(x => console.log(`fetchData() fetched clients`, x)),
      catchError(this.handleError<GetClientsResponse>(`fetchData`))
    );
  }

  public serveNext(): Observable<any> {
    const url = this.baseUrl + 'clients/next';
    return this.http.put(url, {}).pipe(
      tap(x => console.log(`serveNext() =>`, x)),
      catchError(this.handleError('serveNext()'))
    );
  }


  public addClient(name: string) : Observable<any> {
    const url = this.baseUrl + 'clients';
    return this.http.post(url, { name: name }).
      pipe(
        tap(x => console.log(`addClient({name:${name}})=>`, x)),
        catchError(this.handleError('addClient'))
    );
  };

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
 

export interface GetClientsResponse {
  clientsInLine: Client[];
  clientsInProcess: Client[];
}
