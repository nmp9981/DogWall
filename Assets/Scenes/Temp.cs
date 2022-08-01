using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Temp : MonoBehaviour
{
    // Start is called before the first frame update
    DataManager data;
    public InputField A,B,C,D,E,F;
    public Image img;
    string a,b,c,d,e,f;
    void Start()
    {
       data = GameObject.Find("Manager").GetComponent<DataManager>();
       data.Load();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void save()
    {
        a = A.text;
        b = B.text;
        c = C.text;
        d = D.text;
        e = E.text;
        f = F.text;
        data.saveData.list.Add(new Character(img.sprite, a,int.Parse(b),int.Parse(c), d, int.Parse(e), int.Parse(f)));
        img.sprite = null;
        A.text = "";
        B.text = "";
        C.text = "";
        D.text = d;
        E.text = e;
        F.text = f;
    }

    public void Save_Data()
    {
        data.Save();
    }
}
