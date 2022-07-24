using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turn : MonoBehaviour
{
    public Text test;
    public int turnNumber = 1;


    void Start()
    {
        test.text = "시작";
        battle();
    }

    void battle()
    {
        switch(turnNumber)
        {
            case 1:
                Invoke("player1", 2);
                break;
            case 2:
                Invoke("player2", 2);
                break;
            case 3:
                Invoke("player3", 2);
                break;
            case 4:
                Invoke("player4", 2);
                break;
            case 5:
                Invoke("boss", 2);
                break;
            default:
                test.text = "버그발생";
                break;
        }
    }

    void player1()
    {
        turnNumber += 1;
        test.text = "1";
        battle();
    }

    void player2()
    {
        turnNumber += 1;
        test.text = "2";
        battle();
    }

    void player3()
    {
        turnNumber += 1;
        test.text = "3";
        battle();
    }

    void player4()
    {
        turnNumber += 1;
        test.text = "4";
        battle();
    }

    void boss()
    {
        turnNumber += 1;
        test.text = "5";
        battle();
    }

}