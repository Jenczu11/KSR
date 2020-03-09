# KSR

# Lista kroków
* Załadować artykuły
* Załadować stopListe
    * Lista może być przygotowana od góry możemy przygotować kilka list około 900 słów
* Przefiltorwanie danych 
    * Podział na kategorię 
        * Places
        * Topics
        * People
        * Orgs
        * Exchanges
        
        Oznacza to pogrupowanie ich w odpowiednie słowniki [?] czyli np. places podzielić miejscami , USA, UK, JAPAN , CANADA itd,

* Wybieramy ile artykułów (może procent) będzie przeznaczonych na dane treningowe,
* Powinniśmy móc wybrać ilość słów kluczowych oraz metode ich selekcji np.:
    * TF - term frequency
        [PL - wiki](https://pl.wikipedia.org/wiki/Term_frequency)
    * ETF - extended term frequency
    * IDF - inverted term frequency
    * TFIDF
    * DocumentFrequency
        
        [Tutaj ogólnie](https://en.wikipedia.org/wiki/Tf%E2%80%93idf)
    
    To powinno nam dać listę słów kluczowych dla odpowiedniego tagu

* Zbiór cech do klasyfikacji
    * Binarna (Jezeli słowa są równe to 1 inaczej 0)
    * Częstotliwość wystąpienia słów kluczowych
    * Levenshtein'a
    * N-Gramm
    * N-Gramm Niewiadomskiego
    * Procent słów kluczowych
    > To wszystko można x2 dodająć np.: w 30% pierwszych procentach tekstu itp.
    
    Wybrane przez nas
    * Binarna - identyczne z szablonem
    * Ilość słów kluczowych w 20% tekstu
    * Ilość słow kluczowych w całym tekście
    * Ilość słów kluczowych w 1 paragrafie
    * Wartość miary binarnej w tekście
    * Dystans Levenshteina dla tekstu
    * Dystans N-gram dla tekstu
    * Miara N-gram Niewiadomskiego dla tekstu
    * Miara Jaccarda dal tekstu
    * Miara LCS dla tekstu

* Metryki do wyboru
    * Eukildesa
    * Uliczna
    * Czebyszewa
    * wybrana inna
    * wybrana inna

* Ponadto
    * Parametr K (bo kNN)
    * Ilość danych startowych (do zimnego startu)?

* Metryki potrzebne do badań ?
    * Tablica ALL-TP-FP np.:

Tag | All | True Postive (TP) | False Postive (FP)
--- | --- | --- | ---
usa | 164 | 145 | 1
uk | 2 | 1 | 9
japan | 0 | 0 | 10

* Precision and Recall

Tag | Precision | Recall
--- | --- | --- 
usa | 99.32% | 99.32% 
uk | 99.32% | 99.32% 
japan | 99.32% | 99.32% 

* Confusion Matrix

t/t | usa | uk | japan
--- | --- | --- | ---
usa | 145 | 9 | 10 
uk | 1 | 1 | 0 
japan | 0 | 0 | 0 
