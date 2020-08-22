# Features-1

Wersja Unity: (2019.2.4f1, ta sama wersja co główny projekt)  
Notka: Każdy feature jest zamknięty w swoim obiekcie i wyłączenie/włączenie tych obiektów umożliwia wybranie/testowanie konkretnego feature'a

## **Feature 1**: Losowe postacie
  
Aktualnie projekt w Unity pokazuje przykład działającego systemu randomizacji wyglądu postaci z gotowych elementów (w domyśle: włosy, głowa i tułów).  
Jedyne co to wystarczy wrzucić do pliku "ClientParts" odpowiednie grafiki (z podziałem na męskie i żeńskie) i ewentualnie poprawić pozycję obiektów ("Hair", "Head", "Body")

Kod działa na obiekcie "Client" posiadającym  obiekty-dzieci ze SpriteRenderer'ami, w przypadku użycia tego na innym obiekcie wystarczy dać skrypt na obiekt główny, dać trzy pod-obiekty ze Sprite Renderer'ami i podpiąć je do skryptu razem z plikiem "ClientParts" (należy pamiętać aby usunąć Sprite Renderer z obiektu głównego jeżeli takowy posiadał).

PS. Po uruchomieniu można klikać lewym przyciskiem myszy żeby zobaczyć jak grafiki się losowo zmieniają (wystarczy usunąć funkcję "Update" ze skryptu "RandomClient.cs" żeby się tego pozbyć).

## **Feature 2**: Mini-Gra o nalewaniu napoju
  
W grze steruje się padem i można wybrać w obiekcie "Controller" jakim padem się steruję (aktualnie jest do wyboru pad od Xbox i PS4)

Aktualnie minigra opiera się na chwytaniu butelek za pomocą LB, a następnie używanie triggerów, żeby kręcić butelką.  
Napój w kubku zmienia swój kolor w zależności od stosunku wódki i soku.  
Cel jest wypisany na ekranie (w formie stosunku wódki do soku) oraz wynik w formie pieniędzy jakie dostało się za sok.  
Wylewanie napoju z butelki zawsze powoduje jego utratę, przez co trzeba uważać, aby za bardzo nie przelać.

W stosach butelkach można zmienić kolor płynu jaki się w nich znajduje (czyli zmienić na przykład typ soku), następnie należy to podpiąć w obiekcie "Bottle" aby były możliwe do użycia.  
W stosach można dokupywać butelki za odpowiednią cenę, ale nie więcej niż podany limit.  
Ważne: Musi być zawsze stos butelek z kolorem białym (jest ona brana za wódkę).

Większość wartości jest ustawiane w obiekcie "Controller":
* *Goal List* - lista możliwych celów od klienta (w formie stosunku wódki do soku)
* *Max Money*, *Min Money* - maksymalna i minimalna liczba pieniędzy do zdobycia
* *Max Diff* - Maksymalna różnica od celu (jeżeli jest przekroczona, to zawsze jest stawka minimalna, inaczej wynik jest liczony liniowo)

W obiekcie "Cup" można ustawić ile płynu jest warta jedna kropla (wartość: *Liquid Per Drop*)

W obiektach "Bottle" można zmienić:
(Skrypt Pouring)
* *Liquid Per Drop* - ile płynu jest warta jedna kropla podczas wylewania z butelki
-*Bottle Stacks* - Stacki butelek które się wybiera podczas nalewania
(Skrypt Bottle Movement)
* *Rotation Speed* - prędkość obrotu butelki podczas trzymania odpowiedniego klawisza
* *Move Speed* - prędkość poruszania butelką po ekranie
* *Wanka Speed* - prędkość z jaką butelka próbuje wrócić do pozycji pionowej (aktualnie butelka "prostuje" się tym szybciej im bardziej jest odchylona)

W obiektach "Bottle Stacks" można zmienić:
* *Bottles Number* - aktualna ilość butelek
* *Max Bottles Number* - maksymalna ilość butelek w stosie
* *Fron Liquid Value* - aktualna ilość płynu w butelce z przodu
* różne wartości definiujące płyn: kolor, prędkość kropel, wygląd butelki, nazwa płynu (aktualnie nie jest ona nigdzie używana)
* *Price* - cena za nową butelkę

PS. Wynik zawsze wynosi 0 jeżeli nalejemy mniej niż 50% kubka  
PS. Aktualnie dodawanie nowego rodzaju soku niewiele daje, pod tym kontekstem, że wynik liczę ze stosunku wódki do aktualnego zapełnienia szklanki, tak więc rodzaje soku na razie ie mają znaczenia (sprawa jest do dyskusji)
