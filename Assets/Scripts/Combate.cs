using UnityEngine;
using UnityEngine.UI;

public class Combate : MonoBehaviour
{
    // ***********************( Declaraciones )*********************** //
    // --- Declaraciones de barras y IU --- //
    [SerializeField] Image _barraSalud_image;
    [SerializeField] Image _barraEnemigo_image;
    [SerializeField] Image _barraTiempo_image;

    public float _maxSalud_f = 100;
    public float _maxEnemigo_f = 100;
    public float _maxTiempo_f = 21;

    float _salud_f;
    float _enemigo_f;
    float _tiempo_f;

    // --- Delaciones de Pilares --- //
    [SerializeField] GameObject _pilar1_go;
    [SerializeField] GameObject _pilar2_go;
    [SerializeField] GameObject _pilar3_go;
    [SerializeField] GameObject _pilar4_go;

    // --- Declaraciones del Combate --- //
    int _nivel_i = 1;
    int _dañoEnemigo_i = 0;
    int _daño_i = 10; // hardcodeado/Picado, luego cuando funcionen los pilares se hace el sistema.

    // --- Declaraciones de Reloj --- //
    float _tiempoReloj_f = 0;
    int _segundos_i = 0;

    // ***********************( Metodos de UNITY )*********************** //
    void Start()
    {
        _salud_f = _maxSalud_f;
        _enemigo_f = _maxEnemigo_f;
        _tiempo_f = _maxTiempo_f;
    }

    void Update()
    {
        reloj();
    }



    // ***********************( Metodos PROPIOS )*********************** //
    void tiempoTurno()
    {
        _tiempo_f--;
        _barraTiempo_image.fillAmount = _tiempo_f / _maxTiempo_f;

        if (_tiempo_f <= 0)
        {
            _tiempo_f = _maxTiempo_f;
            siguienteTurno();
        }
    }

    void siguienteTurno()
    {
        _salud_f -= _dañoEnemigo_i;
        _barraSalud_image.fillAmount = _salud_f / _maxSalud_f;

        _enemigo_f -= _daño_i;
        _barraEnemigo_image.fillAmount = _enemigo_f / _maxEnemigo_f;

        if (_salud_f <= 0)
        {

        }
    }

    // --- Sistema de Reloj --- //
    void reloj()
    {
        _tiempoReloj_f += Time.deltaTime;

        if (_tiempoReloj_f >= 1)
        {
            _segundos_i++;
            _tiempoReloj_f = 0;
            cadaSegundo();
        }

        if (_segundos_i % 2 == 0)
            cadaParSegundos();

        if (_segundos_i % 5 == 0)
            cadaCincoSegundos();
    }


    void cadaSegundo()
    {
        tiempoTurno();
    }

    void cadaParSegundos()
    {

    }

    void cadaCincoSegundos()
    {

    }
}
}
