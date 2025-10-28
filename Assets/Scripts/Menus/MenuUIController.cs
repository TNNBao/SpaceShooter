using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIController : MonoBehaviour
{
    public static MenuUIController Instance;
    [SerializeField] private TMP_Text scoreText;
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

    private void Update()
    {
        scoreText.text = "Your Score: " + UIController.Instance.score;
    }
}
