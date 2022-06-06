# LIRA
Lira to klon aplikacji Trello. W większości funkcji został uproszczony na
potrzeby zajęć. Jest to aplikacja do zarządzania zadaniami i przerzucaniu
ich pomiędzy kolumnami np. ToDo, In progress i Done.

![](docs/lira.jpg)

## Spis treści

1. [Opis funkcjonalności](#opis-funkcjonalności)
   1. [Strona główna](#strona-gwna)
   2. [Rejestracja](#rejestracja)
   3. [Logowanie](#logowanie)
   4. [Tablice](#boards)
   5. [Widok tablicy](#board)
2. [Modele](#models)
   1. [Board](#models-board)
   2. [Card](#models-card)
   3. [CardMove](#models-cardmove)
   4. [Panel](#models-panel)

## Opis funkcjonalności <a name="opis-funkcjonalności"></a>
### Strona główna <a name="strona-gwna"></a>
![](docs/home.png "")

Strona główna wita nas praktycznie niczym, oprócz jumbotronu z 
przyciskiem do rejestracji użytkownika

![](docs/najman.jpg "")

### Rejestracja <a name="rejestracja"></a>

Widok rejestracji to podstawowy formularz. Użytkownik musi wypełnić
swój e-mail i hasło w celu kontynuacji.

![](docs/register.png "")

Hasło musi być odpowiednio złożone.

![](docs/password.png "")

Użytkownik po rejestracji zostanie poproszony o potwierdzenie adresu
email.

![](docs/confirmation.png "")

### Logowanie <a name="logowanie"></a>

W widoku logowanie użytkownik jest proszony o podanie adresu email i
hasła. Również funkcjonalny jest przycisk "Remember me", który pozwala
na zachowanie sesji.

![](docs/login.png)

Po zalogowaniu w panelu nawigacyjnym staje się dostępna dostępny link
do tablic użytkownika.

![](docs/navbar.png)

### Tablice <a name="boards"></a>
Strona Boards prezentuje listę tablic użytkownika, wraz z przyciskami
służącymi do przejścia do danej tablicy, edycji czy usunięcia tablicy.

![](docs/boards.png)

Użytkownik może dodawać nowe tablice wykorzystując przycisk 
"Create New".

![](docs/create_board.png)

### Widok tablicy <a name="board"></a>

Tablica składa się z paneli, które definiuje użytkownik. Każdy panel
zawiera zadania, które stworzył użytkownik. Zadania można przenosić
pomiędzy panelami poprzez chwycenie i przeciągnięcie zadania do
odpowiedniego panelu.

![](docs/board.png)

Strona tablicy zawiera przycisk do tworzenia nowych paneli oraz każdy
panel jest zaopatrzony w 3 przyciski pozwalające na tworzenie, 
edytowanie i usuwanie zadań.

## Modele <a name="models"></a>

Pomijam modele do Logowania i Rejestracji ale one gdzieś tam są :)

### Board <a name="models-board"></a>
```csharp
public class Board
 {
     [Key]
     public Guid Id { get; set; }
     public string Name { get; set; }
     public Guid UserId { get; set; }
     public virtual IdentityUser User { get; set; }
 }
```
### Card <a name="models-card"></a>
```csharp
public class Card
 {
     [Key] public Guid Id { get; set; }
     public string Title { get; set; }
     public string Description { get; set; }
     public Guid PanelId { get; set; }
     public Panel Panel { get; set; }
 }
```
### CardMove <a name="models-cardmove"></a>
```csharp
public class CardMove
 {
     public Guid CardId { get; set; }
     public Guid PanelId { get; set; }
 }
```
### Panel <a name="models-panel"></a>
```csharp
public class Panel
 {
     [Key] public Guid Id { get; set; }
     public string Name { get; set; }
     public Guid BoardId { get; set; }
     public Board Board { get; set; }
 }
```