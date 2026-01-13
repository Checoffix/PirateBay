using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadLevelComponent : MonoBehaviour
{
    public void Reload()
    {
        var session = FindFirstObjectByType<GameSession>();
        session.ReloadData();
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
