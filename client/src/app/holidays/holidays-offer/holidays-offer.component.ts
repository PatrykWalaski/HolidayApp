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
  mainPhotoUrl: string;
  constructor() { }

  ngOnInit() {
    this.setMainPhotoUrl();
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

  getArraySizeForStars(stars: number){
    return new Array(stars);
  }

  getArraySizeForEmptyStars(stars: number){
    return new Array(5 - stars);
  }

  setMainPhotoUrl(){
    if (this.holiday.photos.length > 0)
    {
      const currentMainPhoto = this.holiday.photos.find(p => p.isMain === true);
      if (currentMainPhoto)
      {
        this.mainPhotoUrl = currentMainPhoto.url;
      }
    }
  }
}
