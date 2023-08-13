import {Component, OnInit} from '@angular/core';
import {BehaviorSubject} from "rxjs";

declare var plantuml: any;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'dres-catwalk';
  plantumlInitialized = new BehaviorSubject(false);

  public ngOnInit() {
    plantuml.initialize('assets/@sakirtemel/plantuml.js').then(() => {
      this.plantumlInitialized.next(true);

      // todo db render on some UI interaction
      const pumlContent = this.getPumlContent();
      this.renderPuml(pumlContent);
    })
  }

  public getPumlContent(): string {
    // todo db from API
    return `
@startuml

!include https://localhost:7090/api/puml/1
!include https://localhost:7090/api/puml/2

left to right direction
title Combined Domain model relationships

@enduml
`;
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
