import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { AppointmentDetailComponent } from './features/appointments/components/appointment-detail/appointment-detail/appointment-detail.component';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AppointmentService } from './core/services/appointment.service';
import { HttpClientModule } from '@angular/common/http';
import { AppointmentCreateComponent } from './features/appointments/components/appointment-create/appointment-create/appointment-create.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatToolbarModule } from '@angular/material/toolbar';
import { AppointmentManagementComponent } from './features/appointments/pages/appointment-management/appointment-management.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatDatetimepickerModule } from '@mat-datetimepicker/core';
import { MatMomentDatetimeModule } from '@mat-datetimepicker/moment';

@NgModule({
  declarations: [
    AppComponent,
    AppointmentDetailComponent,
    AppointmentCreateComponent,
    AppointmentManagementComponent,
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
  ],
  providers: [AppointmentService, provideAnimationsAsync()],
  bootstrap: [AppComponent],
})
export class AppModule {}
