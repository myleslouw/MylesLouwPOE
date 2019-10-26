﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum UnitState { ATTACK, MOVE }

public class Unit
{
    public NavMeshAgent navAgent;
    public Transform target;

    public float speed;

    public float attackRange;
    public float coolDownAfterAttack;
    public float attackTime;
    public float chaseDistance; // distance the unit needs before chasing the attacking unit


    private UnitState unit_State;
    public GameObject attackPoint;

    public void Attack(Vector3 unitPosiion)
    {
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;// stops the unit

        attackTime += Time.deltaTime;
        if (attackTime > coolDownAfterAttack)
        {
            attackTime = 0f;
        }

        if (Vector3.Distance(unitPosiion, target.position) > attackRange + chaseDistance)
        {
            navAgent.isStopped = false;
            unit_State = UnitState.MOVE;
        }
    }

    public void Move(Vector3 unitPosiion)
    {
        navAgent.SetDestination(target.position); //sets destination
        navAgent.speed = speed;

        if (Vector3.Distance(unitPosiion, target.position) <= attackRange) //check if enemy target is in range
        {
            unit_State = UnitState.ATTACK;
        }
    }
}