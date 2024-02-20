import { Overlay, OverlayConfig, OverlayRef } from "@angular/cdk/overlay";
import { ComponentRef, Injectable, inject } from "@angular/core";
import { Subject } from "rxjs";
import { LoadingSpinnerComponent } from "../framework/loading-spinner/loading-spinner.component";
import { ComponentPortal } from "@angular/cdk/portal";

@Injectable({
  providedIn: 'root'
})
export class LoadingSpinnerService {
  private _overlayRef!: OverlayRef;
  private _config: OverlayConfig = new OverlayConfig();
  private _component!: ComponentRef<LoadingSpinnerComponent>;
  
  private _overlay: Overlay = inject(Overlay);

  constructor() {
  }

  public show() 
  { 
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
  }

  /**
   * remove overlay and all attached elements
   * */
  public hide() 
  {
    if (!!this._overlayRef) 
    {
      this._overlayRef.detach();
      this._overlayRef.detachBackdrop();
    }
  }
}