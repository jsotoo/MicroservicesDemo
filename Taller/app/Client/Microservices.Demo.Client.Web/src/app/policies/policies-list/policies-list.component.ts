import { Component, OnInit } from '@angular/core';
import { ReportService } from '../../services/data/policies/report.service';
import { IReport } from '../../models/ireport';

@Component({
  selector: 'app-policies-list',
  templateUrl: './policies-list.component.html',
  styleUrls: ['./policies-list.component.scss']
})
export class PoliciesListComponent implements OnInit {
  reports: IReport[] = [];
  loading = false;
  error: string = '';

  constructor(private reportService: ReportService) { }

  ngOnInit(): void {
    this.loadReport();
  }

  loadReport(): void {
    this.loading = true;
    this.reportService.getPoliciesReport()
      .subscribe({
        next: (data) => {
          this.reports = data;
          this.loading = false;
        },
        error: (error) => {
          this.error = 'Error al cargar el reporte';
          this.loading = false;
          console.error('Error:', error);
        }
      });
  }
}
