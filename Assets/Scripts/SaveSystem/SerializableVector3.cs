using UnityEngine;
using System.Collections;
using System;

namespace JumpInSpace.Editor {

    [Serializable]
    public struct SerializableVector3 {
        public float x;
        public float y;
        public float z;

        public SerializableVector3(float rX, float rY, float rZ) {
            x = rX;
            y = rY;
            z = rZ;
        }

        public override string ToString() => $"{x}, {y}, {z}";

        public static implicit operator Vector3(SerializableVector3 rValue) =>
            new Vector3(rValue.x, rValue.y, rValue.z);

        public static implicit operator SerializableVector3(Vector3 rValue) =>
            new SerializableVector3(rValue.x, rValue.y, rValue.z);
    }

}