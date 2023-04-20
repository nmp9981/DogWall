using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadingScene : MonoBehaviour
{
    // Start is called before the first frame update
    public static string next_scene;
    
    void Start()
    {
        Init();
        StartCoroutine(LoadScene());
    }

    void Init()
    {

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

        float timer = 0.0f;
        
        while(!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
            if(op.progress < 0.9f)
            {
                
            }
            else
            {  
                if(timer > 1f)
                {
                    op.allowSceneActivation = true;          
                    yield break;
                }
            }
        }
    }
}
