using UnityEngine;

public class PhaserBullet : MonoBehaviour
{
    [SerializeField] private GameObject collideEffect;
    void Update()
    {
        transform.position += new Vector3(PhaserWeapon.Instance.speed * Time.deltaTime, 0f);
        if (transform.position.x > 18.5)
        {
            Destroy(gameObject);
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Instantiate(collideEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
