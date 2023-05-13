using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFixed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float setWidth = 1080; // 사용자 설정 너비
        float setHeight = 1920; // 사용자 설정 높이

        float deviceWidth = Screen.width; // 기기 너비 저장
        float deviceHeight = Screen.height; // 기기 높이 저장

        float rate = (setHeight / setWidth);

        if (rate * deviceWidth > deviceHeight)// 가로가 더 긴 상황 (새로에 맞춰야함)
        {
            this.GetComponent<CanvasScaler>().matchWidthOrHeight = 1;
        }
        else // 새로가 더 긴 상황 (가로에 맞춰야함)
        {
            this.GetComponent<CanvasScaler>().matchWidthOrHeight = 0;
        }
    }
}