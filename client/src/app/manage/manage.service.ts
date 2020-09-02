import { Injectable } from '@angular/core';
import { HttpClientModule, HttpClient, HttpParams } from '@angular/common/http';
import { IHoliday } from '../shared/models/holiday';

@Injectable({
  providedIn: 'root'
})
export class ManageService {
baseUrl = 'https://localhost:5001/api/';

constructor(private http: HttpClient) { }

getHolidays() {
  return this.http.get<IHoliday[]>(this.baseUrl + 'holidays');
}

getHoliday(id: number) {
  return this.http.get<IHoliday>(this.baseUrl + 'holidays/' + id);
}

}
