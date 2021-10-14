using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Vector3 _delta = new Vector3(0.0f, 0.0f, -5.0f);
    [SerializeField]
    GameObject _player = null;

    public void SetPlayer(GameObject player) { _player = player; }


    void Start()
    {
    }

    void LateUpdate()
    {

            if (_player == null )
            {
                return;
            }

            transform.position = _player.transform.position + _delta; // 위치 이동

            transform.LookAt(_player.transform);



    }
}
