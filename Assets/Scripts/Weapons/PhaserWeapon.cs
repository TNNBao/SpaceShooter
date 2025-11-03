using UnityEngine;

public class PhaserWeapon : MonoBehaviour
{
    public static PhaserWeapon Instance;

    [SerializeField] private GameObject bulletPrefab;

    public float speed;
    public int damage;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void Shoot()
    {
        Instantiate(bulletPrefab, transform.position, transform.rotation);
    }
}
