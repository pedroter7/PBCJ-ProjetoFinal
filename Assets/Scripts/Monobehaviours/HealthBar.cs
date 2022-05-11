using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script do prefab health bar, responsável por apresentar a vida do jogador em um hud.
/// </summary>
public class HealthBar : MonoBehaviour
{

    public PontosDano pontosDano; //Armazena o pontosDano de vida do player.
    public Player caractere; //Armazena o caracter Player que esse health bar representa.
    public Image medidorImagem; //Armazena a imagem que representa a vida do personagem.
    public Sprite medidorComBonus;
    public Text pdText; //Armazena o texto que indica a quantidade de vida que o usuario tem, de 0 a 100
    float maxPontosDano;

    // Start is called before the first frame update
    void Start()
    {
        maxPontosDano = caractere.MaxPontosDano; //Salva a quantidade maxima de pontos que o player tem.
    }

    // Update is called once per frame
    void Update()
    {
        if (caractere != null) //Atualiza o texto de pontos dano e a imagem do medidor de vida do personagem.
        {
            var percentual = pontosDano.valor / maxPontosDano;
            medidorImagem.fillAmount = percentual;
            pdText.text = "PD: " + (percentual * 100);
            // Caso jogador com bonus
            if (percentual > 1f)
            {
                medidorImagem.sprite = medidorComBonus;
            }

        }
    }
}
