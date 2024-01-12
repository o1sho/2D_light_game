using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Entity : MonoBehaviour
{
    #region Player State Variables
    protected EntityStateMachine StateMachine;

    // Enity States
    public EntityIdleState IdleState { get; private set; }
    public EntityMoveState MoveState { get; private set; }
    public EntityTakingDamageState TakingDamageState { get; private set; }
    public EntityDetectedState DetectedState { get; private set; }
    public EntityChargeState ChargeState { get; private set; }
    public EntityLookForPlayerState LookForPlayerState { get; private set; }

    public EntityMeleeAttackState MeleeAttackState { get; private set; }  

    #endregion

    #region Entity Components
    public Core Core { get; private set; }
    public Animator Animator { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }

    [SerializeField] protected GameObject hitParticles;

    [SerializeField] private SO_EntityData entityData;

    [SerializeField] private Transform attackPosition;

    #endregion

    #region Types of Behavior
    public enum TypesOfBehavior 
    {
        peaceful,
        agressive
    };
    [SerializeField] private TypesOfBehavior typeOfBehavior;
    public string Behavior { get; private set; }
    #endregion

    #region Unity Callback Functions
    public virtual void Awake()
    {
        Core = GetComponentInChildren<Core>();

        StateMachine = new EntityStateMachine(); //

        IdleState = new EntityIdleState(this, StateMachine, "idle", entityData);
        MoveState = new EntityMoveState(this, StateMachine, "move", entityData);
        TakingDamageState = new EntityTakingDamageState(this, StateMachine, "takingDamage", entityData);
        DetectedState = new EntityDetectedState(this, StateMachine, "detected", entityData);
        ChargeState = new EntityChargeState(this, StateMachine, "charge", entityData);
        LookForPlayerState = new EntityLookForPlayerState(this, StateMachine, "lookForPlayer", entityData);

        MeleeAttackState = new EntityMeleeAttackState(this, StateMachine, "meleeAttack", entityData);

    }

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();

        StateMachine.Initialize(IdleState); //

        Behavior = typeOfBehavior.ToString();
    }

    private void Update()
    {
        Core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    /*
    public void Damage(float amount)
    {
        Debug.Log(transform + " Damaged! " + amount + " Damage taken");
        Instantiate(hitParticles, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        StateMachine.ChangeState(TakingDamageState);
    }
    */
}
