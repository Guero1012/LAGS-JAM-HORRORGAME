using UnityEngine;
using UnityEngine.SceneManagement;

public class EsceneLoader : MonoBehaviour
{
    public void LoadSceneByIndex(int index)
    {
        if (index >= 0 && index < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(index);
        }
        else
        {
            Debug.LogError("Índice de escena fuera de rango: " + index);
        }
    }

}
