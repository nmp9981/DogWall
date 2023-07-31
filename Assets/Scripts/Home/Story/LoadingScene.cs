using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadingScene : MonoBehaviour
{
    // Start is called before the first frame update
    public static string next_scene;
    public Text tooltip_text;
    public Sprite FullDot, EmptyDot;
    //public string input;
    private float loading_time = 1.0f;
    private bool _continue = false;
    private string[] Tips;
    private int cur_page;
    private GameObject tips_parent;
    private List<GameObject> Dots = new List<GameObject>();
    public Image black;
    [Header("페이드 인&아웃 속도")]
    [SerializeField]
    private float fadeinnoutspeed;
    [Header("팁 개수")]
    [SerializeField]
    private int max_tip;
    void Start()
    {
        Init();
        StartCoroutine(LoadScene());
    }

    void Init()
    {
        //MakeObject(input);
        cur_page = 0;
        tips_parent = GameObject.Find("Pages").gameObject;
        Tips = new string[max_tip];
        GameObject dots = tips_parent.transform.GetChild(0).gameObject;
        for(int i =0; i< max_tip; i++)
        {
            GameObject temp = Instantiate(dots);
            temp.transform.SetParent(tips_parent.transform);
            temp.name = "Dots" + (i + 1).ToString();
            temp.transform.localScale = new Vector3(1,1,1);
            temp.transform.localPosition = new Vector3(35 * (i - (max_tip -1 )/2),0,0);
            Dots.Add(temp);
        }
        Destroy(dots);
        Change_Page(0);
    }

    public void Change_Page(int page)
    {
        cur_page += page;
        if(cur_page >= max_tip)
            cur_page= 0;
        else if(cur_page < 0)
            cur_page = max_tip -1;
        for(int i = 0; i< max_tip; i++)
        {
            if(i == cur_page)
                Dots[i].GetComponent<Image>().sprite = FullDot;
            else
                Dots[i].GetComponent<Image>().sprite = EmptyDot;
        }
        tooltip_text.text = Tips[cur_page];
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
            if(idx1 == 0)
                idx1++;

        }  
        Tooltip_Content tool = Resources.Load<Tooltip_Content>("Prefabs/String_Content");
        tool.Content = tips;
    }

    public static void SceneLoad(string dst)
    {
        next_scene = dst;
        SceneManager.LoadScene("LoadingScene");
    }

    public void Continue()
    {
        _continue = true;
    }

    IEnumerator LoadScene()
    {
        black.gameObject.SetActive(true);
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(next_scene);
        op.allowSceneActivation = false;
        Tooltip_Content tips = Resources.Load<Tooltip_Content>("Prefabs/Tooltip");
        int random_num = (int)Random.Range(0,tips.Content.Count-1);
        if(random_num + max_tip - 1 < tips.Content.Count)
        {
            for(int i = 0; i< max_tip; i++)
            {
                Tips[i] = tips.Content[i + random_num];
            }
        }
        else
        {
            int temp = max_tip + random_num -1 - tips.Content.Count;
            for(int i =0;i < max_tip -temp;i++)
            {
                Tips[i] = tips.Content[i];
            }
            for(int i = max_tip - temp -1 ; i < max_tip; i++)
            {
                Tips[i] = tips.Content[i - max_tip + temp + 1];
            }
        }
        string tip = tips.Content[random_num];
        float timer = 0.0f;
        tooltip_text.text = tip;

        float t = 1;
        while(t>0)
        {
            yield return new WaitForSeconds(fadeinnoutspeed);
            black.color = new Color(0,0,0,t);
            t -= fadeinnoutspeed;
            if(t < 0)
            {
                black.gameObject.SetActive(false);
            }
        }

        while(!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
            if(op.progress < 0.9f)
            {
                
            }
            else
            {  
                if(timer > loading_time && _continue)
                {
                    black.gameObject.SetActive(true);
                    t = 0;
                    while(true)
                    {
                        yield return new WaitForSeconds(fadeinnoutspeed);
                        black.color = new Color(0,0,0,t);
                        t += fadeinnoutspeed;
                        if(t > 1)
                            break;
                    }
                    op.allowSceneActivation = true;          
                    yield break;
                }
            }
        }
    }
}
