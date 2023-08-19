import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  SpecificationsSelectionComponent,
  SpecificationSummary
} from '../specifications-selection/specifications-selection.component';
import { BehaviorSubject } from 'rxjs';
import { API_BASE_URL, SpecificationsService } from '../api-client/services';

@Component({
  selector: 'app-diagram-preview',
  standalone: true,
  imports: [CommonModule, SpecificationsSelectionComponent],
  templateUrl: './diagram-preview.component.html',
  styleUrls: ['./diagram-preview.component.scss'],
  providers: [SpecificationsService]
})
export class DiagramPreviewComponent {
  plantumlInitialized = new BehaviorSubject(false);
  noSpecificationSelected = true;

  constructor(
    @Inject(API_BASE_URL) private baseUrl: string) {
  }

  private getPumlContent(specificationsIds: number[]): void {
    const queryParams = specificationsIds.map(id => `specIds=${id}`);
    const queryParamsJoined = queryParams.join('&');
    this.plantUmlHtmlElement.data = `${this.baseUrl}/api/puml/combine/svg?${queryParamsJoined}`;
  }

  public get plantUmlHtmlElement(): any {
    return document.getElementById('plantuml-diagram');
  }

  public onSpecificationSelectionChanged($event: SpecificationSummary[]) {
    if ($event.length > 0) {
      this.noSpecificationSelected = false;
      this.getPumlContent($event.map(s => s.id));
    } else {
      this.noSpecificationSelected = true;
    }
  }
}
