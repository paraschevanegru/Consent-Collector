import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'optional'
})
export class OptionalPipe implements PipeTransform {

  transform(value: any) {
    return value ? 'Required' : 'Optional';
}


}
