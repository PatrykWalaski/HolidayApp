import { Component, OnInit, Input } from '@angular/core';
import { HolidaysComponent } from '../holidays.component';
import { IHoliday } from '../../shared/models/holiday';

@Component({
  selector: 'app-holidays-offer',
  templateUrl: './holidays-offer.component.html',
  styleUrls: ['./holidays-offer.component.scss']
})
export class HolidaysOfferComponent implements OnInit {
  @Input() holiday: IHoliday;

  constructor() { }

  ngOnInit() {
  }

  handleDisplayName(displayName: string)
  {
      switch (displayName) {
        case 'BB':
          return 'Breakfast';
          break;
        case 'HB':
          return 'Breakfast and Dinner';
          break;
        case 'FB':
          return 'Three Meals';
          break;
        default:
          return displayName;
          break;
      }
  }
}
