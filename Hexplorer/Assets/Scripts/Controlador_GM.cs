using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System;

public class Controlador_GM : MonoBehaviour {

    public Text Restantes;

	void Start () {

        StreamReader reader = new StreamReader(Path.Combine(Application.persistentDataPath, Path.Combine( GetPath() ,"Game.xml")));
        //StreamReader reader = new StreamReader(Path.Combine( GetPath() ,"Game.xml"));
        XmlSerializer lectorXML = new XmlSerializer(typeof(Game));
        Game nivel = (Game) lectorXML.Deserialize(reader);
        try
        {
            int restantes = nivel.nFases - nivel.Fase;
            Restantes.text = (restantes).ToString();
            if (restantes <= 0)
            {
                CambiarEscena(5);//Si se ha terminado el juego pasar a escena final
            }
        }
        catch (Exception e) {
            Debug.Log(e.ToString());
        }
        

       // Restantes.text = (nivel.nFases - nivel.Fase).ToString();

	}
	
	void Update () {
	
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
