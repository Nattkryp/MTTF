using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveTasklistRowData : MonoBehaviour
{
    public int priority;
    public string description = "none";
    public float posX = 0;
    public float posY = 1;
    

    public Text text0;
    public Text text1;
    public Text text2;
    public Text text3;

    public void UpdateValues() {
        text0.text = priority.ToString();
        text1.text = description;
        text2.text = posX.ToString();
        text3.text = posY.ToString();
    }

    public void ClearRow() { 
    Destroy(gameObject, 1);
    }
}
