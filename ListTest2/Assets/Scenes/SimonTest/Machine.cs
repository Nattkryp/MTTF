using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering; //2019 VERSIONS

public class Machine : MonoBehaviour
{

    //Statestuff
    [SerializeField] int _orderedState; //used to try change the state
    [SerializeField] int _laststate;    //used to evaluate vs _state to see if state changed
    [SerializeField] int _state;        //1 = Started, 2 = Stopped, 3 = Service, 4 = broken

    //Machine Data
    [SerializeField] string _machineDescription; //not used

    //Statistics
    [SerializeField] float _totTime;    //totTime = downtime + uptime (broken + running time) in seconds.
    [SerializeField] float _downTime;    //accumulated seconds of being broken when planned to be producing
    [SerializeField] float _downTimeP;   //Percentage of downTime divided by planned running-hours
    [SerializeField] float _upTime;     //accumulated time of running
    [SerializeField] float _upTimeP;    //Percentage of Calculated uptime = uptime / planned running-hours

    //Machine condition logic variables
    [SerializeField] public float _maxCondition = 100;      //Made public so a Worker need to know what maxCondition is.
    [SerializeField] public float _condition = 100;         //Made public so a worker can know when repair is done. Done at _maxCondition.
    [SerializeField] float _decayAmount = 0;                //Accumulated decay
    [SerializeField] float _decayRate = 5;
    [SerializeField] float _conditionUpdateInterval = 1;    //seconds
    [SerializeField] float _timerConditionUpdate = 1;       //seconds
    [SerializeField] float _breakTryCooldown = 1;           //seconds
    [SerializeField] float _breakTryTimer = 3;              //seconds

    //Functionality
    public IndicatorState indicatorLight;

    public AudioSource soundClick;
    public AudioSource soundRunning;

    private void Start()
    {
        _state = 1; //Start with running (temporary, should probably require an operator to start it)
    }
    public void Update()
    {
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;

        //Events
        if (_state == 1) {
            ConditionChanges(); //adjust condition based on "run time" - over time decay.
        }

        //State
//        OnStateChanged();
        UpdateState();
        

        //Statistics
        TimeLogging();
        UpdateStatistics();

        _laststate = _state;
    }

    
    public void Repair(int x) {
        if (_condition < _maxCondition)
        {
            AddCondition(x);
        }
        else 
        {
            //Sets state to 1 in another task
            
        }
    }
    public void ApplyOrderedState(int orderedState) {
        _state = orderedState;
        _orderedState = 0;
        //Debug.Log("SetOrderedState ran");
    }

    public void SetOrderedState(int orderedState)
    {
        _orderedState = orderedState;
        //Debug.Log("SetOrderedState ran");
    }

    public void AddCondition(float x)
    {
        _condition += x;
        _condition = Mathf.Clamp(_condition, 0, _maxCondition);

    }

    void ConditionChanges()
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
        else
        {

            return;
        }
    }
    void TryBreakDown(float condition)
    {
        float riskRoll = Random.Range(0f, 100f);


        if (riskRoll > condition)
        {
            SetOrderedState(4); //broken state
                                //play breakdown audio
            //Debug.Log("Risk roll set machine in broken state");

            GameObject.Find("TaskManager").GetComponent<TaskManagerScript>().CreateTask_RepairMachine(1, gameObject);// Added this at this way temporary! FOR TEST!
        }
        //Debug.Log("Breakdown Rolled : " + riskRoll + "vs Condition: " + _condition);
    }
    void UpdateStatistics()
    {
        _totTime = _downTime + _upTime;
        _downTime = _downTime / _totTime;
        _upTime = _upTime / _totTime;
    }
    void TimeLogging()
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
    void OnStateChanged()
    {
       
        
        if (_laststate != _state)
        {
            //Debug.Log("OnStateChanged ran");
            SetLight();
            soundClick.Play();
            if (_state == 1)
            {
                soundRunning.Play();
            }
            else
            {
                soundRunning.Stop();
            }
        }

    }
    void SetLight()
    {
        indicatorLight.SetLight(_state);

    }
    void UpdateState()
    {
        
        //Set state = orderedState if it haven't got a state
        if (_state != _orderedState && _orderedState != 0)
        {
            //Debug.Log("UpdateState tries to run SetorderedState since state is" + _state.ToString() + "and _orderedState is:" + _orderedState.ToString());
            ApplyOrderedState(_orderedState);
            OnStateChanged();
        }

        //Indication

    }
    
}

