using System.Collections.Generic; // Ger stöd för att använda Listor
using UnityEngine; // Ger tillgång till Unitys motor och funktioner

public class Snake : MonoBehaviour // Klass som styr ormen i spelet
{
    private Vector2 _direction = Vector2.right; // Ormens start-riktning är höger
    private List<Transform> _segments = new List<Transform>(); // Lista med alla ormens segment
    public Transform segmentPrefab; // Prefab (mall) för att skapa nya segment
    public int initialSize = 4; // Hur många segment ormen ska ha från början

    private void Start() // Körs automatiskt när spelet startar
    {
        ResetState(); // Återställ ormen till startläge
    }

    private void Update() // Körs varje bildruta – används för att läsa in knapptryckningar
    {
        if (Input.GetKeyDown(KeyCode.W) && _direction != Vector2.down) // Om W trycks ner och ormen inte går ner
        {
            _direction = Vector2.up; // Gå uppåt
        }
        else if (Input.GetKeyDown(KeyCode.S) && _direction != Vector2.up) // Om S trycks ner och ormen inte går upp
        {
            _direction = Vector2.down; // Gå neråt
        }
        else if (Input.GetKeyDown(KeyCode.A) && _direction != Vector2.right) // Om A trycks ner och ormen inte går höger
        {
            _direction = Vector2.left; // Gå vänster
        }
        else if (Input.GetKeyDown(KeyCode.D) && _direction != Vector2.left) // Om D trycks ner och ormen inte går vänster
        {
            _direction = Vector2.right; // Gå höger
        }
    }

    private void FixedUpdate() // Körs i jämna steg – perfekt för rörelse och fysik
    {
        // Flytta varje segment till platsen för segmentet framför
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        // Flytta ormens huvud ett steg i riktningen den går i
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f // Z-position hålls 0 för 2D
        );
    }

    private void Grow() // Metod för att lägga till ett nytt segment
    {
        Transform segment = Instantiate(this.segmentPrefab); // Skapa ett nytt segment från prefab
        segment.position = _segments[_segments.Count - 1].position; // Placera segmentet bakom det sista
        _segments.Add(segment); // Lägg till segmentet i listan
    }

    private void ResetState() // Metod för att återställa ormen efter kollision
    {
        // Ta bort alla gamla segment (förutom huvudet)
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }

        _segments.Clear(); // Rensa listan
        _segments.Add(this.transform); // Lägg till ormens huvud igen

        this.transform.position = Vector3.zero; // Flytta ormens huvud till startpositionen

        // Lägg till nya segment för att återgå till startstorlek
        for (int i = 1; i < this.initialSize; i++)
        {
            Transform segment = Instantiate(this.segmentPrefab); // Skapa nytt segment
            segment.position = _segments[_segments.Count - 1].position; // Placera bakom sista segment
            _segments.Add(segment); // Lägg till i listan
        }
    }

    private void OnTriggerEnter2D(Collider2D other) // Körs när ormen träffar något
    {
        if (other.CompareTag("Food")) // Om ormen träffar mat
        {
            Grow(); // Väx
        }
        else if (other.CompareTag("Obstacle")) // Om ormen träffar hinder (vägg eller sig själv)
        {
            ResetState(); // Starta om spelet
        }
    }
}
