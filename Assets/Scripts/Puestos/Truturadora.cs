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
        _combate.siguientePialar();
    }
}
