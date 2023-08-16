using UnityEngine;

namespace JumpInSpace.Gameplay.GameplayObjects {
    public class Savable : MonoBehaviour {
        [SerializeField] SaveType saveType;

        public void ApplySave(Vector3 position, Quaternion rotation) {
            switch (saveType) {
                case SaveType.Planet:
                    transform.position = position;
                    transform.rotation = rotation;
                    break;
                case SaveType.Rocket:
                    transform.position = position;
                    transform.rotation = rotation;
                    GetComponent<Rocket>().LaunchRocket();
                    break;
            }
        }
    }

    enum SaveType {
        Planet,
        Rocket
    }
}