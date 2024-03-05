import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ColorMixerService {
  public colors!: Map<string, string>;
  constructor() {
  }

  private initializeColours() {
    this.colors = new Map<string, string>();
    this.colors.set("A", "#FFB6C1"); // Helles Rosa
    this.colors.set("B", "#FFA07A"); // Lachsfarben
    this.colors.set("C", "#FFD700"); // Gold
    this.colors.set("D", "#FFA500"); // Orange
    this.colors.set("E", "#FFDAB9"); // Pfirsich Puff
    this.colors.set("F", "#EEE8AA"); // Blasse Goldrute
    this.colors.set("G", "#98FB98"); // Blasses Grün
    this.colors.set("H", "#AFEEEE"); // Blasses Türkis
    this.colors.set("I", "#ADD8E6"); // Hellblau
    this.colors.set("J", "#DDA0DD"); // Pflaume
    this.colors.set("K", "#90EE90"); // Hellgrün
    this.colors.set("L", "#FFC0CB"); // Pink
    this.colors.set("M", "#87CEFA"); // Himmelblau
    this.colors.set("N", "#778899"); // Hellgrau
    this.colors.set("O", "#B0C4DE"); // Hellstahlblau
    this.colors.set("P", "#32CD32"); // Limonengrün
    this.colors.set("Q", "#FFD700"); // Gold
    this.colors.set("R", "#FFA07A"); // Lachsfarben
    this.colors.set("S", "#BA55D3"); // Mittleres Orchidee
    this.colors.set("T", "#9370DB"); // Mittleres Lila
    this.colors.set("U", "#3CB371"); // Mittleres Meergrün
    this.colors.set("V", "#7B68EE"); // Mittleres Schieferblau
    this.colors.set("W", "#6A5ACD"); // Schieferblau
    this.colors.set("X", "#48D1CC"); // Mittleres Türkis
    this.colors.set("Y", "#C71585"); // Mittleres Violettrot
    this.colors.set("Z", "#DB7093"); // Blasses Violettrot
    this.colors.set("Ü", "#FF6347"); // Tomatenfarben
    this.colors.set("Ö", "#40E0D0"); // Türkis
    this.colors.set("Ä", "#EE82EE"); // Violett
  }

  private hexToRgb(hex: string) : any {
    var result = /^#?([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})$/i.exec(hex);
    return result ? {
      r: parseInt(result[1], 16),
      g: parseInt(result[2], 16),
      b: parseInt(result[3], 16)
    } : null;
  }

  public setContrast(pColor: string) : string {
    var rgb = this.hexToRgb(pColor);

    var brightness = Math.round(((rgb.r * 299) +
                        (rgb.g * 587) +
                        (rgb.b * 114)) / 1000);
    return brightness > 150 ? '#333' : '#fff';
  }

  public getInitialsFromEmail(pEmail : string) : string {
    var retVal = "";

    if (pEmail.indexOf('@') > 0) {
      pEmail = pEmail.substring(0, pEmail.indexOf('@'));
    }

    pEmail = pEmail.toUpperCase();

    if (this.colors == undefined) {
      this.initializeColours();
    }

    for (var i = 0; i < pEmail.length; i++) {
      if (this.colors.has(pEmail[i])) {
        retVal += pEmail[i];
      }
      if (retVal.length == 2){
        return retVal;
      }
    }

    if (retVal.length <= 0){
      return pEmail[0];
    }

    return retVal;
  }

  public getColourByEmail(pEmail: string): string {
    var firstColor: string | undefined = undefined;
    var secondColor: string | undefined = undefined;
    if (pEmail.indexOf('@') > 0) {
      pEmail = pEmail.substring(0, pEmail.indexOf('@'));
    }

    pEmail = pEmail.toUpperCase();

    if (this.colors == undefined) {
      this.initializeColours();
    }

    for (var i = 0; i < pEmail.length; i++) {
      if (this.colors.has(pEmail[i])) {
        if (firstColor == undefined) {
          firstColor = this.colors.get(pEmail[i]);
        }
        else {
          secondColor = this.colors.get(pEmail[i]);
          break;
        }
      }
    }

    if (firstColor == undefined) {
      firstColor = this.colors.get("A");
    }
    if (!!secondColor && !!firstColor) {
      return this.mixColoursAsHex(firstColor, secondColor);
    }
    else {
      return !!firstColor ? firstColor : "";
    }
  }

  private mixColoursAsHex(pFirstColour: string, pSecondColour: string): string {
    var retVal: string = "";
    pFirstColour = pFirstColour.substring(1);
    pSecondColour = pSecondColour.substring(1);
    var percentage = 0.5;

    var firstColourAsArray: number[] = [parseInt(pFirstColour[0] + pFirstColour[1], 16), parseInt(pFirstColour[2] + pFirstColour[3], 16), parseInt(pFirstColour[4] + pFirstColour[5], 16)];
    var secondColourAsArray: number[] = [parseInt(pSecondColour[0] + pSecondColour[1], 16), parseInt(pSecondColour[2] + pSecondColour[3], 16), parseInt(pSecondColour[4] + pSecondColour[5], 16)];

    var mixedColourAsRGB = [
      (1 - percentage) * firstColourAsArray[0] + percentage * secondColourAsArray[0],
      (1 - percentage) * firstColourAsArray[1] + percentage * secondColourAsArray[1],
      (1 - percentage) * firstColourAsArray[2] + percentage * secondColourAsArray[2]
    ];

    retVal = this.convertRGBToHexValue(mixedColourAsRGB);
    return retVal;
  }

  public convertRGBToHexValue(pRGB: number[]): string {
    var retVal: string = "#";

    pRGB.forEach(value => {
      var parsedHex = Math.round(value).toString(16);
      if (parsedHex.length == 1) {
        retVal += '0' + parsedHex;
      }
      else {
        retVal += parsedHex;
      }
    });

    return retVal;
  }
}
