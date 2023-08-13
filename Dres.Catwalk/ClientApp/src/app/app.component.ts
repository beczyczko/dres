import { Component, OnInit } from '@angular/core';
import { BehaviorSubject, map, Observable, switchMap } from 'rxjs';
import { PumlService } from './api-client/services';

declare var plantuml: any;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  providers: [PumlService]
})
export class AppComponent implements OnInit {
  title = 'dres-catwalk';
  plantumlInitialized = new BehaviorSubject(false);

  constructor(private pumlService: PumlService) {
  }

  public ngOnInit() {
    plantuml.initialize('assets/@sakirtemel/plantuml.js').then(() => {
      this.plantumlInitialized.next(true);

      // todo db render on some UI interaction
      this.getPumlContent()
        .pipe(
          switchMap((pumlContent: string) => this.renderPuml(pumlContent))
        ).subscribe();
    })
  }

  public getPumlContent(): Observable<string> {
    return this.pumlService.getCombined(['1', '2'])
      .pipe(map(res => res.content));
  }

  public renderPuml(pumlContent: string): Promise<any> {
    return plantuml.renderPng(pumlContent)
      .then((blob: any) => {
        this.plantUmlHtmlElement.src = window.URL.createObjectURL(blob)
      });
  }

  public get plantUmlHtmlElement(): any {
    return document.getElementById('plantuml-diagram');
  }
}
