using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Teclas : MonoBehaviour
{
    // --- Variables --- //
    public TextMeshProUGUI _errores_t;

    public TMP_InputField _izquierda_if;
    public TMP_InputField _derecha_if;
    public TMP_InputField _salto_if;
    public TMP_InputField _accion_if;

    // --- Metodos Unity --- //
    void Start()
    {
        _izquierda_if.text = PlayerPrefs.GetString("izquierda", "A");
        _derecha_if.text = PlayerPrefs.GetString("derecha", "D");
        _salto_if.text = PlayerPrefs.GetString("salto", "W");
        _accion_if.text = PlayerPrefs.GetString("accion", "F");
    }

    // --- Metodos mios --- //
    public void cambiarIzquierda(string _tecla_s)
    {
        _tecla_s.ToUpper();

        if
        (
            _tecla_s == PlayerPrefs.GetString("derecha") ||
            _tecla_s == PlayerPrefs.GetString("salto") ||
            _tecla_s == PlayerPrefs.GetString("accion")
        )
        {
            _errores_t.text = "Tecla ya asignada";
        }
        else
        {
            _errores_t.text = "";
            PlayerPrefs.SetString("izquierda", _tecla_s.ToUpper());
            _izquierda_if.text = _tecla_s.ToUpper();
            PlayerPrefs.Save();
        }
    }

    public void cambiarDerecha(string _tecla_s)
    {
        _tecla_s.ToUpper();

        if
        (
            _tecla_s == PlayerPrefs.GetString("izquierda") ||
            _tecla_s == PlayerPrefs.GetString("salto") ||
            _tecla_s == PlayerPrefs.GetString("accion")
        )
        {
            _errores_t.text = "Tecla ya asignada";
        }
        else
        {
            _errores_t.text = "";
            PlayerPrefs.SetString("derecha", _tecla_s.ToUpper());
            _derecha_if.text = _tecla_s.ToUpper();
            PlayerPrefs.Save();
        }
    }

    public void cambiarSalto(string _tecla_s)
    {
        _tecla_s.ToUpper();

        if
        (
            _tecla_s == PlayerPrefs.GetString("izquierda") ||
            _tecla_s == PlayerPrefs.GetString("derecha") ||
            _tecla_s == PlayerPrefs.GetString("accion")
        )
        {
            _errores_t.text = "Tecla ya asignada";
        }
        else
        {
            _errores_t.text = "";
            PlayerPrefs.SetString("salto", _tecla_s.ToUpper());
            _salto_if.text = _tecla_s.ToUpper();
            PlayerPrefs.Save();
        }
    }

    public void cambiarAccion(string _tecla_s)
    {
        _tecla_s.ToUpper();

        if
        (
            _tecla_s == PlayerPrefs.GetString("izquierda") ||
            _tecla_s == PlayerPrefs.GetString("derecha") ||
            _tecla_s == PlayerPrefs.GetString("salto")
        )
        {
            _errores_t.text = "Tecla ya asignada";
        }
        else
        {
            _errores_t.text = "";
            PlayerPrefs.SetString("accion", _tecla_s.ToUpper());
            _accion_if.text = _tecla_s.ToUpper();
            PlayerPrefs.Save();
        }
    }
}
