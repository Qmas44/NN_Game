using MoreMountains.TopDownEngine;
using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Hurt", story: "Agent [Health] is hurt", category: "Conditions", id: "6c77f716db89c8d0ab7c8175196c6517")]
public partial class HurtCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Health> Health;
    
    private float _previousHealth;
    private bool _hasInitialized = false;
    
    public override void OnStart()
    {
        base.OnStart();
        if (Health.Value != null && !_hasInitialized)
        {
            _previousHealth = Health.Value.CurrentHealth;
            _hasInitialized = true;
        }
    }

    public override bool IsTrue()
    {
        if (!_hasInitialized || Health.Value == null)
        {
            return false;
        }
        // Check if health has decreased since last check
        if (Health.Value.CurrentHealth < _previousHealth)
        {
            _previousHealth = Health.Value.CurrentHealth;
            return true;
        }

        // Update previous health for next check
        _previousHealth = Health.Value.CurrentHealth;
        return false;
    }
}