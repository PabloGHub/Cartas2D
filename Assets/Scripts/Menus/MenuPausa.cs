using UnityEngine;

public class MenuPausa : MonoBehaviour
{
    public void Pausa()
    {
        Debug.Log("Pausa");
        Time.timeScale = 0f;
    } 


}
