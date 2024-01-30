import csv
import sqlite3
import os

# Pfad zur SQLite-Datenbankdatei
db_path = 'logisticsDB.sqlite'  # Ersetzen Sie 'your_database.db' mit dem tatsächlichen Datenbanknamen

# Pfad zur CSV-Datei
csv_file_path = 'Article.csv'  # Stellen sicher, dass die CSV-Datei im selben Ordner wie dieses Skript ist

# Verbindung zur SQLite-Datenbank herstellen
conn = sqlite3.connect(db_path)
cursor = conn.cursor()

# CSV-Datei einlesen und in die Datenbank schreiben
with open(csv_file_path, 'r', encoding='utf-8') as csv_file:
    csv_reader = csv.DictReader(csv_file, delimiter=';')
    for row in csv_reader:
        cursor.execute('''
        INSERT INTO Article (BoxGUID, ArticleGUID, Position, Description, GTIN, Quantity, Unit, ExpiryDate) 
        VALUES (?, ?, ?, ?, ?, ?, ?, ?)
        ''', (
            row['BoxGUID'],
            row['ArticleGUID'],
            float(row['Position']) if row['Position'] else None,
            row['Description'] if row['Description'] else None,
            row['GTIN'] if row['GTIN'] else None,
            int(row['Quantity']) if row['Quantity'] else None,
            row['Unit'] if row['Unit'] else None,
            row['ExpiryDate'] if row['ExpiryDate'] else None
        ))

# Änderungen speichern und Verbindung schließen
conn.commit()
conn.close()

print("Daten wurden erfolgreich in die Datenbank importiert.")
