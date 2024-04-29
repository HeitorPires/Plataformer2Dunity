using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void Load(int scene)
    {
        SceneManager.LoadScene(scene);
    }
    
    public void Load(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    
    public void Unload(int scene)
    {
        SceneManager.UnloadSceneAsync(scene);
    }
    
    public void Unload(string scene)
    {
        SceneManager.UnloadSceneAsync(scene);
    }
}
