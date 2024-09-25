import { Component, OnInit, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AppointmentService } from '../../../../core/services/appointment.service';
import { MapTreatmentType } from '../../../shared/enums/treatment-type.enum';
import { Appointment } from '../../../shared/models/appointment.model';

@Component({
  selector: 'app-appointment-detail',
  templateUrl: './appointment-detail.component.html',
  styleUrl: './appointment-detail.component.css',
})
export class AppointmentDetailComponent implements OnInit {
  appointment: Appointment | undefined;
  private route = inject(ActivatedRoute);
  private appointmentService = inject(AppointmentService);

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.appointmentService.getAppointmentById(id).subscribe((data) => {
        this.appointment = data;
      });
    } else {
      console.error('No id found in route');
    }
  }

  getTreatmentTypeName(treatmentTypeIndex: number): string {
    return MapTreatmentType[treatmentTypeIndex];
  }
}
