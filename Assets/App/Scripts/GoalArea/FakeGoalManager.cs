using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class FakeGoalManager : MonoBehaviour
{
    [SerializeField]
    GoalManager goalManager;

    ReactiveProperty<bool> on = new ReactiveProperty<bool>();
    Rigidbody[] rbs;

    private void Start()
    {
        rbs = GetComponentsInChildren<Rigidbody>();


        on.Where(_ => _).Subscribe(_ =>
       {
           foreach (var x in rbs)
               x.useGravity = true;
       });
    }

    private void Update()
    {
        if (goalManager.isGoal)
        {
            on.Value = true;
        }
    }
}
