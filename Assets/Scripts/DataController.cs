using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataController : MonoBehaviour
{
    //�̱������� ����
    static GameObject _container;
    static GameObject Container
    {
        get
        {
            return _container;
        }
    }

    static DataController _instance;
    public static DataController Instance
    {
        get
        {
            if (!_instance)
            {
                _container = new GameObject();
                _container.name = "DataController";
                _instance = _container.AddComponent(typeof(DataController)) as DataController;
                DontDestroyOnLoad(_container);
            }
            return _instance;
        }
    }

    //���� ������ �����̸� ����
    public string GameDataFileName = "SaveData.json";
    public GameData _gameData;
    public GameData gameData
    {
        get
        {
            //������ ���۵Ǹ� �ڵ����� ����ǵ���
            if(_gameData == null)
            {
                LoadGameData();
                SaveGameData();
            }
            return _gameData;
        }
    }

    private void Start()
    {
        LoadGameData();
        SaveGameData();
    }

    //����� ���� �ҷ�����
    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + GameDataFileName;
        //����� ������ �ִٸ�
        if(File.Exists(filePath))
        {
            Debug.Log("�ҷ����� ����");
            string FromJsonData = File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<GameData>(FromJsonData);
        }
        //���� �� ������ ���ٸ�
        else
        {
            Debug.Log("���ο� ���� ����");
            _gameData = new GameData();
        }
    }

    //���� �����ϱ�
    public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(gameData);
        string filePath = Application.persistentDataPath + GameDataFileName;
        //�̹� ����� ������ �ִٸ� �����
        File.WriteAllText(filePath, ToJsonData);
        //�ùٸ��� ����ƴ��� Ȯ��
        Debug.Log("����Ϸ�");
        //Debug.Log("2�� " + gameData.isClear2);
        //Debug.Log("3�� " + gameData.isClear3);
    }
    //é�� ��� ����
    /*public void ChapterUnlock(int chapterNumber)
    {
        switch (chapterNumber)
        {
            case 2:
                isClear2 = true;
                dataCtrl.gameData.isClear2 = isClear2;
                dataCtrl.SaveGameData();
                break;
            case 3:
                isClear3 = true;
                dataCtrl.gameData.isClear3 = isClear3;
                dataCtrl.SaveGameData();
                break;
        }
    }*/
    //������ �����ϸ� �ڵ�����ǵ���
    private void OnApplicationQuit()
    {
        SaveGameData();
    }
}
