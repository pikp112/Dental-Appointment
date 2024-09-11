import { TreatmentType } from '../enums/treatment-type.enum';

export interface Appointment {
  id: string;
  appointmentDateTime: Date;
  patientName: string;
  patientPhoneNumber: string;
  treatmentType: TreatmentType;
  duration: string;
  isConfirmed: boolean;
  isRejected: boolean;
  notes: string;
}
