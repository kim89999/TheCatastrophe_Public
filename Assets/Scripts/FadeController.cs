using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour
{
    public UnityEngine.UI.Image fade;
    float fades = 1.0f;
    float time = 0f;

    public string scenename;

    void Start()
    {
        
    }

    void Update()
    {
        time += Time.deltaTime;
        if(fades > 0.0f && time >= 0.1f)
        {
            fades -= 0.1f;
            fade.color = new Color(0, 0, 0, fades);
            time = 0;
        } else if (fades <= 0.0f)
        {
            //���������� �Ѿ�ų� ���� �ൿ�� ��
            SceneChange();

            time = 0;
        }
    }

    public void SceneChange()
    {
        Debug.Log("SceneChange!!");
        SceneManager.LoadScene(scenename);
    }
}
