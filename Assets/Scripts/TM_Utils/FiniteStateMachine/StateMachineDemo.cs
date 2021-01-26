using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TM.FSM;


public class BossIdleFSM : FiniteStateMachine {
    float       m_Timer = 3.0f;
    Animator    m_Animator;
    GameObject  m_Player;

    public BossIdleFSM(Animator animator, GameObject player) {
        m_Animator  = animator;
        m_Player    = player;
    }
/*
    public override void execute(float deltaTime) {
        switch (m_SubState) {
            case eSubStates.enter:
                enter();
                break;
            case eSubStates.update:
                update(deltaTime);
                break;
            case eSubStates.exit:
                exit();
                break;
        }
    }
*/
    public override void enter() {}
    
    public override string update(float deltaTime) {
        string state = "success";
        m_Timer -= deltaTime;
        if ( m_Timer <0.0001f ) {
            state = "walk";
        }
        return state;
    }
    public override void exit() {}
}

//--------------------------------------

public class BossWalkFSM : FiniteStateMachine {
    float       m_Timer = 3.0f;
    Animator    m_Animator;
    GameObject  m_Player;

    public BossWalkFSM( Animator animator, GameObject player) {
        m_Animator  = animator;
        m_Player    = player;
    }
/*
    public override void execute(float deltaTime) {
        switch (m_SubState) {
            case eSubStates.enter:
                enter();
                break;
            case eSubStates.update:
                update(deltaTime);
                break;
            case eSubStates.exit:
                exit();
                break;
        }
    }
*/
    public override void enter() {}
    
    public override string update(float deltaTime) {
        string state = "success";
        m_Timer -= deltaTime;
        if ( m_Timer <0.0001f ) {
            state = "idle";
        }
        return state;
    }
    public override void exit() {}
}

//--------------------------------------
// Boss Controller
public class StateMachineDemo : MonoBehaviour
{
    public  Dictionary<string, FiniteStateMachine> m_StateDictionary = new Dictionary<string, FiniteStateMachine>();
    Animator                m_Animator;
    GameObject              m_Player;
    FiniteStateMachine      m_State;
    BossIdleFSM             m_BossIdle;
    BossWalkFSM             m_BossWalk;

    void Start()
    {
        m_Animator  = transform.GetComponent<Animator>();
        m_Player    = transform.gameObject;
        m_BossIdle  = new BossIdleFSM(m_Animator,m_Player);
        m_BossWalk  = new BossWalkFSM(m_Animator,m_Player);

        m_StateDictionary.Add("idle", m_BossIdle);
        m_StateDictionary.Add("walk", m_BossWalk);

        m_State     = m_BossIdle;
    }

    void Update()
    {
        if ( m_State != null) {
            string newState = m_State.update(Time.deltaTime);

            if (m_StateDictionary.ContainsKey(newState)) {
                m_State.exit();
                m_State = m_StateDictionary[newState];
                m_State.enter();
            }
        } else {    Debug.Log("m_State NULL: "+m_State);    }
    }
}
