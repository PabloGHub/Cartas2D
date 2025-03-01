using UnityEngine;
using TMPro;

public class Tienda : Absorber
{
    // ****************** Metodos Unity ****************** //
    [SerializeField]
    TextMeshProUGUI _contadorMonedas_tp;
    public int _cantidadMonedas_i = 0;
    private int _cantidadHijos_i = 0;
    float _alturaObjetoTienda_f;
    public GameObject _supuestoMaletin_go;
    public int _nivel_i = 1;

    // --- puestos de cartas --- //
    [SerializeField] GameObject _puesto1_go;
    [SerializeField] GameObject _puesto2_go;
    [SerializeField] GameObject _puesto3_go;

    [SerializeField] GameObject _PrefabCarta;

    private GameObject _carta1_go;
    private GameObject _carta2_go;
    private GameObject _carta3_go;

    // ****************** Metodos Unity ****************** //
    void Start()
    {
        _cantidadHijos_i = transform.childCount;
        _alturaObjetoTienda_f = gameObject.GetComponent<Renderer>().bounds.size.y;

        siguienteRonda();
    }

    void Update()
    {
        if (transform.childCount <= _cantidadHijos_i)
        {
            GetComponent<CircleCollider2D>().enabled = true;
            GetComponent<BoxCollider2D>().enabled = false;

            _supuestoMaletin_go = null;
            _cantidadMonedas_i = 0;
            _contadorMonedas_tp.text = "0";
        }
        else
        {
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = true;

            if (_supuestoMaletin_go != null)
            {
                moverEncimaMaletin();

                if (_supuestoMaletin_go.GetComponent<Maletin>() != null)
                {
                    _cantidadMonedas_i = _supuestoMaletin_go.GetComponent<Maletin>()._numMonedas_i;
                    _contadorMonedas_tp.text = _cantidadMonedas_i.ToString();
                }
            }
        }

        moverEnicimaCartas();
    }

    // ****************** Metodos NUESTROS ****************** //
    protected override void trasAbsorber()
    {
        if (_objeto_go != null)
        {
            _supuestoMaletin_go = _objeto_go;
            _objeto_go = null;


            _supuestoMaletin_go.GetComponent<Rigidbody2D>().simulated = false;

            _supuestoMaletin_go.transform.SetParent(transform);
            moverEncimaMaletin();

            _supuestoMaletin_go.GetComponent<Rigidbody2D>().simulated = true;
        }
    }

    void moverEncimaMaletin()
    {
        _supuestoMaletin_go.transform.rotation = Quaternion.identity;
        _supuestoMaletin_go.transform.localPosition = new Vector3(-4f, (_alturaObjetoTienda_f / 2) - 1f, 0f);
    }

    void moverEnicimaCartas()
    {
        if ((_puesto1_go.GetComponentInChildren<Carta>() != null) && (_carta1_go != null))
        {
            _carta1_go.transform.rotation = Quaternion.identity;
            _carta1_go.transform.localPosition = new Vector3(0f, (_alturaObjetoTienda_f / 2) - 0.5f, 0f);
        }

        if ((_puesto2_go.GetComponentInChildren<Carta>() != null) && (_carta2_go != null))
        {
            _carta2_go.transform.rotation = Quaternion.identity;
            _carta2_go.transform.localPosition = new Vector3(0f, (_alturaObjetoTienda_f / 2) - 0.5f, 0f);
        }

        if ((_puesto3_go.GetComponentInChildren<Carta>() != null) && (_carta3_go != null))
        {
            _carta3_go.transform.rotation = Quaternion.identity;
            _carta3_go.transform.localPosition = new Vector3(0f, (_alturaObjetoTienda_f / 2) - 0.5f, 0f);
        }
    }

    public bool venderCarta(GameObject _cartaAVender_go)
    {
        Debug.Log("Maletin es null? -> " + (_supuestoMaletin_go == null));
        if (_supuestoMaletin_go == null)
            return false;

        Debug.Log("Carta a vender es null? -> " + (_cartaAVender_go == null));
        if (_cartaAVender_go == null)
            return false;


        int _precio_i = 0;
        int _monedasIniciales = _supuestoMaletin_go.GetComponent<Maletin>()._numMonedas_i;
        bool _vendido_b = false;

        Debug.Log("Monedas iniciales: " + _monedasIniciales);

        
        
        _precio_i = (int)(_cartaAVender_go.GetComponent<Carta>()._cantidad_f + 0.2f);
        Debug.Log("Precio: " + _precio_i);

        _supuestoMaletin_go.GetComponent<Maletin>()._numMonedas_i -= (_supuestoMaletin_go.GetComponent<Maletin>()._numMonedas_i >= _precio_i) ? _precio_i : 0;
        _contadorMonedas_tp.text = _cantidadMonedas_i.ToString();

        _vendido_b = (_monedasIniciales != _supuestoMaletin_go.GetComponent<Maletin>()._numMonedas_i);
        if (_vendido_b)
        {
            if (_cartaAVender_go == _carta1_go)
                _carta1_go = null;

            else if (_cartaAVender_go == _carta2_go)
                _carta2_go = null;

            else if (_cartaAVender_go == _carta3_go)
                _carta3_go = null;
        }
        Debug.Log("Vendido: " + _vendido_b);
        

        return _vendido_b;
    }

    // --- Rondas de Cartas --- //
    public void siguienteRonda()
    {
        Carta _carta1 = _puesto1_go.GetComponentInChildren<Carta>();
        if (_carta1 != null)
            Destroy(_carta1.gameObject);

        Carta _carta2 = _puesto2_go.GetComponentInChildren<Carta>();
        if (_carta2 != null)
            Destroy(_carta2.gameObject);

        Carta _carta3 = _puesto3_go.GetComponentInChildren<Carta>();
        if (_carta3 != null)
            Destroy(_carta3.gameObject);

        generarCartas();
    }

    void generarCartas()
    {
        _carta1_go = Instantiate(_PrefabCarta, _puesto1_go.transform);
        _carta1_go.GetComponent<Carta>().nivel = _nivel_i;
        _carta1_go.GetComponent<Carta>()._vendiendose_b = true;

        _carta2_go = Instantiate(_PrefabCarta, _puesto2_go.transform);
        _carta2_go.GetComponent<Carta>().nivel = _nivel_i;
        _carta2_go.GetComponent<Carta>()._vendiendose_b = true;

        _carta3_go = Instantiate(_PrefabCarta, _puesto3_go.transform);
        _carta3_go.GetComponent<Carta>().nivel = _nivel_i;
        _carta3_go.GetComponent<Carta>()._vendiendose_b = true;

        moverEnicimaCartas();
    }
}
