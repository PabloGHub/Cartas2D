using UnityEngine;
using TMPro;

public class ControladorDatos : MonoBehaviour
{
    // --------------------- Variables --------------------- //
    private AccesoJSon _acceso_aj;

    public TextMeshProUGUI _maximoNivel_text;
    public TextMeshProUGUI _menedas_text;

    // --------------------- Metodos Unity --------------------- //
    void Awaken()
    {
        
    }

    void Start()
    {
        //_acceso_aj = new AccesoJSon();

        if (_maximoNivel_text != null)
            _maximoNivel_text.text = "Maximo Nivel: " + DarmeNivel().ToString();

        if (_menedas_text != null)
            _menedas_text.text = "Menedas Extra: " + DarmeMenedas().ToString();
    }


    void Update()
    {
        
    }

    // --------------------- Metodos Nuestros --------------------- //
    // Menedas
    public void SumarMeneda()
    {
        /*
        DatosJuga _datosJuga_dj = new DatosJuga();

        _datosJuga_dj = _acceso_aj.DarmeDatos();
        _datosJuga_dj.cantidadMenedas++;
        _acceso_aj.darDatos(_datosJuga_dj);
        */

        PlayerPrefs.SetInt("Menedas", PlayerPrefs.GetInt("Menedas", 0) + 1);
        PlayerPrefs.Save();
    }
    public void Menedas0()
    {
        /*
         DatosJuga _datosJuga_dj = new DatosJuga();

        _datosJuga_dj = _acceso_aj.DarmeDatos();
        _datosJuga_dj.cantidadMenedas = 0;
        _acceso_aj.darDatos(_datosJuga_dj);
        */
    }
    public int DarmeMenedas()
    {
        /*
         DatosJuga _datosJuga_dj = new DatosJuga();
        _datosJuga_dj = _acceso_aj.DarmeDatos();
        return _datosJuga_dj.cantidadMenedas;
        */

        return PlayerPrefs.GetInt("Menedas", 0);
    }


    // Nivel
    public void ComprobarNivel(int _nivelActual_i)
    {
        /*
         DatosJuga _datosJuga_dj = new DatosJuga();

        _datosJuga_dj = _acceso_aj.DarmeDatos();
        _datosJuga_dj.maximoNivel = (_datosJuga_dj.maximoNivel > _nivelActual_i) ? _datosJuga_dj.maximoNivel : _nivelActual_i;
        _acceso_aj.darDatos(_datosJuga_dj);
        */

        PlayerPrefs.SetInt("Nivel", (_nivelActual_i > PlayerPrefs.GetInt("Nivel", 0)) ? _nivelActual_i : PlayerPrefs.GetInt("Nivel", 0));
    }
    public int DarmeNivel()
    {
        /*
         DatosJuga _datosJuga_dj = new DatosJuga();
        _datosJuga_dj = _acceso_aj.DarmeDatos();
        return _datosJuga_dj.maximoNivel;
        */

        return PlayerPrefs.GetInt("Nivel", 0);
    }
}
