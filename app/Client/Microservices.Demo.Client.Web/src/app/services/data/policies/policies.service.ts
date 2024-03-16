import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { ICreatePolicyCommand } from './icreate-policy-command';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ICreatePolicyResult } from './icreate-policy-result';
import { IPolicies } from '../../../models/ipolicies';

@Injectable({
  providedIn: 'root'
})
export class PoliciesService {
  port = '44399';
  //baseUrl = `${this.window.location.protocol}//${this.window.location.hostname}:${this.port}`;
  baseUrl = `http://${this.window.location.hostname}:${this.port}`;
  policiesApiUrl = this.baseUrl + '/api/report';

  constructor(
    private http: HttpClient,
    @Inject('Window') private window: Window
  ) { }

  getPolicies(): Observable<IPolicies[]> {
    return this.http.post<IPolicies[]>(this.policiesApiUrl, "")
      .pipe(catchError(this.handleError));
  }

  CreatePolicy(createPolicyCommand: ICreatePolicyCommand): Observable<ICreatePolicyResult> {
    return this.http.post<ICreatePolicyResult>(this.policiesApiUrl, createPolicyCommand)
      .pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    console.error('server error', error);
    if (error.error instanceof Error) {
      const erroMessage = error.message;
      return throwError(() => erroMessage);
    }
    return throwError(() => error || 'server error');
  }
}
