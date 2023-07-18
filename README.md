Autor: TeamDB

Opis:

Potrebno je napraviti WEB aplikaciju koja služi za iznajmljivanje vozila.

- Aplikacija treba imati funkcionalnosti:
    - pregled klijenata
    - pregled vozila
    - unosa rezervacije
    - pregled upisanih rezervacija
    - brisanje rezervacija (opcionalno)
  
Specifikacija podataka i pravila:

Entiteti za rad sustava:

    - Klijent treba minimalno sadržavati podatke – ime, prezime, godina rođenja, spol, država
    - Vozilo treba minimalno sadržavati podatke - proizvođač(npr. Kia, Porsche...), tip (npr. suv,
        karavan, sedan...), boja, godina
    - Rezervacija mora sadržavati informacije o vremenu iznajmljivanja (od, do), klijentu i vozilu.
    
Pravila:

    - Nije moguće izraditi rezervaciju za isto vozilo u istom vremena izmajmljivanja
    - Klijent ne može istovremeno iznajmiti više od tri vozila
    - Klijent ne može istovremeno iznajmiti više od jednog vozila po tipu
    - Pregled klijenata treba imati mogućnost pretrage po barem dva parametra (npr. ime,
        prezime)
    - Pregled vozila treba imati mogućnost pretrage po barem dva parametra (npr. tip, model)
    - Pregled rezervacija treba imati mogućnost pretrage po barem dva parametra (npr. datum,
        klijent)
        
Zadatak 1

Napraviti bazu u MSSQL sa potrebnim tablicama (entitetima) koja podržavaju definirani podatkovni
skup.

Zadatak 2

Napraviti WEB aplikaciju prema specifikaciji. Za backend koristiti C#, za frontend proizvoljno odabrati
tehnologiju. Za komunikaciju sa bazom podataka koristiti SQL storane procedure.
