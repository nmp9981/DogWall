using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tutorial",menuName ="Tutorial Content",order = 1)]
public class Tutorial_Content : ScriptableObject
{
    // Start is called before the first frame update
    public List<string> Content { get { return _content; } set { _content = value; } }

    [Tooltip("튜토리얼 내용(페이지별 추가)")]
    [SerializeField]
    private List<string> _content;
}
