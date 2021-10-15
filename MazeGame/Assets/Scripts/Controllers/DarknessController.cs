using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        gameObject.transform.parent = Camera.main.transform;
        gameObject.transform.position = new Vector3(0.0f, 0.0f, 1.0f);//position = Camera.main.ViewportToWorldPoint(new Vector2(0.0f, 0.0f));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
