using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;
using System;

//����
public class AchieveInfo
{
    public int AchieveID;
    public string AchieveTEXT;

    public AchieveInfo(int id, string text)
    {
        AchieveID = id;
        AchieveTEXT = text;
    }
}

public class Achieve : MonoBehaviour
{
    //������ ���� ����
    public string achname;
    public int achinum;

    //���� ���
    public string achipath;

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

    //������ Ŭ���� ���� ���� ����
    public void SaveAchieveInfo()
    {
        _ShowAndroidToastMessage(achname);
        AchieveInfo achieveinfoData = new AchieveInfo(achinum, achname);

        Debug.Log("Save AchieveInfo");

        JsonData infoJson = JsonMapper.ToJson(achieveinfoData);

        File.WriteAllText(Application.persistentDataPath + achipath, infoJson.ToString());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
