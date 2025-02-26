using UnityEngine;

public class Absorber : MonoBehaviour
{
    protected GameObject _objeto_go;
    protected Animator _animator;

    private void OnTriggerEnter2D(Collider2D _colision)
    {
        if (_colision.gameObject.CompareTag("Objeto"))
        {
            _animator = _colision.gameObject.GetComponentInChildren<Animator>();
            _animator.Play("Desvanecer");

            _objeto_go = _colision.gameObject;
            new WaitForSeconds(0.5f);
        }
    }
}
