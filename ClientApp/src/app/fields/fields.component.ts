import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { CheckboxesComponent } from './checkboxes/checkboxes.component';
import { JobsComponent } from './jobs/jobs.component';
import { HttpClient } from "@angular/common/http";
import { environment } from "../../environments/environment";
import { Job } from '../classes/Job';
import { JobEditModel } from '../classes/job.edit.model';
import { CompanyInfo } from '../classes/CompanyInfo';
import { SectionEditModel } from '../classes/section.edit.model';
import { Section } from '../classes/Section';

@Component({
  selector: 'app-fields',
  templateUrl: './fields.component.html',
  styleUrls: ['./fields.component.css']
})
export class FieldsComponent implements OnInit {
  baseUrl: string = environment.apiRootUrl;

  job: Job = new Job();
  jobs: JobEditModel = new JobEditModel();
  companyInfo: CompanyInfo = new CompanyInfo();
  documentSections: SectionEditModel = new SectionEditModel();

  customerForm: FormGroup;
  @ViewChild(CheckboxesComponent, { static: false }) sections;
//  @ViewChild(JobsComponent, { static: false }) jobs;


  constructor(private http: HttpClient) {
  }
  
  
  ngOnInit() {
    this.customerForm = new FormGroup({
      customer_name: new FormControl(),
      customer_address: new FormControl(),
    });
  }

  onSubmit() : void {
    console.log(this.customerForm.value.customer_name) //access values by...value.customer_name
  }

  generateDocument() : void {

    //Jobs API call
    this.http.post(this.baseUrl + "Invoice/CreateJobs", this.jobs).subscribe(function (data) {
      this.data = data;
    }, function (error) {
      this.data = error;
    });

    //Company Info API call
    this.companyInfo.Address = this.customerForm.value.customer_address;
    this.companyInfo.Name = this.customerForm.value.customer_name;
    this.http.post(this.baseUrl + "Invoice/AddNameAddress", this.companyInfo).subscribe(function (data) {
      this.data = data;
    }, function (error) {
      this.data = error;
    });

    //Sections API call
    for(var i = 0; i < this.sections.checkList.length; ++i){
      var a = new Section(this.sections.checkList[i].id, this.sections.checkList[i].value, this.sections.checkList[i].isSelected)
      this.documentSections.Sections.push(a)
    }
    this.http.post(this.baseUrl + "Invoice/Sections", this.documentSections).subscribe(function (data) {
      this.data = data;
    }, function (error) {
      this.data = error;
    });


  }
    


}


