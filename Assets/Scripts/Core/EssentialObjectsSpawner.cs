using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialObjectsSpawner : MonoBehaviour
{
    [SerializeField] GameObject essentialsPrefab;
    private void Awake()
    {

        var existingObjects = FindObjectsOfType<EssentialObjects>();
        if (existingObjects.Length == 0)
        {
            Instantiate(essentialsPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        }

    }
}
