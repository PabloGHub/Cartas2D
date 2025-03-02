using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public GameObject panelOpciones;
    private Animator _animatorOpciones_anim;

    void Start()
    {
        //panelOpciones.SetActive(false);
        _animatorOpciones_anim = panelOpciones.GetComponent<Animator>();
    }

    public void Iniciar()
    {
        SceneManager.LoadScene(1);
    }

    public void AbrirOpciones()
    {
        //panelOpciones.SetActive(true);

        _animatorOpciones_anim.SetBool("aparecer", true);
        _animatorOpciones_anim.SetBool("irse", false);
    }

    public void CerrarOpciones()
    {
        //panelOpciones.SetActive(false);
        //SceneManager.LoadScene(1);

        _animatorOpciones_anim.SetBool("aparecer", false);
        _animatorOpciones_anim.SetBool("irse", true);
    }

    public void Salir()
    {
        Application.Quit();
    }

    public void volverMenu()
    {
        SceneManager.LoadScene(0);
    }
}