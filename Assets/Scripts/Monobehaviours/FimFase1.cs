using UnityEngine;
using UnityEngine.SceneManagement;

public class FimFase1 : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene("Fase2");
    }

}
