using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe abstrata com caracteristicas b�sicas de todo caractere, na qual todos caracteres devem herdar.
/// </summary>
public abstract class Caractere : MonoBehaviour
{
    public float InicioPontosDano; //Quantidade inicial de pontos dano do caractere
    public float MaxPontosDano; //Quantidade m�xima de pontos dano do caractere

    //método abstrato para resetar caractere
    public abstract void ResetCaractere();

    //Muda cor do caractere por um breve momento, quando leva dano.
    public virtual IEnumerator FlickerCaractere()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    //Metodo abstrado que representa o dano recebido pelo caractere
    public abstract IEnumerator DanoCaractere(int dano, float intervalo);
   
    //Destroi o gameObject do caractere, matando-o
    public virtual void KillCaractere()
    {
        Destroy(gameObject);
    }
}
