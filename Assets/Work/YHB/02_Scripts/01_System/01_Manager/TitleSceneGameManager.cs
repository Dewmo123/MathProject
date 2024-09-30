using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneGameManager : MonoBehaviour
{
    public void HandleChangeScene(int num)
    {
        SceneManager.LoadScene(num);
    }
}
