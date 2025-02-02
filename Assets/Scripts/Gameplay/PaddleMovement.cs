using UnityEngine; // Importujemy przestrze� nazw UnityEngine, kt�ra zawiera podstawowe klasy i funkcjonalno�ci silnika Unity.

/// <summary>
/// Klasa PaddleMovement zarz�dza ruchem paletki w grze, implementuj�c podstawowe mechanizmy fizyczne i wej�cia u�ytkownika.
/// Dziedziczy po MonoBehaviour, co umo�liwia jej do��czenie jako komponent do obiekt�w w scenie Unity.
/// </summary>
public class PaddleMovement : MonoBehaviour
{
    // Atrybut [SerializeField] umo�liwia edycj� poni�szych zmiennych w Inspectorze Unity mimo zachowania prywatnego dost�pu.
    
    [SerializeField] private float speed = 5f;         
    // Zmienna speed definiuje pr�dko�� poruszania si� paletki, czyli jak szybko obiekt b�dzie reagowa� na polecenia ruchu.

    [SerializeField] private bool isPlayerOne = true;    
    // Flaga isPlayerOne okre�la, czy dana paletka nale�y do gracza numer 1. Jej warto�� wp�ywa na wyb�r odpowiedniego wej�cia z klawiatury.

    [SerializeField] private float minY = -4f;           
    // Zmienna minY definiuje doln� granic� ruchu paletki na osi Y, zapobiegaj�c zej�ciu obiektu poni�ej okre�lonego poziomu.

    [SerializeField] private float maxY = 4f;            
    // Zmienna maxY okre�la g�rn� granic� ruchu paletki na osi Y, ograniczaj�c mo�liwo�� poruszania si� obiektu powy�ej ustalonego poziomu.

    // Prywatna referencja do komponentu Rigidbody2D, kt�ry odpowiada za symulacj� fizyki w dwuwymiarowej przestrzeni.
    private Rigidbody2D rb;

    // Zmienna przechowuj�ca warto�� wej�cia ruchowego (np. z klawiatury) dla aktualnej klatki.
    private float movementInput;

    /// <summary>
    /// Metoda Awake() jest wywo�ywana natychmiast po za�adowaniu skryptu i s�u�y do inicjalizacji komponent�w.
    /// </summary>
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); 
        // Inicjalizujemy zmienn� rb poprzez pobranie komponentu Rigidbody2D z obiektu, do kt�rego do��czony jest ten skrypt.
    }

    /// <summary>
    /// Metoda Update() jest wywo�ywana raz na ka�d� klatk� renderowania i s�u�y g��wnie do odczytu wej�cia u�ytkownika.
    /// </summary>
    private void Update()
    {
        // Operator warunkowy (ternary) wybiera nazw� osi wej�cia w zale�no�ci od tego, czy paletka nale�y do gracza 1.
        movementInput = Input.GetAxisRaw(isPlayerOne ? "Vertical" : "Vertical2");
        // Pobieramy surow� warto�� wej�cia z odpowiedniej osi:
        // - "Vertical" dla gracza 1,
        // - "Vertical2" dla innego gracza.
    }

    /// <summary>
    /// Metoda FixedUpdate() jest wywo�ywana w sta�ych odst�pach czasowych i jest idealna do operacji zwi�zanych z fizyk�.
    /// </summary>
    private void FixedUpdate()
    {
        // Obliczamy now� warto�� pozycji Y dla paletki:
        // - rb.position.y: aktualna pozycja Y,
        // - movementInput: odczytana warto�� wej�cia, kt�ra mo�e by� ujemna, dodatnia lub zerowa,
        // - speed: mno�nik okre�laj�cy tempo ruchu,
        // - Time.fixedDeltaTime: sta�y interwa� czasu mi�dzy wywo�aniami FixedUpdate, zapewniaj�cy sp�jno�� symulacji fizycznej.
        // Nast�pnie u�ywamy Mathf.Clamp, aby ograniczy� warto�� newY do przedzia�u [minY, maxY].
        float newY = Mathf.Clamp(rb.position.y + movementInput * speed * Time.fixedDeltaTime, minY, maxY);

        // Przesuwamy paletk� do nowej pozycji, zachowuj�c sta�� warto�� osi X:
        rb.MovePosition(new Vector2(rb.position.x, newY));
        // Metoda MovePosition umo�liwia p�ynne przemieszczanie obiektu zgodnie z obliczon� pozycj�, korzystaj�c z mechaniki fizyki.
    }
}
