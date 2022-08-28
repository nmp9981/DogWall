using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Epilog : MonoBehaviour
{
    // Start is called before the first frame update
    [System.Serializable]
    public class Dialog
    {
        public string character_name;//캐릭터의 이름
        public Character character;
        public string dialog;//대화 내용
        public Dialog(string name = "", string text = "")
        {
            character_name = name;
            character = null;
            dialog = text;
        }
    }
    [System.Serializable]
    public class Story
    {
        public string world;
        public List<Dialog> story;

        public Story()
        {
            world = "";
            story = new List<Dialog>();
        }
        public Story(string world, List<Dialog> story)
        {
            this.world = world;
            this.story = story;
        }
    }

    Story cur_story;
    DataManager dm;
    int idx = 0;
    public Image character_img;
    public Text character_name, content, log;    
    void Start()
    {
        dm = GameObject.Find("Manager").GetComponent<DataManager>();
        cur_story = LoadStory(dm.story);
        if(cur_story.story[0].character == null)
        {
            Find_Teller();
            SaveStory(cur_story,dm.story);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Next()
    {
        character_img.sprite = cur_story.story[idx].character.Img_for_dialog;
        character_name.text = cur_story.story[idx].character_name;
        content.text = cur_story.story[idx].dialog;
        log.text += "(" + cur_story.story[idx].character_name + ")\n" + cur_story.story[idx].dialog + "\n";
        if(idx == cur_story.story.Count)
            SceneManager.LoadScene("Home");
        else
            idx++;
    }

    public void Skip()
    {
        idx = cur_story.story.Count;
        Next();
    }

    public Story LoadStory(string filename)
    {
        //이제 우리가 이전에 저장했던 데이터를 꺼내야한다
        //만약 저장한 데이터가 없다면? 이걸 실행 안하고 튜토리얼을 실행하면 그만이다. 그 작업은 씬로더에서 해준다
        Story gameData;
        string loadPath = Application.dataPath;
        string directory = "/Resources/Data";
        string appender = "/" + filename + ".json";
#if UNITY_EDITOR_WIN

#endif

#if UNITY_ANDROID
        loadPath = Application.persistentDataPath;


#endif
        StringBuilder builder = new StringBuilder(loadPath);
        builder.Append(directory);
        //위까지는 세이브랑 똑같다
        //파일스트림을 만들어준다. 파일모드를 open으로 해서 열어준다. 다 구글링이다
        string builderToString = builder.ToString();
        if (!Directory.Exists(builderToString))
        {
            //디렉토리가 없는경우 만들어준다
            //Directory.CreateDirectory(builderToString);
            Directory.CreateDirectory(builderToString);
        }
        builder.Append(appender);

        if (File.Exists(builder.ToString()))
        {
            //세이브 파일이 있는경우
            FileStream stream = new FileStream(builder.ToString(), FileMode.Open);

            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Close();
            string jsonData = Encoding.UTF8.GetString(bytes);

            //텍스트를 string으로 바꾼다음에 FromJson에 넣어주면은 우리가 쓸 수 있는 객체로 바꿀 수 있다
            gameData = JsonUtility.FromJson<Story>(jsonData);
        }
        else
        {
            //세이브파일이 없는경우
            gameData = new Story();
        }

        return gameData;
        //이 정보를 게임매니저나, 로딩으로 넘겨주는 것이당
    }

    public void Find_Teller()
    {
        List<Character> temp = new List<Character>();
        for(int j = 0; j < cur_story.story.Count; j++)
        {
            bool find = false;
            foreach(Character a in temp)//temp에서 먼저 찾아보고
            {
                if(a.Name == cur_story.story[j].character_name)
                {
                    cur_story.story[j].character = a;
                    find = true;
                    break;
                }
            }
            if(find)//있으면 탈출
                break;
            else//아니면 list에서 찾아보기
            {
                foreach(Character a in dm.saveData.list)
                {
                    if(a.Name == cur_story.story[j].character_name)
                    {
                        cur_story.story[j].character = a;
                        temp.Add(a);
                        break;
                    }
                }
            }
        }
    }

    public void SaveStory(Story saveData, string filename)
    {
        string jsonText;


        //안드로이드에서의 저장 위치를 다르게 해주어야 한다
        //Application.dataPath를 이용하면 어디로 가는지는 구글링 해보길 바란다
        //안드로이드의 경우에는 데이터조작을 막기위해 2진데이터로 변환을 해야한다

        string savePath = Application.dataPath;
        string appender = "/Resources/Data/" + filename + ".json";
#if UNITY_EDITOR_WIN

#endif
#if UNITY_ANDROID
        savePath = Application.persistentDataPath;
        
#endif
        //stringBuilder는 최적화에 좋대서 쓰고있다. string+string은 메모리낭비가 심하다
        // 사실 이정도 한두번 쓰는건 상관없긴한데 그냥 써주자. 우리의 컴은 좋으니까..
        StringBuilder builder = new StringBuilder(savePath);
        builder.Append(appender);
        jsonText = JsonUtility.ToJson(saveData, true);
        //이러면은 일단 데이터가 텍스트로 변환이 된다
        //jsonUtility를 이용하여 data인 WholeGameData를 json형식의 text로 바꾸어준다

        //파일스트림을 이렇게 지정해주고 저장해주면된당 끗
        FileStream fileStream = new FileStream(builder.ToString(), FileMode.Create);
        byte[] bytes = Encoding.UTF8.GetBytes(jsonText);
        fileStream.Write(bytes, 0, bytes.Length);
        fileStream.Close();

    }
}
