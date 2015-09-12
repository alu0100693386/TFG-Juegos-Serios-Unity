using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class JuegoDesbloqueo : MonoBehaviour {

    public GameObject[] Imagen;


	void Start () {
        RotarX(3);
        for (int i = 0; i < 9; i++)
        {
            Imagen[i].GetComponent<Pieza>().OnTocado += Pulsado;
        }
            PartirImagen();
	}

    void RotarX(int x)
    {
        for (int i = 0; i < x; i++)
        {
            int random = Random.Range(0, 9);
            Pulsado(random);
        }
    }

    void Pulsado(int index)
    {
        int [] adyacentes = Imagen[index].GetComponent<Pieza>().Adyacenes;
        for (int i = 0; i < adyacentes.Length ; i++)
        {
            Imagen[adyacentes[i]].GetComponent<Pieza>().CambiarEstado();
        }
    }

	void Update () {
        bool Correcto = true;
        for (int i = 0; i < Imagen.Length; i++)
        {
            if (!Imagen[i].GetComponent<Pieza>().Estado)
                Correcto = false;
        }
        if (Correcto)
            Exito();
	}

    void Exito()
    {
    }

    void PartirImagen()
    {
        float tercio = 1.0f / 3;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                AsignarUV(Imagen[i*3+j], tercio * i, tercio*j);
            }
        }
    }

    void AsignarUV(GameObject go, float miny, float minx)
    {
        Mesh mesh = go.GetComponent<MeshFilter>().mesh;
        List<Vector2> UVS = new List<Vector2>();
        float add = 1.0f / 30;
        for (int i = 0; i < 11; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                UVS.Add(new Vector2(minx + (add * (float)j), miny + (add * (float)i)));
            }
        }
        mesh.uv = UVS.ToArray();
    }
}
