using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Carta : MonoBehaviour
{
    // **** Variables **** //
    public int nivel = 1;

    // Privados visibles.
    [SerializeField]
    private float daño = 0f;
    [SerializeField]
    private TextMeshProUGUI dañoReferencia;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        daño = Random.Range(2, 10) + (nivel * 1.25f);
        //Debug.Log("Daño sumar: " + (nivel * 1.25f));

        if (dañoReferencia != null)
        {
            dañoReferencia.text = daño.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
