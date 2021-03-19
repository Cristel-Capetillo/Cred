using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Cred {
    public class UpdateJavaHome : MonoBehaviour {
        [InitializeOnLoadMethod]
        static void SetJavaHome() {
            Debug.Log("JAVA_HOME in editor was: " + Environment.GetEnvironmentVariable("JAVA_HOME"));

            string newJDKPath = EditorApplication.applicationPath.Replace("Unity.app", "PlaybackEngines/AndroidPlayer/OpenJDK");

            if (Environment.GetEnvironmentVariable("JAVA_HOME") != newJDKPath) {
                Environment.SetEnvironmentVariable("JAVA_HOME", newJDKPath);
            }

            Debug.Log("JAVA_HOME in editor set to: " + Environment.GetEnvironmentVariable("JAVA_HOME"));
        }
    }
}