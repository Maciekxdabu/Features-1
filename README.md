# Features-1

  Feature 1: Losowe postacie
Aktualnie projekt w Unity (2020.1.0f1) pokazuje przykład działającego systemu randomizacji wyglądu postaci z gotowych elementów (w domyśle: włosy, głowa i tułów)
Jedyne co to wystarczy wrzucić do pliku "ClientParts" odpowiednie grafiki (z podziałem na męskie i żeńskie) i ewentualnie poprawić pozycję obiektów ("Hair", "Head", "Body")

Kod działa na obiekcie "Client" posiadającym  obiekty-dzieci ze SpriteRenderer'ami, w przypadku użycia tego na innym obiekcie wystarczy dać skrypt na obiekt główny, dać trzy pod-obiekty ze Sprite Renderer'ami i podpiąć je do skryptu razem z plikiem "ClientParts" (należy pamiętać aby usunąć Sprite Renderer z obiektu głównego jeżeli takowy posiadał)

PS. Po uruchomieniu można klikać lewym przyciskiem myszy żeby zobaczyć jak grafiki się losowo zmieniają (wystarczy usunąć funkcję "Update" ze skryptu "RandomClient.cs" żeby się tego pozbyć)
