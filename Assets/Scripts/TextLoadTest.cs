using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;

public class TextLoadTest : MonoBehaviour
{
    //�����͸� ���� ��ųʸ� ����
    public Dictionary<int, TextLoadTest> dicTextLoadText;

    //�������� ���� ���� ����
    public int id;
    public string text;

    //�ؽ�Ʈ �����͸� ǥ���� ��
    public Text label;

    private int dex;
    
    // Start is called before the first frame update
    void Start()
    {
        this.dicTextLoadText = new Dictionary<int, TextLoadTest>();
        this.dex = 101;

        var oj = Resources.Load("Data/test") as TextAsset;
        
        var jlist = JsonConvert.DeserializeObject<List<TextLoadTest>>(oj.text);

        //������ ������� ��ųʸ��� ����
        foreach(var data in jlist)
        {
            this.dicTextLoadText.Add(data.id, data);
        }
    }

    //�ؽ�Ʈ�� ����ִ� �޼���
    public void ShowText()
    {
        label.text = this.dicTextLoadText[dex].text;
        //this.dex += 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //this.OnGUI();
            if (dex <= 112)
            {
                this.ShowText();
                Debug.Log(dex);
                this.dex += 1;
            }
        }
    }
}
