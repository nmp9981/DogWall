using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "String Content",menuName ="Tooltip Content",order = 2)]
public class Tooltip_Content : ScriptableObject
{
    // Start is called before the first frame update
    public List<string> Content { get { return _content; } set { _content = value; } }

    [Tooltip("툴팁 내용(페이지별 추가)")]
    [SerializeField]
    private List<string> _content;
}