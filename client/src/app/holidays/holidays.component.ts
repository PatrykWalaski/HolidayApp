import { Component, OnInit } from '@angular/core';
import { HolidaysService } from './holidays.service';
import { IHoliday } from '../shared/models/holiday';
import { ITravelAgency } from '../shared/models/travelAgency';
import { IMealPlan } from '../shared/models/mealPlan';
import { ICountry } from '../shared/models/country';

import { HolidayParams } from '../shared/models/holidayParams';
import { TimeoutError } from 'rxjs';

@Component({
  selector: 'app-holidays',
  templateUrl: './holidays.component.html',
  styleUrls: ['./holidays.component.scss'],
})
export class HolidaysComponent implements OnInit {
  holidays: IHoliday[];
  countries: ICountry[];
  mealPlans: IMealPlan[];
  travelAgencies: ITravelAgency[];
  currentlySelectedMeals = [];
  currentlySelectedAgencies = [];
  currentlySelectedCountries = [];


  totalCount: number;
  holidayParams = new HolidayParams();
  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Low To High', value: 'priceAsc' },
    { name: 'Price: High To Low', value: 'priceDesc' },
    { name: 'Duration: Short To Long', value: 'durationAsc' },
    { name: 'Duration: Long To Short', value: 'durationDesc' },
  ];
  constructor(private holidaysService: HolidaysService) {}

  ngOnInit(): void {
    this.getHolidays();
    this.getMealPlans();
    this.getTravelAgencies();
    this.getCountries();
  }

  getHolidays(): void {
    this.holidaysService.getHolidays(this.holidayParams).subscribe(
      (response) => {
        this.holidays = response.data;
        this.totalCount = response.count;
        this.holidayParams.pageNumber = response.pageIndex;
        this.holidayParams.pageSize = response.pageSize;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  onPageChanged(event: any){
    this.holidayParams.pageNumber = event;
    this.getHolidays();
  }

  onSearch(minPrice, maxPrice, minDuration, maxDuration)
  {
      this.holidayParams.minPrice = minPrice.value;
      this.holidayParams.maxPrice = maxPrice.value;
      this.holidayParams.minDuration = minDuration.value;
      this.holidayParams.maxDuration = maxDuration.value;
      this.holidayParams.pageNumber = 1;
      this.getHolidays();
  }

  onMealPlanSelected(mealPlan: string): void {
    let deleted;
    this.currentlySelectedMeals.forEach((element, index) => {
      if (element === mealPlan) {
        this.currentlySelectedMeals.splice(index, 1);
        deleted = true;
        this.holidayParams.meals = this.getFiltersAsString('meals');
        //this.getHolidays();
      }
    });

    if (deleted === true) {
      return;
    }

    this.currentlySelectedMeals.push(mealPlan);
    this.holidayParams.meals = this.getFiltersAsString('meals');
    this.holidayParams.pageNumber = 1;
    //this.getHolidays();
  }

  onCountrySelected(country: string): void {
    let deleted;
    this.currentlySelectedCountries.forEach((element, index) => {
      if (element === country) {
        this.currentlySelectedCountries.splice(index, 1);
        deleted = true;
        this.holidayParams.countries = this.getFiltersAsString('countries');
        //this.getHolidays();
      }
    });

    if (deleted === true) {
      return;
    }

    this.currentlySelectedCountries.push(country);
    this.holidayParams.countries = this.getFiltersAsString('countries');
    this.holidayParams.pageNumber = 1;
    //this.getHolidays();
  }

  onTravelAgencySelected(travelAgency: string): void {
    let deleted;
    this.currentlySelectedAgencies.forEach((element, index) => {
      if (element === travelAgency) {
        this.currentlySelectedAgencies.splice(index, 1);
        deleted = true;
        this.holidayParams.agencies = this.getFiltersAsString('agencies');
        //this.getHolidays();
      }
    });

    if (deleted === true) {
      return;
    }

    this.currentlySelectedAgencies.push(travelAgency);
    this.holidayParams.agencies = this.getFiltersAsString('agencies');
    this.holidayParams.pageNumber = 1;
    //this.getHolidays();
  }

  onSortSelected(sort: string): void {
    console.log(sort);
    this.holidayParams.sortSelected = sort;
    this.getHolidays();
  }

  onStarsSelected(stars: number): void {
    this.holidayParams.minStars = stars;
  }

  getFiltersAsString(filterType: string): string {
    let filtersAsString = '';

    if (filterType === 'meals')
    {
      this.currentlySelectedMeals.forEach((element) => {
        filtersAsString += element + ',';
      });
    } else if (filterType === 'agencies')
    {
      this.currentlySelectedAgencies.forEach((element) => {
        filtersAsString += element + ',';
      });
    } else if (filterType === 'countries')
    {
      this.currentlySelectedCountries.forEach((element) => {
        filtersAsString += element + ',';
      });
    }

    return filtersAsString;
  }

  isMealSelected(filterName: string): boolean{
    return this.currentlySelectedMeals.includes(filterName);
  }

  isAgencySelected(filterName: string): boolean{
    return this.currentlySelectedAgencies.includes(filterName);
  }

  isCountrySelected(filterName: string): boolean{
    return this.currentlySelectedCountries.includes(filterName);
  }

  getStarArraySize(star: number){
    return Array(star);
  }

  getArraySizeForEmptyStars(stars: number){
    return new Array(5 - stars);
  }


  // ----------------- GET ARRAYS FROM DATABASE -----------------

  getMealPlans(): void {
    this.holidaysService.getMealPlans().subscribe(
      (response) => {
        this.mealPlans = response;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  getCountries(): void {
    this.holidaysService.getCountries().subscribe(
      (response) => {
        this.countries = response;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  getTravelAgencies(): void {
    this.holidaysService.getTravelAgencies().subscribe(
      (response) => {
        this.travelAgencies = response;
      },
      (error) => {
        console.log(error);
      }
    );
  }
}
