import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'romaniaDate',
})
export class RomaniaDatePipe implements PipeTransform {
  transform(value: any): string {
    if (!value) return '';

    const date = new Date(value);
    return date.toLocaleString('ro-RO', {
      timeZone: 'Europe/Bucharest',
      weekday: 'long',
      year: 'numeric',
      month: 'long',
      day: 'numeric',
      hour: '2-digit',
      minute: '2-digit',
      hour12: false,
    });
  }
}
