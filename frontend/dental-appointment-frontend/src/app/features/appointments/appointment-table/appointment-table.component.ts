import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MapTreatmentType } from '../../shared/enums/treatment-type.enum';
import { Appointment } from '../../shared/models/appointment.model';

@Component({
  selector: 'app-appointment-table',
  templateUrl: './appointment-table.component.html',
  styleUrl: './appointment-table.component.css',
})
export class AppointmentTableComponent implements OnInit {
  @Input() appointments: MatTableDataSource<Appointment> =
    new MatTableDataSource<Appointment>();
  @Input() displayedColumns: string[] = [];

  dataSource: MatTableDataSource<Appointment> =
    new MatTableDataSource<Appointment>();

  @ViewChild(MatPaginator) paginator: MatPaginator;

  ngOnInit(): void {
    console.log('AppointmentTableComponent initialized');
    this.dataSource = this.appointments;
    console.log(this.dataSource);
  }

  ngAfterViewInit() {
    console.log('AppointmentTableComponent view initialized');
    this.dataSource.paginator = this.paginator;
  }

  getTreatmentTypeName(treatmentTypeIndex: number): string {
    return MapTreatmentType[treatmentTypeIndex];
  }
}
