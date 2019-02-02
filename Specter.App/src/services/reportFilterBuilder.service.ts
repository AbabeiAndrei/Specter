import { Injectable } from '@angular/core';
import { KeyedCollection } from 'src/utils/dictionary';

@Injectable({ providedIn: 'root' })
export class ReportFilterBuilder {
    private dictionary: KeyedCollection<string> = new KeyedCollection();

    set(key: string, value: string): void {
        if (this.dictionary.ContainsKey(key)) {
            value = this.dictionary.Remove(key) + ' OR ' + value;
        }

        this.dictionary.Add(key, value);
    }

    clear(): void {
        this.dictionary.Clear();
    }

    default(date: Date, user: string = null): string {
        if (!user) {
            user = '#Me';
        }

        return '=[USER:' + user + '];[DATE:' + date.toISOString() + '-' + date.toISOString() + ']';
    }

    toString(): string {
        let sb = '=';

        this.dictionary.Keys().forEach(key => {
            sb += '[' + key.toUpperCase() + ':' + this.dictionary.Item(key) + '];';
        });

        if (sb.endsWith(';')) {
            sb = sb.substring(0, sb.length - 1);
        }

        return sb;
    }
}
