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

            _cantidadMonedas_i = 0;
            _contadorMonedas_tp.text = "0";
        }
        else
        {
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = true;

            moverEncimaMaletin();

            if (_objeto_go != null)
            {
                if (_objeto_go.GetComponent<Maletin>() != null)
                {
                    _cantidadMonedas_i = _objeto_go.GetComponent<Maletin>()._numMonedas_i;
                    _contadorMonedas_tp.text = _cantidadMonedas_i.ToString();
                }
            }
        }

        moverEnicimaCartas();
    }

    // ****************** Metodos NUESTROS ****************** //
    protected override void trasAbsorber()
    {
        if ((_objeto_go != null) && (_objeto_go.GetComponent<Maletin>() != null))
        {
            _objeto_go.GetComponent<Rigidbody2D>().simulated = false;

            _objeto_go.transform.SetParent(transform);
            moverEncimaMaletin();

            _objeto_go.GetComponent<Rigidbody2D>().simulated = true;
        }
    }

    void moverEncimaMaletin()
    {
        _objeto_go.transform.rotation = Quaternion.identity;
        _objeto_go.transform.localPosition = new Vector3(-4f, (_alturaObjetoTienda_f / 2) - 1f, 0f);
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
        int _precio_i = 0;
        int _monedasIniciales = _objeto_go.GetComponent<Maletin>()._numMonedas_i;

        if (_cartaAVender_go != null)
        {

            if (_cartaAVender_go.GetComponent<Carta>()._accion_i == 0)
                _precio_i = (int)(_cartaAVender_go.GetComponent<Carta>()._cantidad_f + 0.2f);

            else
                _precio_i = (int)(_cartaAVender_go.GetComponent<Carta>()._cantidad_f + 0.2f);


            _objeto_go.GetComponent<Maletin>()._numMonedas_i -= _objeto_go.GetComponent<Maletin>()._numMonedas_i >= _precio_i ? _precio_i : 0;


            if (_cartaAVender_go == _carta1_go)
                _carta1_go = null;

            else if (_cartaAVender_go == _carta2_go)
                _carta2_go = null;

            else if (_cartaAVender_go == _carta3_go)
                _carta3_go = null;


            _contadorMonedas_tp.text = _cantidadMonedas_i.ToString();
        }

        return _monedasIniciales != _objeto_go.GetComponent<Maletin>()._numMonedas_i;
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
        _carta2_go = Instantiate(_PrefabCarta, _puesto2_go.transform);
        _carta3_go = Instantiate(_PrefabCarta, _puesto3_go.transform);

        moverEnicimaCartas();
    }
}
