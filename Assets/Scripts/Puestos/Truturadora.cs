using Unity.VisualScripting;
using UnityEngine;

public class Truturadora : Absorber
{
    // ****************** Metodos Unity ****************** //
    [SerializeField]
    Combate _combate;

    // ****************** Metodos Unity ****************** //
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // ****************** Metodos NUESTROS ****************** //
    protected override void trasAbsorber()
    {
        if (_objetoAbsorbido_go.GetComponent<Carta>() != null)
        {
            _combate.siguientePialar();
            Destroy(_objetoAbsorbido_go);
        }
    }
}
