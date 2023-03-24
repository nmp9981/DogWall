using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
public class Epilog2 : MonoBehaviour
{
    private class StoryWithIndex
    {
        public int idx;
        public string content;

        public StoryWithIndex(int _idx, string _content)
        {
            idx = _idx;
            content = _content;
        }
    }

    class Story
    {
        public List<StoryWithIndex> temp = new List<StoryWithIndex>();
    }
    private List<string> Name;
    private List<Sprite> Img;
    private List<StoryWithIndex> Content;
    private int index = 0;

    [Header("이미지")]
    public Image character_img;
    [Header("텍스트")]
    public Text character_name, content, log;

    [Header("TEST")]
    [SerializeField]
    string info;
    void Awake()
    {
        //TODO
        //1. 퀘스트 클래스에서 정보 받기

        //2. 정보에 따라서 맞는 스토리 에셋 찾기
        
        Load(info);
        
        Next();
    }

    public void Next()
    {
        //TODO
        //1. idx >= Content.Count ? 홈으로 돌아가기
        if(Content.Count >= index)
        {
            LoadingScene.SceneLoad("Home");
        }
        //2. idx < Content.Count ? idx++ && 이름,이미지,내용 바꿔주기
        else
        {
            character_name.text = Name[Content[index].idx];
            character_img.sprite = Img[Content[index].idx];
            content.text = Content[index].content;
            log.text += ("/n" + Content[index].content);
            index++;
        }
    }

    private void Load(string name)
    {
        StoryData temp = Resources.Load<StoryData>("Epilog/" + name);
        Name = temp.Name;
        Img  = temp.Img;
        string path = temp.Input;
        string loadPath = Application.dataPath;
        string directory = "/Resources/Epilog";
        string appender = "/" + path + ".json";
#if UNITY_EDITOR_WIN

#endif

#if UNITY_ANDROID
        loadPath = Application.persistentDataPath;


#endif
        StringBuilder builder = new StringBuilder(loadPath);
        builder.Append(directory);
        
        string builderToString = builder.ToString();
        if (!Directory.Exists(builderToString))
        {
            Directory.CreateDirectory(builderToString);
        }
        builder.Append(appender);

        if (File.Exists(builder.ToString()))
        {
            FileStream stream = new FileStream(builder.ToString(), FileMode.Open);

            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Close();
            string jsonData = Encoding.UTF8.GetString(bytes);
            
            Story t = JsonUtility.FromJson<Story>(jsonData);
            Content = t.temp;
        }
        else
            Debug.LogError("Content를 찾을 수 없음!");
    }
}
