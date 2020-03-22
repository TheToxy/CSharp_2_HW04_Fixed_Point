Ahoj,

mam zadani pro Tvoje lidi, a potrebuju to do tydne:

* Naimplementovat tridu/strukturu (rozhodnuti soucasti reseni) pro implementaci fixed point aritmetiky nad 32-bitovym integer cisly (nebudeme chtit jinou nez 32-bitovou presnost, at se ani o jinou nesnazi!). Tento typ Fixed<T> je genericky, kde pomoci T (aby bylo vse typesafe) chceme rozhodnout o pozici desetinne carky (dvojkove vlastne) - tj. pro 3 zajimave varianty maji pripravit 3 typy Q8_24, Q16_16, Q24_8, ktere za T muzu dosadit a tim urcim presnost rozdeleni cela vs. realna cast - prvni cislo je pocet bitu pred desetinnou carkou, druhe cislo je pocet bitu za desetinou carkou - tj. napr. Q8_24 je 8 pred (cela cast) a 24 za ("zlomkova cast").

* Na typu Fixed<T> musi jit:
1) zavolat konstruktor s intem (zkontruuje se Fixed reprezentujici dane cele cislo!)
2) zavolat metody .Add, .Subtract, .Multiply, .Divide, ktere berou jako argument Fixed o stejne presnosti (pouze!) a vraci vysledek (nemodifikuji zadnou zdrojovou hodnotu).
3) moznost pouzit instanci Fixed v Console.WriteLine, kde se hodnota reprezentovana instanci Fixed zobrazi jako hodnota double.

Vsechny implementace at se snazi delat co nejjednodussi. Infomace (tutorialy) o fixed point aritmetice a implementaci nasobeni a deleni, jsou zde (odkazy jim dat):
https://www.allaboutcircuits.com/technical-articles/fixed-point-representation-the-q-format-and-addition-examples/

https://www.allaboutcircuits.com/technical-articles/multiplication-examples-using-the-fixed-point-representation/

https://sestevenson.wordpress.com/fixed-point-division-of-two-q15-numbers/

* Maji udelat solution managovatne gitem!, ktere bude obsahovat 3 projekty:
1) EXE assembly, ktera ma program FixedPointApiTest.cs (tenhle soubor jim dat k dispozici). Tento soubor nesmi menit - jejich kod vuci nemu musi fungovat beze zmen.
2) DLL assembly Cuni.Arithmetics.FixedPoint, kde naimplementuji typ Fixed a vse s nim souvisejici (typy Q*, apod.).
3) Projekt unit testu (pro MSTest, nebo nUnit, nebo xUnit), ktery bude zevrubne testovat Cuni.Arithmetics.FixedPoint (meli by dosahnout co nejvetsi code coverage - to by nemel byt problem, implementace je jednoducha + ale opravdu otestovat ruzne pripady, ktere mohou nastat).

Reseni budou odevzdavat do SISu (do Grupiku [Studijni mezivysledky]) - bude tam sloupecek na reseni (jeste neni). Prijmeme POUZE reseni, kde bude odevzdano:
Vyclonovany git repozitar celeho solution se 3 projekty (abychom videli, ze pouzivali git + aby tam nebyly zadne docasne soubory v bin, obj, apod.), ktery bude zabaleny do ZIPu (zadne RAR, zadne 7ZIP, apod.)!

Dekuju a mej se fajn,
Pavel