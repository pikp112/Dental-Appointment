import { TreatmentType } from '../enums/treatment-type.enum';

export interface UpdateAppointmentCommand {
  appointmentDateTime: Date;
  newAppointmentDateTime?: Date;
  patientName?: string;
  patientPhoneNumber?: string;
  isConfirmed?: boolean;
  isRejected?: boolean;
  treatmentType?: TreatmentType;
  notes?: string;
}
