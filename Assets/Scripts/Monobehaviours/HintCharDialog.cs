using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script para controlar o behaviour de NPCs que dao dicas para o player no jogo
/// </summary>
public class HintCharDialog : MonoBehaviour
{
    public GameObject Dialog;   // Dialog=Canvas que exibe o texto de dica
    public string DialogText;   // Texto da dica
    public Text TextComponent;  // Componente onde o texto eh escrito

    // Start is called before the first frame update
    void Start()
    {
        Dialog.SetActive(false); // Evita que o dialogo fique visivel se o player der respawn fora do collider
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TextComponent.text = DialogText;
        Dialog.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Dialog.SetActive(false);
    }

    /// <summary>
    /// Força o update do texto de dica, utilizado quando outros scripts mudam o texto dinamicamente
    /// </summary>
    public void ForceTextUpdate()
    {
        TextComponent.text = DialogText;
    }
}
