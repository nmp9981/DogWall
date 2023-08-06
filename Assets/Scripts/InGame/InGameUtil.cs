using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUtil : MonoBehaviour
{
    public static InGameUtil instance;

    void Awake()
    {
        instance = this.GetComponent<InGameUtil>();
    }

    public void Util(GameObject target, string name)
    {
        StartCoroutine(name, target);
    }

    IEnumerator FadeIn(GameObject target)
    {
        Image targetImage = target.GetComponent<Image>();
        targetImage.color = new Color(1f, 1f, 1f, 0f);

        Color plus = new Color(0, 0, 0, 0.01f);
        WaitForSeconds time = new WaitForSeconds(0.01f);

        targetImage.gameObject.SetActive(true);

        for (int i = 0; i < 100; i++)
        {
            yield return time;
            targetImage.color += plus;
        }

        targetImage.color = Color.white;
    }

    IEnumerator FadeOut(GameObject target)
    {
        Image targetImage = target.GetComponent<Image>();
        targetImage.color = Color.white;

        Color minus = new Color(0, 0, 0, 0.01f);
        WaitForSeconds time = new WaitForSeconds(0.01f);

        for (int i = 0; i < 100; i++)
        {
            yield return time;
            targetImage.color -= minus;
        }

        targetImage.gameObject.SetActive(false);
        targetImage.color = Color.white;
    }
}
