using UnityEngine;
using TMPro;

public class Maletin : MonoBehaviour
{
    // ****************** Declaraciones ****************** //
    public int _numMonedas_i = 0;
    [SerializeField] private TextMeshProUGUI _contador_si;
    private AudioSource _audioSource;

    // ****************** Metodos UNITY ****************** //
    void Start()
    {
           _audioSource = GetComponentInParent<AudioSource>();
        
    }

    void Update()
    {
        _contador_si.text = _numMonedas_i.ToString();
    }

    private void OnTriggerEnter2D(Collider2D _colision)
    {
        if (_colision.gameObject.CompareTag("Entidad"))
        {
            Meneda moneda = _colision.gameObject.GetComponent<Meneda>();
            if (moneda != null)
            {
                _numMonedas_i++;
                _contador_si.text = _numMonedas_i.ToString();

                if (_audioSource != null && _audioSource.clip != null)
                {
                    _audioSource.Play();
                }

                Destroy(_colision.gameObject);
            }
        }
    }
}
