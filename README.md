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

A step by step series of examples that tell you have to get a development env running

Say what the step will be

```
Give the example
```

And repeat

```
until finished
```

End with an example of getting some data out of the system or using it for a little demo

## Running the tests

Explain how to run the automated tests for this system

### Break down into end to end tests

Explain what these tests test and why

```
Give an example
```

### And coding style tests

Explain what these tests test and why

```
Give an example
```

## Deployment

Add additional notes about how to deploy this on a live system

## Built With

* [Dropwizard](http://www.dropwizard.io/1.0.2/docs/) - The web framework used
* [Maven](https://maven.apache.org/) - Dependency Management
* [ROME](https://rometools.github.io/rome/) - Used to generate RSS Feeds

## Contributing

Please read [CONTRIBUTING.md](https://gist.github.com/PurpleBooth/b24679402957c63ec426) for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/your/project/tags). 

## Authors

* **Aleksandra Milivojevic sw66-2014** 

email: aleksandra.milivojevic.94@gmail.com

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* Hat tip to anyone who's code was used
* Inspiration
* etc
