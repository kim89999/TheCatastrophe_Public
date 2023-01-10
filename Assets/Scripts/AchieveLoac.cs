using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using LitJson;

public class AchieveLoac : MonoBehaviour
{
    public Text JsonText;

    //���� ���� ���
    public string LoadAchi;

    // Start is called before the first frame update
    void Start()
    {
        string Jsonstring = File.ReadAllText(Application.persistentDataPath + LoadAchi);
        JsonData Jsondata = JsonMapper.ToObject(Jsonstring);
        //Debug.Log(Jsonstring);
        
        //JsonText.text = Jsonstring;
        JsonText.text = Jsondata["AchieveTEXT"].ToString();

        /*TextAsset sourcefile = Resources.Load<TextAsset>(LoadAchi);
        StringReader sr = new StringReader(sourcefile.text);
        Debug.Log(sr);*/
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape)) //�ڷΰ��� Ű �Է�
            {
                Debug.Log("SceneChange!!");
                SceneManager.LoadScene("Main");
            }

        }
    }
}
