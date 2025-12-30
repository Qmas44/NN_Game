using MoreMountains.TopDownEngine;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AimAtTarget", story: "[Aim] at target", category: "Action", id: "404b9e8fbb62a4c3735c00a5b6b72296")]
public partial class AimAtTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<AIActionShoot3D> Aim;

    protected override Status OnStart()
    {
        Aim.Value.Aim();
        return Status.Running;
    }
    protected override Status OnUpdate()
    {
        Aim.Value.TestAimAtTarget();
        Aim.Value.Aim();
        return Status.Running;
    }
}

