using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using System.Collections.Generic;
using System.Collections;

public class Pilar : Absorber
{
    // ****************** Metodos Unity ****************** //
    void Start()
    {
        
    }

    void Update()
    {
        RaycastHit2D _HitCarta_h = Physics2D.Raycast(transform.position, Vector2.up, 0.5f, LayerMask.GetMask("Objeto"));
        
        if (_HitCarta_h.collider != null)
        {
            Debug.Log(_HitCarta_h.collider.name);
            GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    // ****************** Metodos NUESTROS ****************** //
    protected override void trasAbsorber()
    {
        //StartCoroutine(AnimacionSecuencial());
        _animator.Play("Aparecer");

        Rigidbody2D _rbObjeto_rb = _objeto_go.GetComponent<Rigidbody2D>();
        _rbObjeto_rb.Sleep();
        _rbObjeto_rb.simulated = false;
        
        float alturaObjeto = gameObject.GetComponent<Renderer>().bounds.size.y;

        _objeto_go.transform.SetParent(transform);
        _objeto_go.transform.rotation = Quaternion.identity;
        _objeto_go.transform.localPosition = new Vector3(0f, (alturaObjeto / 2) + 0.8f, 0f);

    }

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
