using UnityEngine;

/// <summary>
/// Classe responsavel pelo trigger da municao
/// </summary>
public class Municao : MonoBehaviour
{

    public int DanoCausado;     // Poder de dano da municao

    //Valida colisao da municao com o caractere
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider is BoxCollider2D)
        {
            Inimigo inimigo = collider.gameObject.GetComponent<Inimigo>();
            StartCoroutine(inimigo.DanoCaractere(DanoCausado, 0.0f));
            gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
