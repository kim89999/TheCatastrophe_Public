﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LitJson;
using System.IO;
using System;

public class TypeWriterEffect : MonoBehaviour
{
    //넘어갈 씬
    public string scenename;

    //변경할 변수
    public float delay;
    public float Skip_delay;
    public int cnt;

    //타이핑효과 변수
    public string[] fulltext;
    public int dialog_cnt;
    public string currentText;
    public string newline;

    //타이핑확인 변수
    public bool text_exit;
    public bool text_full;
    public bool text_cut;

    //업적명 변수 선언
    public string achname;
    public int achinum;

    //업적 경로
    public string achipath;

    //UI Destroy 선언
    public GameObject ui;

    //조건변수
    public bool achievements = false;

    private void _ShowAndroidToastMessage(string message)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        if (unityActivity != null)
        {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, 0);
                toastObject.Call("show");
            }));
        }
    }

    //선택지 클릭시 업적 내용 저장
    public void SaveAchieveInfo()
    {
        _ShowAndroidToastMessage(achname);
        AchieveInfo achieveinfoData = new AchieveInfo(achinum, achname);

        Debug.Log("Save AchieveInfo");

        JsonData infoJson = JsonMapper.ToJson(achieveinfoData);

        File.WriteAllText(Application.persistentDataPath + achipath, infoJson.ToString());
    }

    //시작과 동시에 타이핑시작
    void Start()
    {
        Get_Typing(dialog_cnt,fulltext);
        ui = GameObject.Find("UI");
    }


    //모든 텍스트 호출완료시 탈출
    void Update()
    {
        if (text_full == true)
        {
            End_Typing();
        }

        if (cnt == dialog_cnt)
        {
            if (achievements == true)
            {
                SaveAchieveInfo();
                GameObject.Destroy(ui);
                SceneManager.LoadScene(scenename);
            }
            else
            {
                SceneManager.LoadScene(scenename);
                //GameObject.Destroy(ui);
            }
        }
        
    }

    //다음버튼함수
    public void End_Typing()
    {
        //다음 텍스트 호출
        if (text_full == true)
        {
            cnt++;
            text_full = false;
            text_cut = false;
            StartCoroutine(ShowText(fulltext));
        }
        //텍스트 타이핑 생략
        else
        {
            text_cut = true;
        }
    }

    //텍스트 시작호출
    public void Get_Typing(int _dialog_cnt, string[] _fullText)
    {
        //재사용을 위한 변수초기화
        text_exit = false;
        text_full = false;
        text_cut = false;
        cnt = 0;

        //변수 불러오기
        dialog_cnt = _dialog_cnt;
        fulltext = new string[dialog_cnt];
        fulltext = _fullText;

        //타이핑 코루틴시작
        StartCoroutine(ShowText(fulltext));
    }

    IEnumerator ShowText(string[] _fullText)
    {
        //모든텍스트 종료
        if (cnt >= dialog_cnt)
        {
            text_exit = true;
            StopCoroutine("showText");
        }
        else
        {
            //기존문구clear
            currentText = "";

            for (int i = 0; i < _fullText[cnt].Length; i++)
            {
                //타이핑중도탈출
                if (text_cut == true)
                {
                    break;
                }
                //단어하나씩출력
                this.GetComponent<Text>().text = newline;
                currentText = _fullText[cnt].Substring(0, i + 1);
                this.GetComponent<Text>().text = currentText;
                yield return new WaitForSeconds(delay);
            }
            //탈출시 모든 문자출력
            Debug.Log("Typing 종료");
            this.GetComponent<Text>().text = _fullText[cnt];
            yield return new WaitForSeconds(Skip_delay);

            //스킵_지연후 종료
            Debug.Log("Enter 대기");
            text_full = true;
        }
    }
}
