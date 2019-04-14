import { Injectable } from '@angular/core';
import { Observable, of, throwError } from 'rxjs';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';
import { BaseEntity } from '../entity/base-entity';

@Injectable({
  providedIn: 'root'
})
export class BaseSchemaGetService {

  protected apiUrl: string;

  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  public schemaName: string;

  constructor(private http: HttpClient) {
  }

  getAll(): Observable<any> {
    const url = `${this.apiUrl}/${this.schemaName}`;
    return this.http.get<any[]>(url)
      .pipe(
        catchError(this.handleError)
      );
  }

  get(id: string): Observable<any> {
    const url = `${this.apiUrl}/${this.schemaName}/${id}`;
    return this.http.get<any>(url)
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    // return an observable with a user-facing error message
    return throwError(
      'Something bad happened; please try again later.');
  };
}