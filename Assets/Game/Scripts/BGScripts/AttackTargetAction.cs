using MoreMountains.TopDownEngine;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Attack Target", story: "Agent [attacks] current target", category: "Action", id: "8ea88dda1c56ed9769e79d9ac585355f")]
public partial class AttackTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<AIActionShoot3D> Attacks;

    private bool _shooting = true;
    private float _shootTimer = 0f;
    private const float SHOOT_DURATION = 0.5f; // Small delay of 1 second

    protected override Status OnStart()
    {
        _shootTimer = 0f; //Reset shot timer
        Attacks.Value.Shoot();
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        _shootTimer += Time.deltaTime;

        if (_shootTimer >= SHOOT_DURATION)
        {
            Attacks.Value.StopShoot();
            return Status.Success;
        }

        return Status.Running;
    }
}

