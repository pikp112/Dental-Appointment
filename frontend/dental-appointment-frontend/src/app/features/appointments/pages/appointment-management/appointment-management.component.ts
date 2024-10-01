import {
  AfterViewInit,
  Component,
  OnInit,
  ViewChild,
  inject,
} from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AppointmentService } from '../../../../core/services/appointment.service';
import { MapTreatmentType } from '../../../shared/enums/treatment-type.enum';
import { Appointment } from '../../../shared/models/appointment.model';
import { DeleteAppointmentCommand } from '../../../shared/models/delete-appointment.command.model';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-appointment-management',
  templateUrl: './appointment-management.component.html',
  styleUrl: './appointment-management.component.css',
})
export class AppointmentManagementComponent implements OnInit, AfterViewInit {
  appointments: MatTableDataSource<Appointment> =
    new MatTableDataSource<Appointment>();
  private appointmentService = inject(AppointmentService);
  private snackBar = inject(MatSnackBar);
  displayedColumns: string[] = [
    'number',
    'appointmentDateTime',
    'patientName',
    'patientPhoneNumber',
    'treatmentType',
    'status',
    'actions',
  ];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  ngOnInit(): void {
    this.loadAppointments();
  }
  ngAfterViewInit() {
    this.appointments.paginator = this.paginator;
    this.appointments.sort = this.sort;
  }

  loadAppointments(): void {
    this.appointmentService.getAllAppointments().subscribe((data) => {
      const appointmentsWithRowNumber = data.map((appointment, index) => ({
        ...appointment,
        rowNumber: index + 1,
      }));

      this.appointments = new MatTableDataSource(appointmentsWithRowNumber);

      this.appointments.paginator = this.paginator;
      this.appointments.sort = this.sort;
    });
  }

  getTreatmentTypeName(treatmentTypeIndex: number): string {
    return MapTreatmentType[treatmentTypeIndex];
  }

  deleteAppointment(appointmentDateTime: Date): void {
    const command: DeleteAppointmentCommand = { appointmentDateTime };
    console.log(command);

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

  // getAppointmentNumber(index: number): number {
  //   return this.paginator
  //     ? index + 1 + this.paginator.pageIndex * this.paginator.pageSize
  //     : index + 1;
  // }
}
