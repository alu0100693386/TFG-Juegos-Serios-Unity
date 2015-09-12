using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml.Serialization;
using System.Xml;
using System.Linq;
using System.Text.RegularExpressions;

public class Controlador_Ed : MonoBehaviour {

    public GameObject Parte1;
    public Text Name;

    public GameObject Parte2;
    public Text Clue;
    public Text Etiqueta;

    public Button[] Disparadores;

    FileBrowser fb;
    string output = "URL";
    bool active;

    Game Nivel;
    List<string> pistas;
    List<string> fotos;


	void Start () {
        Nivel = new Game();
        pistas = new List<string>();
        fotos = new List<string>();
        active = false;
        Parte1.SetActive(true);
        Parte2.SetActive(false);
        
	}

    public void nombreListo()
    {
        try { 
        Nivel.nombre = Name.text.ToString() ;
        }
        catch (Exception e)
        {
            Debug.Log("Nombre no reconocido");
            Nivel.nombre = "No Name";
        }
        Parte1.SetActive(false);
        Parte2.SetActive(true);
    }

    public void openBrowser()
    {
        fb = new FileBrowser();
        active = true;
        foreach (Button b in Disparadores)
        {
            b.interactable = false;
        }

    }

    void OnGUI()
    {
        if (active)
        {
            if (fb.draw())
            {
                if (fb.outputFile == null)
                {
                    Debug.Log("Cancel hit");
                    output = "No file";
                    foreach (Button b in Disparadores)
                    {
                        b.interactable = true;
                    }
                }
                else
                {
                    Debug.Log("Ouput File = \"" + fb.outputFile.ToString() + "\"");
                    /*the outputFile variable is of type FileInfo from the .NET library "http://msdn.microsoft.com/en-us/library/system.io.fileinfo.aspx"*/
                    output = fb.outputFile.ToString();
                    Etiqueta.text = output.ToString();
                    foreach (Button b in Disparadores)
                    {
                        b.interactable = true;
                    }
                }
                active = false;
            }
        }
    }

	// Update is called once per frame
	void Update () {
	}

    public void SiguienteBaliza()
    {
        GuardarBaliza();
        Clue.text = "";
        Etiqueta.text = "URL";
    }

    void GuardarBaliza()
    {
        fotos.Add(Etiqueta.text.ToString());
        pistas.Add(Regex.Replace(Clue.text.ToString()," ", "_" ));
        Nivel.nFases++;
    }

    public void Finalizar()
    {
        GuardarBaliza();

        // Guardar fotos y eso
        string path = Path.Combine(Name.text.ToString(), "Game.xml");
        Directory.CreateDirectory(Name.text.ToString());
        for (int i = 0; i < Nivel.nFases; i++)
        {
            string subPath = Path.Combine(Name.text.ToString(), "Foto_" + i + ".png");
            CrearImagen(fotos[i], subPath);
            fotos[i] = subPath;
        }

        Nivel.FotoUrl = fotos.ToArray();
        Nivel.Clues = pistas.ToArray();

        try
        {
            XmlSerializer xmls = new XmlSerializer(typeof(Game));
            StreamWriter stream = new StreamWriter(path);
            xmls.Serialize(stream, Nivel);
            stream.Close();
        }
        catch (Exception e)
        {
            Debug.Log("Error!");
        }
    }

    void CrearImagen(string url, string save)
    {

        //Cargar imagen.
        FileStream streamR = new FileStream(url, FileMode.Open);
        BinaryReader reader = new BinaryReader(streamR);
        byte[] buffer = reader.ReadBytes(10000000);
        reader.Close();
        streamR.Close();

        //Crear nueva imagen.
        FileStream streamS = new FileStream(save, FileMode.OpenOrCreate);
        BinaryWriter writer = new BinaryWriter(streamS);
        writer.Write(buffer);
        streamS.Close();
        writer.Close();

    }
}
