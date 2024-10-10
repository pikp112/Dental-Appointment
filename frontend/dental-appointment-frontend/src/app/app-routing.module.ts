import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppointmentManagementComponent } from './features/appointments/components/appointment-management/appointment-management.component';
import { AppointmentCreateComponent } from './features/appointments/components/appointment-create/appointment-create.component';
import { AppointmentDetailComponent } from './features/appointments/components/appointment-detail/appointment-detail.component';
import { UpdateAppointmentComponent } from './features/appointments/components/update-appointment/update-appointment.component';

const routes: Routes = [
  { path: 'appointments/:id', component: AppointmentDetailComponent },
  { path: 'create-appointment', component: AppointmentCreateComponent },
  { path: 'manage-appointments', component: AppointmentManagementComponent },
  { path: 'update-appointment/:id', component: UpdateAppointmentComponent },
  { path: '', redirectTo: '/appointments', pathMatch: 'full' },
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
