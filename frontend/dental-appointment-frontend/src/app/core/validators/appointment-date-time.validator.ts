import {
  AbstractControl,
  ValidationErrors,
  AsyncValidatorFn,
} from '@angular/forms';
import { Observable, of } from 'rxjs';

export function appointmentDateTimeValidator(): AsyncValidatorFn {
  return (control: AbstractControl): Observable<ValidationErrors | null> => {
    const appointmentDate = new Date(control.value);
    const now = new Date();

    if (appointmentDate < new Date(now.getTime() + 60 * 60 * 1000)) {
      return of({ dateTooSoon: true }).pipe();
    }

    const romaniaTime = new Date(
      appointmentDate.toLocaleString('en-US', { timeZone: 'Europe/Bucharest' })
    );
    const startOfWorkDay = new Date(romaniaTime);
    startOfWorkDay.setHours(9, 0, 0);
    const endOfWorkDay = new Date(romaniaTime);
    endOfWorkDay.setHours(17, 30, 0);

    if (romaniaTime < startOfWorkDay || romaniaTime > endOfWorkDay) {
      return of({ outsideWorkingHours: true }).pipe();
    }

    if (romaniaTime.getDay() === 0 || romaniaTime.getDay() === 6) {
      return of({ weekend: true }).pipe();
    }

    return of(null).pipe();
  };
}
