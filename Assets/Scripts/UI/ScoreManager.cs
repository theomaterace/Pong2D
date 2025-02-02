using UnityEngine; // Importuje bibliotek� UnityEngine, kt�ra zawiera podstawowe klasy i funkcje niezb�dne dla silnika Unity.
using TMPro; // Importuje bibliotek� TextMeshPro, umo�liwiaj�c� zaawansowane renderowanie tekstu, co jest szczeg�lnie przydatne przy tworzeniu interfejsu u�ytkownika.

/// <summary>
/// Klasa ScoreManager zarz�dza punktacj� graczy w grze.
/// Umo�liwia aktualizacj� wynik�w dla lewego i prawego gracza oraz synchronizacj� tych wynik�w z interfejsem u�ytkownika,
/// wykorzystuj�c komponenty TextMeshProUGUI.
/// </summary>
public class ScoreManager : MonoBehaviour // Definiuje klas� ScoreManager, dziedzicz�c� po MonoBehaviour, co pozwala jej by� do��czon� jako komponent do obiektu w scenie Unity.
{
    // Publiczne zmienne umo�liwiaj�ce przypisanie referencji do element�w UI w Inspectorze Unity.

    public TextMeshProUGUI scoreLeftText; // Przechowuje referencj� do komponentu TextMeshProUGUI, kt�ry b�dzie wy�wietla� wynik lewego gracza.
    public TextMeshProUGUI scoreRightText; // Przechowuje referencj� do komponentu TextMeshProUGUI, kt�ry b�dzie wy�wietla� wynik prawego gracza.

    // Prywatne zmienne do przechowywania aktualnych wynik�w obu graczy.

    private int scoreLeft = 0; // Inicjuje wynik lewego gracza jako 0.
    private int scoreRight = 0; // Inicjuje wynik prawego gracza jako 0.

    /// <summary>
    /// Metoda AddPoint umo�liwia dodanie punktu do wyniku wybranego gracza.
    /// W zale�no�ci od warto�ci parametru <paramref name="isLeft"/>, punkt zostaje dodany
    /// albo lewemu graczowi (gdy <paramref name="isLeft"/> ma warto�� true), albo prawego gracza (gdy false).
    /// Po zaktualizowaniu wyniku, metoda synchronizuje zmienion� warto�� z interfejsem u�ytkownika.
    /// </summary>
    /// <param name="isLeft">
    /// Okre�la, czy punkt ma zosta� dodany do wyniku lewego gracza (true) czy prawego gracza (false).
    /// </param>
    public void AddPoint(bool isLeft) // Definicja metody publicznej, kt�ra umo�liwia dodawanie punktu poprzez wywo�anie z zewn�trz.
    {
        // Rejestruje w konsoli informacj�, do kt�rego gracza punkt zostaje dodany.
        Debug.Log("Dodawanie punktu: " + (isLeft ? "Lewy gracz" : "Prawy gracz"));

        if (isLeft) // Je�eli parametr isLeft jest prawdziwy, operujemy na wyniku lewego gracza.
        {
            scoreLeft++; // Zwi�ksza wynik lewego gracza o 1.
            // Logowanie aktualnej warto�ci wyniku lewego gracza.
            Debug.Log("Nowy wynik lewego gracza: " + scoreLeft);
            if (scoreLeftText != null) // Sprawdza, czy referencja do obiektu UI nie jest pusta, aby unikn�� b��d�w podczas pr�by aktualizacji tekstu.
                scoreLeftText.text = scoreLeft.ToString(); // Aktualizuje tekst wy�wietlany na ekranie, konwertuj�c aktualny wynik lewego gracza na ci�g znak�w.
        }
        else // W przeciwnym przypadku, gdy isLeft ma warto�� false, operujemy na wyniku prawego gracza.
        {
            scoreRight++; // Zwi�ksza wynik prawego gracza o 1.
            // Logowanie aktualnej warto�ci wyniku prawego gracza.
            Debug.Log("Nowy wynik prawego gracza: " + scoreRight);
            if (scoreRightText != null) // Sprawdza, czy referencja do obiektu UI nie jest pusta.
                scoreRightText.text = scoreRight.ToString(); // Aktualizuje tekst wy�wietlany dla wyniku prawego gracza, przekszta�caj�c liczb� w string.
        }
    }
}
