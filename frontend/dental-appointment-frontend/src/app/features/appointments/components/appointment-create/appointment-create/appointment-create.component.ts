import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AppointmentService } from '../../../../../core/services/appointment.service';
import { TreatmentType } from '../../../../shared/enums/treatment-type.enum';
import { CreateAppointmentCommand } from '../../../../shared/models/create-appointment-command.model';
import { Guid } from 'guid-typescript';
import { appointmentDateTimeValidator } from '../../../../../core/validators/appointment-date-time.validator';

@Component({
  selector: 'app-appointment-create',
  templateUrl: './appointment-create.component.html',
  styleUrls: ['./appointment-create.component.css'],
})
export class AppointmentCreateComponent {
  appointmentForm: FormGroup;
  treatmentTypes = Object.values(TreatmentType); // Assuming TreatmentType is an enum

  constructor(
    private fb: FormBuilder,
    private appointmentService: AppointmentService,
    private snackBar: MatSnackBar // Injectați MatSnackBar
  ) {
    this.appointmentForm = this.fb.group({
      patientName: ['', Validators.required],
      patientPhoneNumber: [
        '',
        [Validators.required, Validators.pattern('^\\+?\\d{10,15}$')],
      ],
      treatmentType: [TreatmentType.Consultation],
      notes: [''],
      appointmentDateTime: [
        '',
        Validators.required,
        appointmentDateTimeValidator(),
      ],
    });
  }

  onSubmit(): void {
    console.log(this.appointmentForm.value);
    if (this.appointmentForm.valid) {
      const appointmentDateTimeISO = new Date(
        this.appointmentForm.value.appointmentDateTime
      ).toISOString();

      const command: CreateAppointmentCommand = {
        ...this.appointmentForm.value,
        appointmentDateTime: appointmentDateTimeISO,
        id: Guid.create().toString(),
      };

      this.appointmentService.createAppointment(command).subscribe(
        (response) => {
          this.snackBar.open('Appointment created successfully!', 'Close', {
            duration: 3000,
          });
          this.appointmentForm.reset();
          Object.keys(this.appointmentForm.controls).forEach((key) => {
            this.appointmentForm.controls[key].setErrors(null);
          });
          this.appointmentForm.markAsPristine();
          this.appointmentForm.markAsUntouched();
        },
        (error) => {
          this.snackBar.open(
            'Error creating appointment. Please try again.',
            'Close',
            {
              duration: 3000,
            }
          );
          console.error('Error creating appointment:', error);
        }
      );
    } else {
      this.handleErrors();
    }
  }
  handleErrors(): void {
    const appointmentDateTimeControl = this.appointmentForm.get(
      'appointmentDateTime'
    );

    if (appointmentDateTimeControl?.errors) {
      if (appointmentDateTimeControl.errors['weekend']) {
        this.snackBar.open(
          'Appointments can only be scheduled from Monday to Friday.',
          'Close',
          {
            duration: 3000,
          }
        );
      } else if (appointmentDateTimeControl.errors['outsideWorkingHours']) {
        this.snackBar.open(
          'The appointment time must be between 09:00 and 17:30.',
          'Close',
          {
            duration: 3000,
          }
        );
      } else if (appointmentDateTimeControl.errors['dateTooSoon']) {
        this.snackBar.open(
          'The appointment date must be at least 1 hour from now.',
          'Close',
          {
            duration: 3000,
          }
        );
      }
    }
  }

  clear(): void {
    this.appointmentForm.reset();
  }
}
