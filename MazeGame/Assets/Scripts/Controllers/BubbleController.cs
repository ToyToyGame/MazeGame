using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    MonsterController monster;
    void Start()
    {
        monster = Managers.Game.GetMonster().GetComponent<MonsterController>();
    }

    public void StartBubble()
    {
        gameObject.SetActive(true);
    }
    public void EndBubble()
    {
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = monster.transform.position;
    }
}
