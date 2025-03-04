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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AbrirOpciones();
        }
    }

    public void AbrirOpciones()
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
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene(1);
    }
}
