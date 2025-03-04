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
        sliderMusica.value = PlayerPrefs.GetFloat("VolumenMusica", 0.5f);
        cambiarMusica(sliderMusica.value);

        // Efectos
        sliderEfectos.value = PlayerPrefs.GetFloat("VolumenEfectos", 0.5f);
        cambiarEfectos(sliderEfectos.value);
    }

    public void cambiarMusica(float valor)
    {
        sliderValueMusica = valor;

        PlayerPrefs.SetFloat("VolumenMusica", valor);
        PlayerPrefs.Save();

        if (valor > 0)
            audioMixer.SetFloat("MurciaVOL", 20 * (float)Math.Log10(valor));
        else
            audioMixer.SetFloat("MurciaVOL", -80);

        RevisarSiEstoyMuteMusica();
    }

    public void cambiarEfectos(float valor)
    {
        sliderValueEfectos = valor;

        PlayerPrefs.SetFloat("VolumenEfectos", valor);
        PlayerPrefs.Save();

        if (valor > 0)
            audioMixer.SetFloat("SFXVOL", 20 * (float)Math.Log10(valor));
        else
            audioMixer.SetFloat("SFXVOL", -80);

        RevisarSiEstoyMuteEfectos();
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