import { Component, Inject, OnInit } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { API_BASE_URL, PumlService } from './api-client/services';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  providers: [PumlService]
})
export class AppComponent implements OnInit {
  title = 'dres-catwalk';
  plantumlInitialized = new BehaviorSubject(false);

  constructor(@Inject(API_BASE_URL) private baseUrl?: string) {
  }

  public ngOnInit() {
    this.getPumlContent(); // todo db move to some other component
  }

  private getPumlContent(): void {
    const specificationsIds = [2, 3, 4]; // todo db from frontend
    const queryParams = specificationsIds.map(id => `specIds=${id}`);
    const queryParamsJoined = queryParams.join('&');
    this.plantUmlHtmlElement.data = `${this.baseUrl}/api/puml/combine/svg?${queryParamsJoined}`;
  }

  public get plantUmlHtmlElement(): any {
    return document.getElementById('plantuml-diagram');
  }
}

