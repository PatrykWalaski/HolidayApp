import { Component, OnInit, Input, Self } from '@angular/core';
import { IHoliday } from '../../shared/models/holiday';
import { FormGroup, FormBuilder, Validators, NgControl } from '@angular/forms';
import { ICountry } from 'src/app/shared/models/country';
import { IHolidayToCreate } from 'src/app/shared/models/holidayToCreate';
import { IMealPlan } from 'src/app/shared/models/mealPlan';
import { ITravelAgency } from 'src/app/shared/models/travelAgency';
import { ManageService } from '../manage.service';
import { Router, ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-manage-holiday',
  templateUrl: './manage-holiday.component.html',
  styleUrls: ['./manage-holiday.component.css']
})
export class ManageHolidayComponent implements OnInit {
  holidayForm: FormGroup;

  countries: ICountry[];
  mealPlans: IMealPlan[];
  travelAgencies: ITravelAgency[];

  constructor(private fb: FormBuilder, private manageService: ManageService, private router: Router, 
              private activateRoute: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.holidayForm = this.fb.group({
      hotelName: [null, Validators.required,],
      stars: [null, Validators.required],
      city: [null, Validators.required],
      description: [null, Validators.required],
      durationOfStay: [null, [Validators.required, Validators.max(30)]],
      price: [null, Validators.required],
      countryId: [null, Validators.required],
      mealPlanId: [null, Validators.required],
      travelAgencyId: [null, Validators.required],
  });

    this.manageService.getCountries().subscribe(response => {
      this.countries = response;
    });

    this.manageService.getMealPlans().subscribe(response => {
      this.mealPlans = response;
    });

    this.manageService.getTravelAgencies().subscribe(response => {
      this.travelAgencies = response;
    });
  }

  submitForm(): void{
    console.log(this.holidayForm.value);
    this.manageService.createHoliday(new Array(this.convertToMatchingTypes())).subscribe(response => {
      console.log('TEST');
      this.router.navigateByUrl('/manage');
    }, error => {
      console.log(error);
    });
  }

  convertToMatchingTypes(): any{
      return {
        hotelName: this.holidayForm.get('hotelName').value,
        description: this.holidayForm.get('description').value,
        city: this.holidayForm.get('city').value,
        durationOfStay: +this.holidayForm.get('durationOfStay').value,
        stars: +this.holidayForm.get('stars').value,
        price: +this.holidayForm.get('price').value,
        mealPlanId: +this.holidayForm.get('mealPlanId').value,
        travelAgencyId: +this.holidayForm.get('travelAgencyId').value,
        countryId:  +this.holidayForm.get('countryId').value
      };
  }
}
