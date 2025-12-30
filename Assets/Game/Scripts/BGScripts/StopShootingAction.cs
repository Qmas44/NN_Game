using MoreMountains.TopDownEngine;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "StopShooting", story: "Stop [Shooting]", category: "Action", id: "b1844b7a7ff9ea844f118a2883be6123")]
public partial class StopShootingAction : Action
{
    [SerializeReference] public BlackboardVariable<AIActionShoot3D> Shooting;

    protected override Status OnStart()
    {
        Shooting.Value.StopShoot();
        return Status.Success;
    }
}

