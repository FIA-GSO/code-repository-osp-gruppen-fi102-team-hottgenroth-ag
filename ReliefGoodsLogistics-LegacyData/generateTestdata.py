import sqlite3
from faker import Faker
import random

# Initialisierung des Faker-Generators
fake = Faker()

# Verbindung zur SQLite-Datenbank herstellen (wird erstellt, falls nicht vorhanden)
conn = sqlite3.connect('logisticsDB.sqlite')
cursor = conn.cursor()

# Funktion zum Generieren von Testdaten für Article
def generate_article_data(n):
    articles = []
    for _ in range(n):
        articles.append((fake.uuid4(), fake.word(), fake.text(), fake.random_number(digits=13), fake.word()))
    return articles

# Funktion zum Generieren von Testdaten für Transportbox
def generate_transportbox_data(n):
    boxes = []
    for _ in range(n):
        boxes.append((fake.uuid4(), fake.random_number(digits=5), fake.text(), None, fake.address(), fake.address(), fake.address()))
    return boxes

# Funktion zum Generieren von Testdaten für ArticleBoxAssignment
def generate_assignment_data(articles, boxes, n):
    assignments = []
    for _ in range(n):
        article_id = random.choice(articles)[0]
        box_id = random.choice(boxes)[0]
        assignments.append((fake.uuid4(), article_id, box_id, random.random() * 100, random.randint(1, 6), random.randint(1, 100), fake.date()))
    return assignments

# Testdaten generieren
articles = generate_article_data(100)
boxes = generate_transportbox_data(100)
assignments = generate_assignment_data(articles, boxes, 100)

# Daten in die Datenbank einfügen
cursor.executemany('INSERT INTO Article VALUES (?, ?, ?, ?, ?)', articles)
cursor.executemany('INSERT INTO Transportbox VALUES (?, ?, ?, ?, ?, ?, ?)', boxes)
cursor.executemany('INSERT INTO ArticleBoxAssignment VALUES (?, ?, ?, ?, ?, ?, ?)', assignments)

# Änderungen commiten und Verbindung schließen
conn.commit()
conn.close()
