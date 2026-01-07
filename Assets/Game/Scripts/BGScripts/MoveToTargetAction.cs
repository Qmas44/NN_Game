using MoreMountains.TopDownEngine;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MoveToTarget", story: "Agent [MovesToTarget]", category: "Action", id: "9ed2538040f8dabd30db1917ddab8ed5")]
public partial class MoveToTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<AIActionAlwaysMoveTowardsTarget3D> MovesToTarget;
    protected override Status OnStart()
    {
        MovesToTarget.Value.PerformAction();
        return Status.Success;
    }


}

