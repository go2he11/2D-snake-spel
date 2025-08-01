using UnityEngine; // Ger tillgång till Unitys funktioner, t.ex. MonoBehaviour, Collider2D, etc.

public class Food : MonoBehaviour // Klassen styr matobjektet i spelet
{
    public BoxCollider2D gridArea; // Område där maten kan dyka upp – definierat i Unity-editorn

    private void Start() // Körs automatiskt när spelet startar
    {
        RandomizePosition(); // Flyttar maten till en slumpmässig position i början
    }

    private void RandomizePosition() // Metod för att placera maten på ett slumpmässigt ställe
    {
        Bounds bounds = gridArea.bounds; // Hämtar gränserna (min/max) för spelområdet

        float x = Mathf.Floor(Random.Range(bounds.min.x, bounds.max.x)); // Slumpar ett heltal inom X-området
        float y = Mathf.Floor(Random.Range(bounds.min.y, bounds.max.y)); // Slumpar ett heltal inom Y-området

        this.transform.position = new Vector3(x, y, 0.0f); // Flyttar matens position till den slumpade punkten
    }

    private void OnTriggerEnter2D(Collider2D other) // Körs när något kolliderar med maten
    {
        if (other.CompareTag("Player")) // Om det som träffar har taggen "Player" (ormen)
        {
            RandomizePosition(); // Flytta maten till en ny slumpmässig plats
        }
    }
}

