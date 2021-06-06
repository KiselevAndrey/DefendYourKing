using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private bool hideSliderIsFullHealth = true;
    [SerializeField, ConditionalHide(nameof(hideSliderIsFullHealth))] private GameObject hiddenObject;
    [SerializeField] private float scaleX = 1f;

    [Header("UI elements")]
    [SerializeField] private Slider slider;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image fillImage;

    [Header("Color differencial")]
    [SerializeField] private Color fullHealthColor = Color.green;
    [SerializeField] private Color zeroHealthColor = Color.red;

    private float _maxHealth;

    private void Start()
    {
        Vector3 temp = transform.localScale;
        temp.x = scaleX;

        transform.localScale = temp;
    }

    public void SetMaxHealt(float value) 
    { 
        _maxHealth = value;
        slider.maxValue = value;
    }


    public void SetHealth(float currentHealth)
    {
        slider.value = currentHealth;
        fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, currentHealth / _maxHealth);

        if (hideSliderIsFullHealth && currentHealth == _maxHealth)
            hiddenObject.SetActive(false);
        else if(!hiddenObject.activeSelf)
            hiddenObject.SetActive(true);
    }
}
