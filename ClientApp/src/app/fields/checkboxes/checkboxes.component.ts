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
  masterSelect: boolean;
  fileNames$: any;
  testdata: string[];
  checkList: any = [];
  fileInputPath: string;


  constructor(private http: HttpClient) {

      this.masterSelect = false;
    
  }

  ngOnInit(): void {
    //this.GetFileNames();
  }

  // GetFileNames() : void {
  //   let that = this;

  //   that.fileNames$ = that.http.get<any>(that.baseUrl + "Invoice/GetFileNames").subscribe(function (data) {
  //     that.checkList = data;
  //   }, function (error) {
  //     that.testdata = error;
  //   });

  // }

  checkUncheckAll() : void {
    for(var i = 0; i < this.checkList.length; ++i){
      this.checkList[i].isSelected = this.masterSelect;
    }
  }

  test(){
    console.log("test method");
    for(var i = 0; i < this.checkList.length; ++i){
      console.log(this.checkList[i].value, this.checkList[i].isSelected)
    }
  }

  FilePathInput() : void {
    //C:\Users\Zach\Desktop\SCRIBE\testfiles
    let that = this;
    
    var path = {path: that.fileInputPath}
    that.http.post(that.baseUrl + "Invoice/AddFileInputPath", path).subscribe(function (data) {
      that.checkList = data;
    }, function (error) {
      that.checkList = error;
    });
    
  }

}
