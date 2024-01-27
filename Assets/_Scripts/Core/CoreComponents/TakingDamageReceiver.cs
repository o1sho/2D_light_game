using UnityEngine;

namespace Oisho.CoreSystem
{
    public class TakingDamageReceiver : CoreComponent, IDamageable
    {
        [SerializeField] private GameObject hitParticles;

        private CoreComp<Stats> stats;
        private CoreComp<ParticleManager> particleManager;

        public bool Damaged { get => damaged; set => damaged = value; }
        [SerializeField] bool damaged;

        public void TakingDamage(float amount)
        {
            damaged = true;
            Debug.Log(core.transform.parent.name + " Damaged! " + amount + " Damage taken");
            particleManager.Comp?.StartParticlesWithRandomRotation(hitParticles);
            stats.Comp?.DecreaseHealth(amount);
        }

        protected override void Awake()
        {
            base.Awake();

            stats = new CoreComp<Stats>(core);
            particleManager = new CoreComp<ParticleManager>(core);
        }
    }
}
