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

    }

    protected void trasAbsorber()
    {
        EsperarAnimacion("Desvanecer", "Aparecer");
        EsperarAnimacion("Aparecer", "Quieto");

        if (_objeto_go != null) 
        {
            Rigidbody2D _rbObjeto_rb = _objeto_go.GetComponent<Rigidbody2D>();
            _rbObjeto_rb.Sleep();
            _rbObjeto_rb.simulated = false;
        }
        float alturaObjeto = gameObject.GetComponent<Renderer>().bounds.size.y;

        _objeto_go.transform.SetParent(transform);
        _objeto_go.transform.rotation = Quaternion.identity;
        _objeto_go.transform.localPosition = new Vector3(0, (alturaObjeto / 2) + 1f, 0);

        _objeto_go = null;
    }

}
