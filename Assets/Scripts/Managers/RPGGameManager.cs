using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controla o jogo, player, inimigos e qualquer caractere.
/// </summary>
public class RPGGameManager : MonoBehaviour
{

    public static RPGGameManager instanciaCompartilhada = null; //Manager da cena
    public RPGCameraManager cameraManager; //Manager da camera do player

    public PontoSpawn playerPontoSpawn; //Local onde o player vai spawnar


    //Garante que a instancia � unica, tornando o script em um companion object
    private void Awake()
    {
        if (instanciaCompartilhada != null && instanciaCompartilhada != this)
        {
            Destroy(gameObject);
        } else
        {
            instanciaCompartilhada = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SetupScene();   
    }

    //Configura inicialmente a cena
    public void SetupScene()
    {
        SpawnPlayer();
    }

    //Spawna o player na cena
    public void SpawnPlayer()
    {
        if (playerPontoSpawn != null)
        {
            GameObject player = playerPontoSpawn.SpawnO();
            cameraManager.virtualCamera.Follow = player.transform;
        }
        else print("helou");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
