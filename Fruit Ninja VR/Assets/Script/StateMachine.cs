using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine
{
    public enum State
    {
        IDLE,
        ANIM,
        SHOOT,
        MOVE
    }

    public enum Command
    {
        StartIdle,
        StartAnim,
        StartShoot,
        StartMove
    }

    public class StateCommand
    {
        readonly State CurrentState;
        readonly Command Command;

        public StateCommand(State currentState, Command command)
        {
            CurrentState = currentState;
            Command = command;
        }

        public override int GetHashCode()
        {
            return 17 + 31 * CurrentState.GetHashCode() + 31 * Command.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            StateCommand other = obj as StateCommand;
            return other != null && this.CurrentState == other.CurrentState && this.Command == other.Command;
        }
    }

    public Dictionary<StateCommand, State> transitions;
    public State currentState;

    public StateMachine()
    {
        currentState = State.IDLE;

        transitions = new Dictionary<StateCommand, State>
        {
            { new StateCommand(State.IDLE, Command.StartShoot), State.SHOOT },
            { new StateCommand(State.SHOOT, Command.StartMove), State.MOVE },
            { new StateCommand(State.SHOOT, Command.StartAnim), State.ANIM },
            { new StateCommand(State.ANIM, Command.StartIdle), State.IDLE },
            { new StateCommand(State.MOVE, Command.StartIdle), State.IDLE }
        };
    }

    private State GetNext(Command command)
    {
        StateCommand transition = new StateCommand(currentState, command);
        State nextState;

        if (!transitions.TryGetValue(transition, out nextState))
            throw new System.Exception("Invalid transition: " + currentState + " -> " + command);

        return nextState;
    }

    public State Next(Command command)
    {
        currentState = GetNext(command);
        OnStateChanged(currentState);
        return currentState;
    }

    public void AI()
    {
        if (currentState == State.IDLE)
        {
            currentState = Next(Command.StartShoot);
        }
        else if (currentState == State.SHOOT)
        {
            int random = UnityEngine.Random.Range(0, 2);
            if(random == 0)
            {
                currentState = Next(Command.StartMove);
            }
            else
            {
                currentState = Next(Command.StartAnim);
            }
        }
        else if(currentState == State.MOVE)
        {
            currentState = Next(Command.StartIdle);
        }
        else if (currentState == State.ANIM)
        {
            currentState = Next(Command.StartIdle);
        }
        else
        {
            currentState = Next(Command.StartIdle);
        }
    }

    public void OnStateChanged(State state)
    {
        if (state == State.IDLE)
        {
            OnStateIDLE();
        }

        if (state == State.MOVE)
        {
            OnStateMOVE();
        }

        if (state == State.SHOOT)
        {
            OnStateSHOOT();
        }

        if (state == State.ANIM)
        {
            OnStateANIM();
        }
    }

    public abstract void OnStateIDLE(); // code for anim idle

    public abstract void OnStateSHOOT(); // code to shoot the player

    public abstract void OnStateANIM(); // code for anim make color

    public abstract void OnStateMOVE(); // code for moving the boss to new place



    //public class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        StateMachine p = new StateMachine();
    //        Console.WriteLine("Current State = " + p.currentState);
    //        Console.WriteLine("Command.Begin: Current State = " + p.Next(Command.Begin));
    //        Console.WriteLine("Command.Pause: Current State = " + p.Next(Command.Pause));
    //        Console.WriteLine("Command.End: Current State = " + p.Next(Command.End));
    //        Console.WriteLine("Command.Exit: Current State = " + p.Next(Command.Exit));
    //        Console.ReadLine();
    //    }
    //}

}
