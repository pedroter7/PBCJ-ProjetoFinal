using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FimFase1 : MonoBehaviour
{

    public GameObject Dialog;
    public Text dialogMessage;

    private void Start()
    {
        Dialog.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int moedas = PlayerPrefs.GetInt(ItemKeys.moeda);
            if (moedas >= 3)
            {
                Player player = collision.gameObject.GetComponent<Player>();               
                float finalHealth = player.GetHealth();           
                PlayerPrefs.SetFloat(ItemKeys.playerHealth, finalHealth);
                SceneManager.LoadScene("Fase2");
            }
            else
            {
                string message = string.Format("Você tem apenas " + moedas + " moedas, colete o restante para passar de fase!");
                dialogMessage.text = message;
                Dialog.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Dialog.SetActive(false);
    }

}
