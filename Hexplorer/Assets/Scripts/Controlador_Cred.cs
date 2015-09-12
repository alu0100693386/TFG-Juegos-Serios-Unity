using UnityEngine;
using System.Collections;

public class Controlador_Cred : MonoBehaviour {

    public void CambiarEscena(int scena)
    {
        Application.LoadLevel(scena);
    }
}
