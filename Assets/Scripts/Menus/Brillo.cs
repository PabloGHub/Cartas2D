using UnityEngine;

using UnityEngine.UI;

public class Brillo : MonoBehaviour
{
    public Slider slider;
    public float sliderValue;
    public Image panelBrillo;
    public float valorBlack;
    public float valorWhite;
    // Start is called before the first frame update
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("Brillo", 0.5f);

        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, sliderValue / 3);
    }

    // Update is called once per frame
    void Update()
    {
        valorBlack = 1 - sliderValue - 0.5f;
        valorWhite = sliderValue - 0.5f;
        if (sliderValue < 0.5f)
        {
            panelBrillo.color = new Color(0, 0, 0, valorBlack);
        }
        if (sliderValue > 0.5f)
        {
            panelBrillo.color = new Color(255, 255, 255, valorWhite);
        }
    }
    public void ChangeSlider(float valor)
    {
        sliderValue = valor;
        PlayerPrefs.SetFloat("Brillo", sliderValue);
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, sliderValue / 3);
    }

}
