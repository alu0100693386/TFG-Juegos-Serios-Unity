using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.IO;
using System;

public class Controlador_Pe: MonoBehaviour
{

    //Patron singleton.
    private static Controlador_Pe _instancia = null;
    public static Controlador_Pe Instancia
    {
        get {return _instancia;}
    }

    public GameObject UIName;
    public GameObject UIPersonaje;
    public Text Nombre;
    public Text Salida;

    public GameObject[] Heads;
    public GameObject[] Bodys;
    public GameObject[] Legss;
    
    //Variables intefaz
    public enum tipoParte { Head, Body, Legs };

    private GameObject[][] Parts;
    private string _name;
    private int[] _person;
    //XmlSerializer xmls = new XmlSerializer(typeof(Personaje));

	void Start () {
        _instancia = this;
        Parts = new GameObject[3][];
        Parts[(int) tipoParte.Head] = Heads;
        Parts[(int) tipoParte.Body] = Bodys;
        Parts[(int) tipoParte.Legs] = Legss;
        _person = new int[3];
        ActivarName();
	}

    public void ActivarName()
    {
        UIName.SetActive(true);
        UIPersonaje.SetActive(false);
    }

    void saveName(){
        _name = Nombre.text;
    }

    public void ActivarPersonaje()
    {
        if (Nombre.text != null)
        {
            saveName();
            ActualizarPersonaje();
            UIName.SetActive(false);
            UIPersonaje.SetActive(true);
        }
        
    }

    public void RotarHead(int mov)
    {
        Rotar(tipoParte.Head, mov);
    }

    public void RotarBody(int mov)
    {
        Rotar(tipoParte.Body, mov);
    }

    public void RotarLegs(int mov)
    {
        Rotar(tipoParte.Legs, mov);
    }

    private void Rotar(tipoParte parte, int mov)
    {
        _person[(int) parte] += mov;
        //Si hemos exedido el numero de partes disponibles (p.e. modelos de camisas), comenzamos de nuevo.
        if (_person[(int)parte] == Parts[(int)parte].Length)
        {
            _person[(int)parte] = 0;
        }
        else
        {
            //Para hacer el sistema circular, si buscamos el modelo anterior al cero, devolvemos el ultimo modelo. 
            if (_person[(int)parte] < 0)
            {
                _person[(int)parte] = (Parts[(int)parte].Length - 1);
            }
        }
        ActualizarPersonaje();
    }

    private void ActualizarPersonaje()
    {
        for (int i = 0; i < 3; ++i)
        {
            for (int j = 0; j < 3; ++j)
            {
                if (_person[i] == j)
                {
                    Parts[i][j].SetActive(true);
                }
                else
                {
                    Parts[i][j].SetActive(false);
                }
            }
        }
    }

    public void Serializar()
    {
        try
        {
            string path = "Personalizacion.xml";
            XmlSerializer xmls = new XmlSerializer(typeof(Personaje));
            StreamWriter stream = new StreamWriter(Path.Combine(Application.persistentDataPath, path));
            xmls.Serialize(stream, new Personaje(_name, _person[(int)tipoParte.Head], _person[(int)tipoParte.Body], _person[(int)tipoParte.Legs]));
            stream.Close();
        }
        catch (Exception e)
        {
            Salida.text = "Error: " + e.ToString();
            ActivarName();
        }
    }

    public void CambiarEscena(int escena)
    {
        Serializar();
        Application.LoadLevel(escena);
    }
}
