import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  SpecificationsSelectionComponent,
  SpecificationSummary
} from '../specifications-selection/specifications-selection.component';
import { BehaviorSubject } from 'rxjs';
import { API_BASE_URL, SpecificationsService } from '../api-client/services';
import * as svgPanZoom from 'svg-pan-zoom';

@Component({
  selector: 'app-diagram-preview',
  standalone: true,
  imports: [CommonModule, SpecificationsSelectionComponent],
  templateUrl: './diagram-preview.component.html',
  styleUrls: ['./diagram-preview.component.scss'],
  providers: [SpecificationsService]
})
export class DiagramPreviewComponent implements OnInit {
  plantumlInitialized = new BehaviorSubject(false);
  noSpecificationSelected = true;

  constructor(
    @Inject(API_BASE_URL) private baseUrl: string) {
  }

  ngOnInit(): void {
    if (this.plantUmlHtmlElement) {
      this.plantUmlHtmlElement.onload = () => {
        svgPanZoom('#plantuml-diagram', {
          zoomEnabled: true,
          controlIconsEnabled: true
        });
      }
    }
  }

  private getPumlContent(specificationsIds: string[]): void {
    const queryParams = specificationsIds.map(id => `specIds=${id}`);
    const queryParamsJoined = queryParams.join('&');

    if (this.plantUmlHtmlElement) {
      this.plantUmlHtmlElement.data = `${this.baseUrl}/api/puml/combine/svg?${queryParamsJoined}`;
    }
  }

  public get plantUmlHtmlElement(): HTMLObjectElement | undefined {
    return Array.from(document.getElementsByTagName('object'))
      .find(value => value.id === 'plantuml-diagram');
  }

  public onSpecificationSelectionChanged($event: SpecificationSummary[]) {
    if ($event.length > 0) {
      this.noSpecificationSelected = false;
      this.getPumlContent($event.map(s => s.id.value));
    } else {
      this.noSpecificationSelected = true;
    }
  }
}
