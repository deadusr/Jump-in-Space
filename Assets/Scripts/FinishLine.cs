using System;
using UnityEngine;

public class FinishLine : MonoBehaviour {

    [SerializeField]
    LevelFinishedUI levelFinishedUI;

    [SerializeField]
    WinUI winUI;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent<Rocket>(out var launcher)) {
            launcher.Land(transform.position);
            if (GameplayController.Instance.HasNextLevel) {
                levelFinishedUI.ShowPanel();
            }
            else {
                winUI.ShowPanel();
            }
        }
    }


}