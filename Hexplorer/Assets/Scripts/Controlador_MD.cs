﻿using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;

public class Controlador_MD : MonoBehaviour {


    public Material foto;
    public Texture2D defTex;
    public Text Pista;
    public GameObject[] Heads;
    public GameObject[] Bodies;
    public GameObject[] Shoes;

	void Start () {

        Game nivel = null;
        StreamReader reader = new StreamReader(Path.Combine(Application.persistentDataPath, Path.Combine(GetPath(), "Game.xml")));
        //StreamReader reader = new StreamReader("Prueba/Game.xml");
        XmlSerializer lectorXML = new XmlSerializer(typeof(Game));
        nivel = (Game)lectorXML.Deserialize(reader);
        reader.Close();


        if (nivel.Fase < nivel.nFases)
        {
            CargarImagen(nivel.FotoUrl[nivel.Fase]);
            Pista.text = Regex.Replace(nivel.Clues[nivel.Fase], "_", " ");
        }

        string path = "Personalizacion.xml";
        Personaje pj = null;
        try
        {
            StreamReader reader2 = new StreamReader(Path.Combine(Application.persistentDataPath, path));
            XmlSerializer lectorXML2 = new XmlSerializer(typeof(Personaje));
            pj = (Personaje)lectorXML2.Deserialize(reader2);
            reader2.Close();
        }
        catch (Exception e)
        {
            //Pista.text = e.ToString();
            Debug.Log(e.ToString());
        }
        if (pj != null)
        {
            activarParte(Heads, pj.Head);
            activarParte(Bodies, pj.Body);
            activarParte(Shoes, pj.Legs);
        }
        
	}

    void activarParte(GameObject[] partes, int parte)
    {
        for (int index = 0; index < partes.Length; ++index)
        {
            if (index == parte)
            {
                partes[index].SetActive(true);
            }
            else
            {
                partes[index].SetActive(false);
            }
        }
    }

    void CargarImagen(string path)
    {
        try
        {
            FileStream stream = new FileStream(Path.Combine(Application.persistentDataPath, path), FileMode.Open);
            //FileStream stream = new FileStream(path, FileMode.Open);
            BinaryReader reader = new BinaryReader(stream);
            byte[] buffer = reader.ReadBytes(10000000);
            Texture2D textura = new Texture2D(1, 1);
            textura.LoadImage(buffer);
            foto.mainTexture = textura;
            stream.Close();
            reader.Close();
        }
        catch (Exception e)
        {
            //Pista.text = e.ToString() ;
        }
       
    }

    public void CargarEscena(int escena)
    {
        Application.LoadLevel(escena);
    }

	// Update is called once per frame
	void Update () {
	
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
            reader.Close();
        }

        catch (Exception e)
        {
            Pista.text = e.ToString();
            Debug.Log(e.ToString());
        }

        return ini.juego;
    }
}
