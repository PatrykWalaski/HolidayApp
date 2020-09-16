import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Photo } from 'src/app/shared/models/photo';
import { FileUploader } from 'ng2-file-upload';
import { environment } from 'src/environments/environment';
import { timeoutWith } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ManageService } from '../manage.service';
import { identifierModuleUrl } from '@angular/compiler';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {
  @Input() photos: Photo[];
  uploader: FileUploader;
  hasBaseDropZoneOver: boolean;
  response: string;
  baseUrl = environment.apiUrl;
  currentMainPhoto: Photo;

  constructor(private activateRoute: ActivatedRoute, private toastr: ToastrService, private manageService: ManageService) { }

  ngOnInit() {
    this.initializeUploader();
  }

  public fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  initializeUploader(){
    this.uploader = new FileUploader({
      url: this.baseUrl + 'holidays/' + (+this.activateRoute.snapshot.paramMap.get('id')) + '/photos',
      authToken: 'Bearer ' + localStorage.getItem('token'),
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024,
    });

    this.uploader.onAfterAddingFile = (file) => file.withCredentials = false;

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        const res: Photo = JSON.parse(response);
        const photo = {
          id: res.id,
          url: res.url,
          isMain: res.isMain
        };
        this.photos.push(photo);
      }
    };
  }

  deletePhoto(photoId: number){
      this.manageService.deletePhoto(+this.activateRoute.snapshot.paramMap.get('id'), photoId).subscribe(() => {
         this.photos.splice(this.photos.findIndex(p => p.id === photoId), 1);
         this.toastr.success('Photo has been deleted.');
      }, error => {
         this.toastr.error('Failed to delete the photo');
      });
  }

  setMainPhoto(photoId: number){
    this.manageService.setMainPhoto(+this.activateRoute.snapshot.paramMap.get('id'), photoId).subscribe(() => {
      this.photos.find(x => x.isMain === true).isMain = false;
      this.photos.find(x => x.id === photoId).isMain = true;
      this.toastr.success('Main photo updated.');
    }, error => {
     this.toastr.error('Failed to update main photo');
    });
  }
}
