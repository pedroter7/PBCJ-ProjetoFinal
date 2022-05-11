using UnityEngine;
using Cinemachine;

/// <summary>
/// Manager da camera do player
/// </summary>
public class RPGCameraManager : MonoBehaviour
{

    public static RPGCameraManager instanciaCompartilhada = null; //Armazena a instancia unica do cameraManager

    [HideInInspector]
    public CinemachineVirtualCamera virtualCamera; //Recebe a camera virtual para o controle dela.

    //Garante instancia unica para o script
    private void Awake()
    {
        if( instanciaCompartilhada != null && instanciaCompartilhada != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instanciaCompartilhada = this;
        }
        GameObject vCamGameObject = GameObject.FindWithTag("Virtual Camera");
        virtualCamera = vCamGameObject.GetComponent<CinemachineVirtualCamera>();
    }
}
