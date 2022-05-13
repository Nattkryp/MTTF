using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TimeController timeController;
    public Text timeText;

    // Start is called before the first frame update
    void Start()
    {
        timeController = GetComponent<TimeController>();
    }

    // Update is called once per frame
    void Update()
    {
        timeText.text = timeController.gameTime;
    }
}
