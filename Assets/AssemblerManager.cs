using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AssemblerManager : MonoBehaviour
{
    private static int index = 1;
    private static GameObject lastObject = null;
    public static List<GameObject> sequence;
    public List<GameObject> sequenceInitializer;

    // Start is called before the first frame update
    void Start()
    {
        sequence = sequenceInitializer;  // Primeiro, atribua um valor a sequence
        lastObject = sequence[0];        // Em seguida, acesse sequence[0]
    }

    public static bool dropObject(GameObject obj)
    {
        if (obj == sequence[index])
        {
            index++;
            obj.GetComponent<MeshRenderer>().enabled = true;
            return true;
        }
        return false;
    }
}

