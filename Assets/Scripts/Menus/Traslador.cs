using UnityEngine;

public class Traslador : MonoBehaviour
{
    public float _velocidad_f = 0.5f;
    public bool renderEntero = false;

    private Vector3 _largo;
    private Vector3 _posicionInicial;
    private int _divisor = 2;

    void Start()
    {
        _posicionInicial = transform.position;

        if (GetComponent<Renderer>() != null)
            _largo = transform.GetComponent<Renderer>().bounds.size;
        else 
            Debug.LogError("El objeto: " + gameObject.name + " no tiene Renderer");

        if (renderEntero)
            _divisor = 1;
    }

    void Update()
    {
        if (transform.position.x > _posicionInicial.x - _largo.x / _divisor)
        {
            transform.Translate(Vector3.left * _velocidad_f * Time.deltaTime);
        }
        else
        {
            transform.position = _posicionInicial;
        }
    }
}
