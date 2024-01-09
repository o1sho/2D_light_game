using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Player State Variables
    public EntityStateMachine StateMachine { get; private set; }

    // Player States
    public EntityIdleState IdleState { get; private set; }
    public EntityMoveState MoveState { get; private set; }

    #endregion

    #region Entity Components
    public Core Core { get; private set; }
    public Animator Animator { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }

    //[SerializeField] private SO_EntityData entityData;
    [SerializeField] private SO_EntityIdleStateData entityIdleStateData;
    [SerializeField] private SO_EntityMoveStateData entityMoveStateData;

    //[SerializeField] private SO_PlayerData playerData;
    #endregion

    #region Unity Callback Functions
    public virtual void Awake()
    {
        Core = GetComponentInChildren<Core>();

        StateMachine = new EntityStateMachine(); //

        IdleState = new EntityIdleState(this, StateMachine, "idle", entityIdleStateData);
        MoveState = new EntityMoveState(this, StateMachine, "move", entityMoveStateData);
    }

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();

        StateMachine.Initialize(IdleState); //
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
}
