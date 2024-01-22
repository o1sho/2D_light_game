using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oisho.Weapons
{
    public class AnimationEventHandler : MonoBehaviour
    {
        public event Action OnFinished;

        private void AnimationFinishedTrigger() => OnFinished?.Invoke();
    }
}
