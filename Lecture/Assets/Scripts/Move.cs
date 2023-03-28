using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("unity");
    }

    // Update is called once per frame
    void Update()
    {
        
        //transform.Translate(1, 0, 0);
        transform.Translate(1 * Time.deltaTime, 0, 0);
    }
}
