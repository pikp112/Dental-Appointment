import { TreatmentType } from '../enums/treatment-type.enum';
import { BaseModel } from './base-model.model';

export interface CreateAppointmentCommand extends BaseModel {
  patientName: string;
  patientPhoneNumber: string;
  treatmentType?: TreatmentType;
  notes?: string;
}
