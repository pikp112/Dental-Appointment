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
  validAppointments: MatTableDataSource<Appointment> =
    new MatTableDataSource<Appointment>();
  invalidAppointments: MatTableDataSource<Appointment> =
    new MatTableDataSource<Appointment>();
  private appointmentService = inject(AppointmentService);
  private snackBar = inject(MatSnackBar);

  @ViewChild('validPaginator') validPaginator: MatPaginator;
  @ViewChild('invalidPaginator') invalidPaginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  ngOnInit(): void {
    console.log('AppointmentManagementComponent initialized');
    this.loadAppointments();
  }
  ngAfterViewInit() {
    console.log('AppointmentManagementComponent view initialized');
    this.validAppointments.paginator = this.validPaginator;
    this.invalidAppointments.paginator = this.invalidPaginator;
  }

  loadAppointments(): void {
    this.appointmentService.getAllAppointments().subscribe((data) => {
      const currentDate = new Date();

      const appointmentsWithRowNumber = data.map((appointment, index) => ({
        ...appointment,
        rowNumber: index + 1,
      }));

      const valid = appointmentsWithRowNumber.filter(
        (appointment) =>
          new Date(appointment.appointmentDateTime) >
          new Date(currentDate.getTime() + 60 * 60 * 1000)
      );

      const invalid = appointmentsWithRowNumber.filter(
        (appointment) =>
          new Date(appointment.appointmentDateTime) <=
          new Date(currentDate.getTime() + 60 * 60 * 1000)
      );

      this.validAppointments = new MatTableDataSource(
        valid.map((appointment, index) => ({
          ...appointment,
          rowNumber: index + 1,
        }))
      );
      this.invalidAppointments = new MatTableDataSource(
        invalid.map((appointment, index) => ({
          ...appointment,
          rowNumber: index + 1,
        }))
      );

      this.validAppointments.paginator = this.validPaginator;
      this.invalidAppointments.paginator = this.invalidPaginator;
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
