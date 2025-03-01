using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Carta : MonoBehaviour
{
    // **** Variables **** //
    public int nivel = 1;
    public int _accion_i = 1; // 0 = cura, pa´lante = ataque.
    public bool _vendiendose_b = true;
    public int _rango_i = 4;

    // Privados visibles.
    public float _cantidad_f = 0f;
    [SerializeField]
    private TextMeshProUGUI _cantidad_text;
    [SerializeField]
    private Image _imagenAtaque_image;
    [SerializeField]
    private Image _imagenCurar_image;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _accion_i = Random.Range(0, _rango_i);

        if (_accion_i == 0)
        {
            _imagenAtaque_image.enabled = false;
            _cantidad_text.color = Color.green;
        }
        else
        {
            _imagenCurar_image.enabled = false;
            _cantidad_text.color = Color.red;
        }


        _cantidad_f = Random.Range(5, 11) + (nivel * Random.Range(1, 4));
        if (_cantidad_text != null)
        {
            _cantidad_text.text = _cantidad_f.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
