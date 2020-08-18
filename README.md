# Features-1

Wersja Unity: (2020.1.0f1)
Notka: Każdy feature jest zamknięty w swoim obiekcie i wyłączenie/włączenie tych obiektów umożliwia wybranie/testowanie konkretnego feature'a

  **Feature 1**: Losowe postacie
  
Aktualnie projekt w Unity pokazuje przykład działającego systemu randomizacji wyglądu postaci z gotowych elementów (w domyśle: włosy, głowa i tułów)
Jedyne co to wystarczy wrzucić do pliku "ClientParts" odpowiednie grafiki (z podziałem na męskie i żeńskie) i ewentualnie poprawić pozycję obiektów ("Hair", "Head", "Body")

Kod działa na obiekcie "Client" posiadającym  obiekty-dzieci ze SpriteRenderer'ami, w przypadku użycia tego na innym obiekcie wystarczy dać skrypt na obiekt główny, dać trzy pod-obiekty ze Sprite Renderer'ami i podpiąć je do skryptu razem z plikiem "ClientParts" (należy pamiętać aby usunąć Sprite Renderer z obiektu głównego jeżeli takowy posiadał)

PS. Po uruchomieniu można klikać lewym przyciskiem myszy żeby zobaczyć jak grafiki się losowo zmieniają (wystarczy usunąć funkcję "Update" ze skryptu "RandomClient.cs" żeby się tego pozbyć)

  **Feature 2**: Mini-Gra o nalewaniu napoju
  
Aktualnie minigra opiera się na chwytaniu butelek myszką (lewym klawiszem), a następnie używanie klawiszy A i D, żeby kręcić butelką.
Napój w kubku zmienia swój kolor w zależności od stosunku wódki i soku.
Cel jest wypisany na ekranie (w formie stosunku wódki do soku) oraz wynik w formie pieniędzy jakie dostało się za sok.
Aktualnie wylanie napoju poza kubek nie ma żadnych efektów ubocznych.

W butelkach można zmienić kolor płynu jaki się z nich wylewa (czyli zmienić na przykład typ soku)
Ważne: Musi być zawsze butelka z kolorem białym (jest ona brana za wódkę)

Większość wartości jest ustawiane w obiekcie "Controller":
-*Goal List* - lista możliwych celów od klienta (w formie stosunku wódki do soku)
-*Max Money*, *Min Money* - maksymalna i minimalna liczba pieniędzy do zdobycia
-*Max Diff* - Maksymalna różnica od celu (jeżeli jest przekroczona, to zawsze jest stawka minimalna, inaczej wynik jest liczony liniowo)

W obiekcie "Cup" można ustawić ile płynu jest warta jedna kropla (wartość: *Liquid Per Drop*)

W obiektach "Bottle" można zmienić:
-*Liquid Color* - kolor płynu z butelki
-*Liquid Speed* - prędkość z jaką krople spadają

PS. Wynik zawsze wynosi 0 jeżeli nalejemy mniej niż 50% kubka
