using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

public class Personajej : MonoBehaviour {
    public GameObject[] Heads;
    public GameObject[] Bodys;
    public GameObject[] Shoess;

    public enum tipoParte { Head, Body, Shoes };

    Character personajeActual;

    void Start()
    {
        personajeActual = new Character(0, 0, 0);
        activarParte(tipoParte.Head, personajeActual.Head);
        activarParte(tipoParte.Body, personajeActual.Body);
        activarParte(tipoParte.Shoes, personajeActual.Shoes);
    }

    public void activarParte(tipoParte parte, int index)
    {
        switch (parte)
        {
            case tipoParte.Head:
                for (int i = 0; i < Heads.Length; i++)
                {
                    if (index == i)
                        Heads[i].SetActive(true);
                    else
                        Heads[i].SetActive(false);
                }
                    break;
            case tipoParte.Body:
                for (int i = 0; i < Bodys.Length; i++)
                    {
                        if (index == i)
                            Bodys[i].SetActive(true);
                        else
                            Bodys[i].SetActive(false);
                    }
                break;
            case tipoParte.Shoes:
                for (int i = 0; i < Shoess.Length; i++)
                {
                    if (index == i)
                        Shoess[i].SetActive(true);
                    else
                        Shoess[i].SetActive(false);
                }
                break;
            default:
                throw new System.Exception("Error al cargar parte.");
        }
    }

    public void  MoverHead(int sentido){
        personajeActual.Head += sentido;
        if (personajeActual.Head < 0)
            personajeActual.Head = Heads.Length - 1;
        if (personajeActual.Head >= Heads.Length)
            personajeActual.Head = 0;
        activarParte(tipoParte.Head, personajeActual.Head);
    }

    public void MoverBody(int sentido)
    {
        personajeActual.Body += sentido;
        if (personajeActual.Body < 0)
            personajeActual.Body = Bodys.Length - 1;
        if (personajeActual.Body >= Bodys.Length)
            personajeActual.Body = 0;
        activarParte(tipoParte.Body, personajeActual.Body);
    }

    public void MoverShoes(int sentido)
    {
        personajeActual.Shoes += sentido;
        if (personajeActual.Shoes < 0)
            personajeActual.Shoes = Shoess.Length - 1;
        if (personajeActual.Shoes >= Shoess.Length)
            personajeActual.Shoes = 0;
        activarParte(tipoParte.Shoes, personajeActual.Shoes);
    }
}

public class Character
{
    public Character(int h, int b, int s)
    {
        Head = h;
        Body = b;
        Shoes = s;
    }

    public int Head;
    public int Body;
    public int Shoes;
}