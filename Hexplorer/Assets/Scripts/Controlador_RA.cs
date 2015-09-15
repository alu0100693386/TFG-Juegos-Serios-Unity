using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using Vuforia;
using System;



public class Controlador_RA : MonoBehaviour {

    public GameObject Boton;
    public GameObject [] Marcadores;
    Game nivel;
	// Use this for initialization
	void Start () {
        try
        {
            StreamReader reader = new StreamReader(Path.Combine(Application.persistentDataPath, Path.Combine( GetPath() ,"Game.xml")));
            //StreamReader reader = new StreamReader(Path.Combine(GetPath(), "Game.xml"));
            XmlSerializer lectorXML = new XmlSerializer(typeof(Game));
            nivel = (Game)lectorXML.Deserialize(reader);
            reader.Close();
        }
        catch (Exception e)
        {
        }

        ActivarMarcador(nivel.Fase);

	}

    void ActivarMarcador(int marc)
    {
        int x = marc % Marcadores.Length;
        for (int n = 0; n < Marcadores.Length; ++n)
        {
            if (n == x)
            {
                Marcadores[n].SetActive(true);
            }
            else
            {
                Marcadores[n].SetActive(false);
            }
        }
    }

    /*
     * Metodos de respuesta a evento VUFORIA
     */ 

    public void MarcadorEncontrado()
    {
        Boton.SetActive(true);
    }

    public void MarcadorPerdido()
    {
        Boton.SetActive(false) ;
    }

    //Metodo de actualización del progreso de juego.
    public void TesoroEncontrado(int escena)
    {

        nivel.Fase = nivel.Fase + 1;

        try
        {
            XmlSerializer xmls = new XmlSerializer(typeof(Game));
            StreamWriter stream = new StreamWriter(Path.Combine(Application.persistentDataPath, Path.Combine( GetPath() ,"Game.xml")));
            //StreamWriter stream = new StreamWriter(Path.Combine(GetPath(), "Game.xml"));
            xmls.Serialize(stream, nivel);
            stream.Close();
            //Debug.Log("!!!!!!!!!!!!!!!!!!!!!! TODO CORRECTO");
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }

        CambiarEscena(escena);
    }

    public void CambiarEscena(int escena)
    {
        Application.LoadLevel(escena);
    }

    string GetPath()
    {
        string iniPath = "Config.xml";
        Config ini = null;
        try
        {
            StreamReader reader = new StreamReader(Path.Combine(Application.persistentDataPath, iniPath));
            XmlSerializer lectorXML = new XmlSerializer(typeof(Config));
            ini = (Config)lectorXML.Deserialize(reader);

        }

        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }

        return ini.juego;
    }
}
