using System;
using System.Collections.Generic;
using JumpInSpace.Utils;
using UnityEngine;

namespace JumpInSpace.Gameplay.GameplayObjects.PowerUps {
    public class PowerUpsController : MonoBehaviour {
        [SerializeField]
        Rocket rocket;

        public Rocket Rocket => rocket;

        List<PowerUp> powerUpList = new List<PowerUp>();
    }
}