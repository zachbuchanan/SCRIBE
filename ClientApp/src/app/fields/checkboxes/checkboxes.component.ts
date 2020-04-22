import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from "rxjs/operators";
//import { checkServerIdentity } from 'tls';
import { ThrowStmt } from '@angular/compiler';

@Component({
  selector: 'app-checkboxes',
  templateUrl: './checkboxes.component.html',
  styleUrls: ['./checkboxes.component.css']
})
export class CheckboxesComponent implements OnInit {
  baseUrl: string = environment.apiRootUrl;
  masterSelect: Boolean;
  checkList: any;
  fileNames$: any;
  testdata: string[];
  



  constructor(private http: HttpClient) { 
    this.checkList = [
      {id:1,value:'Section1',isSelected:false},
      {id:2,value:'Section2',isSelected:false},
      {id:3,value:'Section3',isSelected:false},
      {id:4,value:'Section4',isSelected:false},
      {id:5,value:'Section5',isSelected:false},
      {id:6,value:'Section6',isSelected:false},
      {id:7,value:'Section7',isSelected:false},
    ]

    this.fileNames$ = this.http.get<any>(this.baseUrl + "Invoice/GetFileNames").subscribe(function (data) {

      this.testdata = data;
      // for (let i = 0; i < this.testdata.length; i++) {
      //   //console.log(this.testdata[i])
      //   this.checkList[i].value = this.testdata[i];
        
      // }
      
    }, function (error) {
      this.testdata = error;
    });


    
    this.masterSelect = false;
    // this.checkList = [
    //   {id:1,value:'Section1',isSelected:false},
    //   {id:2,value:'Section2',isSelected:false},
    //   {id:3,value:'Section3',isSelected:false},
    //   {id:4,value:'Section4',isSelected:false},
    //   {id:5,value:'Section5',isSelected:false},
    //   {id:6,value:'Section6',isSelected:false},
    //   {id:7,value:'Section7',isSelected:false},
    // ]
  }

  ngOnInit(): void {
    // this.fileNames$ = this.http.get<any>(this.baseUrl + "Invoice/GetFileNames").subscribe(function (data) {

    //   this.testdata = data;
      
    // }, function (error) {
    //   this.testdata = error;
    // });
    
    // console.log("here");
    // console.log(this.testdata);
    
    
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
