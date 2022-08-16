using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonFileReaders : MonoBehaviour
{
    //파일 경로 읽기
    public static string LoadJsonAsResource(string path)
    {
        string jsonFilePath = path.Replace(".json", "");//파일 형식
        TextAsset loadedJsonFile = Resources.Load<TextAsset>(path);
        return loadedJsonFile.text;
    }
}
