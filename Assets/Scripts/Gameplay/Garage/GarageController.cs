using System;
using JumpInSpace.Gameplay.Economy;
using JumpInSpace.Gameplay.GameplayObjects;
using Unity;
using UnityEngine;


namespace JumpInSpace.Gameplay.Garage {
    public class GarageController : MonoBehaviour {

        Rocket activeRocket;
        void OnEnable() {
            EconomyManager.Instance.activeRocketChanged += OnActiveRocketChanged;
        }

        void OnDisable() {
            EconomyManager.Instance.activeRocketChanged -= OnActiveRocketChanged;
        }

        void Start() {
            OnActiveRocketChanged(EconomyManager.Instance.ActiveRocket);
        }

        void OnActiveRocketChanged(RocketSpec rocket) {
            if (activeRocket)
                Destroy(activeRocket.gameObject);
            activeRocket = Instantiate(rocket.RocketGO, new Vector3(0, 0, 15), Quaternion.Euler(0, 0, -90));
            activeRocket.LockedControls = true;
        }
    }
}