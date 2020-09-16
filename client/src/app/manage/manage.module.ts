import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ManageComponent } from './manage.component';
import { ManageRoutingModule } from '../manage/manage-routing.module';
import { SharedModule } from '../shared/shared.module';
import { ManageHolidayComponent } from './manage-holiday/manage-holiday.component';
import { PhotoEditorComponent } from './photo-editor/photo-editor.component';
import { FileUploadModule } from 'ng2-file-upload';

@NgModule({
  imports: [
    CommonModule,
    ManageRoutingModule,
    SharedModule,
    FileUploadModule
  ],
  declarations: [ManageComponent, ManageHolidayComponent, PhotoEditorComponent]
})
export class ManageModule { }
