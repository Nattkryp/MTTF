using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
class WorkerListRowData : MonoBehaviour
{
    public Text txtCharacterName;
    public Text txtActivity;
    public Text txtEnergy;

    public void UpdateValues(string characterName, string currentTask, int currentEnergy)
    {
        txtCharacterName.text = characterName;
        txtActivity.text = currentTask;
        txtEnergy.text = currentEnergy.ToString();
    }

    public void ClearRow()
    {
        Destroy(gameObject, 1);
    }
}