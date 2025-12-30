using MoreMountains.TopDownEngine;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CheckTarget", story: "Check if [DetectTarget] has a Target and set [CurrentTarget]", category: "Action", id: "6754428ed7226fe04d332aa0ca9c25c2")]
public partial class CheckTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<AIDecisionDetectTargetRadius3D> DetectTarget;
    [SerializeReference] public BlackboardVariable<GameObject> CurrentTarget;
    protected override Status OnStart()
    {
        //return DetectTarget.Value.CurrentTarget == null ? Status.Failure : Status.Success;
        if (DetectTarget.Value.CurrentTarget == null)
        {
            return Status.Failure;
        }
        CurrentTarget.ObjectValue = DetectTarget.Value.CurrentTarget.gameObject;
        return Status.Success;
    }

}

