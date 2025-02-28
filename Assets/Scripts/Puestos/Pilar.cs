using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using System.Collections.Generic;
using System.Collections;

public class Pilar : Absorber
{
    // ****************** Metodos Unity ****************** //
    public bool _cartaEncima_b = false;

    // ****************** Metodos Unity ****************** //
    void Start()
    {
        
    }

    void Update()
    {
        if (transform.childCount <= 0)
        {
            GetComponent<CircleCollider2D>().enabled = true;
            GetComponent<BoxCollider2D>().enabled = false;
            _cartaEncima_b = false;
        }
        else
        {
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = true;
            _cartaEncima_b = true;

            moverEncima();
        }
    }

    // ****************** Metodos NUESTROS ****************** //
    protected override void trasAbsorber()
    {
        //StartCoroutine(AnimacionSecuencial());
        _animator.Play("Aparecer");

        if (_objeto_go != null)
        {
            _objeto_go.GetComponent<Rigidbody2D>().simulated = false;

            moverEncima();
        }
    }

    void moverEncima()
    {
        float alturaObjeto = gameObject.GetComponent<Renderer>().bounds.size.y;

        _objeto_go.transform.SetParent(transform);
        _objeto_go.transform.rotation = Quaternion.identity;
        _objeto_go.transform.localPosition = new Vector3(0f, (alturaObjeto / 2) + 0.8f, 0f);

        _objeto_go.GetComponent<Rigidbody2D>().simulated = true;
    }

    // Desvanecer esta descartado, automaticamente al ejecutar Aparecer pasa a Quieto.
    // Desvanecer -> Aparecer -> Quieto
    private IEnumerator AnimacionSecuencial()
    {
        yield return StartCoroutine(EsperarAnimacion("Desvanecer", "Aparecer"));
        yield return StartCoroutine(EsperarAnimacion("Aparecer", "Quieto"));

        if (_objeto_go != null)
        {
            Rigidbody2D _rbObjeto_rb = _objeto_go.GetComponent<Rigidbody2D>();
            _rbObjeto_rb.Sleep();
            _rbObjeto_rb.simulated = false;
        }
        float alturaObjeto = gameObject.GetComponent<Renderer>().bounds.size.y;

        _objeto_go.transform.SetParent(transform);
        _objeto_go.transform.rotation = Quaternion.identity;
        _objeto_go.transform.localPosition = new Vector3(0f, (alturaObjeto / 2) + 1f, 0f);

        _objeto_go = null;
    }
}
