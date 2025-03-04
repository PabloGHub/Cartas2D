using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject panelOpciones;
    public Player _jugador;

    private Animator _animatorOpciones_anim;

    void Start()
    {
        if (panelOpciones != null)
        {
            _animatorOpciones_anim = panelOpciones.GetComponent<Animator>();
            _animatorOpciones_anim.SetBool("aparecer", false);
            _animatorOpciones_anim.SetBool("irse", true);
        }
    }


    public void AbrirMenu()
    {
        if (_animatorOpciones_anim.GetBool("aparecer") && !_animatorOpciones_anim.GetBool("irse"))
        {
            _animatorOpciones_anim.SetBool("aparecer", false);
            _animatorOpciones_anim.SetBool("irse", true);
        }
        else
        {
            _animatorOpciones_anim.SetBool("aparecer", true);
            _animatorOpciones_anim.SetBool("irse", false);
        }

        _jugador._pausado_b = !_jugador._pausado_b;
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene(1);
    }

    public void irMenu()
    {
        SceneManager.LoadScene(0);
    }
}
