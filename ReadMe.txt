---Backend--- C#
Visual Studio installieren
In Logistics.API die Logistics.API.sln öffnen
Projekt einmal Rebuilden + Starten um .vs Ordner zu erzeugen und wieder schließen
Einmal Projekt Logistic.API unter Properties in die launchSettings.Json gehen
Dort bei "applicationUrl": "http://localhost:44365" localhost durch Server IP ersetzen
Jetzt unter Logistics.API\.vs in config und applicationhost.config die IP Adresse unter 
          <binding protocol="http" bindingInformation="*:44365:localhost" />
          <binding protocol="https" bindingInformation="*:44349:localhost" />
statt localhost eintragen
Jetzt sollte noch eine Firewall regel gemacht werden die Ports 44365 und 44349 freigeben oder man schaltet die Firewall aus
Nun sollte man die Logistics.API starten

---Frontend--- Angular
Visual Studio Code installieren
NodeJS installieren
Visual Studio Code öffnen
Ordner öffen logistics-app in Visual Studio Code
npm i -g  @angular/cli in VSCode Konsole eingeben um Angular Global zu Installieren
npm i in VSCode Konsole eingeben um alle Abhänigkeiten zu installieren
Server Ip Adresse in enviorment.ts eintragen in Zeile "const domainUrl :string = "https://localhost:44349";"
ng s um das Projekt zu starten

