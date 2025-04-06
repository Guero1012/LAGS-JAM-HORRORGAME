using UnityEngine;
using UnityEngine.UI;

public class PerillaSliderControl : MonoBehaviour
{
    [Header("Slider asignado")]
    public Slider slider;

    [Header("�ngulos de rotaci�n")]
    public float anguloMin = -90f;
    public float anguloMax = 90f;

    void Update()
    {
        if (slider != null)
        {
            float valor = slider.value; // 0 a 1
            float anguloZ = Mathf.Lerp(anguloMin, anguloMax, valor);
            transform.localRotation = Quaternion.Euler(0f, 0f, anguloZ);
        }
    }
}
