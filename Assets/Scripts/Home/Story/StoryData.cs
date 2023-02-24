using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Epilog", menuName = "EpilogData", order = 1)]
public class StoryData : ScriptableObject
{
    public List<string> Name { get { return _name; } set { _name = value; } }
    public List<Sprite> Img { get { return _img; } set { _img = value; } }
    public string Input { get { return _input; } set { _input = value; } }

    [Tooltip("캐릭터 이름")]
    [SerializeField] private List<string> _name;
    [Tooltip("캐릭터 이미지")]
    [SerializeField] private List<Sprite> _img;

    [Tooltip("스토리 이름")]
    [SerializeField] private string _input;

}