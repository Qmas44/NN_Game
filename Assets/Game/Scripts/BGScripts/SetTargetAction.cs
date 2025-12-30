using MoreMountains.TopDownEngine;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetTarget", story: "Set [target] to [DetectedTarget]", category: "Action", id: "f1bf426307f8dbefa991c017abdebb45")]
public partial class SetTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<AIDecisionDetectTargetRadius3D> DetectedTarget;

    protected override Status OnStart()
    {
        if (Target == null || DetectedTarget == null)
            {
                return Status.Failure;
            }
            Target.ObjectValue = DetectedTarget.Value.CurrentTarget.gameObject;
            return Status.Success;
    }
}

