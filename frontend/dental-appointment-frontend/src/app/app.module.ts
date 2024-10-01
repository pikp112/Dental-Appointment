import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AppointmentService } from './core/services/appointment.service';
import { HttpClientModule } from '@angular/common/http';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatToolbarModule } from '@angular/material/toolbar';
import { AppointmentManagementComponent } from './features/appointments/pages/appointment-management/appointment-management.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import {
  MAT_DATETIME_FORMATS,
  MatDatetimepickerModule,
} from '@mat-datetimepicker/core';
import { MatMomentDatetimeModule } from '@mat-datetimepicker/moment';
import { AppointmentDetailComponent } from './features/appointments/components/appointment-detail/appointment-detail.component';
import { AppointmentCreateComponent } from './features/appointments/components/appointment-create/appointment-create.component';
import { UpdateAppointmentComponent } from './features/appointments/components/update-appointment/update-appointment.component';
import { MatCardModule } from '@angular/material/card';
import { AppointmentFormComponent } from './features/shared/classes/appointment-form/appointment-form.component';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { RomaniaDatePipe } from './core/pipes/romania-date.pipe';

@NgModule({
  declarations: [
    AppComponent,
    AppointmentDetailComponent,
    AppointmentCreateComponent,
    AppointmentManagementComponent,
    UpdateAppointmentComponent,
    AppointmentFormComponent,
    RomaniaDatePipe,
  ],
  imports: [
    BrowserModule,
    CommonModule,
    FormsModule,
    RouterModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule,
    MatSelectModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatTableModule,
    MatToolbarModule,
    MatButtonModule,
    MatSnackBarModule,
    MatDatetimepickerModule,
    MatMomentDatetimeModule,
    MatButtonModule,
    MatCardModule,
    MatPaginatorModule,
    MatSortModule,
  ],
  exports: [RomaniaDatePipe],
  providers: [AppointmentService, provideAnimationsAsync()],
  bootstrap: [AppComponent],
})
export class AppModule {}
