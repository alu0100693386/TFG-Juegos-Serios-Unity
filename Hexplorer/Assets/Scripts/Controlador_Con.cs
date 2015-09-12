using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;

public class Controlador_Con : MonoBehaviour {

    public Text Input;

    public void CambiarNivel()
    {
        string iniPath = "Config.xml";
        Config ini = null;
        try
        {
            StreamReader reader = new StreamReader(Path.Combine(Application.persistentDataPath, iniPath));
            XmlSerializer lectorXML = new XmlSerializer(typeof(Config));
            ini = (Config)lectorXML.Deserialize(reader);
            reader.Close();

        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
        if (ini != null)
        {
            try
            {
                XmlSerializer xmls = new XmlSerializer(typeof(Config));
                StreamWriter stream = new StreamWriter(Path.Combine(Application.persistentDataPath, iniPath));
                xmls.Serialize(stream, new Config(Input.text.ToString()));
                stream.Close();
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }
        }
    }

    public void CambiarEscena(int scena)
    {
        Application.LoadLevel(scena);
    }
}
