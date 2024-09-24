import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppointmentDetailComponent } from './features/appointments/components/appointment-detail/appointment-detail/appointment-detail.component';
import { AppointmentCreateComponent } from './features/appointments/components/appointment-create/appointment-create/appointment-create.component';
import { AppointmentManagementComponent } from './features/appointments/pages/appointment-management/appointment-management.component';

const routes: Routes = [
  { path: 'appointments/:id', component: AppointmentDetailComponent },
  { path: 'create-appointment', component: AppointmentCreateComponent },
  { path: 'manage-appointments', component: AppointmentManagementComponent },
  { path: '', redirectTo: '/appointments', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
