import { Injectable } from '@angular/core';
import { KeyedCollection } from 'src/utils/dictionary';

@Injectable({ providedIn: 'root' })
export class ReportFilterBuilder {
    dictionary: KeyedCollection<string>

    set(key: string, value: string): void {
        if(this.dictionary.ContainsKey(key))
            value = this.dictionary.Remove(key) + " AND " + value;

        this.dictionary.Add(key, value);
    }

    toString() : string {
        var sb = "=";

        this.dictionary.Keys().forEach(key => {
            sb += "[" + key.toUpperCase() + ":" + this.dictionary.Item(key) + "];" 
        });

        if(sb.endsWith(";"))
            sb = sb.substring(0, sb.length - 1);

        return sb;
    }
}