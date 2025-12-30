using MoreMountains.TopDownEngine;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "RotateToTarget", story: "[Rotate] to Target and [Aim]", category: "Action", id: "9040755b54a6b0624d19b5a2ce58873d")]
public partial class RotateToTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<AIActionRotateTowardsTarget3D> Rotate;
    [SerializeReference] public BlackboardVariable<AIActionAimWeaponAtMovement> Aim;
    protected override Status OnStart()
    {
        Rotate.Value.PerformAction();
        //Aim.Value.PerformAction();
        return Status.Success;
    }
}

