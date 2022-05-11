using UnityEngine;
using UnityEngine.SceneManagement;

public class FimFase2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene("Fase3");
    }
}
