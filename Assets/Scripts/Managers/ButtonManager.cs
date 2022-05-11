using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    public void IniciaJogo()
    {
        SceneManager.LoadScene("Fase1");
    }

    public void VaiParaCreditos()
    {
        SceneManager.LoadScene("Creditos");
    }

    public void VaiParaHome()
    {
        SceneManager.LoadScene("Home");
    }

    
}
