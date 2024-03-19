import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
 name: 'truncate',
 standalone: true
})
export class TruncatePipe implements PipeTransform 
{
  //Wir schneiden Strings an einer definierten Länge ab und hänngen ... dran 
  transform(value: string, args: any[]): string 
  {
    const limit = args.length > 0 ? parseInt(args[0], 10) : 20;
    const trail = args.length > 1 ? args[1] : '...';
    return value.length > limit ? value.substring(0, limit) + trail : value;
  }
}