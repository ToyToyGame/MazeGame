using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Lobby : MonoBehaviour
{
    public void ButtonClickEventPlay()
    {
        LoadGameScene();
    }
    public void LoadGameScene()
    {
        // TODO
        // 나중에 Lobby Scene Manager 만들어서, Lobby Scene 자리로 옮기던지 해야할 듯..
        string sceneName = System.Enum.GetName(typeof(Define.Scene), Define.Scene.Game);
        SceneManager.LoadScene(sceneName);
    }
}
