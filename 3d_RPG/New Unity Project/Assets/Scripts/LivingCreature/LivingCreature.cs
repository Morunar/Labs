using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody), typeof(Collider), typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public abstract class LivingCreature : MonoBehaviour
{
    public Rigidbody CreatureRB { get; private set; }
    public Collider CreatureCollider { get; private set; }
    public NavMeshAgent CreatureNavMeshAgent { get; private set; }
    public Animator CreatureAnimator { get; private set; }

    public ServiceManager ServiceManager { get; private set; }

    public LivingCreatureActionController ActionController { get; protected set; }

    public Action DestroyHandler = delegate { };


    protected virtual void Start()
    {
        CreatureRB = GetComponent<Rigidbody>();
        CreatureCollider = GetComponent<Collider>();
        CreatureNavMeshAgent = GetComponent<NavMeshAgent>();
        CreatureAnimator = GetComponent<Animator>();
        ServiceManager = ServiceManager.Instance;
    }

    private void OnDestroy()
    {
        DestroyHandler();
    }

    

}
