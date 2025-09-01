import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filterFish'
})
export class FilterFishPipe implements PipeTransform {

  transform(items: any[], fishQuery:string|undefined): any {
    if (fishQuery == undefined)
      return items;

    return items.filter(item => item.fishName.toLowerCase().includes(fishQuery.toLocaleLowerCase()));
  }

}
