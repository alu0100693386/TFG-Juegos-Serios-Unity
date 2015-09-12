using UnityEngine;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;

public class Controlador_UI : MonoBehaviour {

    void Start()
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
        if (ini == null)
        {
            try
            {
                Debug.Log("Ejecutado");
                XmlSerializer xmls = new XmlSerializer(typeof(Config));
                StreamWriter stream = new StreamWriter(Path.Combine(Application.persistentDataPath, iniPath));
                //StreamWriter stream = new StreamWriter(iniPath);
                xmls.Serialize(stream, new Config("Prueba"));
                stream.Close();
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }
        }
    }

    public void ComprobarDatos(int scena){

        string path = "Personalizacion.xml";
        Personaje pj = null;
        try
        {
            StreamReader reader = new StreamReader(Path.Combine(Application.persistentDataPath, path));
            XmlSerializer lectorXML = new XmlSerializer(typeof (Personaje));
            pj = (Personaje) lectorXML.Deserialize(reader);

        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
        if (pj == null)
        {
            CambiarEscena(1);
        }
        else
        {
            Debug.Log(pj.Name);
            CambiarEscena(2);
        }
    }

    public void CambiarEscena(int scena)
    {
        Application.LoadLevel(scena);
    }

    public void ComprobarPath()
    {
        
    }
}
