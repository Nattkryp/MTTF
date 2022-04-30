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
    [SerializeField] float _totTime;
    [SerializeField] float _downTime;
    [SerializeField] float _upTime;

    //Machine condition logic variables
    [SerializeField] public float _maxCondition = 100; // Mad public so a Worker need to know what maxCondition is.
    [SerializeField] public float _condition = 100; //Made public so a worker can know when repair is done. Done at _maxCondition.
    [SerializeField] float _decayAmount = 0;                 //Accumulated decay
    [SerializeField] float _decayRate = 5;
    [SerializeField] float _conditionUpdateInterval = 1;   //seconds
    [SerializeField] float _timerConditionUpdate = 1;       //seconds
    [SerializeField] float _breakTryCooldown = 1;        //seconds
    [SerializeField] float _breakTryTimer = 3;            //seconds

    //Functionality
    public IndicatorState indicatorLight;

    public AudioSource soundClick;
    public AudioSource soundRunning;



    public void Update()
    {
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;

        //Events
        if (_state == 1) {
            ConditionChanges(); //adjust condition based on "run time" - over time decay.
        }

        OnStateChanged();
        UpdateState();
        _laststate = _state;

        //timers
        AccumulateTimes();
    }

    public void Repair(int x) {
        if (_condition < _maxCondition)
        {
            AddCondition(x);

        }
        else 
        {
            //Sets state to 1 in another task
            //SetOrderedState(1); //Should in future set "stopped" (2) and require "Operator" to set the machine to running (1). But this is enough for now
        }
    }

    public void AccumulateTimes()
    {
        if (_state == 4)
        {
            _downTime += Time.deltaTime;
        }
        if (_state == 1)
        {
            _upTime += Time.deltaTime;
        }
        _totTime = _downTime + _upTime;
    }
    public void ConditionChanges()
    {
        //timers

        _timerConditionUpdate -= Time.deltaTime;
        _breakTryTimer -= Time.deltaTime;

        //Time accumulated
        _decayAmount += _decayRate * Time.deltaTime;


        if (_timerConditionUpdate <= 0f)
        {
            _timerConditionUpdate = _conditionUpdateInterval;
            AddCondition(-_decayAmount);
            _decayAmount = 0;
        }

        if (_breakTryTimer <= 0f)               //Timer is out but not already broken. // <= 0f && _state != 4
        {

            _breakTryTimer = _breakTryCooldown;
            TryBreakDown(_condition);
            //Debug.Log("Try breakdown roll");
        }
        else {

            return; }
    }

    void TryBreakDown(float condition)
    {
        float riskRoll = Random.Range(0f, 100f);


        if (riskRoll > condition)
        {
            SetOrderedState(4); //broken state
                                //play breakdown audio
            Debug.Log("Risk roll set machine in broken state");

            GameObject.Find("TaskManager").GetComponent<TaskManagerScript>().CreateTask_RepairMachine(1, gameObject);// Added this at this way temporary! FOR TEST!
        }
        Debug.Log("Breakdown Rolled : " + riskRoll + "vs Condition: " + _condition);
    }


    public void UpdateState() {

        //Set state = orderedStateif it haven't got a state
        if (_state != _orderedState && _orderedState != 0)
        {
            SetOrderedState(_orderedState);
        }

        //Indication
        SetLight();

        //Audio
        if (_state == 1)
        {
            soundRunning.Play();
            soundRunning.loop = true;
        }
        else
        {
            soundRunning.Stop();
        }
    }
    void SetLight() {
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
    void OnStateChanged()
    {
        if (_laststate != _state)
        {
            soundClick.Play();

        }
    }


    public void SetOrderedState(int orderedState)
    {
        _orderedState = 0;
        _state = orderedState;
    }

    void AddCondition(float x)
    {
        _condition += x;
        _condition = Mathf.Clamp(_condition, 0, _maxCondition);

    }
}

