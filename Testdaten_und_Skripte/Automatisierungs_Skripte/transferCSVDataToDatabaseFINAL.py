import csv
import sqlite3
import uuid

# Pfad zur SQLite-Datenbankdatei
db_path = 'logisticsDB.sqlite'

# Verbindung zur SQLite-Datenbank herstellen
conn = sqlite3.connect(db_path)
cursor = conn.cursor()

# # Inhalt von Article und Transportbox löschen
# cursor.execute('DROP TABLE IF EXISTS Article')
# cursor.execute('DROP TABLE IF EXISTS Transportbox')
# cursor.execute('DROP TABLE IF EXISTS ArticleBoxAssignment')

# # Article und Transportbox neu erstellen
# cursor.execute('''
# CREATE TABLE Transportbox (
#     BoxGUID TEXT PRIMARY KEY,
#     Number INTEGER,
#     Description TEXT,
#     ProjectGUID TEXT, 
#     LocationTransport TEXT,
#     LocationHome TEXT,
#     LocationDeployment TEXT,
#     BoxCategory TEXT,
#     FOREIGN KEY (ProjectGUID) REFERENCES Project(ProjectGUID)
# )''')

# cursor.execute('''
# CREATE TABLE Article (
#     ArticleGUID TEXT PRIMARY KEY,
#     ArticleName TEXT,
#     Description TEXT,
#     GTIN INTEGER,
#     Unit TEXT
# )''')

# cursor.execute('''
# CREATE TABLE ArticleBoxAssignment (
#     AssignmentGUID TEXT PRIMARY KEY,
#     ArticleGUID TEXT,
# 	BoxGUID TEXT,
# 	Position REAL,
# 	Status INTEGER,
# 	Quantity INTEGER,
# 	ExpiryDate DATE,
#     ENUM Status,
#     FOREIGN KEY (ArticleGUID) REFERENCES Article(ArticleGUID),
#     FOREIGN KEY (BoxGUID) REFERENCES Transportbox(BoxGUID),
# 	FOREIGN KEY (Status) REFERENCES Status(StatusID)
# )''')

# Funktion zum Importieren von Daten aus der CSV-Datei in die Transportbox-Tabelle
def import_transportbox(csv_file_path):
    with open(csv_file_path, 'r', encoding='utf-8-sig') as csv_file:
        csv_reader = csv.DictReader(csv_file, delimiter=';')
        for row in csv_reader:
            try:
                cursor.execute('''
                INSERT INTO Transportbox (BoxGUID, Number, Description, BoxCategory) 
                VALUES (?, ?, ?, ?)
                ''', (
                    row['BoxGUID'].lower(),
                    int(row['Number']),
                    row['Description'],
                    row['Category']
                ))
            except Exception as e:
                print(f"Ein Fehler ist aufgetreten: {e} in Zeile: {row}")

# Funktion zum Importieren von Daten aus der CSV-Datei in die Article- und ArticleBoxAssignment-Tabelle
def import_article(csv_file_path):
    with open(csv_file_path, 'r', encoding='utf-8-sig') as csv_file:
        csv_reader = csv.DictReader(csv_file, delimiter=';')
        for row in csv_reader:
            # Erzeuge eine neue AssignmentGUID für jeden Eintrag
            assignment_guid = str(uuid.uuid4())

            # Überprüfe, ob GTIN numerisch ist, und konvertiere oder verwende None
            try:
                gtin = int(row['GTIN']) if row['GTIN'].isdigit() else None
            except ValueError:
                gtin = None
            
            # Füge Daten in die Article-Tabelle ein
            cursor.execute('''
            INSERT INTO Article (ArticleGUID, ArticleName, Description, GTIN, Unit) 
            VALUES (?, ?, ?, ?, ?)
            ON CONFLICT(ArticleGUID) DO UPDATE SET
            ArticleName=excluded.ArticleName,
            Description=excluded.Description,
            GTIN=excluded.GTIN,
            Unit=excluded.Unit
            ''', (
                row['ArticleGUID'].lower(),
                row.get('ArticleName', None),
                row.get('Description', None),
                gtin,
                row.get('Unit', None)
            ))

            # Füge Daten in die ArticleBoxAssignment-Tabelle ein
            cursor.execute('''
            INSERT INTO ArticleBoxAssignment (AssignmentGUID, ArticleGUID, BoxGUID, Position, Status, Quantity, ExpiryDate) 
            VALUES (?, ?, ?, ?, ?, ?, ?)
            ''', (
                assignment_guid,
                row['ArticleGUID'].lower(),
                row.get('BoxGUID', None).lower(),
                float(row['Position']) if row['Position'] else None,
                None,  # ENUM Status, 
                int(row['Quantity']) if row['Quantity'] else None,
                row.get('ExpiryDate', None)
            ))

# Importiere Daten in die Transportbox-Tabelle
import_transportbox('Boxen.csv')

# Importiere Daten in die Article-Tabelle
import_article('Article.csv')

# Änderungen speichern und Verbindung schließen
conn.commit()
conn.close()

print("Daten wurden erfolgreich in die Datenbank importiert.")
