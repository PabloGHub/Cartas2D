using UnityEngine;
using TMPro;

public class Tienda : Absorber
{
    // ****************** Metodos Unity ****************** //
    [SerializeField]
    TextMeshProUGUI _contadorMonedas_tp;
    public int _cantidadMonedas_i = 0;

    private int _cantidadHijos_i = 0;

    // ****************** Metodos Unity ****************** //
    void Start()
    {
        _cantidadHijos_i = transform.childCount;
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

            // Si dejo de simular las fisicas no puedo volverlo a coger el maletin.
            // Al simularlas el maletin se puede mover al poner una carta alado.
            moverEncima();

            if (_objeto_go != null)
            {
                if (_objeto_go.GetComponent<Maletin>() != null)
                {
                    _cantidadMonedas_i = _objeto_go.GetComponent<Maletin>()._numMonedas_i;
                    _contadorMonedas_tp.text = _cantidadMonedas_i.ToString();
                }
            }
        }
    }

    // ****************** Metodos NUESTROS ****************** //
    protected override void trasAbsorber()
    {
        if ((_objeto_go != null) && (_objeto_go.GetComponent<Maletin>() != null))
        {
            _objeto_go.GetComponent<Rigidbody2D>().simulated = false;

            _objeto_go.transform.SetParent(transform);
            moverEncima();

            _objeto_go.GetComponent<Rigidbody2D>().simulated = true;
        }
    }

    void moverEncima()
    {
        float alturaObjeto = gameObject.GetComponent<Renderer>().bounds.size.y;

        _objeto_go.transform.rotation = Quaternion.identity;
        _objeto_go.transform.localPosition = new Vector3(-4f, (alturaObjeto / 2) - 1f, 0f);
    }
}
