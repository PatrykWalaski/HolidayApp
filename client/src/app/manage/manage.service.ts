import { Injectable } from '@angular/core';
import { HttpClientModule, HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { IHoliday } from '../shared/models/holiday';
import { ICountry } from '../shared/models/country';
import { ITravelAgency } from '../shared/models/travelAgency';
import { IMealPlan } from '../shared/models/mealPlan';
import { map } from 'rxjs/operators';
import { IPagination } from '../shared/models/pagination';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ManageService {
baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { }

getHolidays(pageNumber: number, pageSize: number, sort: string) {
  let params = new HttpParams();

  params = params.append('pageIndex', pageNumber.toString());
  params = params.append('pageSize', pageSize.toString());
  params = params.append('sort', sort);

  return this.http.get<IPagination>(this.baseUrl + 'holidays', { observe: 'response', params})
    .pipe( // inside pipe methods we can chain as many rxjs operators as we want
      map(response => { // we are converting the 'response' to a IPagination object from body of the response
        return response.body;
      })
    );
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
  return this.http.post<IHoliday[]>(this.baseUrl + 'holidays', offer);
}

updateHoliday(id: number, offer: any){
  return this.http.put(this.baseUrl + 'holidays/' + id, offer).pipe(
    map((user: IHoliday) => {
    }));
}

deleteHoliday(id: number){
  return this.http.delete(this.baseUrl + 'holidays/' + id);
}


deletePhoto(holidayId: number, photoId: number){
  return this.http.delete(this.baseUrl + 'holidays/' + holidayId + '/photos/' + photoId);
}

setMainPhoto(holidayId: number, photoId: number){
  return this.http.post(this.baseUrl + 'holidays/' + holidayId + '/photos/' + photoId + '/setMain', {});
}
}
