import { Injectable, OnInit } from '@angular/core';
import { HttpClientModule, HttpClient, HttpParams } from '@angular/common/http';
import { IHoliday } from '../shared/models/holiday';
import { HolidayParams } from '../shared/models/holidayParams';
import { map } from 'rxjs/operators';
import { ICountry } from '../shared/models/country';
import { IPagination } from '../shared/models/pagination';
import { IMealPlan } from '../shared/models/mealPlan';
import { ITravelAgency } from '../shared/models/travelAgency';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HolidaysService{
  baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { }

 
  getHolidays(holidayParams: HolidayParams){
    let params = new HttpParams();

    if (holidayParams.countries)
    {
        params = params.append('countries', holidayParams.countries.toString());
    }

    if (holidayParams.meals)
    {
        params = params.append('meals', holidayParams.meals.toString());
    }

    if (holidayParams.agencies)
    {
        params = params.append('agencies', holidayParams.agencies.toString());
    }

    if (holidayParams.sortSelected)
    {
      params = params.append('sort', holidayParams.sortSelected.toString());
    }

    if (holidayParams.minPrice)
    {
      params = params.append('minPrice', holidayParams.minPrice.toString());
    }

    if (holidayParams.maxPrice)
    {
      params = params.append('maxPrice', holidayParams.maxPrice.toString());
    }

    if (holidayParams.minDuration)
    {
      params = params.append('minDuration', holidayParams.minDuration.toString());
    }

    if (holidayParams.maxDuration)
    {
      params = params.append('maxDuration', holidayParams.maxDuration.toString());
    }

    if (holidayParams.minStars)
    {
      params = params.append('minStars', holidayParams.minStars.toString());
    }

    params = params.append('pageIndex', holidayParams.pageNumber.toString());
    params = params.append('pageSize', holidayParams.pageSize.toString());

    return this.http.get<IPagination>(this.baseUrl + 'holidays', { observe: 'response', params})
    .pipe( // inside pipe methods we can chain as many rxjs operators as we want
      map(response => { // we are converting the 'response' to a IPagination object from body of the response
        return response.body;
      })
    );
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

  getHolidayById(id: number)
  {
    return this.http.get<IHoliday>(this.baseUrl + 'holidays/' + id);
  }

}
