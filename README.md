# ComplexityPasswordEnforcer
ComplexityPasswordEnforcer

## Uvod

Windows OS pruža mogućnost korisniku da prilikom pravljenja naloga na lokalnoj mašini može (ali i ne mora) da postavi lozinku. Administrator sistema je taj koji postavlja politiku lozinki (end. Local Security Polocy) na datoj mašini i tako specificira zahteve koji se očekuju od novog korisnika po pitanju bezbednosti.  
Glavni razlog za uvođenje ovog vida zaštite je činjenica da ljudi imaju veoma predvidivo izabrane šifre koje čak mogu i da se potpuno uklapaju u (loše) zadate zahteve.  
Na primer, ako se od korisnika Pere Perića očekuje da njegova lozinka sadrži bar 3 od 4 grupe karaktera (velika i mala slova abecede, cifre i specijalni znakovi), validna lozinka bi bila Peraperic1999 što je napadačima veoma lako da otkriju jer se ponaša po jednoj od veoma frekventnih topologija lozinki: 
```
UL...LDDDD  
```
- U - velika slova abecede (eng. uppercase letter)  
- L - mala slova abecede (eng. lowercase letter)  
- D - cifra (eng. digit), pogotovo često ove cifre označavaju trenutnu godinu ili godinu rođenja   

Iz ovih razloga, potrebno je dodatno onemogućiti korisnicima da odaberu lozinke koje će se uklapati u neku od frekventnih topologija lozinki.

## Konfigurisanje politike bezbednosti na Windows OS

Sistem administratori su zaduženi za konfigurisanje politike bezbednosti na Windows OS. Međutim, svi korisnici mogu da podese neke osnovne politike za kreiranje ili ažuriranje lozinke za prijavljivanje na sistem.  
Control Panel -> Administrative tools -> Security settings -> Account policies -> Password policy  
Važne opcije za podešavanje su: minimalna dužina lozinke i omogućavanje kompleksnih zahteva.  
<img src="https://user-images.githubusercontent.com/17849956/27335697-ba969418-55cd-11e7-96c8-56012476ca31.png" width="563" height="400" />  


### Password must meet complexity

Ako se omogući ovo pravilo (izabrano Enabled), od korisnika se zahteva da lozinka:
- ne sadrži korisničko ime ili deo punog imena korisnika
- bude dugačka bar 6 karaktera
- sadrži bar 3 od 4 pomenute kategorije karaktera

## PasswordFilterRegEx

PasswordFilterRegEx je prvi deo projekta koji sadrži dll (Dynamic-link library) PasswordFilterRegEx.dll, koncept sistemske deljene biblioteke čiji je cilj da eksportovanjem i implementacijom tri funkcije napravi novi filter koji će novi i menjane lozinke kasnije koristiti.  
Prilikom pokretanja sistema LSA (eng. Local Security Authority) učitava sve filtere i poziva <code>InitializeChangeNotify</code> funkciju. Kada LSA dobije TRUE kao povratnu vrednost, filter je usešno učitan i LSA počinje da pravi lanac dostupnih filtera.  
LSA poziva funkciju <code> PasswordFilter</code> za svaki filter u lancu, i ukoliko neki od filtera vrati FALSE vrednost, ne nastavlja se sa pozivanjem ove funkcije za sledeći filter. U tom slučaju, korisnik treba da postavi novu lozinku. Ako svaki filter vrati TRUE vrednost, to znači da je lozinka prošla kroz sve filtere i svaki od njih je o ovome obavešten preko  <code>PasswordChangeNotify</code> funkcije.  

```
BOOLEAN __stdcall InitializeChangeNotify(void);	 
```
```
BOOLEAN __stdcall PasswordFilter(  
  PUNICODE_STRING AccountName,  
  PUNICODE_STRING FullName,  
  PUNICODE_STRING Password,  
  BOOLEAN SetOperation  
  );   
  ```
  ```
NTSTATUS __stdcall PasswordChangeNotify(  
  PUNICODE_STRING UserName,  
  ULONG RelativeId,  
  PUNICODE_STRING NewPassword  
  ); 
```
### Podešavanja za PasswordFilterRegEx

Instalacija filtera:
1. kopirati fajl PasswordFilterRegEx.dll (nalazi se na putanji ComplexityPasswordEnforcer/10489/x64/Release/) u folder %SystemRoot%\system32
2. Otvoriti Registry Editor (regedit.exe) i na putanji HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Lsa dodati u registar sa imenom NotificationPackages PasswordFilterRegEx (slika)
3. U Registry Editor (regedit.exe) na putanji HKEY_LOCAL_MACHINE\Software\DevX\PasswordFilter dodati ključ RegEx i u njega upisati željeni regularni izraz prema kom ć


<img src="https://user-images.githubusercontent.com/17849956/27342415-6a416b9c-55e0-11e7-8590-282276069ed9.png" width="362" height="300" /> 

## Built With

* [Dropwizard](http://www.dropwizard.io/1.0.2/docs/) - The web framework used
* [Maven](https://maven.apache.org/) - Dependency Management
* [ROME](https://rometools.github.io/rome/) - Used to generate RSS Feeds

 

## Authors

* **Aleksandra Milivojevic sw66-2014** 

email: aleksandra.milivojevic.94@gmail.com

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

