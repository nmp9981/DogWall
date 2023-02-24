using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadingScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public static void SceneLoad(string dst)
    {
        SceneManager.LoadScene(dst);
    }
}
