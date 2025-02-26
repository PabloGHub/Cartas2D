using UnityEngine;
using UnityEngine.UI;

public class Scripter : MonoBehaviour
{
    // ***********************( Declaraciones )*********************** //
    // --- Declaraciones de barras y IU --- //
    [SerializeField] Image _barraSalud_image;
    [SerializeField] Image _barraEnemigo_image;
    [SerializeField] Image _barraTiempo_image;

    public float _maxSalud_f = 100;
    public float _maxEnemigo_f = 100;
    public float _maxTiempo_f = 20;

    float _salud_f;
    float _enemigo_f;
    float _tiempo_f;

    // --- Delaciones de Pilares --- //
    [SerializeField] GameObject _pilar1_go;
    [SerializeField] GameObject _pilar2_go;
    [SerializeField] GameObject _pilar3_go;
    [SerializeField] GameObject _pilar4_go;

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
    void tiempoTunro()
    {
        _tiempo_f--;
        _barraTiempo_image.fillAmount = _tiempo_f / _maxTiempo_f;

        if (_tiempo_f <= 0)
        {
            _tiempo_f = _maxTiempo_f;
            // TODO: Acion del turno.
            Debug.Log("Accion del Turno.");
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
        tiempoTunro();
    }

    void cadaParSegundos()
    {
        
    }

    void cadaCincoSegundos()
    {

    }
}
