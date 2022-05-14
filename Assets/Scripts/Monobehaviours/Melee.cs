using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script para causar dano em area no ataque melee
/// </summary>
public class Melee : MonoBehaviour
{

    public int DanoCausado;     // Dano causado pelo ataque melee

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is BoxCollider2D)
        {
            CausaDano(collision);
        }
    }

    private void CausaDano(Collider2D collider)
    {
        Inimigo inimigo = collider.gameObject.GetComponent<Inimigo>();
        // Caso atingido uma area vulneravel que eh um child gameobject do inimigo
        if (inimigo == null)
        {
            inimigo = collider.gameObject.GetComponentInParent<Inimigo>();
        }
        StartCoroutine(inimigo.DanoCaractere(DanoCausado, 0.0f));
    }
}
