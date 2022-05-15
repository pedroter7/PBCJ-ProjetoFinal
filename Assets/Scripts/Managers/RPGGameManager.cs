using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (SceneManager.GetActiveScene().name.Equals("Fase1"))
        {
            ResetPlayerPrefs();
        }
        SpawnPlayer();
    }

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.SetInt(ItemKeys.moeda, 0);
        PlayerPrefs.SetInt(ItemKeys.coxa, 0);
        PlayerPrefs.SetInt(ItemKeys.carne, 0);
        PlayerPrefs.SetInt(ItemKeys.ovo, 0);
        PlayerPrefs.SetInt(ItemKeys.maca, 0);
        PlayerPrefs.SetInt(ItemKeys.queijo, 0);
        PlayerPrefs.SetFloat(ItemKeys.playerHealth, 0);
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
