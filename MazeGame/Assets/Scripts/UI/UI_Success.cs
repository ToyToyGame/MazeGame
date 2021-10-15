using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UI_Success : MonoBehaviour
{
    // Start is called before the first frame update
    public void ButtonClickEventReplay()
    {
        LoadGameScene();
    }
    public void LoadGameScene()
    {
        // TODO
        string sceneName = System.Enum.GetName(typeof(Define.Scene), Define.Scene.Game);
        SceneManager.LoadScene(sceneName);
    }
}
