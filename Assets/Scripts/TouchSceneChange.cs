using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchSceneChange : MonoBehaviour
{
    public string scenename;

    private GameObject Music;

    public void SceneChange()
    {
        Debug.Log("SceneChange!!");
        SceneManager.LoadScene(scenename);
    }

    // Start is called before the first frame update
    void Start()
    {
        //UI���ӿ�����Ʈ �˻�
        Music = GameObject.Find("backgroundMusic");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void btnClick()
    {
        GameObject.Destroy(Music);
        Debug.Log("Music OFF");
    }
}
