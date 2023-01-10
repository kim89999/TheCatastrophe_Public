using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Breaker : MonoBehaviour
{
    public UIGenerator ug;

    // Start is called before the first frame update
    void Start()
    {
        ug = GameObject.Find("UI").GetComponent<UIGenerator>();
    }

    public void MentalBreaker()
    {
        ug.mentalDamage += 1;
        Debug.Log("��Ż����");
    }

    public void HPBreaker()
    {
        ug.heartDamage += 1;
        Debug.Log("ü�°���");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
