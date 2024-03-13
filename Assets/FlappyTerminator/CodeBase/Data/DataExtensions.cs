﻿using UnityEngine;

namespace Assets.FlappyTerminator.CodeBase.Data
{
    public static class DataExtensions
    {
        public static Vector3Data AsVector3Data(this Vector3 vector) =>
            new Vector3Data(vector.x, vector.y, vector.z);

        public static Vector3 AsUnityVector3(this Vector3Data vector) =>
            new Vector3(vector.X, vector.Y, vector.Z);

        public static T ToDeserialized<T>(this string json) =>
            JsonUtility.FromJson<T>(json);

        public static string ToJson(this object obj) =>
            JsonUtility.ToJson(obj);
    }
}