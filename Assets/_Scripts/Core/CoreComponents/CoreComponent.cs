using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oisho.CoreSystem
{
    public class CoreComponent : MonoBehaviour
    {
        protected Core core;

        protected virtual void Awake()
        {
            core = transform.parent.GetComponent<Core>();

            if (core == null) { Debug.LogError("No Core on the parent"); }
            core.AddComponent(this);
        }

        public virtual void LogicUpdate() { }
    }
}

