using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameExtensionLibrary
{
    /// <summary>
    /// Describes a State Machine
    /// </summary>
    /// <typeparam name="States"></typeparam>
    public class StateMachine<States> where States : System.Enum
    {
        State<States> currentState;
        
        /// <summary>
        /// Returns Current State after attempting to Update current state
        /// </summary>
        public States CurrentState
        {
            get
            {
                UpdateCurrentState();
                return currentState.GetStateEnum;
            }
        }

        /// <summary>
        /// Allows access to a State, given the type of the state
        /// </summary>
        public Dictionary<States, State<States>> StateInfo = new Dictionary<States, State<States>>();

        /// <summary>
        /// Basic constructor of the State machine that takes in a lot of states
        /// </summary>
        /// <param name="startState"></param>
        /// <param name="states"></param>
        public StateMachine(State<States> startState, params State<States>[] states)
        {
            currentState = startState;
            StateInfo.Add(startState.GetStateEnum, startState);
            foreach (State<States> current in states)
            {
                if (!StateInfo.ContainsKey(current.GetStateEnum))
                {
                    StateInfo.Add(current.GetStateEnum, current);
                }
                else
                {
                    throw new ArgumentException("States Already exist as StateInfo");
                }
            }
        }
        /// <summary>
        /// Attempts to see if the conditions for the Next State are true and whether it should check again
        /// </summary>
        public void UpdateCurrentState()
        {
            if (currentState.TestForNext(out State<States> c))
            {
                currentState = c;
                if (currentState.FlowThroughToNext)
                {
                    UpdateCurrentState();
                }
            }
        }
    }
}
