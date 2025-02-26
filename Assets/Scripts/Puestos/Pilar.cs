using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using System.Collections.Generic;
using System.Collections;

public class Pilar : MonoBehaviour
{
    // ****************** Variables ****************** //
    protected GameObject _objeto_go;
    protected Animator _animator;


    // ****************** Metodos Unity ****************** //
    void Start()
    {
        
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D _colision)
    {
        if (_colision.gameObject.CompareTag("Objeto"))
        {
            _animator = _colision.gameObject.GetComponentInChildren<Animator>();
            _animator.Play("Desvanecer");

            _objeto_go = _colision.gameObject;

            StartCoroutine(animarRetardado("Aparecer"));

            Rigidbody2D _rbObjeto_rb = _objeto_go.GetComponent<Rigidbody2D>();
            if (_rbObjeto_rb != null)
            {
                _rbObjeto_rb.Sleep();
                _rbObjeto_rb.simulated = false;
            }
            float alturaObjeto = gameObject.GetComponent<Renderer>().bounds.size.y;

            _objeto_go.transform.SetParent(transform);
            _objeto_go.transform.rotation = Quaternion.identity;
            _objeto_go.transform.localPosition = new Vector3(0, (alturaObjeto / 2) + 1f, 0);

            _objeto_go = null;

            StartCoroutine(animarRetardado("Quieto"));
        }
    }

    IEnumerator animarRetardado(string _animacion_s)
    {
        yield return new WaitForSeconds(0.5f);
        _animator.Play(_animacion_s);
    }
}
