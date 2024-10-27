# Instruktioner
Du ska bygga ett adminstrationssystem för ett parkeringshus. I parkeringshuset kan tre typer av fordon parkera:
- Bil
  - Tar en parkeringsplats
- Buss
  - Tar två parkeringsplatser
- Motorcykel
  - Tar en halv parkeringsplats

Det ska inte gå att parkera ett Fordon/Vehicle (bas-klassen)

De properties som varje fordon har är:
- Registreringsnummer
- Färg på fordonet

Unika egenskaper är:
- Bil: Elbil (bool)
- Motorcykel: Märke (string)
- Buss: Antal passagerare (int)

Antal parkeringsplatser: 15

# Regler
- Ett slumpmässigt fordon anländer till parkeringshuset. 
- Beroende på vilken typ av fordon ska användaren mata in uppgifterna (Regnr kan automatgenereras)
- Därefter ska en algoritm placera fordonet på lämplig plats
- En lista med alla fordon ritas ut, som kan se ut såhär:

| Plats | Typ  | Registreringsnummer | Färg  | Övrigt  |
|-------|------|---------------------|-------|---------|
| 1     | Bil  | ABC123              | Röd   | Elbil   |
| 2     | MC   | GHJ456              | Svart | Harley  |
| 2     | MC   | LKJ987              | Grön  | Yamaha  |
| 3-4   | Buss | LKJ223              | Gul   | 55      |
...

- Det ska gå att checka ut valfritt fordon genom att ange registreringsnummer:
  - Priset för parkeringen visas, som är baserat på priset 1.5 kr per minut.
  - Fordonet försvinner från parkeringshuset
- Målet är att se till att parkeringsplatserna är optimalt utnyttjade:
  - Se till att bilarna inte står med luckor så inte en buss får plats
  - Se till att motorcyklarna delar plats så de inte tar upp en hel parkeringsruta
 
# Krav
- Uppgiften ska göras på ett genomtänkt sätt som påvisar djupare förståelse för kodens uppbyggnad.
- För betyget VG ska applikationen uppvisa en smart hantering av fordonen och parkeringshuset.
- Gränssnittet ska vara enkelt att använda, och ha kontroll för felaktig inmatning.
- Koden ska uppvisa god ordning, bra struktur och vara uppdelad på ett relevant sätt.
- Variabler, properties, klasser och metoder ska vara korrekt angivna, med korrekt camel-case och substantiv för klasser och verb för metoder.
- Koden ska visa god förståelse för objektorienterade principer, med tanke på inkapsling, arv, astraktion och polymorfism.
- Koden ska vara “DRY”
- Uppgiften ska göras individuellt.
# Övrigt
- Uppgiften är inte extra svår, men ställer mer krav på hur koden ser ut och fungerar.
- Tiden är en faktor, då du samtidigt ska hinna göra G-uppgiften.
- Det är inte säkert att betyget blir VG bara för att du lämnar in uppgiften, den kommer granskas extra noggrant.
- Den ska utföras självständigt, utan större hjälp.
