using UnityEngine;

/// <summary>
/// Klasa <c>BallMovement</c> odpowiada za inicjalizację i obsługę ruchu obiektu (piłki) przy użyciu komponentu <c>Rigidbody2D</c>.
/// Po starcie gry obiekt otrzymuje losowy kierunek ruchu, przy czym prędkość jest stała, niezależnie od wybranej trajektorii.
/// </summary>
public class BallMovement : MonoBehaviour
{
    /// <summary>
    /// Prędkość, z jaką porusza się piłka. 
    /// Wartość ta jest modyfikowalna w Inspectorze dzięki atrybutowi <c>[SerializeField]</c>.
    /// </summary>
    [SerializeField, Tooltip("Prędkość piłki (jednostki/s)")]
    private float speed = 5f;

    /// <summary>
    /// Referencja do komponentu <c>Rigidbody2D</c> przypisanego do obiektu,
    /// umożliwiająca sterowanie fizyką i symulację ruchu.
    /// </summary>
    private Rigidbody2D rb;

    /// <summary>
    /// Metoda <c>Start</c> jest wywoływana przy aktywacji obiektu w grze.
    /// Służy do inicjalizacji komponentów oraz rozpoczęcia ruchu piłki.
    /// </summary>
    private void Start()
    {
        // Pobranie referencji do komponentu Rigidbody2D przypisanego do tego obiektu.
        rb = GetComponent<Rigidbody2D>();

        // Uruchomienie metody inicjalizującej losowy ruch piłki.
        LaunchBall();
    }

    /// <summary>
    /// Metoda <c>LaunchBall</c> inicjalizuje ruch piłki, nadając jej losowy kierunek i ustalając prędkość.
    /// Losowany jest kierunek poziomy (o wartości -1 lub 1), co gwarantuje, że piłka poruszy się w lewo lub w prawo,
    /// oraz losowany jest kierunek pionowy z przedziału [-1, 1]. Wektor kierunku jest następnie normalizowany,
    /// aby zapewnić jednolitą prędkość niezależnie od kąta ruchu.
    /// </summary>
    private void LaunchBall()
    {
        // Losowanie kierunku w osi X: losujemy wartość 0 lub 1, a następnie mapujemy ją na -1 lub 1,
        // co determinuje ruch w lewo lub w prawo.
        float xDirection = Random.Range(0, 2) == 0 ? -1f : 1f;

        // Losowanie kierunku w osi Y, z przedziału od -1 do 1.
        float yDirection = Random.Range(-1f, 1f);

        // Utworzenie wektora kierunku z wylosowanymi wartościami dla osi X i Y.
        // Metoda <c>normalized</c> zapewnia, że długość wektora wynosi 1, co pozwala na utrzymanie stałej prędkości.
        Vector2 direction = new Vector2(xDirection, yDirection).normalized;

        // Ustawienie prędkości liniowej komponentu Rigidbody2D,
        // jako iloczyn znormalizowanego wektora kierunku i zdefiniowanej prędkości.
        rb.linearVelocity = direction * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {

        }

    }
}



//Oto kilka propozycji ulepszeń:
//2️ Zwiększanie prędkości piłki z czasem – Możemy dodać mechanizm przyspieszania.
//3️ Resetowanie piłki po zdobyciu punktu – Jeśli piłka wypadnie poza ekran, resetujemy jej pozycję.