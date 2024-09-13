import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppointmentListComponent } from './features/appointments/components/appointment-list/appointment-list/appointment-list.component';
import { AppointmentDetailComponent } from './features/appointments/components/appointment-detail/appointment-detail/appointment-detail.component';
import { AppointmentCreateComponent } from './features/appointments/components/appointment-create/appointment-create/appointment-create.component';
import { AppointmentManagemeentComponent } from './features/appointments/pages/appointment-management/appointment-managemeent/appointment-managemeent.component';

const routes: Routes = [
  { path: 'appointments', component: AppointmentListComponent },
  { path: 'appointments/:id', component: AppointmentDetailComponent },
  { path: 'create-appointment', component: AppointmentCreateComponent },
  { path: 'manage-appointments', component: AppointmentManagemeentComponent },
  { path: '', redirectTo: '/appointments', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
