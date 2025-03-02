using UnityEngine;

public class Venta : Absorber
{
    // ***********************( Declaraciones )*********************** //
    [SerializeField]
    private Combate _combate_script;

    // ***********************( Metodos de UNITY )*********************** //
    void Start()
    {
        
    }

    void Update()
    {

    }

    // ***********************( Metodos propios )*********************** //
    protected override void trasAbsorber()
    {
        if (_objetoAbsorbido_go.GetComponent<Carta>() != null)
        {
            _combate_script.intanciarMenedas(_objetoAbsorbido_go.GetComponent<Carta>()._cantidad_f);
            Destroy(_objetoAbsorbido_go);
        }
    }
}
