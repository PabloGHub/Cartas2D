using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using System.Collections.Generic;
using System.Collections;

public class Pilar : Absorber
{
    // ****************** Metodos Unity ****************** //
    public bool _cartaEncima_b = false;
    float _alturaObjetoTienda_f;

    // ****************** Metodos Unity ****************** //
    void Start()
    {

        _alturaObjetoTienda_f = gameObject.GetComponent<Renderer>().bounds.size.y;
    }

    void Update()
    {
        if (transform.childCount <= 0)
        {
            GetComponent<CircleCollider2D>().enabled = true;
            GetComponent<BoxCollider2D>().enabled = false;

            _cartaEncima_b = false;
            _objetoAbsorbido_go = null;
        }
        else
        {
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = true;

            if (_objetoAbsorbido_go.GetComponent<Carta>() != null)
                _cartaEncima_b = true;
        }
    }

    // ****************** Metodos NUESTROS ****************** //
    protected override void trasAbsorber()
    {
        if (_objetoAbsorbido_go != null)
        {
            if (_animator != null)
                _animator.Play("Aparecer");

            _objetoAbsorbido_go.GetComponent<Rigidbody2D>().simulated = false;
            _objetoAbsorbido_go.transform.SetParent(transform);
            moverEncima();
        }
    }

    void moverEncima()
    {
        _objetoAbsorbido_go.transform.rotation = Quaternion.identity;
        _objetoAbsorbido_go.transform.localPosition = new Vector3(0f, (_alturaObjetoTienda_f / 2) + 0.8f, 0f);
    }

    // Desvanecer esta descartado, automaticamente al ejecutar Aparecer pasa a Quieto.
    // Desvanecer -> Aparecer -> Quieto
    private IEnumerator AnimacionSecuencial()
    {
        yield return StartCoroutine(EsperarAnimacion("Desvanecer", "Aparecer"));
        yield return StartCoroutine(EsperarAnimacion("Aparecer", "Quieto"));

        if (_objetoAbsorbido_go != null)
        {
            Rigidbody2D _rbObjeto_rb = _objetoAbsorbido_go.GetComponent<Rigidbody2D>();
            _rbObjeto_rb.Sleep();
            _rbObjeto_rb.simulated = false;
        }
        float alturaObjeto = gameObject.GetComponent<Renderer>().bounds.size.y;

        _objetoAbsorbido_go.transform.SetParent(transform);
        _objetoAbsorbido_go.transform.rotation = Quaternion.identity;
        _objetoAbsorbido_go.transform.localPosition = new Vector3(0f, (alturaObjeto / 2) + 1f, 0f);

        _objetoAbsorbido_go = null;
    }
}
