using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{

    [SerializeField] int _orderedState;
    [SerializeField] int _laststate;
    [SerializeField] int _state;    //1 = Started, 2 = Stopped, 3 = Service
    [SerializeField] string _machineDescription;

    public IndicatorState indicatorLight;


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
        void OnStateChanged() {
            if (_laststate != _state)
            {
                if (_state == 1)          //started - green
                {
                    indicatorLight.SetLight(1);
                }
                else if(_state == 2)          //stopped - yellow
                {
                    indicatorLight.SetLight(2);
                }
                else if (_state == 3)          //service blue
                {
                    indicatorLight.SetLight(3);
                }
                else if (_state == 4)          //stopped
                {
                    indicatorLight.SetLight(4);
                }

        }
        }
        

         void SetState() {
            //Set state if it haven't got a state
            if (_state != _orderedState && _orderedState != 0)
            {
            _state = _orderedState;
            _orderedState = 0;

                Debug.Log("Machine state changed to" + _state.ToString());
            }
            else
            {
            return;
            }
        }

    public void SetOrderedState(int orderedState) {
        _orderedState = 0;
        _orderedState = orderedState;
    }

    
}
