using UnityEngine;

public class SavePointsController : MonoBehaviour {

    public static SavePointsController Instance;
    void Awake() {
        DontDestroyOnLoad(gameObject);
        MakeInstance();
    }

    void MakeInstance() {
        if (Instance == null) {
            Instance = this;
        }
    }

    void SavePoint() {
        
    }
    
    
}