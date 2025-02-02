using UnityEngine; // Importuje bibliotekê UnityEngine, która zawiera podstawowe klasy i funkcje niezbêdne dla silnika Unity.
using TMPro; // Importuje bibliotekê TextMeshPro, umo¿liwiaj¹c¹ zaawansowane renderowanie tekstu, co jest szczególnie przydatne przy tworzeniu interfejsu u¿ytkownika.

/// <summary>
/// Klasa ScoreManager zarz¹dza punktacj¹ graczy w grze.
/// Umo¿liwia aktualizacjê wyników dla lewego i prawego gracza oraz synchronizacjê tych wyników z interfejsem u¿ytkownika,
/// wykorzystuj¹c komponenty TextMeshProUGUI.
/// </summary>
public class ScoreManager : MonoBehaviour // Definiuje klasê ScoreManager, dziedzicz¹c¹ po MonoBehaviour, co pozwala jej byæ do³¹czon¹ jako komponent do obiektu w scenie Unity.
{
    // Publiczne zmienne umo¿liwiaj¹ce przypisanie referencji do elementów UI w Inspectorze Unity.

    public TextMeshProUGUI scoreLeftText; // Przechowuje referencjê do komponentu TextMeshProUGUI, który bêdzie wyœwietla³ wynik lewego gracza.
    public TextMeshProUGUI scoreRightText; // Przechowuje referencjê do komponentu TextMeshProUGUI, który bêdzie wyœwietla³ wynik prawego gracza.

    // Prywatne zmienne do przechowywania aktualnych wyników obu graczy.

    private int scoreLeft = 0; // Inicjuje wynik lewego gracza jako 0.
    private int scoreRight = 0; // Inicjuje wynik prawego gracza jako 0.

    /// <summary>
    /// Metoda AddPoint umo¿liwia dodanie punktu do wyniku wybranego gracza.
    /// W zale¿noœci od wartoœci parametru <paramref name="isLeft"/>, punkt zostaje dodany
    /// albo lewemu graczowi (gdy <paramref name="isLeft"/> ma wartoœæ true), albo prawego gracza (gdy false).
    /// Po zaktualizowaniu wyniku, metoda synchronizuje zmienion¹ wartoœæ z interfejsem u¿ytkownika.
    /// </summary>
    /// <param name="isLeft">
    /// Okreœla, czy punkt ma zostaæ dodany do wyniku lewego gracza (true) czy prawego gracza (false).
    /// </param>
    public void AddPoint(bool isLeft) // Definicja metody publicznej, która umo¿liwia dodawanie punktu poprzez wywo³anie z zewn¹trz.
    {
        // Rejestruje w konsoli informacjê, do którego gracza punkt zostaje dodany.
        Debug.Log("Dodawanie punktu: " + (isLeft ? "Lewy gracz" : "Prawy gracz"));

        if (isLeft) // Je¿eli parametr isLeft jest prawdziwy, operujemy na wyniku lewego gracza.
        {
            scoreLeft++; // Zwiêksza wynik lewego gracza o 1.
            // Logowanie aktualnej wartoœci wyniku lewego gracza.
            Debug.Log("Nowy wynik lewego gracza: " + scoreLeft);
            if (scoreLeftText != null) // Sprawdza, czy referencja do obiektu UI nie jest pusta, aby unikn¹æ b³êdów podczas próby aktualizacji tekstu.
                scoreLeftText.text = scoreLeft.ToString(); // Aktualizuje tekst wyœwietlany na ekranie, konwertuj¹c aktualny wynik lewego gracza na ci¹g znaków.
        }
        else // W przeciwnym przypadku, gdy isLeft ma wartoœæ false, operujemy na wyniku prawego gracza.
        {
            scoreRight++; // Zwiêksza wynik prawego gracza o 1.
            // Logowanie aktualnej wartoœci wyniku prawego gracza.
            Debug.Log("Nowy wynik prawego gracza: " + scoreRight);
            if (scoreRightText != null) // Sprawdza, czy referencja do obiektu UI nie jest pusta.
                scoreRightText.text = scoreRight.ToString(); // Aktualizuje tekst wyœwietlany dla wyniku prawego gracza, przekszta³caj¹c liczbê w string.
        }
    }
}
