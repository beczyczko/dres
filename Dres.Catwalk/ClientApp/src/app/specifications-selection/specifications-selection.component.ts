import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { SpecificationsService } from '../api-client/services';
import { debounceTime, map, Observable } from 'rxjs';

export class SpecificationSummary {
  constructor(
    public id: number,
    public name: string,
    public tag: string,
    public createdOn: Date,
  ) {
  }
}

@Component({
  selector: 'app-specifications-selection',
  standalone: true,
  imports: [MatFormFieldModule, MatSelectModule, FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './specifications-selection.component.html',
  styleUrls: ['./specifications-selection.component.scss']
})
export class SpecificationsSelectionComponent {
  selectedSpecifications = new FormControl<SpecificationSummary[]>([]);
  allSpecifications$: Observable<SpecificationSummary[]>;

  @Output()
  selectionChanged = new EventEmitter<SpecificationSummary[]>();

  constructor(
    private readonly _specificationsService: SpecificationsService) {
    this.allSpecifications$ = this._specificationsService.all()
      .pipe(
        map(value => value.map(s => new SpecificationSummary(s.id, s.name, s.tag, s.createdOn)))
      );

    this.selectedSpecifications.valueChanges
      .pipe(debounceTime(1000))
      .subscribe(val => {
        if (val) {
          this.selectionChanged.emit(val);
        }
      });
  }
}
