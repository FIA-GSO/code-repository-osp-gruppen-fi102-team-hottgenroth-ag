/**
 * Here we have a base service to store data that we need
 * for the whole app or for some contexts. It is based on
 * redux stores but with all techniques that we have in
 * Angular
 * https://www.maestralsolutions.com/angular-application-state-management-you-do-not-need-external-data-stores/
 */

//Hier haben wir eine gemeinsame Datenhaltung, die auch alle Referenzen in den anderen Komponentent upadtet

import { EventEmitter, inject } from "@angular/core";
import { BehaviorSubject } from "rxjs";


export abstract class BaseStoreService<T>
{
  public errorReceived: EventEmitter<any> = new EventEmitter();

  protected initialized: boolean = false;
  public get isInitialized(): boolean
  {
    return this.initialized;
  }

  // behaviour subject das die Referenzen speichert und alle subscriber benachrichtigt
  private readonly _source = new BehaviorSubject<T[]>([]);

  // Read only Observable woran die Subscriber sich dran hängen können
  public readonly items$ = this._source.asObservable();

  // Gibt die letzen Änderungen des Observerables zurück.
  public getItems(): T[] {
    return !!this._source.getValue() ? this._source.getValue() : [];
  }

  /** Clear the whole store */
  public clear(): void
  {
    this.setItems([]);
    this.initialized = false;
  }

  public getById(id: string): T | undefined
  {
    return this.getItems().find(f => (f as any).identifier === id);
  }

  public abstract delete(itemID: string): Promise<boolean>;

  protected setItems(items: T[]): void {
    this._source.next(items);
  }

  /** Add an item to the store */
  protected addItem(item: T): void {
    const items = [...this.getItems(), item];
    this.setItems(items);
  }

  /** Remove an item from the store */
  protected removeItem(item: T): void {
    const items = this.getItems().filter(p => p !== item);
    this.setItems(items);
  }
}
