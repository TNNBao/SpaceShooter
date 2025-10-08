using UnityEngine;

public class ParaliaxBackground : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    float backgroundImageWidth;

    void Start()
    {
        
    }

    void Update()
    {
        float moveX = moveSpeed * PlayerController.Instance.boost * Time.deltaTime;
        transform.position += new Vector3(moveX, 0);
        if (Mathf.Abs(transform.position.x) - 97 > 0)
        {
            transform.position = new Vector3(97f, transform.position.y);
        }
    }
}
