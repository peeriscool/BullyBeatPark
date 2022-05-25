using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSMExample
{
    public interface IActor
    {
        Transform transform { get; }
        float speed { get; }
    }

    public interface IState
    {
        bool IsActive { get; set; }

        List<Transition> Transitions { get; }
        void Start(IActor actor);
        void Run();
        void Complete();
    }
}