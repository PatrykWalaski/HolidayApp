import { Component, OnInit, Input } from '@angular/core';
import { IHoliday } from '../../shared/models/holiday';
import { HolidaysService } from '../holidays.service';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryImage, NgxGalleryOptions, NgxGalleryAnimation } from 'ngx-gallery-9';

@Component({
  selector: 'app-holidays-detail',
  templateUrl: './holidays-detail.component.html',
  styleUrls: ['./holidays-detail.component.css']
})
export class HolidaysDetailComponent implements OnInit {
  holiday: IHoliday;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];

  constructor(private holidaysService: HolidaysService, private activateRoute: ActivatedRoute) { }

  ngOnInit() {
    this.loadHoliday();

    this.galleryOptions = [
      {
        width: '750px',
        height: '600px',
        imagePercent: 100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: true,
      }
    ];
  }

  getImages() {
    const imageUrls = [];
    for (const photo of this.holiday.photos) {
        imageUrls.push({
          small: photo.url,
          medium: photo.url,
          big: photo.url,
        });
    }

    return imageUrls;
  }

  loadHoliday() {
    this.holidaysService.getHolidayById(+this.activateRoute.snapshot.paramMap.get('id')).subscribe(response => {
      this.holiday = response;
      this.galleryImages = this.getImages();
    }, error => {
      console.log(error);
    });
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
