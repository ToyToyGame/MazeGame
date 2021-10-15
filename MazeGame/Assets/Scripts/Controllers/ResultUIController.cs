using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultUIController : MonoBehaviour
{
    [SerializeField]
    GameObject UI_Fail;
    [SerializeField]
    GameObject UI_Success;

    // Update is called once per frame
    void LateUpdate()
    {
        if (Managers.Game.gameState == Define.GameState.End)
            return;
        if (Managers.Game.gameState == Define.GameState.Fail)
            StartCoroutine(ShowUIAfterSeconds(1.0f, UI_Fail));
        if (Managers.Game.gameState == Define.GameState.Success)
            StartCoroutine(ShowUIAfterSeconds(1.0f, UI_Success));
    }

    IEnumerator ShowUIAfterSeconds(float seconds, GameObject ui)
    {
        Managers.Game.gameState = Define.GameState.End;
        yield return new WaitForSeconds(seconds);
        ui.SetActive(true);

    }
}
