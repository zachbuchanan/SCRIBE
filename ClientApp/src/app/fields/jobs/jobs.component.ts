import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, FormArray } from '@angular/forms';
import { TestBed } from '@angular/core/testing';
import { Job } from 'src/app/classes/Job';
import { JobEditModel } from 'src/app/classes/job.edit.model';

@Component({
  selector: 'app-jobs',
  templateUrl: './jobs.component.html',
  styleUrls: ['./jobs.component.css']
})
export class JobsComponent implements OnInit {
  
  @Input() jobList: JobEditModel = new JobEditModel();
  
  constructor(private fb: FormBuilder) { }

  jobsForm: FormGroup;

  jobs: JobEditModel = new JobEditModel();

  ngOnInit(): void {
    this.jobsForm = this.fb.group({
      jobs: this.fb.array([this.fb.group({title:'', hours:'', rate:''})])
    })
  }


  get allJobs() {
    return this.jobsForm.get('jobs') as FormArray;
  }

  addNewJob() {
    var newJob: Job = new Job();
    newJob.Hours = 2;
    this.jobList.Jobs.push(newJob);
  }

  removeJob(index) {
    this.jobList.Jobs.splice(index, 1);
  } 

}
