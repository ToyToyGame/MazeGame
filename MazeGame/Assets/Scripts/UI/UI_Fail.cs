using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Fail : MonoBehaviour
{
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
