import { Injectable, inject } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { Appointment } from '../../features/shared/models/appointment.model';
import { CreateAppointmentCommand } from '../../features/shared/models/create-appointment-command.model';
import { UpdateAppointmentCommand } from '../../features/shared/models/update-appointment-command.model';
import { DeleteAppointmentCommand } from '../../features/shared/models/delete-appointment.command.model';

@Injectable({
  providedIn: 'root',
})
export class AppointmentService {
  private apiUrl = `${environment.apiBaseUrl}/appointments`;
  private http = inject(HttpClient);

  getAllAppointments(): Observable<Appointment[]> {
    return this.http.get<Appointment[]>(this.apiUrl);
  }

  getAppointmentById(id: string): Observable<Appointment> {
    return this.http.get<Appointment>(`${this.apiUrl}/id/${id}`);
  }

  getAppointmentByDateTime(dateTime: Date): Observable<Appointment> {
    return this.http.get<Appointment>(
      `${this.apiUrl}/appointments/${dateTime}`
    );
  }

  createAppointment(
    appointment: CreateAppointmentCommand
  ): Observable<Appointment> {
    return this.http.post<Appointment>(
      `${this.apiUrl}/appointments`,
      appointment
    );
  }

  updateAppointment(appointment: UpdateAppointmentCommand): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/appointments`, appointment);
  }

  deleteAppointment(appointment: DeleteAppointmentCommand): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/appointments`, {
      body: appointment,
    });
  }
}
