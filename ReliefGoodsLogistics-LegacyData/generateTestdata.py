import sqlite3
from faker import Faker
import random
import uuid

# Initialisierung des Faker-Generators
fake = Faker()

# Verbindung zur SQLite-Datenbank herstellen
conn = sqlite3.connect('test_database.db')
cursor = conn.cursor()

# Tabellen löschen, falls sie existieren
cursor.execute('DROP TABLE IF EXISTS ArticleBoxAssignment')
cursor.execute('DROP TABLE IF EXISTS Article')
cursor.execute('DROP TABLE IF EXISTS Transportbox')
cursor.execute('DROP TABLE IF EXISTS Project')
cursor.execute('DROP TABLE IF EXISTS Status')

# CREATE TABLE Anweisungen für die Tabellenstruktur
cursor.execute('''
CREATE TABLE Status (
    StatusID INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL CHECK (Name IN ('Defect', 'Lost', 'Discarded', 'Consumed', 'Donated', 'Received'))
)''')

cursor.execute('''
CREATE TABLE Project (
    ProjectGUID TEXT PRIMARY KEY,
    ProjectName TEXT,
    CreationDate DATE
)''')

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

cursor.execute('''
CREATE TABLE ArticleBoxAssignment (
    AssignmentGUID TEXT PRIMARY KEY,
    ArticleGUID TEXT,
    BoxGUID TEXT,
    Position REAL,
    Status INTEGER,
    Quantity INTEGER,
    ExpiryDate DATE,
    FOREIGN KEY (ArticleGUID) REFERENCES Article(ArticleGUID),
    FOREIGN KEY (BoxGUID) REFERENCES Transportbox(BoxGUID),
    FOREIGN KEY (Status) REFERENCES Status(StatusID)
)''')

# Funktion zum Generieren von Testdaten für Article
def generate_article_data(n):
    articles = []
    for _ in range(n):
        articles.append((
            str(uuid.uuid4()),
            fake.catch_phrase(),
            fake.paragraph(),
            fake.random_int(min=100000000000, max=999999999999),  # GTIN-12 ohne führende 0
            fake.word()
        ))
    return articles

# Funktion zum Generieren von Testdaten für Transportbox
def generate_transportbox_data(n, project_guid):
    boxes = []
    for i in range(n):
        boxes.append((
            str(uuid.uuid4()),
            i+1,  # Box Number
            fake.sentence(),
            project_guid,  # Annahme: Es gibt ein Projekt mit diesem GUID
            fake.city(),
            fake.city(),
            fake.city()
        ))
    return boxes

# Funktion zum Generieren von Testdaten für ArticleBoxAssignment
def generate_assignment_data(articles, boxes):
    assignments = []
    for article in articles:
        box = random.choice(boxes)
        assignments.append((
            str(uuid.uuid4()),
            article[0],
            box[0],
            round(random.uniform(1, 10), 2),  # Position mit maximal zwei Dezimalstellen
            random.randint(1, 6),  # Zufälliger Status
            fake.random_int(min=1, max=100),  # Quantity
            fake.date_between(start_date='-1y', end_date='today').isoformat()  # ExpiryDate
        ))
    return assignments

# Projekt-Daten generieren und einfügen (Annahme: Es gibt nur ein Projekt)
project_guid = str(uuid.uuid4())
cursor.execute('INSERT INTO Project (ProjectGUID, ProjectName, CreationDate) VALUES (?, ?, ?)',
               (project_guid, fake.company(), fake.date_between(start_date='-2y', end_date='today').isoformat()))

# Status-Daten generieren und einfügen
status_names = ['Defect', 'Lost', 'Discarded', 'Consumed', 'Donated', 'Received']
for status_name in status_names:
    cursor.execute('INSERT INTO Status (Name) VALUES (?)', (status_name,))

# Testdaten generieren
articles = generate_article_data(100)  # 100 Artikel
boxes = generate_transportbox_data(10, project_guid)  # 10 Boxen, alle zum gleichen Projekt
assignments = generate_assignment_data(articles, boxes)

# Daten in die Datenbank einfügen
cursor.executemany('INSERT INTO Article VALUES (?, ?, ?, ?, ?)', articles)
cursor.executemany('INSERT INTO Transportbox VALUES (?, ?, ?, ?, ?, ?, ?)', boxes)
cursor.executemany('INSERT INTO ArticleBoxAssignment VALUES (?, ?, ?, ?, ?, ?, ?)', assignments)

# Änderungen commiten und Verbindung schließen
conn.commit()
conn.close()
