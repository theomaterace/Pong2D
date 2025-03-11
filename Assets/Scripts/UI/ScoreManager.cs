using UnityEngine; // Importuje bibliotek� Unity, umo�liwiaj�c� korzystanie z funkcji silnika Unity
using TMPro; // Importuje bibliotek� TextMeshPro, pozwalaj�c� na zaawansowane renderowanie tekstu w interfejsie

public class ScoreManager : MonoBehaviour // Klasa odpowiedzialna za zarz�dzanie punktacj� graczy
{
    public TextMeshProUGUI scoreLeftText; // Referencja do komponentu TextMeshProUGUI wy�wietlaj�cego wynik lewego gracza
    public TextMeshProUGUI scoreRightText; // Referencja do komponentu TextMeshProUGUI wy�wietlaj�cego wynik prawego gracza

    private int scoreLeft = 0; // Przechowuje aktualny wynik lewego gracza
    private int scoreRight = 0; // Przechowuje aktualny wynik prawego gracza

    [SerializeField] private int maxScore = 10; // Maksymalna liczba punkt�w, po kt�rej gra si� ko�czy

    public void AddPoint(bool isLeft) // Metoda dodaj�ca punkt do wyniku wskazanego gracza
    {
        Debug.Log("Dodawanie punktu: " + (isLeft ? "Lewy gracz" : "Prawy gracz")); // Wy�wietla w konsoli, do kt�rego gracza dodano punkt

        if (isLeft) // Je�li punkt ma by� dodany lewemu graczowi
        {
            scoreLeft++; // Zwi�ksza liczb� punkt�w lewego gracza o 1
            Debug.Log("Nowy wynik lewego gracza: " + scoreLeft); // Wy�wietla aktualny wynik lewego gracza w konsoli
            if (scoreLeftText != null) // Sprawdza, czy komponent tekstowy dla wyniku lewego gracza nie jest pusty
                scoreLeftText.text = scoreLeft.ToString(); // Aktualizuje tekst w UI, konwertuj�c warto�� liczbow� na string
        }
        else // Je�li punkt ma by� dodany prawemu graczowi
        {
            scoreRight++; // Zwi�ksza liczb� punkt�w prawego gracza o 1
            Debug.Log("Nowy wynik prawego gracza: " + scoreRight); // Wy�wietla aktualny wynik prawego gracza w konsoli
            if (scoreRightText != null) // Sprawdza, czy komponent tekstowy dla wyniku prawego gracza nie jest pusty
                scoreRightText.text = scoreRight.ToString(); // Aktualizuje tekst w UI, konwertuj�c warto�� liczbow� na string
        }
        CheckGameEnd(); // Wywo�uje metod� sprawdzaj�c�, czy gra si� zako�czy�a
    }

    private void CheckGameEnd() // Metoda sprawdzaj�ca, czy kt�ry� gracz osi�gn�� maksymaln� liczb� punkt�w
    {
        if (scoreLeft >= maxScore || scoreRight >= maxScore) // Je�li kt�rykolwiek gracz osi�gn�� maksymalny wynik
        {
            Debug.Log("Koniec gry! Zwyci�zca: " + (scoreLeft >= maxScore ? "Lewy gracz" : "Prawy gracz")); // Wy�wietla w konsoli zwyci�zc�
            Time.timeScale = 0; // Zatrzymuje czas gry, blokuj�c dalsz� rozgrywk�
        }
    }
}