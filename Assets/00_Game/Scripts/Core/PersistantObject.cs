using System;
using UnityEngine;

namespace Cred._00_Game.Scripts.Core {
    public class PersistantObject : MonoBehaviour {
        public GameObject persistantObject;
        bool hasBeenInstantiate;

        void Start() {
            if (hasBeenInstantiate) return;

            InstantiateObject();
            hasBeenInstantiate = true;
        }

        void InstantiateObject() {
            var instance = Instantiate(persistantObject);

            DontDestroyOnLoad(instance);
        }
    }
}