using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Define o ponto spawn do caractere
/// </summary>
public class PontoSpawn : MonoBehaviour
{

    public GameObject prefabParaSpawn; //Prefab para spawnar na cena

    public float intervaloRepeticao; //Intervalo de repeticao para spawn (no caso do inimigo)

    // Start is called before the first frame update
    void Start()
    {
       if (intervaloRepeticao > 0)
        {
            InvokeRepeating("SpawnO", 0.0f, intervaloRepeticao);
        }
    }

    //Instancia o prefab no local marcado
    public GameObject SpawnO()
    {
        if (prefabParaSpawn != null)
        {
            return Instantiate(prefabParaSpawn, transform.position, Quaternion.identity);
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
