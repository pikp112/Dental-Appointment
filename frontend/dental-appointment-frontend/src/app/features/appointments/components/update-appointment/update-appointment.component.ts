import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AppointmentService } from '../../../../core/services/appointment.service';
import { UpdateAppointmentCommand } from '../../../shared/models/update-appointment-command.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { appointmentDateTimeValidator } from '../../../../core/validators/appointment-date-time.validator';

@Component({
  selector: 'app-update-appointment',
  templateUrl: './update-appointment.component.html',
  styleUrls: ['./update-appointment.component.css'],
})
export class UpdateAppointmentComponent implements OnInit {
  appointmentForm: FormGroup;

  constructor(
    private route: ActivatedRoute,
    public router: Router,
    private fb: FormBuilder,
    private appointmentService: AppointmentService,
    private snackBar: MatSnackBar
  ) {
    this.appointmentForm = this.fb.group({
      appointmentDateTime: [
        '',
        Validators.required,
        appointmentDateTimeValidator(),
      ],
      newAppointmentDateTime: [''],
      patientName: [''],
      patientPhoneNumber: [''],
      isConfirmed: [''],
      isRejected: [''],
      treatmentType: [''],
      notes: [''],
    });
  }

  ngOnInit(): void {
    const appointmentId = this.route.snapshot.paramMap.get('id');
    if (appointmentId) {
      this.loadAppointment(appointmentId);
    }
  }

  loadAppointment(id: string): void {
    this.appointmentService.getAppointmentById(id).subscribe((appointment) => {
      this.appointmentForm.patchValue(appointment); // Populate the form with fetched data
    });
  }

  updateAppointment(): void {
    console.log(this.appointmentForm);
    const updateCommand: UpdateAppointmentCommand = this.appointmentForm.value;
    if (this.appointmentForm.valid) {
      this.appointmentService.updateAppointment(updateCommand).subscribe(
        (response) => {
          this.snackBar.open('Appointment updated successfully!', 'Close', {
            duration: 3000,
          });
          this.router.navigate(['/manage-appointments']);
        },
        (error) => {
          this.snackBar.open(
            'Error updating appointment. Please try again.',
            'Close',
            {
              duration: 3000,
            }
          );
          console.error('Error updating appointment:', error);
        }
      );
    } else {
      this.handleErrors();
    }
  }

  handleErrors(): void {
    const appointmentDateTimeControl = this.appointmentForm.get(
      'newAppointmentDateTime'
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
}
