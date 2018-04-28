using System;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance = null;
    public static bool IsAwake { get { return (_instance != null); } }

    public static T Instance
    {
        get
        {
            Type mytype = typeof(T);
            if (_instance == null)
                _instance = (T)FindObjectOfType(mytype);


            return _instance;
        }
    }
}
