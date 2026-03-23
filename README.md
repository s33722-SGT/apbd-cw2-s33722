INSTRUKCJA URUCHOMIENIA:

W pliku Program.cs jest napisana symulacja przedstawiająca sposób działania aplikacji (uwzględniając wywoływanie błędów). Wystarczy uruchomić ten plik i nic więcej nie trzeba ruszać, aby móc przetestować aplikację.


Klasy w projekcie podzieliłem na 4 kategorie:
1. Models (dane)
2. Repositories (zarządzanie danymi)
3. Services (logika biznesowa)
4. Views (wyświetlanie danych)

Dzięki temu logika od danych jest odseparowana.

W projekcie zadbałem o Kohezję poprzez precyzyjne przypisanie zadań do klas:
- Modele (Rental, Equipment, User): Odpowiadają wyłącznie za przechowywanie swojego stanu i za proste wyliczenia.
- RentalService: Mózg aplikacji - zna on zasady wypożyczeń takie jak limity itd. lecz nie zajmuje się składowaniem danych.
- ConsoleUI: Odpowiada wyłącznie za prezentację danych.

Zastosowałem niskie sprzężenie dzięki wykorzystaniu interfejsów w repozytoriach:
- RendalService nie zależy od klasy InMemoryEquipmentRepository, lecz od interfejsu IEquipmentRepository.
- Pozwala to na zmianę sposobu przechowywania danych bez zmiany ani jednej linijki kodu w serwisach