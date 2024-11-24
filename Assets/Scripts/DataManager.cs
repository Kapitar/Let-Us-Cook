using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static int[] arrayFromScene1;
    public static int[] arrayFromScene2;
    public static bool start = false;
    public static int score = 0;

    public static bool CompareArrays()
    {
        if (arrayFromScene1 == null || arrayFromScene2 == null)
        {
            Debug.LogError("One or both arrays are not initialized!");
            return false;
        }

        if (arrayFromScene1.Length != arrayFromScene2.Length)
        {
            return false;
        }

        for (int i = 0; i < arrayFromScene1.Length; i++)
        {
            if (arrayFromScene1[i] != arrayFromScene2[i])
            {
                return false;
            }
        }

        return true;
    }
}