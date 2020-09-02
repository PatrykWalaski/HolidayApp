import { Component, OnInit, Input } from '@angular/core';
import { IHoliday } from '../../shared/models/holiday';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';


@Component({
  selector: 'app-manage-holiday',
  templateUrl: './manage-holiday.component.html',
  styleUrls: ['./manage-holiday.component.css']
})
export class ManageHolidayComponent implements OnInit {
  //@Input() holidayToUpdate: IHoliday;
  //checkoutForm: FormGroup;
 
  constructor() { }
   // private fb: FormBuilder <- this in constructor breaks routing to this page
  // createCheckoutForm(): void {
  //   this.checkoutForm = this.fb.group({
  //       hotelName: [null, Validators.required],
  //       stars: [null, Validators.required],
  //       price: [null, Validators.required],
  //       country: [null, Validators.required],
  //       city: [null, Validators.required],
  //       mealPlan: [null, Validators.required],
  //       durationOfStay: [null, Validators.required],
  //       description: [null, Validators.required],
  //   });
  // }

  ngOnInit() {
  }

}
