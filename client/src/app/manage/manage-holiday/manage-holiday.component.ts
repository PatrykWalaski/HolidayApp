import { Component, OnInit, Input, Self } from '@angular/core';
import { IHoliday } from '../../shared/models/holiday';
import { FormGroup, FormBuilder, Validators, NgControl } from '@angular/forms';
import { ICountry } from 'src/app/shared/models/country';
import { IHolidayToCreate } from 'src/app/shared/models/holidayToCreate';
import { IMealPlan } from 'src/app/shared/models/mealPlan';
import { ITravelAgency } from 'src/app/shared/models/travelAgency';
import { ManageService } from '../manage.service';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';


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

  holiday: IHoliday;
  id: number;

  constructor(private fb: FormBuilder, private manageService: ManageService, private router: Router, 
              private activateRoute: ActivatedRoute, private toastr: ToastrService) {
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
      this.getHolidayFormValues();
    });

  }


  updateHoliday(): void{
    console.log('click');
    this.manageService.updateHoliday(this.id, this.convertToMatchingTypes()).subscribe( response => {
      console.log(response);
      this.toastr.success('Offer updated.');
      this.router.navigateByUrl('/manage');
    }, error => {
      this.toastr.error('Failed while updating an offer.');
      console.log(error);
    });
  }

  createHoliday(): void{
    this.manageService.createHoliday(new Array(this.convertToMatchingTypes())).subscribe(response => {
      console.log(this.convertToMatchingTypes());
      this.toastr.success('Offer created.');
      this.router.navigateByUrl('/manage');
    }, error => {
      this.toastr.error('Failed while creating an offer.');
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

  // [value]="!holiday.hotelName != null ? holiday.hotelName : ''"

  getHolidayFormValues(): void{
    this.id = +this.activateRoute.snapshot.paramMap.get('id');
    if (this.id > 0){
      this.manageService.getHoliday(this.id).subscribe(response => {
        this.holiday = response;
        console.log(this.holiday);
        this.patchValues();
      }, error => {
        console.log(error);
      });
    }
  }

  patchValues(): void{
    this.holidayForm.setValue({
        hotelName: this.holiday.hotelName,
        description: this.holiday.description,
        city: this.holiday.city,
        durationOfStay: this.holiday.durationOfStay,
        stars: this.holiday.stars,
        price: this.holiday.price,
        mealPlanId: this.mealPlans.find(x => x.name === this.holiday.mealPlan).id,
        travelAgencyId: this.travelAgencies.find(x => x.name === this.holiday.travelAgency).id,
        countryId:  this.countries.find(x => x.name === this.holiday.country).id,
    });
  }


}
