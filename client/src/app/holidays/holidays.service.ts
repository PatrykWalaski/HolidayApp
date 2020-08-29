import { Injectable } from '@angular/core';
import { HttpClientModule, HttpClient, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class HolidaysService {
  baseUrl = 'https://localhost:5001/api/';

constructor(private http: HttpClient) { }

  getHolidays(){
    return this.http.get<any>(this.baseUrl + 'offers');
  }

}
