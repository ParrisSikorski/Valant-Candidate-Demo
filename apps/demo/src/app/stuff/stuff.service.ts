import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ValantDemoApiClient } from '../api-client/api-client';

@Injectable({
  providedIn: 'root',
})
export class StuffService {
  constructor(private http: HttpClient) {}

  getAllMazes(): Observable<string[][]> {
    return this.http.get<string[][]>('http://localhost:47181/api/Maze/GetAllMazes');
  }
}
