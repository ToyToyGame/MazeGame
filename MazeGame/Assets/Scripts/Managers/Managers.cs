using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{

    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }

    ResourceManager _resource = new ResourceManager();
    InputManager _input = new InputManager();
    GameManager _game = new GameManager();
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static InputManager Input { get { return Instance._input; } }
    public static GameManager Game { get { return Instance._game; } }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }


    // Update is called once per frame
    void Update()
    {
        _input.OnUpdate();
    }

    static void Init()
    {
        if (s_instance == null)
        {

            GameObject go = GameObject.Find("@Managers");

            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);// »èÁ¦ Èûµé°Ô
            s_instance = go.GetComponent<Managers>();

        }
    }

}
