using MoreMountains.TopDownEngine;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "StopMovement", story: "[StopAIMovement]", category: "Action", id: "b072661f492842074ba337f5f7e40a06")]
public partial class StopMovementAction : Action
{
    [SerializeReference] public BlackboardVariable<AIActionStopMovement> StopAIMovement;

    protected override Status OnStart()
    {
        StopAIMovement.Value.PerformAction();
        return Status.Success;
    }
}

