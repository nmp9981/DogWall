using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadingScene : MonoBehaviour
{
    // Start is called before the first frame update
    public static string next_scene;
    public Text tt;
    public string input;
    private float loading_time = 1.0f;
    void Start()
    {
        Init();
        StartCoroutine(LoadScene());
    }

    void Init()
    {

    }
    void MakeObject(string input)
    {
        List<string> tips = new List<string>();
        string temp = "";
        int idx1 = 0, idx2 = 0;
        int length = input.Length;

        while(input.Contains("."))
        {
            idx2 = input.IndexOf(".");
            temp = input.Substring(idx1,idx2+1);
            input = input.Remove(idx1,idx2+1);
            tips.Add(temp);
        }  
        Tooltip_Content tool = Resources.Load<Tooltip_Content>("Prefabs/Tooltip");
        tool.Content = tips;
    }

    public static void SceneLoad(string dst)
    {
        next_scene = dst;
        SceneManager.LoadScene("LoadingScene");
    }

    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(next_scene);
        op.allowSceneActivation = false;
        Tooltip_Content tips = Resources.Load<Tooltip_Content>("Prefabs/Tooltip");
        string tip = tips.Content[(int)Random.Range(0,tips.Content.Count-1)];
        float timer = 0.0f;
        tt.text = tip;
        while(!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
            if(op.progress < 0.9f * loading_time)
            {
                
            }
            else
            {  
                if(timer > loading_time)
                {
                    op.allowSceneActivation = true;          
                    yield break;
                }
            }
        }
    }
}
