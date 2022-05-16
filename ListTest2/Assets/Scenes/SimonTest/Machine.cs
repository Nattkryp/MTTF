using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering; //2019 VERSIONS

public class Machine : MonoBehaviour
{

    //Statestuff
    [SerializeField]State state;
    //MachineManager
    MachineManager manager;
    

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
    [SerializeField] float _decayRate = 0.07f;
    [SerializeField] float _conditionUpdateInterval = 1;    //seconds
    [SerializeField] float _timerConditionUpdate = 1;       //seconds
    [SerializeField] float _breakTryCooldown = 5;           //seconds
    [SerializeField] float _breakTryTimer = 3;              //seconds





    //Functionality
    public IndicatorState lightIndicator;
    public float lightIntensity = 5f;

    public AudioSource soundClick;
    public AudioSource soundRunning;

    //Interaction positions
    public Transform operatorStartPos;

    public enum State
    {
        Stopped,
        Running,
        Broken,
        Repairing
    }

    private void Start()
    {
        state = State.Stopped;
        _condition -= Random.Range(0, 50);

    }
    public void Update()
    {

        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;

        //timers
        _timerConditionUpdate -= Time.deltaTime;
        _breakTryTimer -= Time.deltaTime;

        switch (state)
        {

            case State.Stopped:
                soundRunning.loop = false;
                lightIndicator.SetLight(Color.yellow, lightIntensity, false, 0, 0);
                break;

            case State.Running:
                lightIndicator.SetLight(Color.green, lightIntensity, false, 0, 0);
                soundRunning.Play();
                soundRunning.loop = true;
                _condition -= _decayRate * Time.deltaTime;

                if (_breakTryTimer <= 0)
                {
                    _breakTryTimer = _breakTryCooldown;
                    TryBreakDown(_condition);
                }

                break;

            case State.Broken:
                lightIndicator.SetLight(Color.red, lightIntensity, true, 1, 1);
                break;

            case State.Repairing:
                lightIndicator.SetLight(Color.red, lightIntensity, false, 0, 0);
                break;
        }

        //SetLight(); //Machines state to set the lights accordinglyo
        //soundClick.Play(); //"Press button" sound to be played if worker sets start or stop
    }

    public void SetState(State _state) {
        state = _state;
    }
    
    public void Repair(int x) {
        if (_condition < _maxCondition)
        {
            AddCondition(x);
        }
    }

    public void AddCondition(float x)
    {
        _condition += x;
        _condition = Mathf.Clamp(_condition, 0, _maxCondition);
    }

    void TryBreakDown(float condition)
    {
        float riskRoll = Random.Range(0f, 100f);
        riskRoll -= 30f;
        Debug.Log("Rolled " + riskRoll + " vs " + condition);

        if (riskRoll > condition)
        {
            SetState(state = State.Broken);
            //Play breaking sound

            //Create task (temporary) to be repaired
            GameObject.Find("TaskManager").GetComponent<TaskManagerScript>().CreateTask_RepairMachine(1, gameObject);// Added this at this way temporary! FOR TEST!
        }
    }

    void SetLight()
    {
        //indicatorLight.SetLight(_state);

    }

    public Transform GetOperatorStartPos() {
        return operatorStartPos;
    }

    public State GetState() {
        return state;
    }

    public float GetCondition() { return _condition; }
    
}

