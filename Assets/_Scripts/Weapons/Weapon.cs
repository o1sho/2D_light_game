using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oisho.Utilities;

namespace Oisho.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private int numberOfAttacks;
        [SerializeField] private float attackCounterResetCooldown;

        public int CurrentAttackCounter
        {
            get => currentAttackCounter;
            private set => currentAttackCounter = value >= numberOfAttacks ? 0 : value;
        }

        public event Action OnExit;

        private Animator anim;
        private GameObject baseGameObject;

        private AnimationEventHandler eventHandler;

        private int currentAttackCounter;

        private Timer attackCounterResetTimer;

        public void Enter()
        {
            print($"{transform.name} enter");

            attackCounterResetTimer.StopTimer();

            anim.SetBool("active", true);
            anim.SetInteger("counter", CurrentAttackCounter);
        }

        private void Exit()
        {
            anim.SetBool("active", false);

            CurrentAttackCounter++;
            attackCounterResetTimer.StartTimer();

            OnExit?.Invoke();
        }

        private void Awake()
        {
            baseGameObject = transform.Find("Base").gameObject;
            anim = baseGameObject.GetComponent<Animator>();

            eventHandler = baseGameObject.GetComponent<AnimationEventHandler>();

            attackCounterResetTimer = new Timer(attackCounterResetCooldown);
        }

        private void Update()
        {
            attackCounterResetTimer.Tick();
        }

        private void ResetAttackCounter() => currentAttackCounter = 0;

        private void OnEnable()
        {
            eventHandler.OnFinished += Exit;
            attackCounterResetTimer.OnTimerDone += ResetAttackCounter;
        }

        private void OnDisable()
        {
            eventHandler.OnFinished -= Exit;
            attackCounterResetTimer.OnTimerDone -= ResetAttackCounter;
        }
    }
}
