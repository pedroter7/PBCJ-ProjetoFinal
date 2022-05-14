using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Classe do inimigo, herdando um caractere.
/// </summary>
public class Inimigo : Caractere
{

    public bool bossFinal;      // Se o inimigo eh boss final
    public GameObject ItemDropar;   // Um item para dropar na morte

    float pontosVida;   //Sa�de que o inimigo possui
    public int forcaDano; //A quantidade de dano que o inimigo causar�

    Coroutine danoCoroutine;

    //Metodo chamado quando há colisao entre caracteres, adicionando dano ao player
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (danoCoroutine == null)
            {
                danoCoroutine = StartCoroutine(player.DanoCaractere(forcaDano, 1.0f));
            }
        }
    }

    //Metodo ao sair da colisao, parando de dar dano ao player
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (danoCoroutine != null)
            {
                StopCoroutine(danoCoroutine);
                danoCoroutine = null;
            }
        }
    }

    //Quando inimigo é criado, o onEnable é chamado e reseta a vida do inimigo.
    private void OnEnable()
    {
        ResetCaractere();
    }

    //  Recebe dano, e caso chegue na vida com valor 0, mata o player
    public override IEnumerator DanoCaractere(int dano, float intervalo)
    {
        while (true)
        {
            StartCoroutine(FlickerCaractere());
            pontosVida = pontosVida - dano;
            if (pontosVida <= float.Epsilon)
            {
                var posicaoDropar = transform.position;
                KillCaractere();
                if (ItemDropar != null)
                {
                    Instantiate(ItemDropar, posicaoDropar, Quaternion.identity);
                }
                break;
            }
            if (intervalo > float.Epsilon)
            {
                yield return new WaitForSeconds(intervalo);
            }
            else break;
        }
    }

    //reseta a vida do caractere.
    public override void ResetCaractere()
    {
        pontosVida = InicioPontosDano;
    }
}
