import { HttpClient, HttpEventType, HttpRequest } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { LoggingService } from './logging/logging.service';
import { StuffService } from './stuff/stuff.service';

@Component({
  selector: 'valant-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less'],
})
export class AppComponent implements OnInit {
  public title = 'Valant demo';
  public data: string[];
  public progress: number;
  public message: string;
  public mazeContent: string[][];

  constructor(private logger: LoggingService, private stuffService: StuffService, private http: HttpClient) {}

  
  upload(files) {
    if (files.length ===0 )
    return;

    const formData = new FormData();

    for (let file of files){
      formData.append(file.name, file); 
    }

    const uploadRequest = new HttpRequest('POST', 'http://localhost:47181/api/Upload', formData, {
      reportProgress: true,
    });

    this.http.request(uploadRequest)
    .subscribe(
      event => {
        if (event.type === HttpEventType.UploadProgress){
          this.progress = Math.round(100 * event.loaded / event.total);
        } else if (event.type == HttpEventType.Response){
          this.message = event.body.toString();
        }
        this.getAllMazes();
    
      });
  }
  ngOnInit() {
    this.logger.log('Welcome to the AppComponent');
    //this.getStuff();

    const mazeRequest = new HttpRequest('GET','http://localhost:47181/api/Maze/GetAllMazes',
    {
      reportProgress: true,
    });

    this.getAllMazes();
    
    
  }

  getAllMazes() {
      this.stuffService.getAllMazes()
      .subscribe(
        response => {
          response = response
          this.mazeContent = response;
          
        }
      )
    }



  
}
