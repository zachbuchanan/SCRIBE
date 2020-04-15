import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LayoutPreviewComponent } from './layout-preview/layout-preview.component';
import { FieldsComponent } from './fields/fields.component';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CheckboxesComponent } from './fields/checkboxes/checkboxes.component';
import { JobsComponent } from './fields/jobs/jobs.component';
import { HttpClientModule, HttpClient } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    LayoutPreviewComponent,
    FieldsComponent,
    CheckboxesComponent,
    JobsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
