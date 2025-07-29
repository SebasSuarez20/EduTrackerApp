import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Params } from '@angular/router';
import { catchError, Observable, throwError } from 'rxjs';
import { IResponse } from '../interface/IResponse';
import { environment } from '../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class HttpRequestService {

  private URL_API:string = environment.apiUrl;

  constructor(private httpClient: HttpClient) { }

  public getHttp<T>(url: string, params?: Params): Observable<IResponse<T>> {

    return this.httpClient.get<IResponse<T>>(`${this.URL_API}${url}`, { params: params }).pipe(

      catchError((err) => {
        return throwError(() => new Error(`Error: ${err.message}`));
      })
    )
  }

   public postHttp<T>(url: string, body: T): Observable<IResponse<T>> {

    return this.httpClient.post<IResponse<T>>(`${this.URL_API}${url}`,body).pipe(

      catchError((err) => {
        return throwError(() => new Error(`Error: ${err.message}`));
      })
    )
  }

  public putHttp<T>(url: string, body: T): Observable<IResponse<T>> {

    return this.httpClient.put<IResponse<T>>(`${this.URL_API}${url}`, body).pipe(

      catchError((err) => {
        return throwError(() => new Error(`Error: ${err.message}`));
      })
    )
  }

  public deleteHttp<T>(url: string): Observable<IResponse<T>> {

    return this.httpClient.delete<IResponse<T>>(`${this.URL_API}${url}`).pipe(

      catchError((err) => {
        return throwError(() => new Error(`Error: ${err.message}`));
      })
    )
  }
}
