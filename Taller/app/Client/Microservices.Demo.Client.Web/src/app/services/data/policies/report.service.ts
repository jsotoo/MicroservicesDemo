import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IReport } from '../../../models/ireport';


@Injectable({
  providedIn: 'root'
})
export class ReportService {
  port = '44399';
  //baseUrl = `${this.window.location.protocol}//${this.window.location.hostname}:${this.port}`;
  baseUrl = `http://${window.location.hostname}:${this.port}`;
  reportsApiUrl = this.baseUrl + '/api/Report/policy';

  constructor(
    private http: HttpClient
  ) { }

  getPoliciesReport(): Observable<IReport[]> {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    return this.http.get<IReport[]>(this.reportsApiUrl, { headers });
  }
}
