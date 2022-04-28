using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering; //2019 VERSIONS

public class Machine : MonoBehaviour
{

    //Statestuff
    [SerializeField] int _orderedState; //used to try change the state
    [SerializeField] int _laststate;    //used to evaluate vs _state to see if state changed
    [SerializeField] int _state;        //1 = Started, 2 = Stopped, 3 = Service

    //Machine Data
    [SerializeField] string _machineDescription;

    //Functionality
    public IndicatorState indicatorLight;

    public AudioSource soundClick;



    private void Start()
    {
        if (GetComponent<AudioSource>() != null)
        {
            soundClick = GetComponent<AudioSource>();
        }
        else
        {
            Debug.LogWarning("No click sound component used");
        }



    }

    public void Update()
    {
        //Visuals

        //Audio

        //Task stuff


        //Statestuff
        SetState();
        OnStateChanged();
        _laststate = _state;
    }
    void OnStateChanged()
    {
        if (_laststate != _state)
        {
            soundClick.Play();
            if (_state == 1)          //started - green
            {
                indicatorLight.SetLight(1);
            }
            else if (_state == 2)          //stopped - yellow
            {
                indicatorLight.SetLight(2);
            }
            else if (_state == 3)          //service blue
            {
                indicatorLight.SetLight(3);
            }
            else if (_state == 4)          // broken - red
            {
                indicatorLight.SetLight(4);
            }

        }
    }
    void SetState()
    {
        //Set state if it haven't got a state
        if (_state != _orderedState && _orderedState != 0)
        {
            SetOrderedState(_orderedState);
            Debug.Log("Machine state changed to" + _state.ToString());
        }
        else
        {
            return;
        }
    }
    public void SetOrderedState(int orderedState)
    {
        _orderedState = 0;
        _state = orderedState;
    }


}
