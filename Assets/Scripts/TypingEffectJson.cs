using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using LitJson;
using System.IO;

public class TypingEffectJson : MonoBehaviour
{
    //UI ���� ����
    public GameObject ui;

    //�����͸� ���� ��ųʸ� ����
    public Dictionary<int, TextLoadTest> dicTextLoadText;

    //IEnumerator ���� ����
    private IEnumerator coroutine;

    //�������� ���� ���� ����
    public int id;
    public string text;

    //�ؽ�Ʈ �����͸� ǥ���� ��
    public Text label;

    //id�� ī��Ʈ�� ���� ����
    private int dex;

    //label�� ������ ���� ����
    public string m_text;

    //if�� ���Թ���
    public bool Dnobs = true;
    public bool test = false;

    //�����ġ ���� ����
    public string serif;

    //������ ����
    public GameObject ChoicePrefab1;
    public GameObject ChoicePrefab2;
    public GameObject ChoicePrefabImage;
    public int count;

    //�̹������� ī��Ʈ ����
    public ChangeImage ci;
    public int changenumber1;
    public int changenumber2;
    public int changenumber3;
    public int changenumber4;
    public int changenumber5;
    public int wantimagenumber1;
    public int wantimagenumber2;
    public int wantimagenumber3;
    public int wantimagenumber4;
    public int wantimagenumber5;

    //����é�� �Ѿ��
    public string scenename;
    public bool nextchaptercheck = false;

    //����� ����
    private GameObject Music;

    //�̹������� ī��Ʈ ����
    void CountChangeImage()
    {
        if (changenumber1 == dex)
        {
            ci.pnum = wantimagenumber1; //�ٲ� �̹��� ��ȣ
            ci.NextImage();
            Debug.Log("���ϴ� ù��° ����");
        }
        else if (changenumber2 == dex)
        {
            ci.pnum = wantimagenumber2; //�ٲ� �̹��� ��ȣ
            ci.NextImage();
            Debug.Log("���ϴ� �ι�° ����");
        }
        else if (changenumber3 == dex)
        {
            ci.pnum = wantimagenumber3; //�ٲ� �̹��� ��ȣ
            ci.NextImage();
            Debug.Log("���ϴ� ����° ����");
        }
        else if (changenumber4 == dex)
        {
            ci.pnum = wantimagenumber4; //�ٲ� �̹��� ��ȣ
            ci.NextImage();
            Debug.Log("���ϴ� �׹�° ����");
        }
        else if (changenumber5 == dex)
        {
            ci.pnum = wantimagenumber5; //�ٲ� �̹��� ��ȣ
            ci.NextImage();
            Debug.Log("���ϴ� �ټ���° ����");
        }

    }

    //������ Ȱ��
    void ShowChoice()
    {
        if (count == this.dex)
        {
            ChoicePrefab1.SetActive(true);
            ChoicePrefab2.SetActive(true);
            ChoicePrefabImage.SetActive(true);
        }
    }

    //Ÿ����ȿ�� �޼ҵ�
    public IEnumerator _typing()
    {
        bool qw = false;
        Debug.Log("��������ȣ : " + dex);
        for (int i = 0; i <= m_text.Length; i++)
        {
            if (!Dnobs)
            {
                Debug.Log("Ÿ���� �ݺ��� ���");
                _endtyping();
                qw = true;
                yield break;
            }
            label.text = m_text.Substring(0, i);
            yield return new WaitForSeconds(0.05f);
        }

        if(!qw)
        { 
            this.dex++;
            test = true;
        }
        
    }

    //�ڷ�ƾ ����
    void _readJson(int i)
    {
        m_text = this.dicTextLoadText[i].text;
        coroutine = _typing();
        StartCoroutine(coroutine);
    }

    //�ڷ�ƾ ����
    void _endJson()
    {
        StopCoroutine(coroutine);
        test = false;
        if (nextchaptercheck == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(scenename);
                GameObject.Destroy(Music);
                Debug.Log("Music OFF");

            }
        }
        else if (Input.GetMouseButtonDown(0))
            ShowChoice();
    }

    void _endtyping()
    {
        label.text = m_text;
        Debug.Log("EndTyping");
        Dnobs = true;
        this.dex++;
        test = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        //�̹��� ��ũ��Ʈ ����
        ci = GameObject.Find("Image").GetComponent<ChangeImage>();

        //������ ��Ȱ��
        this.ChoicePrefab1 = GameObject.Find("Choice1");
        this.ChoicePrefab2 = GameObject.Find("Choice2");
        this.ChoicePrefabImage = GameObject.Find("ChoiceBackImage");
        ChoicePrefab1.SetActive(false);
        ChoicePrefab2.SetActive(false);
        ChoicePrefabImage.SetActive(false);

        this.dicTextLoadText = new Dictionary<int, TextLoadTest>();
        this.dex = 1;

        TextAsset oj = (TextAsset)Resources.Load(serif, typeof(TextAsset));
        var jlist = JsonConvert.DeserializeObject<List<TextLoadTest>>(oj.text);

        //������ ������� ��ųʸ��� ����
        foreach (var data in jlist)
        {
            this.dicTextLoadText.Add(data.id, data);
        }
        _readJson(this.dex);

        //UI���ӿ�����Ʈ �˻�
        ui = GameObject.Find("UI");
        //����������Ʈ �˻�
        Music = GameObject.Find("backgroundMusic");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && test == false)
        {
            Debug.Log("���콺 Ŭ��");
            Dnobs = false;
        }

        if (Input.GetMouseButtonDown(0) && test == true)
        {
            Dnobs = true;
            test = false;
            _readJson(this.dex);
            CountChangeImage();
        }

        if (count == this.dex)
        {
            _endJson();
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape)) //�ڷΰ��� Ű �Է�
            {
                Debug.Log("SceneChange!!");
                SceneManager.LoadScene("Main");
                Debug.Log("Destroy");
                GameObject.Destroy(ui);
            }

        }
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("SceneChange!!");
            SceneManager.LoadScene("Main");
            Debug.Log("Destroy");
            GameObject.Destroy(ui);
        }
    }
}
