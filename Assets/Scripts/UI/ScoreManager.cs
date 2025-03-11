using UnityEngine; // Importuje bibliotekê Unity, umo¿liwiaj¹c¹ korzystanie z funkcji silnika Unity
using TMPro; // Importuje bibliotekê TextMeshPro, pozwalaj¹c¹ na zaawansowane renderowanie tekstu w interfejsie

public class ScoreManager : MonoBehaviour // Klasa odpowiedzialna za zarz¹dzanie punktacj¹ graczy
{
    public TextMeshProUGUI scoreLeftText; // Referencja do komponentu TextMeshProUGUI wyœwietlaj¹cego wynik lewego gracza
    public TextMeshProUGUI scoreRightText; // Referencja do komponentu TextMeshProUGUI wyœwietlaj¹cego wynik prawego gracza

    private int scoreLeft = 0; // Przechowuje aktualny wynik lewego gracza
    private int scoreRight = 0; // Przechowuje aktualny wynik prawego gracza

    [SerializeField] private int maxScore = 10; // Maksymalna liczba punktów, po której gra siê koñczy

    public void AddPoint(bool isLeft) // Metoda dodaj¹ca punkt do wyniku wskazanego gracza
    {
        Debug.Log("Dodawanie punktu: " + (isLeft ? "Lewy gracz" : "Prawy gracz")); // Wyœwietla w konsoli, do którego gracza dodano punkt

        if (isLeft) // Jeœli punkt ma byæ dodany lewemu graczowi
        {
            scoreLeft++; // Zwiêksza liczbê punktów lewego gracza o 1
            Debug.Log("Nowy wynik lewego gracza: " + scoreLeft); // Wyœwietla aktualny wynik lewego gracza w konsoli
            if (scoreLeftText != null) // Sprawdza, czy komponent tekstowy dla wyniku lewego gracza nie jest pusty
                scoreLeftText.text = scoreLeft.ToString(); // Aktualizuje tekst w UI, konwertuj¹c wartoœæ liczbow¹ na string
        }
        else // Jeœli punkt ma byæ dodany prawemu graczowi
        {
            scoreRight++; // Zwiêksza liczbê punktów prawego gracza o 1
            Debug.Log("Nowy wynik prawego gracza: " + scoreRight); // Wyœwietla aktualny wynik prawego gracza w konsoli
            if (scoreRightText != null) // Sprawdza, czy komponent tekstowy dla wyniku prawego gracza nie jest pusty
                scoreRightText.text = scoreRight.ToString(); // Aktualizuje tekst w UI, konwertuj¹c wartoœæ liczbow¹ na string
        }
        CheckGameEnd(); // Wywo³uje metodê sprawdzaj¹c¹, czy gra siê zakoñczy³a
    }

    private void CheckGameEnd() // Metoda sprawdzaj¹ca, czy któryœ gracz osi¹gn¹³ maksymaln¹ liczbê punktów
    {
        if (scoreLeft >= maxScore || scoreRight >= maxScore) // Jeœli którykolwiek gracz osi¹gn¹³ maksymalny wynik
        {
            Debug.Log("Koniec gry! Zwyciêzca: " + (scoreLeft >= maxScore ? "Lewy gracz" : "Prawy gracz")); // Wyœwietla w konsoli zwyciêzcê
            Time.timeScale = 0; // Zatrzymuje czas gry, blokuj¹c dalsz¹ rozgrywkê
        }
    }
}