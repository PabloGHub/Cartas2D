using System.IO;
using UnityEngine;
//using Newtonsoft.Json;

public class AccesoJSon 
{
    private string _ruta_s = "Assets/datos/DatosJugador.json";

    public AccesoJSon()
    {
        if (!File.Exists(_ruta_s))
        {
            File.Create(_ruta_s).Close();
        }
    }
    public AccesoJSon(string _ruta_s)
    {
        this._ruta_s = _ruta_s;

        if (!File.Exists(_ruta_s))
        {
            File.Create(_ruta_s).Close();
        }
    }

    public DatosJuga DarmeDatos()
    {
        //string _rutaCompleta_s = Path.Combine(Application.streamingAssetsPath, _ruta_s);
        string _rutaCompleta_s = _ruta_s;

        if (File.Exists(_rutaCompleta_s))
        {
            string _contenido_s = File.ReadAllText(_rutaCompleta_s);
            return JsonUtility.FromJson<DatosJuga>(_contenido_s);
        }
        else
        {
            File.Create(_ruta_s).Close();
            string _contenido_s = File.ReadAllText(_rutaCompleta_s);
            return JsonUtility.FromJson<DatosJuga>(_contenido_s);
        }
    }

    public bool darDatos(DatosJuga _datosGuardar_dj)
    {
        //string _rutaCompleta_s = Path.Combine(Application.streamingAssetsPath, _ruta_s);
        string _rutaCompleta_s = _ruta_s;

        if (File.Exists(_rutaCompleta_s))
        {
            string _contenido_s = JsonUtility.ToJson(_datosGuardar_dj);
            File.WriteAllText(_rutaCompleta_s, _contenido_s);
            return true;
        }
        else
        {
            return false;
        }
    }
}

[System.Serializable]
public class DatosJuga
{
    public int maximoNivel;
    public int cantidadMenedas;
}