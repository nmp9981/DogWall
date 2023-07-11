using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    private int pages, cur_page;
    Tutorial_Content tuto;
    public Button L,R;
    public GameObject Pages,Dots;
    public List<GameObject> buttons;
    public Text content;
    public Sprite[] dot;

    void Start()
    {

    }

    public void Init()
    {
        tuto = Resources.Load<Tutorial_Content>("Tutorial/Tutorial");
        pages = tuto.Content.Count / 4;
        if(pages == 0)
        {
            L.gameObject.SetActive(false);
            R.gameObject.SetActive(false);
            Pages.SetActive(false);
        }
        cur_page = 0;
        for(int i = 0; i < pages; i++)
        {
            GameObject temp = Instantiate(Dots);
            temp.transform.SetParent(Pages.transform);
            temp.transform.localScale = new Vector3(1,1,1);
            temp.transform.localPosition = new Vector3(0,0,0);
            temp.transform.localPosition += new Vector3((i-pages/2) * 40f,0,0);
            buttons.Add(temp);
        }
        page_update();
    }

    public void Btn_Click(int t)
    {
        L.interactable = true;
        R.interactable = true;
        if(cur_page == 0)
        {
            L.interactable = false;
        }
        else if( cur_page == tuto.Content.Count -1)
        {
            R.interactable = false;
        }
        cur_page += t;
        page_update();
    }

    void page_update()
    {
        content.text = "";
        for(int i = 0; i < 4; i++)
        {
            content.text += (tuto.Content[4 * cur_page + i] + "\n");
        }
        
        for(int i = 0; i < buttons.Count; i++)
        {
            if(i == cur_page)
                buttons[i].GetComponent<Image>().sprite = dot[0];
            else
                buttons[i].GetComponent<Image>().sprite = dot[1];
        }
    }
    
}
