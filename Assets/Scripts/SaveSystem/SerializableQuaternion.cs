using UnityEngine;
using System.Collections;
using System;

namespace JumpInSpace.Editor {

    [Serializable]
    public struct SerializableQuaternion {
        public float x;
        public float y;
        public float z;
        public float w;

        public SerializableQuaternion(float rX, float rY, float rZ, float rW) {
            x = rX;
            y = rY;
            z = rZ;
            w = rW;
        }

        public override string ToString() => $"{x}, {y}, {z}, {w}";

        public static implicit operator Quaternion(SerializableQuaternion rValue) =>
            new Quaternion(rValue.x, rValue.y, rValue.z, rValue.w);

        public static implicit operator SerializableQuaternion(Quaternion rValue) =>
            new SerializableQuaternion(rValue.x, rValue.y, rValue.z, rValue.w);
    }

}