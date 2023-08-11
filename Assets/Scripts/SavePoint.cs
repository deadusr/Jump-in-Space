using System;
using UnityEngine;

public class SavePoint : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D other) {
        if (other.transform.TryGetComponent(out Rocket rocket)) {
        }
    }
}