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
        da�o = Random.Range(2, 10) + (nivel * 1.25f);
        //Debug.Log("Da�o sumar: " + (nivel * 1.25f));

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
