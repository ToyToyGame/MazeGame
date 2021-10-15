using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    public Action KeyAction = null;

    public void OnUpdate()
    {
        if (KeyAction != null)
            KeyAction.Invoke(); // 누군가 구독중이면 구독중인것을 실행시켜라
    }
}
