using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public enum AsteroidSize { Massive, Big, Small }
    public AsteroidSize size;
    public GameObject bigAsteroidPrefab;
    public GameObject smallAsteroidPrefab;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private int hp;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private int scoreValue = 5;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        switch (size)
        {
            case AsteroidSize.Massive:
                hp = 6;
                break;
            case AsteroidSize.Big:
                hp = 4;
                break;
            case AsteroidSize.Small:
                hp = 2;
                break;
        }
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        float pushX = Random.Range(-1f, 0);
        float pushY = Random.Range(-1f, 1f);

        rb.linearVelocity = new Vector2(pushX, pushY);
    }

    void Update()
    {
        float moveX = GameManager.Instance.worldSpeed * PlayerController.Instance.boost * Time.deltaTime;
        transform.position += new Vector3(-moveX, 0);
        if (transform.position.x < -25)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            hp--;
            if (hp <= 0)
            {
                Split();
                Destroy(gameObject);
            }
        }
    }
    
    private void Split()
    {
        switch (size)
        {
            case AsteroidSize.Massive:
                // Vỡ từ Massive -> 2 cái Big
                if (bigAsteroidPrefab != null)
                {
                    Instantiate(bigAsteroidPrefab, transform.position, transform.rotation);
                    Instantiate(bigAsteroidPrefab, transform.position, transform.rotation);
                }
                break;
                
            case AsteroidSize.Big:
                // Vỡ từ Big -> 2 cái Small
                if (smallAsteroidPrefab != null)
                {
                    Instantiate(smallAsteroidPrefab, transform.position, transform.rotation);
                    Instantiate(smallAsteroidPrefab, transform.position, transform.rotation);
                }
                break;

            case AsteroidSize.Small:
                UIController.Instance.UpdateScore(scoreValue);
                break;
        }
    }
}
