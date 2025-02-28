using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using System.Collections.Generic;
using System.Collections;

public abstract class Absorber : MonoBehaviour
{
    // ****************** DECALRACIONES ****************** //
    public GameObject _objeto_go;
    protected Animator _animator;


    // ****************** Metodos UNITY ****************** //
    private void OnTriggerEnter2D(Collider2D _colision)
    {
        if (_colision.gameObject.CompareTag("Objeto"))
        {
            _animator = _colision.gameObject.GetComponentInChildren<Animator>();
            //_animator.Play("Desvanecer");
            _objeto_go = _colision.gameObject;

            trasAbsorber();
        }
    }


    // ****************** Metodos NUESTROS ****************** //
    protected abstract void trasAbsorber();

    protected IEnumerator animarRetardado(string _animacion_s, float _tiempo_f)
    {
        yield return new WaitForSeconds(_tiempo_f);
        _animator.Play(_animacion_s);
    } // StartCoroutine(animarRetardado("Aparecer"));


    protected IEnumerator EsperarAnimacion(string _animacionActual_s, string _AnimacionSiguiente_s)
    {
        while (_animator.GetCurrentAnimatorStateInfo(0).IsName(_animacionActual_s) &&
            _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }
        _animator.Play(_AnimacionSiguiente_s);
        Debug.Log(_AnimacionSiguiente_s);
    }
}
