using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Epilog : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DataManager dm = GameObject.Find("Manager").GetComponent<DataManager>();
        dm.saveData.Story = new List<Story>();
        dm.saveData.Story[0].story.Add(new Dialog("캐릭터","이런내용"));

        dm.Save();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
