using UnityEngine;
using System.Collections;

public class Controlador_Fin : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CambiarEscena(int escena)
    {
        Application.LoadLevel(escena);
    }
}
