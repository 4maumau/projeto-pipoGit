using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Monobehaviours
{
    public class SetPassaro : MonoBehaviour
    {
        public AnimatorOverrideController[] overrideControllers;
        [SerializeField] private AnimatorOverrider overrider;

        private int passaroID;

        public void Set(int value)
        {
            overrider.SetAnimations(overrideControllers[value]);
        }

        private void Start()
        {
            overrider = GetComponent<AnimatorOverrider>();

            // rng para spawnar o passaro
            float randomValue = Random.value;
            if (randomValue > 0.9f) passaroID = 2; // cardeal (príncipe)
            else if (randomValue > 0.7f) passaroID = 0; // bem-te-vi
            else if (randomValue > 0.4f) passaroID = 3; // joão de barro
            else passaroID = 1; // pombo

            Set(passaroID);
        }
    }
}
