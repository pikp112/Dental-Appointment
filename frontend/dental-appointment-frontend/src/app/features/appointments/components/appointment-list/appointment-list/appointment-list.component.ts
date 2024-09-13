import { Component, OnInit, inject } from '@angular/core';
import { Appointment } from '../../../../shared/models/appointment.model';
import { AppointmentService } from '../../../../../core/services/appointment.service';
import { MapTreatmentType } from '../../../../shared/enums/treatment-type.enum';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DeleteAppointmentCommand } from '../../../../shared/models/delete-appointment.command.model';

@Component({
  selector: 'app-appointment-list',
  templateUrl: './appointment-list.component.html',
  styleUrls: ['./appointment-list.component.css'],
})
export class AppointmentListComponent implements OnInit {
  appointments: Appointment[] = [];
  private appointmentService = inject(AppointmentService);
  private snackBar = inject(MatSnackBar);
  displayedColumns: string[] = [
    'id',
    'appointmentDateTime',
    'patientName',
    'patientPhoneNumber',
    'treatmentType',
    'status',
    'actions',
  ];

  ngOnInit(): void {
    this.loadAppointments();
  }

  loadAppointments(): void {
    this.appointmentService.getAllAppointments().subscribe((data) => {
      this.appointments = data;
    });
  }

  getTreatmentTypeName(treatmentTypeIndex: number): string {
    return MapTreatmentType[treatmentTypeIndex];
  }

  deleteAppointment(appointmentDateTime: Date): void {
    const command: DeleteAppointmentCommand = { appointmentDateTime };

    this.appointmentService.deleteAppointment(command).subscribe(
      () => {
        this.snackBar.open('Appointment deleted successfully', 'Close', {
          duration: 3000,
        });
        this.loadAppointments();
      },
      (error) => {
        console.error('Error deleting appointment:', error);
        this.snackBar.open('Error deleting appointment', 'Close', {
          duration: 3000,
        });
      }
    );
  }
}
