using UnityEngine; // Importujemy przestrzeñ nazw UnityEngine, która zawiera podstawowe klasy i funkcjonalnoœci silnika Unity.

/// <summary>
/// Klasa PaddleMovement zarz¹dza ruchem paletki w grze, implementuj¹c podstawowe mechanizmy fizyczne i wejœcia u¿ytkownika.
/// Dziedziczy po MonoBehaviour, co umo¿liwia jej do³¹czenie jako komponent do obiektów w scenie Unity.
/// </summary>
public class PaddleMovement : MonoBehaviour
{
    // Atrybut [SerializeField] umo¿liwia edycjê poni¿szych zmiennych w Inspectorze Unity mimo zachowania prywatnego dostêpu.
    
    [SerializeField] private float speed = 5f;         
    // Zmienna speed definiuje prêdkoœæ poruszania siê paletki, czyli jak szybko obiekt bêdzie reagowa³ na polecenia ruchu.

    [SerializeField] private bool isPlayerOne = true;    
    // Flaga isPlayerOne okreœla, czy dana paletka nale¿y do gracza numer 1. Jej wartoœæ wp³ywa na wybór odpowiedniego wejœcia z klawiatury.

    [SerializeField] private float minY = -4f;           
    // Zmienna minY definiuje doln¹ granicê ruchu paletki na osi Y, zapobiegaj¹c zejœciu obiektu poni¿ej okreœlonego poziomu.

    [SerializeField] private float maxY = 4f;            
    // Zmienna maxY okreœla górn¹ granicê ruchu paletki na osi Y, ograniczaj¹c mo¿liwoœæ poruszania siê obiektu powy¿ej ustalonego poziomu.

    // Prywatna referencja do komponentu Rigidbody2D, który odpowiada za symulacjê fizyki w dwuwymiarowej przestrzeni.
    private Rigidbody2D rb;

    // Zmienna przechowuj¹ca wartoœæ wejœcia ruchowego (np. z klawiatury) dla aktualnej klatki.
    private float movementInput;

    /// <summary>
    /// Metoda Awake() jest wywo³ywana natychmiast po za³adowaniu skryptu i s³u¿y do inicjalizacji komponentów.
    /// </summary>
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); 
        // Inicjalizujemy zmienn¹ rb poprzez pobranie komponentu Rigidbody2D z obiektu, do którego do³¹czony jest ten skrypt.
    }

    /// <summary>
    /// Metoda Update() jest wywo³ywana raz na ka¿d¹ klatkê renderowania i s³u¿y g³ównie do odczytu wejœcia u¿ytkownika.
    /// </summary>
    private void Update()
    {
        // Operator warunkowy (ternary) wybiera nazwê osi wejœcia w zale¿noœci od tego, czy paletka nale¿y do gracza 1.
        movementInput = Input.GetAxisRaw(isPlayerOne ? "Vertical" : "Vertical2");
        // Pobieramy surow¹ wartoœæ wejœcia z odpowiedniej osi:
        // - "Vertical" dla gracza 1,
        // - "Vertical2" dla innego gracza.
    }

    /// <summary>
    /// Metoda FixedUpdate() jest wywo³ywana w sta³ych odstêpach czasowych i jest idealna do operacji zwi¹zanych z fizyk¹.
    /// </summary>
    private void FixedUpdate()
    {
        // Obliczamy now¹ wartoœæ pozycji Y dla paletki:
        // - rb.position.y: aktualna pozycja Y,
        // - movementInput: odczytana wartoœæ wejœcia, która mo¿e byæ ujemna, dodatnia lub zerowa,
        // - speed: mno¿nik okreœlaj¹cy tempo ruchu,
        // - Time.fixedDeltaTime: sta³y interwa³ czasu miêdzy wywo³aniami FixedUpdate, zapewniaj¹cy spójnoœæ symulacji fizycznej.
        // Nastêpnie u¿ywamy Mathf.Clamp, aby ograniczyæ wartoœæ newY do przedzia³u [minY, maxY].
        float newY = Mathf.Clamp(rb.position.y + movementInput * speed * Time.fixedDeltaTime, minY, maxY);

        // Przesuwamy paletkê do nowej pozycji, zachowuj¹c sta³¹ wartoœæ osi X:
        rb.MovePosition(new Vector2(rb.position.x, newY));
        // Metoda MovePosition umo¿liwia p³ynne przemieszczanie obiektu zgodnie z obliczon¹ pozycj¹, korzystaj¹c z mechaniki fizyki.
    }
}
