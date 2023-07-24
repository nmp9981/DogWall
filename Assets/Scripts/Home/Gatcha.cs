using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gatcha", menuName = "GatchaContent", order = 1)]
public class Gatcha : ScriptableObject
{
    public Sprite banner { get { return _Banner;} set { _Banner = value;}}
    public bool Empryean { get { return  _Empyrean;} set {_Empyrean = value;}}
    public bool RestrictedArea { get { return  _RestrictedArea;} set {_RestrictedArea = value;}}
    public bool Wulryeong { get { return  _Wulryeong;} set {_Wulryeong = value;}}
    public bool Past{ get { return _Past;} set { _Past = value;}}
    public bool Present{ get { return _Present;} set { _Present = value;}}
    public bool Future{ get { return _Future;} set { _Future = value;}}
    
    
    [Header("배너 이미지")]
    [SerializeField]
    private Sprite _Banner;

    [Header("장소")]
    [SerializeField]
    private bool _Empyrean;
    [SerializeField]
    private bool _RestrictedArea;
    [SerializeField]
    private bool _Wulryeong;

    [Header("시대")]
    [SerializeField]
    private bool _Past;
    [SerializeField]
    private bool _Present;
    [SerializeField]
    private bool _Future;

}
