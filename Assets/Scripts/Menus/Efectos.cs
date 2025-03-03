using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Efectos : MonoBehaviour
{
    public Slider slider;
    public AudioMixer audioMixer;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("SFX", 0.75f);
        ChangeSlider(slider.value);
    }

    public void ChangeSlider(float valor)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(valor) * 20);

        PlayerPrefs.SetFloat("SFX", valor);
    }
}
