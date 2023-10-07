import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { SpecificationId, SpecificationsService } from '../api-client/services';
import { debounceTime, map, Observable } from 'rxjs';
import { groupBy } from '../../utils/group.by';

export class SpecificationSummary {
  constructor(
    public id: SpecificationId,
    public createdOn: Date,
  ) {
  }
}

export class SpecificationGroup {
  constructor(
    public name: string,
    public specifications: SpecificationSummary[]
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
  specificationGroups$: Observable<SpecificationGroup[]>;

  @Output()
  selectionChanged = new EventEmitter<SpecificationSummary[]>();

  constructor(
    private readonly _specificationsService: SpecificationsService) {
    this.specificationGroups$ = this._specificationsService.all()
      .pipe(
        map(value => {
          const groups = groupBy(value, s => s.specificationId.name);
          return groups.map(group => new SpecificationGroup(
            group.key,
            group.values.map(s => new SpecificationSummary(s.specificationId, s.createdOn))));
        })
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
