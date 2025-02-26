using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Carta : MonoBehaviour
{
    // **** Variables **** //
    public int nivel = 1;

    // Privados visibles.
    [SerializeField]
    private float da�o = 0f;
    [SerializeField]
    private TextMeshProUGUI da�oReferencia;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        da�o = Random.Range(5, 10) + (nivel * Random.Range(1, 3));

        if (da�oReferencia != null)
        {
            da�oReferencia.text = da�o.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
