using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Volumen : MonoBehaviour
{
    public Slider slider;
    public float sliderValue;
    public Image imagenMute;
    public AudioMixer audioMixer;
    // Start is called before the first frame update
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f); // ¿?
        AudioListener.volume = slider.value;
        RevisarSiEstoyMute();
    }

    public void ChangeSlider(float valor)
    {
        sliderValue = valor;
        float _decibelios = 20 * (float)Math.Log10(valor / 100);
        audioMixer.SetFloat("Murcia", _decibelios);
        AudioListener.volume = slider.value;
        RevisarSiEstoyMute();
    }

    private void RevisarSiEstoyMute()
    {
        if (sliderValue <= 0)
        {
            imagenMute.enabled = true;
        }
        else
        {
            imagenMute.enabled = false;
        }

    }
}