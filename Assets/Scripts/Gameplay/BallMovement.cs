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

    private ScoreManager scoreManager; // Referencja do menadźera punktów

    /// <summary>
    /// Metoda <c>Start</c> jest wywoływana przy aktywacji obiektu w grze.
    /// Służy do inicjalizacji komponentów oraz rozpoczęcia ruchu piłki.
    /// </summary>
    private void Start()
    {
        // Pobranie referencji do komponentu Rigidbody2D przypisanego do tego obiektu.
        rb = GetComponent<Rigidbody2D>();

        scoreManager = Object.FindAnyObjectByType<ScoreManager>(); // Znalezienie ScoreManagera w scenie

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

    /// <summary>
    /// Metoda <c>OnCollisionEnter2D</c> jest wywoływana w momencie zderzenia piłki z innymi obiektami dwuwymiarowymi w grze.
    /// Odpowiada za obsługę kolizji z obiektami oznaczonymi tagami "Wall" oraz "Paddle".
    /// Dla zderzenia z "Wall" następuje odbicie w osi pionowej,
    /// natomiast dla zderzenia z "Paddle" wyliczany jest nowy kierunek w zależności od miejsca kontaktu.
    /// </summary>
    /// <param name="collision">Obiekt typu <c>Collision2D</c> zawierający informacje na temat zdarzenia kolizji.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Sprawdzenie, czy obiekt, z którym nastąpiło zderzenie, posiada tag "Wall".
        // W tym przypadku następuje odbicie piłki w osi pionowej.
        if (collision.gameObject.CompareTag("Wall"))
        {
            Vector2 newVelocity = new Vector2(rb.linearVelocity.x, -rb.linearVelocity.y);
            rb.linearVelocity = newVelocity;
        }

        // Sprawdzenie, czy obiekt, z którym nastąpiło zderzenie, posiada tag "Paddle".
        // W tym przypadku obliczany jest nowy kierunek piłki w oparciu o punkt kontaktu.
        if (collision.gameObject.CompareTag("Paddle"))
        {
            // Współczynnik określający, jak daleko od środka paletki piłka trafiła.
            float hitFactor = (transform.position.y - collision.transform.position.y) / collision.collider.bounds.size.y;

            // Utworzenie nowego kierunku, w którym piłka zostanie odbita.
            // Oś X jest negowana, aby zmienić stronę, a oś Y zależy od hitFactor.
            Vector2 newDirection = new Vector2(-rb.linearVelocity.x, hitFactor).normalized;

            // Ustawienie nowej prędkości piłki, biorąc pod uwagę znormalizowany kierunek.
            rb.linearVelocity = newDirection * speed;
        }
    }

    /// <summary>
    /// Metoda Update() sprawdza, czy piłka przekroczyła granice pola gry.
    /// - Jeśli piłka wyjdzie poza lewą krawędź (-6.7f), punkt otrzymuje prawy gracz.
    /// - Jeśli piłka wyjdzie poza prawą krawędź (6.7f), punkt otrzymuje lewy gracz.
    /// - Po przyznaniu punktu piłka jest resetowana za pomocą metody ResetBall().
    /// </summary>
    private void Update()
    {
        if (transform.position.x < -6.7f) // Przekroczenie lewej granicy
        {
            scoreManager.AddPoint(false); // Punkt dla prawego gracza
            ResetBall(); // Reset piłki

        }
        else if(transform.position.x > 6.7f) // Przekroczenie prawej granicy
        {
            scoreManager.AddPoint(true); // Punkt dla lewego gracza
            ResetBall(); // Reset piłki

        }
    }

    /// <summary>
    /// Metoda ResetBall() resetuje pozycję piłki po zdobyciu punktu.
    /// - Przenosi piłkę na środek ekranu.
    /// - Zatrzymuje jej ruch, ustawiając prędkość na zero.
    /// - Po krótkim opóźnieniu ponownie uruchamia piłkę.
    /// </summary>
    private void ResetBall()
    {
        transform.position = Vector2.zero; // Ustawia pozycję piłki na środek ekranu (0,0).
        rb.linearVelocity = Vector2.zero; // Zatrzymuje ruch piłki, ustawiając jej prędkość na zero.
        Invoke(nameof(LaunchBall), 1.5f); // Po 1.5 sekundy ponownie uruchamia piłkę.
    }
}
