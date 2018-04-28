using UnityEngine;
using System.Collections.Generic;

public static class Utils
{
    public enum Axis { LeftAxis, RightAxis, TopAxis, BottomAxis };
    public enum Rotation { Up, Down, Left, Right };

    public static List<Transform> GetTransformChilders(this Transform transform)
    {
        List<Transform> childrens = new List<Transform>();

        foreach (Transform child in transform)
        {
            childrens.Add(child);
        }

        return childrens;
    }

    public static void MaterialColorFade(this Transform transform, float alpha)
    {
        Material m = transform.GetComponent<Renderer>().material;

        if (m != null)
        {
            Color color = m.color;
            color.a = alpha;
            m.color = color;
        }
    }

    public static bool CheckAllEquals<T>(this T[] array)
    {
        bool equal = true;
        for (int i = 0; i < array.Length; i++)
        {
            T value = array[i];
            for (int j = 0; j < array.Length; j++)
            {
                equal &= value.Equals(array[j]);
            }
        }
        return equal;
    }


    public static bool CheckAllEquals<T>(this T[,] array)
    {
        bool equal = true;
        int bound0 = array.GetLength(0);
        int bound1 = array.GetLength(1);
        for (int i = 0; i < bound0; i++)
        {
            for (int j = 0; j < bound1; j++)
            {
                T value = array[i, j];

                for (int h = 0; h < bound0; h++)
                {
                    for (int k = 0; k < bound1; k++)
                    {
                        equal &= value.Equals(array[h, k]);
                    }
                }
            }
        }
        return equal;
    }


    public static void DebugToString<T>(this T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            Debug.Log(array[i]);
        }
    }


    public static void DebugToString<T>(this T[,] array)
    {
        int bound0 = array.GetLength(0);
        int bound1 = array.GetLength(1);
        for (int i = 0; i < bound0; i++)
        {
            for (int j = 0; j < bound1; j++)
            {
                Debug.Log(array[i, j]);
            }
        }
    }

}