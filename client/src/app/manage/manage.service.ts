import { Injectable } from '@angular/core';
import { HttpClientModule, HttpClient, HttpParams } from '@angular/common/http';
import { IHoliday } from '../shared/models/holiday';
import { ICountry } from '../shared/models/country';
import { ITravelAgency } from '../shared/models/travelAgency';
import { IMealPlan } from '../shared/models/mealPlan';
import { map } from 'rxjs/operators';

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

getCountries(){
  return this.http.get<ICountry[]>(this.baseUrl + 'holidays/countries');
}

getTravelAgencies(){
  return this.http.get<ITravelAgency[]>(this.baseUrl + 'holidays/agencies');
}

getMealPlans(){
  return this.http.get<IMealPlan[]>(this.baseUrl + 'holidays/mealPlans');
}

createHoliday(offer: any){
  return this.http.post(this.baseUrl + 'holidays', offer).pipe(
    map((user: IHoliday) => {
      console.log("test");
    }));
}
}
