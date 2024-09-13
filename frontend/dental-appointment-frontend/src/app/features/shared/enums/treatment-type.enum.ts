export enum TreatmentType {
  Consultation = 'Consultation',
  Cleaning = 'Cleaning',
  Filling = 'Filling',
  Extraction = 'Extraction',
  RootCanal = 'Root Canal',
  Whitening = 'Whitening',
  Checkup = 'Checkup',
  Orthodontics = 'Orthodontics',
}

export const MapTreatmentType = [
  TreatmentType.Consultation,
  TreatmentType.Cleaning,
  TreatmentType.Filling,
  TreatmentType.Extraction,
  TreatmentType.RootCanal,
  TreatmentType.Whitening,
  TreatmentType.Checkup,
  TreatmentType.Orthodontics,
] as const;
