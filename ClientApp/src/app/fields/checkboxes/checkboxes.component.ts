import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from "rxjs/operators";
@Component({
  selector: 'app-checkboxes',
  templateUrl: './checkboxes.component.html',
  styleUrls: ['./checkboxes.component.css']
})
export class CheckboxesComponent implements OnInit {
  baseUrl: string = environment.apiRootUrl;
  masterSelect: Boolean;
  checkList: any;
  fileNames: Observable<string[]>;



  constructor(private http: HttpClient) { 
    this.masterSelect = false;
    this.checkList = [
      {id:1,value:'Section1',isSelected:false},
      {id:2,value:'Section2',isSelected:false},
      {id:3,value:'Section3',isSelected:false},
      {id:4,value:'Section4',isSelected:false},
      {id:5,value:'Section5',isSelected:false},
      {id:6,value:'Section6',isSelected:false},
      {id:7,value:'Section7',isSelected:false},
    ]
  }

  ngOnInit(): void {
    // this.http.get(this.baseUrl + "Invoice/GetFileNames").subscribe(function (data) {
    //   this.data = data;
    // }, function (error) {
    //   this.data = error;
    // });
    // this.fileNames = this.http.get<string[]>(this.baseUrl + "Invoice/GetFileNames").subscribe(function (data) {
    //     this.data = data;
    //   }, function (error) {
    //     this.data = error;
    //   });
    // console.log(this.fileNames);
    
  }

  checkUncheckAll() : void{
    for(var i = 0; i < this.checkList.length; ++i){
      this.checkList[i].isSelected = this.masterSelect;
    }
  }

  test(){
    for(var i = 0; i < this.checkList.length; ++i){
      console.log(this.checkList[i].value, this.checkList[i].isSelected)
    }
  }
}
