using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPanelManager : MonoBehaviour
{
    public GameObject[] panelTargets;

    private void Start()
    {
        //Link the order of button childs to the order of panels childs, to later be able to activate on index
        if (transform.childCount > 0)
        {
            panelTargets = new GameObject[transform.childCount];

            for (int i = 0; i < transform.childCount; i++)
            {
                panelTargets[i] = transform.GetChild(i).gameObject;
            }
        }
    }

    public void TogglePanel(int indexNumber)
    {
        GameObject panelTarget = panelTargets[indexNumber];
        panelTarget.SetActive(!panelTarget.activeSelf);
    }

}


//masterTaskList.Clear();
//if (transform.childCount > 0)
//{
//    for (int i = 0; i < transform.childCount; i++)
//    {
//        Destroy(transform.GetChild(i).gameObject);
//    }
//}