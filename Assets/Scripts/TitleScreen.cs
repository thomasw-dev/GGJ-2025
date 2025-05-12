using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] GameObject background;
    [SerializeField] Transform mailsParent;
    [SerializeField] bool showBackground;

    void Start()
    {
        Player.posBoundEnabled = false;
        foreach (Transform mail in mailsParent)
        {
            Rigidbody2D rb = mail.GetComponent<Rigidbody2D>();
            // Random rotation
            rb.AddTorque(Random.Range(2f, 5f) * (Random.value < 0.5f ? 1 : -1));
            // Random move direction
            rb.AddForce(Random.insideUnitCircle.normalized * 0.2f, ForceMode2D.Impulse);
        }
        showBackground = false;
    }

    void Update()
    {
        // Press tab to toggle background
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            showBackground = !showBackground;
        }
        background.SetActive(!showBackground);

        // Press space to reload scene
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
