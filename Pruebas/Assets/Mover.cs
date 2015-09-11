using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Mover : MonoBehaviour {

    public float Radio = 2;
    public float SqrSize = 0.5f;
    public int NumTrig = 9; 
    private List<Vector3> Direcciones;

	// Use this for initialization
    void Start()
    {
        Direcciones = new List<Vector3>();
        Quaternion q = Quaternion.Euler(0, 60, 0);
        Direcciones.Add(Vector3.left);
        for (int i = 0; i < 5; i++)
            Direcciones.Add(q * Direcciones[i]);
    }

    void OnMouseDown()
    {
        GameObject nuevo = new GameObject();
        nuevo.transform.parent = this.transform;
        nuevo.AddComponent<MeshFilter>();
        nuevo.AddComponent<MeshRenderer>();
        Mesh mesh = nuevo.GetComponent<MeshFilter>().mesh;
        GenerarHexagono(transform.position, mesh);
    }

    void GenerarHexagono(Vector3 pocision, Mesh mesh)
    {
        List<Vector3> vertices = mesh.vertices.ToList<Vector3>();
        List<Vector2> uvs = mesh.uv.ToList<Vector2>();
        List<int> triangulos = mesh.triangles.ToList<int>();

        int elemXradio = (int)(Radio / SqrSize);

        for (int i = 0; i < 6; i++)
        {
            Vector3 Actual, Siguiente;
            //Preparamos puntos de union.
            Actual = Direcciones[i];
            if (i == 5)
                Siguiente = Direcciones[0];
            else
                Siguiente = Direcciones[i + 1];

            //Calcular rectangulo
            for (int x = 0; x <= NumTrig; x++)
            {
                for (int l = 0; l < elemXradio; l++)
                {
                    Vector3 aux = Vector3.Lerp(Actual * l * SqrSize, Siguiente * l * SqrSize, ((float)x) / ((float) NumTrig));
                    vertices.Add(aux + pocision);
                    uvs.Add(new Vector2(aux.x / (aux.normalized * Radio).x, aux.z / (aux.normalized * Radio).z));
                }
            }
        }
        
        for (int i = 0; i < (6 * (NumTrig + 1)) - 1; i++)
        {
            for (int j = 0; j < elemXradio - 1; j++)
            {
                triangulos.Add(i * elemXradio + j);
                triangulos.Add((i + 1) * elemXradio + j + 1);
                triangulos.Add((i + 1) * elemXradio + j);

                triangulos.Add(i * elemXradio + j);
                triangulos.Add(i * elemXradio + j + 1);
                triangulos.Add((i + 1) * elemXradio + j + 1);
            }
        }

        Debug.Log("Vertices" + vertices.Count);
        Debug.Log("Triangul" + triangulos.Count);

            mesh.vertices = vertices.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.triangles = triangulos.ToArray();
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

}
