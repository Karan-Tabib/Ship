import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filterByBoat'
})
export class FilterByBoatPipe implements PipeTransform {

  transform(items: any[], boatvalue: number): any {
    if (!items || boatvalue == -1)
      return items;

    return items.filter(item => item.boatId == boatvalue)
  }
}
