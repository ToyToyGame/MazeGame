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
        // ���߿� Lobby Scene Manager ����, Lobby Scene �ڸ��� �ű���� �ؾ��� ��..
        string sceneName = System.Enum.GetName(typeof(Define.Scene), Define.Scene.Game);
        SceneManager.LoadScene(sceneName);
    }
}
