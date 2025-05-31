# CsvReaderApi

Ett REST API som läser in data från en CSV-fil och returnerar informationen i JSON-format.  
Byggt med ASP.NET Core 8.0 och designat enligt principer för separation av ansvar, robust felhantering och tydlig struktur.

---

## Hur man bygger applikationen

Förutsätter att du har [.NET 8 SDK](https://dotnet.microsoft.com/download) installerat.

git clone https://github.com/Potatismoose/CsvReaderApi.git  
cd CsvReaderApi  
dotnet build  

## Kör applikationen
dotnet run  

## Testa applikationen via swagger eller postman
https://localhost:5001/swagger  

### Endpoint
Hämta alla rader  
GET /api/data  


Möjlighet finns att lägga till en frivillig parameter för att hämta begränsat antal träffar  
GET /api/data?limit=10

## Exempel på JSON respons
[
  {
    "name": "Alice",
    "age": 28,
    "email": "alice@example.com"
  },
  {
    "name": "Bea",
    "age": 12,
    "email": "bea@example.com"
  }
]

## Filstruktur CSV filen
Semikolonseparerade rader enligt nedan format  
1;Alice;28;alice@example.com  
2;Bea;12;bea@example.com  
3;Ceasar;56;ceasar@example.com  

### Kolumnbeskrivning
Id - int  
Intern identifierare (visas ej i API)


Name - string  
Namn på personen


Age - int  
Ålder


Email - string  
E-postadress

## Felhantering
CSV-filen saknas - 500	{ "error": "CSV-filen saknas." }  
Ogiltig limit (t.ex. 0 eller negativt) 400 { "error": "Limit måste vara ett positivt heltal." }  
Filen är tom eller ogiltig 204 (Ingen responskropp)  

