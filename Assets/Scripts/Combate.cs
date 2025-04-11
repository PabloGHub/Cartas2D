using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Combate : MonoBehaviour
{
    // ***********************( Declaraciones )*********************** //
    // --- Declaraciones de barras y IU --- //
    [SerializeField] Image _barraSalud_image;
    [SerializeField] Image _barraEnemigo_image;
    [SerializeField] Image _barraTiempo_image;

    public float _maxSalud_f = 20;
    public float _maxSaludEnemigo_f = 20;
    public float _maxTiempo_f = 5;

    private float _salud_f;
    private float _saludEnemigo_f;
    private float _tiempo_f;


    // --- Delaciones de Pilares --- //
    [SerializeField] private GameObject _pilar1_go;
    [SerializeField] private GameObject _pilar2_go;
    [SerializeField] private GameObject _pilar3_go;
    [SerializeField] private GameObject _pilar4_go;
    private int _pilaresActivos_i = 0;


    // --- Declaraciones del Combate --- //
    [SerializeField] private TextMeshProUGUI _TextoDañoEnemigo_text;
    [SerializeField] private TextMeshProUGUI _TextoSalud_text;
    [SerializeField] private TextMeshProUGUI _TextoEnemigo_text;
    [SerializeField] private TextMeshProUGUI _TextoNivel_text;

    public int _nivel_i = 1;
    private float _dañoMinimo_f;
    private float _dañoEnemigo_f = 1f;
    private int _daño_i = 0; 
    private int _enemigosAbatidos_i = 0;

    private ControladorDatos _controladorDatos_script;


    // --- Declaraciones Tienda --- //
    [SerializeField]
    private Tienda _tienda_script;
    [SerializeField]
    private GameObject _PrefabMeneda_go;


    // --- Declaraciones de Reloj --- //
    [SerializeField]
    private Player _player_script;
    private float _tiempoReloj_f = 0;
    private int _segundos_i = 0;


    // ***********************( Metodos de UNITY )*********************** //
    void Start()
    {
        _salud_f = _maxSalud_f;
        _saludEnemigo_f = _maxSaludEnemigo_f;
        _tiempo_f = _maxTiempo_f;
        _dañoMinimo_f = _dañoEnemigo_f;

        _controladorDatos_script = GetComponent<ControladorDatos>();
        if (_controladorDatos_script.DarmeMenedas() > 0)
        {
            intanciarMenedas(_controladorDatos_script.DarmeMenedas());
            _controladorDatos_script.Menedas0();
        }

        actulizarCartel();
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
            _tiempo_f = _maxTiempo_f + 1;
            siguienteTurno();
        }
    }

    void siguienteTurno()
    {
        float _dannoAcer_f = 0;
        float _saludAcer_f = 0;

        comprobarPialres();

        switch (_pilaresActivos_i)
        {
            case 4:
                if (_pilar4_go.GetComponent<Pilar>()._cartaEncima_b)
                {
                    if (_pilar4_go.GetComponentInChildren<Carta>() != null)
                    {
                        Carta _carta = _pilar4_go.GetComponentInChildren<Carta>();
                        if (_carta._accion_i == 0)
                            _saludAcer_f += _carta._cantidad_f;
                        else
                            _dannoAcer_f += _carta._cantidad_f;
                    }
                }
            goto case 3;
            case 3:
                if (_pilar3_go.GetComponent<Pilar>()._cartaEncima_b)
                {
                    if (_pilar3_go.GetComponentInChildren<Carta>() != null)
                    {
                        Carta _carta = _pilar3_go.GetComponentInChildren<Carta>();
                        if (_carta._accion_i == 0)
                            _saludAcer_f += _carta._cantidad_f;
                        else
                            _dannoAcer_f += _carta._cantidad_f;
                    }
                }
            goto case 2;
            case 2:
                if (_pilar2_go.GetComponent<Pilar>()._cartaEncima_b)
                {
                    if (_pilar2_go.GetComponentInChildren<Carta>() != null)
                    {
                        Carta _carta = _pilar2_go.GetComponentInChildren<Carta>();
                        if (_carta._accion_i == 0)
                            _saludAcer_f += _carta._cantidad_f;
                        else
                            _dannoAcer_f += _carta._cantidad_f;
                    }
                }
            goto case 1;
            case 1:
                if (_pilar1_go.GetComponent<Pilar>()._cartaEncima_b)
                {
                    if (_pilar1_go.GetComponentInChildren<Carta>() != null)
                    {
                        Carta _carta = _pilar1_go.GetComponentInChildren<Carta>();
                        if (_carta._accion_i == 0)
                            _saludAcer_f += _carta._cantidad_f;
                        else
                            _dannoAcer_f += _carta._cantidad_f;
                    }
                }
            break;
        }

        _daño_i = (int)_dannoAcer_f;
        _saludEnemigo_f -= _daño_i;

        _salud_f -= _dañoEnemigo_f;
        _salud_f += _saludAcer_f;

        if (_salud_f > _maxSalud_f)
            _salud_f = _maxSalud_f;

        actulizarCartel();

        // Aqui Morir
        if (_salud_f <= 0)
        {
            Debug.Log("LLevar a muerte");
            SceneManager.LoadScene(2);
        }

        // Aqui Enemigo muere
        if (_saludEnemigo_f <= 0)
        {
            Debug.Log("Enemigo Abatido");

            _enemigosAbatidos_i++;
            if (_enemigosAbatidos_i % 2 == 0)
            {
                _nivel_i++;
                _maxSaludEnemigo_f += 10;
                _maxSalud_f += 10;
                _maxTiempo_f++;

                _controladorDatos_script.ComprobarNivel(_nivel_i);
                _controladorDatos_script.SumarMeneda();
            }

            _saludEnemigo_f = _maxSaludEnemigo_f;

            intanciarMenedas(_dañoEnemigo_f);


            int _dañoMaximo_i = _nivel_i + (_nivel_i * 3);
            if (_dañoMinimo_f >= _dañoMaximo_i)
            {
                _dañoEnemigo_f = _dañoMaximo_i;
            }
            else
            {
                _dañoEnemigo_f = _nivel_i + (_nivel_i * Random.Range(1, 4));
                while (_dañoEnemigo_f <= _dañoMinimo_f)
                    _dañoEnemigo_f = _nivel_i + (_nivel_i * Random.Range(1, 4));
            }
            _dañoMinimo_f = _dañoEnemigo_f;


            Debug.Log
            (
                "-- Daño del Enemigo --" +
                "\nDaño Maximo: " + _dañoMaximo_i +
                "\nDaño Minimo: " + _dañoMinimo_f +
                "\nDaño Enemigo: " + _dañoEnemigo_f
            );
        }

        _tienda_script.siguienteRonda();
        _tienda_script._nivel_i = _nivel_i;
        _TextoNivel_text.text = _nivel_i.ToString();

        actulizarCartel();
    }

    void actulizarCartel()
    {
        _barraSalud_image.fillAmount = _salud_f / _maxSalud_f;
        _barraEnemigo_image.fillAmount = _saludEnemigo_f / _maxSaludEnemigo_f;

        _TextoDañoEnemigo_text.text = _dañoEnemigo_f.ToString();
        _TextoSalud_text.text = _salud_f.ToString();
        _TextoEnemigo_text.text = _saludEnemigo_f.ToString();
    }

    public void intanciarMenedas(float _cantidad_f)
    {
        int _cantidad_i = ((int)((_cantidad_f / 2) + 0.20f)) + 1;
        for (int i = 0; i <= _cantidad_i; i++)
        {
            Vector3 _posicion_v3;
            if (Random.Range(0, 2) == 0)
                _posicion_v3 = new Vector3(Random.Range(-15, -4), 30f);
            else
                _posicion_v3 = new Vector3(Random.Range(8, 16), 30f);

            Instantiate(_PrefabMeneda_go, _posicion_v3, Quaternion.identity);
        }
            
        Debug.Log("Menedas Instanciadas: " + _cantidad_i);
    }

    // --- Sistema de Pilares --- //
    void comprobarPialres()
    {
        _pilaresActivos_i = 0;

        if (_pilar1_go.activeInHierarchy) _pilaresActivos_i++;
        if (_pilar2_go.activeInHierarchy) _pilaresActivos_i++;
        if (_pilar3_go.activeInHierarchy) _pilaresActivos_i++;
        if (_pilar4_go.activeInHierarchy) _pilaresActivos_i++;

        //Debug.Log("Pilares Activos: " + _pilaresActivos_i);
    }

    public void siguientePialar()
    {
        comprobarPialres();

        switch (_pilaresActivos_i)
        {
            case 1:
                activarPilar(_pilar2_go);
                Debug.Log("Activando Pilar 2");
            break;

            case 2:
                activarPilar(_pilar3_go);
                Debug.Log("Activando Pilar 3");
            break;

            case 3:
                activarPilar(_pilar4_go);
                Debug.Log("Activando Pilar 4");
            break;
        }
    }
    void activarPilar(GameObject _pilar_go)
    {
        _pilar_go.SetActive(true);
    }


    // --- Sistema de Reloj --- //
    void reloj()
    {
        if (_player_script._pausado_b)
            return;

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
