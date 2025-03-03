using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Volumen : MonoBehaviour // Bonito es B
{
    // --- Variables Musica --- //
    public Slider sliderMusica;
    public float sliderValueMusica;
    public GameObject imagenMuteMusica;

    // --- Variables Efectos --- //
    public Slider sliderEfectos;
    public float sliderValueEfectos;
    public GameObject imagenMuteEfectos;

    // --- AudioMixer --- //
    public AudioMixer audioMixer;

    // Start is called before the first frame update
    void Start()
    {
        // Musica
        float _volumenMusica;
        audioMixer.GetFloat("MurciaVOL", out _volumenMusica);

        if (_volumenMusica <= 0)
            sliderMusica.value = 0.5f;
        else
            sliderMusica.value = (float)Math.Log10(_volumenMusica);

        RevisarSiEstoyMuteMusica();

        // Efectos
        float _volumenEfectos;
        audioMixer.GetFloat("SFXVOL", out _volumenEfectos);

        if (_volumenEfectos <= 0)
            sliderEfectos.value = 0.5f;
        else
            sliderEfectos.value = (float)Math.Log10(_volumenEfectos);

        RevisarSiEstoyMuteEfectos();
    }


    public void cambiarMusica(float valor)
    {
        sliderValueMusica = valor;

        if (valor > 0)
            audioMixer.SetFloat("MurciaVOL", 20 * (float)Math.Log10(valor));
        else
            audioMixer.SetFloat("MurciaVOL", -80);

        RevisarSiEstoyMuteMusica();
    }
    public void cambiarEfectos(float valor)
    {
        sliderValueEfectos = valor;

        if (valor > 0)
            audioMixer.SetFloat("SFXVOL", 20 * (float)Math.Log10(valor));
        else
            audioMixer.SetFloat("SFXVOL", -80);

        RevisarSiEstoyMuteMusica();
    }


    private void RevisarSiEstoyMuteMusica()
    {
        if (sliderValueMusica <= 0)
        {
            imagenMuteMusica.SetActive(true);
        }
        else
        {
            imagenMuteMusica.SetActive(false);
        }
    }
    private void RevisarSiEstoyMuteEfectos()
    {
        if (sliderValueEfectos <= 0)
        {
            imagenMuteEfectos.SetActive(true);
        }
        else
        {
            imagenMuteEfectos.SetActive(false);
        }
    }
}