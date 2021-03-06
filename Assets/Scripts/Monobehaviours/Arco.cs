using System.Collections;
using UnityEngine;

/// <summary>
/// Classe responsavel pela arma ARCO, calculando sua trajetoria.
/// </summary>
public class Arco : MonoBehaviour
{

    //Calcula a trajetoria do tiro do arco
    public IEnumerator ArcoTrajetoria(Vector3 destino, float duracao)
    {
        var posicaoInicial = transform.position;
        var percentualCompleto = 0.0f;
        while (percentualCompleto < 1.0f)
        {
            percentualCompleto += Time.deltaTime / duracao;
            var alturaCorrente = Mathf.Sin(Mathf.PI * percentualCompleto);
            transform.position = Vector3.Lerp(posicaoInicial, destino, percentualCompleto) + Vector3.up*alturaCorrente;
            percentualCompleto += Time.deltaTime / duracao;
            yield return null;
        }
        gameObject.SetActive(false);
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
