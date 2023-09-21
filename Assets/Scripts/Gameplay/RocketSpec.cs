using JumpInSpace.Gameplay.GameplayObjects;
using UnityEngine;

namespace JumpInSpace.Gameplay {

    [CreateAssetMenu(fileName = "Assets/Rockets/Rocket", menuName = "Rocket", order = 1)]
    public class RocketSpec : ScriptableObject {

        [SerializeField]
        string rocketName;

        [SerializeField]
        string id;

        [SerializeField]
        float rocketSpeed = 10f;

        [SerializeField]
        float boostMultiplier = 1.5f;

        [SerializeField]
        float boostTime = 1f;

        [SerializeField]
        float fuelTank;

        [SerializeField]
        AnimationCurve boostCurve;

        [SerializeField]
        Rocket rocketGO;


        [SerializeField]
        int price;

        [SerializeField]
        bool ownedByPlayer;

        public bool OwnedByPlayer {
            set => ownedByPlayer = value;
            get => ownedByPlayer;
        }


        public string RocketName => rocketName;
        public string Id => id;
        public int Price => price;
        public float RocketSpeed => rocketSpeed;
        public float BoostMultiplier => boostMultiplier;
        public float BoostTime => boostTime;
        public float FuelTank => fuelTank;

        public Rocket RocketGO => rocketGO;

    }
}