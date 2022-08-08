using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

public class Temp : MonoBehaviour
{
    // Start is called before the first frame update
    DataManager data;
    public InputField A,B,C,D,E,F,G;
    public Image img;
    string a,b,c,d,e,f,g;
    void Start()
    {
       data = GameObject.Find("Manager").GetComponent<DataManager>();
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
        g = G.text;
        string path = AssetDatabase.GetAssetPath(img.sprite);
        path = path.Replace("Assets/Resources/",string.Empty);
        path = path.Replace(".png",string.Empty);
        Debug.Log("*" + path);
        data.saveData.list.Add(new Character(AssetDatabase.GetAssetPath(img.sprite), a,int.Parse(b),int.Parse(c),int.Parse(g), d, int.Parse(e), int.Parse(f)));
        Debug.Log(AssetDatabase.GetAssetPath(img.sprite));
        img.sprite = null;
        A.text = "";
        B.text = "";
        C.text = "";
        D.text = d;
        E.text = e;
        F.text = f;
        G.text = g;
    }

    public void Save_Data()
    {
        data.Save();
    }
}
