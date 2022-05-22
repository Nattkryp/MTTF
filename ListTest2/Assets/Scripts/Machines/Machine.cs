using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering; //2019 VERSIONS

public class Machine : MonoBehaviour
{

    //Statestuff
    [SerializeField]State state;

    //Machine condition logic variables
    [SerializeField] public float _maxCondition = 100;      //Made public so a Worker need to know what maxCondition is.
    [SerializeField] public float _condition = 100;         //Made public so a worker can know when repair is done. Done at _maxCondition.
    [SerializeField] float _decayRate = 0.07f;
    [SerializeField] float _breakTryCooldown = 5;           //seconds
    [SerializeField] float _breakTryTimer = 3;              //seconds

    //Functionality
    public IndicatorState lightIndicator;
    public float lightIntensity = 5f;

    public AudioSource soundClick;
    public AudioSource soundRunning;

    //Interaction positions
    public Transform operatorStartPos; //Where operator walks to start a machine
    public Transform[] repairPositions;

    public float _repairDifficulty { get; private set; }

    public enum State
    {
        Stopped,
        Running,
        Broken,
        Repairing
    }

    public Transform GetRandomRepairPosition()
    { 
        return repairPositions[Random.Range(0, repairPositions.Length)];
    }

    private void Start()
    {
        state = State.Stopped;
        _condition -= Random.Range(0, 50);  //To avoid having all machines break at once, and a long wait at start of game
        _repairDifficulty = 0.75f;
    }
    public void Update()
    {

        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;

        //timers
        _breakTryTimer -= Time.deltaTime;

        switch (state)
        {

            case State.Stopped:
                soundRunning.Stop();
                soundRunning.loop = false;
                lightIndicator.SetLight(Color.yellow, lightIntensity, false, 0, 0);
                transform.Find("ropes").GetComponent<SpriteRenderer>().enabled = false;
                break;

            case State.Running:
                lightIndicator.SetLight(Color.green, lightIntensity, false, 0, 0);
                if (!soundRunning.isPlaying)
                {
                soundRunning.Play();
                soundRunning.loop = true;
                }
                
                _condition -= _decayRate * Time.deltaTime;

                if (_breakTryTimer <= 0)
                {
                    _breakTryTimer = _breakTryCooldown;
                    TryBreakDown(_condition);
                }
                break;

            case State.Broken:
                soundRunning.Stop();
                soundRunning.loop = false;
                lightIndicator.SetLight(Color.red, lightIntensity, true, 1, 1);
                break;

            case State.Repairing:
                soundRunning.Stop();
                soundRunning.loop = false;
                lightIndicator.SetLight(Color.blue, lightIntensity, false, 0, 0);
                transform.Find("ropes").GetComponent<SpriteRenderer>().enabled = true;
                break;
        }

    }

    public void SetState(State _state) {
        state = _state;
    }
    
    public void Repair(int x)
    {
        

        Debug.Log("Machine repaired for: " + x.ToString());
        if (_condition < _maxCondition)
        {
            AddCondition(x);
        }
    }

    public float GetRepairDifficulty() 
    {
        return _repairDifficulty;
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
        //Debug.Log("Rolled " + riskRoll + " vs " + condition);

        if (riskRoll > condition)
        {
            SetState(state = State.Broken);
            //Play breaking sound

            //Create task (temporary) to be repaired
            RequestFastRepair(1, gameObject);     //generates a task to get the machine running quickly
        }
    }

    //Emergency repair request (limited repair)
    void RequestFastRepair(int prio, GameObject goToBeRepaired)
        {
        GameObject.Find("TaskManager").GetComponent<TaskManagerScript>().CreateTask_RepairMachine(prio, goToBeRepaired, Mathf.CeilToInt(_maxCondition * 0.1f)); //Generate a task that requires a limited (20% of max condition)
        Debug.Log("Machine broke down unplanned, generating a quick repair task of repair: " + _maxCondition * 0.1f);
    }

    //Full repair request (repair to max condition)
    void RequestRepair(int prio, GameObject goToBeRepaired)
    {
        GameObject.Find("TaskManager").GetComponent<TaskManagerScript>().CreateTask_RepairMachine(prio, goToBeRepaired, Mathf.CeilToInt(_maxCondition - _condition)); //Generate a task that requires a repair to reach full condition
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

    public float GetCondition() { 
        return _condition;
    }
    
}

