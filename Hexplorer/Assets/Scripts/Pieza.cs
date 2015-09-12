using UnityEngine;
using System.Collections;

public class Pieza : MonoBehaviour {

    public GameObject Cruz;
    public bool Estado;
    public int[] Adyacenes;

    public delegate void _tocado (int i);
    public event _tocado OnTocado;
    

	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void CambiarEstado()
    {
        if (Estado)
        {
            transform.position += Vector3.forward;
            Cruz.transform.position += Vector3.back;
            Estado = false;
        }
        else
        {
            Cruz.transform.position += Vector3.forward;
            transform.position += Vector3.back;
            Estado = true;
        }
    }

    void OnMouseDown()
    {
        OnTocado(Adyacenes[0]);
    }

}
