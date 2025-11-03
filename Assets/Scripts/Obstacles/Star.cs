using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] private int scoreValue = 10;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(-1f, 0f);
    }

    void Update()
    {
        float moveX = GameManager.Instance.worldSpeed * PlayerController.Instance.boost * Time.deltaTime;
        transform.position += new Vector3(-moveX, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UIController.Instance.UpdateScore(scoreValue);

            Destroy(gameObject);
        } 
        else if (other.CompareTag("Bullet"))
        {
            UIController.Instance.UpdateScore(scoreValue);
            
            Destroy(other.gameObject); 
            
            Destroy(gameObject);
        }
    }
}
