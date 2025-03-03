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
        float _volumen;
        audioMixer.GetFloat("MurciaVOL", out _volumen);

        if (_volumen <= 0)
            slider.value = 0.5f;
        else
            slider.value = (float)Math.Log10(_volumen);

        Debug.Log("Volumen: " + _volumen);

        //AudioListener.volume = slider.value;
        RevisarSiEstoyMute();
    }

    public void ChangeSlider(float valor)
    {
        sliderValue = valor;


        if (valor > 0)
            audioMixer.SetFloat("MurciaVOL", 20 * (float)Math.Log10(valor));
        else
            audioMixer.SetFloat("MurciaVOL", -80);

        //audioMixer.SetFloat("MurciaVOL", _decibelios);

        Debug.Log("Volumen: " + valor);

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