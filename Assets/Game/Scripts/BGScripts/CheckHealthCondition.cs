using MoreMountains.TopDownEngine;
using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "CheckHealth", story: " Current [Health] [Operator] [Threshold]", category: "Conditions", id: "727c994f4f780f5f9fcd264a4532ae4e")]
public partial class CheckHealthCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Health> Health;
    [Comparison(comparisonType: ComparisonType.All)]
    [SerializeReference] public BlackboardVariable<ConditionOperator> Operator;
    [SerializeReference] public BlackboardVariable<float> Threshold;

    public override bool IsTrue()
    {
        if (Health.Value == null)
        {
            return false;
        }

        float CurrentHealth = Health.Value.CurrentHealth;
        return ConditionUtils.Evaluate(CurrentHealth, Operator, Threshold.Value);
    }
}
