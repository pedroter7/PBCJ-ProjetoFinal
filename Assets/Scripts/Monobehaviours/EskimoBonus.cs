using System.Linq;
using UnityEngine;

/// <summary>
/// Script que controla o personagem Eskimo que da bonus para o player se ele tiver coletado todos itens pedidos
/// </summary>
public class EskimoBonus : MonoBehaviour
{

    private static readonly string _textoSucesso = "Parab�ns! Voc� coletou todos os itens que te pedi, portanto voc� pode usar minha po��o m�gica. Ao tomar essa po��o voc� dobrar� sua vida. Boa sorte com o grande frango!";
    private static readonly string _textoFalha = "Voc� n�o coletou todos os itens que te pedi... Voc� pode voltar e colet�-los ou pode enfrentar o grande frango sem a po��o b�nus no estilo old-school!";

    public GameObject PocaoBonus;       // Gameobject para dar ou nao a pocao bonus
    private Player _player;               // Referencia para checar se o player coletou todo o necessario

    private HintCharDialog _dialogo;    // Dialogo do NPC

    // Start is called before the first frame update
    void Start()
    {
        _dialogo = GetComponent<HintCharDialog>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _player = collision.gameObject.GetComponent<Player>();
            bool playerGanhaPocao = JogadorRecebeBonus();
            string textoDialogo = "";
            if (playerGanhaPocao)
            {
                textoDialogo = _textoSucesso;
            }
            else
            {
                textoDialogo = _textoFalha;
            }

            PocaoBonus.SetActive(playerGanhaPocao);
            _dialogo.DialogText = textoDialogo;
            _dialogo.ForceTextUpdate();
        }
        
    }

    private bool JogadorRecebeBonus()
    {
        return _player.Inventario.Itens.All(i => i != null);
    }
}
