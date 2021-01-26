using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TM.FSM {

    public class FiniteStateMachine { 
        public virtual void         enter()             {}
        public virtual string       update(float dt)    { return ""; }
        public virtual void         exit()              {}
    }

    public class FSMcriptableObject : FiniteStateMachine {}

}
