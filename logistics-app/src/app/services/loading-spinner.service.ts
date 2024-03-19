import { Overlay, OverlayConfig, OverlayRef } from "@angular/cdk/overlay";
import { ComponentRef, Injectable, inject } from "@angular/core";
import { LoadingSpinnerComponent } from "../framework/loading-spinner/loading-spinner.component";
import { ComponentPortal } from "@angular/cdk/portal";
import { ISpinnerEvent } from "../models/ISpinnerEvent";

@Injectable({
  providedIn: 'root'
})
export class LoadingSpinnerService {
  //Wir haben einen zentralen Loadingspinner der als Overlay über den Bildschirm gelegt wird.
  //Dieser Spinner wird so lange angezeigt wie in dem SpinnerEvents-Array items sind. Jedes Item enthält
  //asynchrone Promises die nacheinander abgearbeitet werden 

  private _overlayRef!: OverlayRef;
  private _config: OverlayConfig = new OverlayConfig();
  private _component!: ComponentRef<LoadingSpinnerComponent>;
  
  private _overlay: Overlay = inject(Overlay);

  private _spinnerEvents: ISpinnerEvent[] = [];

  /** text message under the spinner */
  private _message!: string;
  public get message(): string
  {
    return this._message;
  }

  public set message(pMessage: string)
  {
    this._message = pMessage;

    if (!!this._component) {
      this._component.instance.message = pMessage;
    }
  }
  
  constructor() {
  }

  public show(msg: string, action: Promise<void>)
  {
    this._spinnerEvents.push({
      message: msg,
      action: [action]
    });

    if(this._spinnerEvents.length > 0)
    {
      this.doAction();
    }
  }


  private async doAction()
  {
    //show spinner
    if (this._overlayRef === undefined) 
    {
      this._config.positionStrategy = this._overlay.position().global().centerHorizontally().centerVertically();
      this._config.hasBackdrop = true;
      this._config.backdropClass = "custom-overlay-backdrop";
      this._overlayRef = this._overlay.create(this._config);
    }

    this._overlayRef.detach();
    
    //create portal to attach the actual spinnercomponent to
    const overlayPortal = new ComponentPortal(LoadingSpinnerComponent); // the portal to hold the actual spinner-_component
    this._component = this._overlayRef.attach(overlayPortal); //attach the portal to the overlay element
    
    this.message = this._spinnerEvents[0].message;

    try
    {
      //do work 
      await Promise.all(this._spinnerEvents[0].action);
    }
    catch(reason: any)
    {
      console.log("Error in spinner", reason);
    }

    //remove spinner
    if(!!this._overlayRef) 
    {
      this._overlayRef.detach();
      this._overlayRef.detachBackdrop();
    }

    this._spinnerEvents.splice(0, 1);

    if(this._spinnerEvents.length > 0)
    {
      this.doAction();
    }
  }
}