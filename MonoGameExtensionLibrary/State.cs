using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameExtensionLibrary
{
    /// <summary>
    /// State and Exit condition must be defined, exitaction is not needed
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct NextState<T> where T : System.Enum
    {
        public State<T>.ExitCondition exit;
        public State<T> state;
        public State<T>.ExitAction exitAction;

    }
    /// <summary>
    /// An Object that refers to an enumerator state and contains details on how it should check for the next states
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class State<T> where T : System.Enum
    {
        public delegate bool ExitCondition();
        public delegate void ExitAction();

        List<NextState<T>> next;

        T identifier;
        bool flowthrough;
        /// <summary>
        /// Gets the Enum Representation of this State
        /// </summary>
        public T GetStateEnum
        {
            get
            {
                return identifier;
            }
        }
        /// <summary>
        /// Returns whether after entering state it should attempt to immediately test for the next state
        /// </summary>
        public bool FlowThroughToNext
        {
            get
            {
                return flowthrough;
            }
        }
        public State(T identifier, bool flowthrough = true, params NextState<T>[] nexts)
        {
            this.identifier = identifier;
            this.flowthrough = flowthrough;
            next = nexts.OfType<NextState<T>>().ToList();
        }
        /// <summary>
        /// Adds a possible next state
        /// </summary>
        /// <param name="state"></param>
        public void AddNextState(NextState<T> state)
        {
            next.Add(state);
        }
        /// <summary>
        /// Goes through all the next states, checking the next states' exit conditions
        /// </summary>
        /// <param name="current">Returns the Current state</param>
        /// <returns></returns>
        public bool TestForNext(out State<T> current)
        {
            foreach (NextState<T> nextState in next)
            {
                if (nextState.exit.Invoke())
                {
                    nextState.exitAction?.Invoke();
                    current = nextState.state;
                    return true;
                }
            }
            current = this;
            return false;
        }
    }
}
