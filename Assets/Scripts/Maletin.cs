using UnityEngine;
using TMPro;

public class Maletin : MonoBehaviour
{
    // ****************** Declaraciones ****************** //
    public int _numMonedas_i = 0;
    [SerializeField]
    GameObject _prefab_go;
    [SerializeField]
    TextMeshProUGUI _contador_si;

    // ****************** Metodos UNITY ****************** //
    void Start()
    {
        
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D _colision)
    {
        if (_colision.gameObject.CompareTag("Entidad"))
        {
            if (_colision.gameObject.GetComponent<Meneda>() != null)
            {
                _numMonedas_i++;
                _contador_si.text = _numMonedas_i.ToString();
                Destroy(_colision.gameObject);
            }
        }
    }

    // ****************** Metodos Nuestros ****************** //
    private bool esInstanciaPrefab(GameObject _obj_go)
    {
        return _obj_go != null && _prefab_go != null && _obj_go.name.Contains(_prefab_go.name);
    }
}
