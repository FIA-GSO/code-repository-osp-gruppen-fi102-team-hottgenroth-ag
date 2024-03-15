import csv
import sqlite3

# Pfad zur SQLite-Datenbankdatei
db_path = 'logisticsDB.sqlite'

# Verbindung zur SQLite-Datenbank herstellen
conn = sqlite3.connect(db_path)
cursor = conn.cursor()

# Inhalt von Article und Transportbox löschen
cursor.execute('DROP TABLE IF EXISTS Article')
cursor.execute('DROP TABLE IF EXISTS Transportbox')

# Article und Transportbox neu erstellen
cursor.execute('''
CREATE TABLE Transportbox (
    BoxGUID TEXT PRIMARY KEY,
    Number INTEGER,
    Description TEXT,
    ProjectGUID TEXT, 
    LocationTransport TEXT,
    LocationHome TEXT,
    LocationDeployment TEXT,
    FOREIGN KEY (ProjectGUID) REFERENCES Project(ProjectGUID)
)''')

cursor.execute('''
CREATE TABLE Article (
    ArticleGUID TEXT PRIMARY KEY,
    ArticleName TEXT,
    Description TEXT,
    GTIN INTEGER,
    Unit TEXT
)''')

# Funktion zum Importieren von Daten aus der CSV-Datei in die Transportbox-Tabelle
def import_transportbox(csv_file_path):
    with open(csv_file_path, 'r', encoding='utf-8-sig') as csv_file:
        csv_reader = csv.DictReader(csv_file, delimiter=';')
        for row in csv_reader:
            try:
                cursor.execute('''
                INSERT INTO Transportbox (BoxGUID, Number, Description, Category) 
                VALUES (?, ?, ?, ?)
                ''', (
                    row['BoxGUID'],
                    int(row['Number']),
                    row['Description'],
                    row['Category']
                ))
            except Exception as e:
                print(f"Ein Fehler ist aufgetreten: {e} in Zeile: {row}")

# Funktion zum Importieren von Daten aus der CSV-Datei in die Article-Tabelle
def import_article(csv_file_path):
    with open(csv_file_path, 'r', encoding='utf-8-sig') as csv_file:
        csv_reader = csv.DictReader(csv_file, delimiter=';')
        for row in csv_reader:
            try:
                cursor.execute('''
                INSERT INTO Article (BoxGUID, ArticleGUID, Position, Description, GTIN, Quantity, Unit, ExpiryDate) 
                VALUES (?, ?, ?, ?, ?, ?, ?, ?)
                ''', (
                    row['BoxGUID'],
                    row['ArticleGUID'],
                    float(row['Position']),
                    row['Description'],
                    int(row['GTIN']) if row['GTIN'] else None,
                    int(row['Quantity']),
                    row['Unit'],
                    row['ExpiryDate'] if row['ExpiryDate'] else None
                ))
            except Exception as e:
                print(f"Ein Fehler ist aufgetreten: {e} in Zeile: {row}")

# Importiere Daten in die Transportbox-Tabelle
import_transportbox('Boxen.csv')

# Importiere Daten in die Article-Tabelle
import_article('Article.csv')

# Änderungen speichern und Verbindung schließen
conn.commit()
conn.close()

print("Daten wurden erfolgreich in die Datenbank importiert.")
