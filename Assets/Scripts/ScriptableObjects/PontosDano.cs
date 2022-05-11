using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Objeto script que armazena o valor que um pontoDano possui (PontosDano sendo uma caracteristica dos itens do jogo, como moedas e coracoes)
/// </summary>
 [CreateAssetMenu(menuName = "PontosDano")]
public class PontosDano : ScriptableObject
{
    public float valor; //Armazena quanto vale o objeto script
}
