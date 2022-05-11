using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Objeto script que armazena os valores de um item no jogo e suas caracteristicas.
/// </summary>
[CreateAssetMenu(menuName = "Item")]
public class Item : ScriptableObject
{

    public string NomeObjeto; //Armazena o nome do objeto (Moeda, Coracao, etc...)
    public Sprite sprite; //Armazena a sprite que representa o objeto
    public int quantidade; //A quantia que esse Item possui de tal objeto
    public bool empilhavel; //Define se o item é empilhavel
    public enum TipoItem //Enum dos tipos de itens utilizados no lab
    {
        MOEDA, 
        HEALTH,
        COLAR,
        CHAVE,
        PERGAMINHO,
        TABUA,
        POCAO_PODER
    }

    public TipoItem tipoItem; //Armazena o tipo do do objeto script item
}
