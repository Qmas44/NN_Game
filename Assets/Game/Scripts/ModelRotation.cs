using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MoreMountains.Tools;

public class ModelRotation : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private Vector3 ModelDirection;
    [SerializeField] private float _movementDirectionX;
	[SerializeField] private float _movementDirectionZ;
    protected const string _movementDirectionXAnimationParameterName = "MovementDirectionX";
	protected const string _movementDirectionZAnimationParameterName = "MovementDirectionZ";


    // Update is called once per frame
    void FixedUpdate()
    {
        ModelDirection = agent.desiredVelocity.normalized;

        if(agent.speed != 0)
        {  
            _movementDirectionX = Mathf.Round(ModelDirection.x);
            _movementDirectionZ = Mathf.Round(ModelDirection.z);

            animator.SetFloat(_movementDirectionXAnimationParameterName, _movementDirectionX);
            animator.SetFloat(_movementDirectionZAnimationParameterName, _movementDirectionZ);
        }
    }

}
