import { Component } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'JWTNg';
  apiResponse = 'Hello World!';
  Url = 'https://localhost:44372';
  token = '';

  constructor(private http: HttpClient) {
  }

  reset() {
    this.apiResponse = 'Hello World!';
    this.token = '';
  }

  generate() {
    this.token = '';

    const httpHeaders = new HttpHeaders({
      'Content-Type': 'application/json'
    });

    let model = { model: this.title }; 

    this.http.post(this.Url + '/weatherforecast/authenticate/', {}, {}
    ).subscribe(res => {
      console.log(res);
      this.apiResponse = <string>res;
      this.token = <string>res;

    }, error => {
      console.log('error');
      console.log(error);
      console.log(typeof error.status);
      console.log(error.status);

      this.apiResponse = <string>error;
    });

  }

  verify() {
    const httpHeaders = new HttpHeaders({
      'Authorization': 'Bearer ' + this.token
    });


    this.http.get(this.Url + '/weatherforecast/', {
      headers: httpHeaders      
    }).
      subscribe(
        data => {
          console.log(data);
          this.apiResponse = JSON.stringify(data);
        },
        error => {
          console.log('error');
          console.log(error);
          console.log(typeof error.status);
          console.log(error.status);
          this.apiResponse = <string>error.message;
        }
      )
  }
}
