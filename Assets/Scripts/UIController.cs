using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    [SerializeField] private Slider energySlider;
    [SerializeField] private TMP_Text energyText;

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

    public void UpdateEnergySlider(float current, float max)
    {
        energySlider.value = current;
        energySlider.maxValue = max;
        energyText.text = (int)current + "/" + energySlider.maxValue;
    }
}
