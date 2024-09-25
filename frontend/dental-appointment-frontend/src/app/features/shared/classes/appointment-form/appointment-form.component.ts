import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AppointmentService } from '../../../../core/services/appointment.service';
import { TreatmentType } from '../../enums/treatment-type.enum';

@Component({
  selector: 'app-appointment-form',
  templateUrl: './appointment-form.component.html',
  styleUrls: ['./appointment-form.component.css'],
})
export class AppointmentFormComponent implements OnInit {
  appointmentForm: FormGroup;
  isEditMode: boolean = false;
  treatmentTypes = Object.values(TreatmentType); // Assuming TreatmentType is an enum

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private appointmentService: AppointmentService // Serviciul pentru gestionarea întâlnirilor
  ) {
    this.appointmentForm = this.fb.group({
      patientName: ['', Validators.required],
      patientPhoneNumber: [
        '',
        [Validators.required, Validators.pattern('^[0-9]*$')],
      ], // Validare simplă pentru număr
      treatmentType: ['', Validators.required],
      appointmentDateTime: [
        { value: '', disabled: false },
        Validators.required,
      ], // Editabil în modul de creare
      newAppointmentDateTime: [
        { value: '', disabled: true },
        Validators.required,
      ], // Doar pentru actualizare
      notes: [''],
    });
  }

  ngOnInit(): void {
    // Verificăm dacă suntem în modul de editare
    const appointmentId = this.route.snapshot.paramMap.get('id'); // Presupunem că ID-ul este transmis în ruta
    if (appointmentId) {
      this.isEditMode = true;
      this.loadAppointment(appointmentId);
    }
  }

  loadAppointment(id: string): void {
    this.appointmentService.getAppointmentById(id).subscribe((appointment) => {
      this.appointmentForm.patchValue({
        patientName: appointment.patientName,
        patientPhoneNumber: appointment.patientPhoneNumber,
        treatmentType: appointment.treatmentType,
        appointmentDateTime: appointment.appointmentDateTime,
        notes: appointment.notes,
      });
    });
  }

  onSubmit(): void {
    if (this.appointmentForm.valid) {
      if (this.isEditMode) {
        this.updateAppointment();
      } else {
        this.createAppointment();
      }
    }
  }

  createAppointment(): void {
    this.appointmentService
      .createAppointment(this.appointmentForm.value)
      .subscribe(() => {
        this.router.navigate(['/appointments']); // Redirecționează utilizatorul după crearea întâlnirii
      });
  }

  updateAppointment(): void {
    this.appointmentService
      .updateAppointment(this.appointmentForm.value)
      .subscribe(() => {
        this.router.navigate(['/appointments']); // Redirecționează utilizatorul după actualizarea întâlnirii
      });
  }
}
