using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{
    public GameObject myPrefab;
    public Transform parentTransform;

    void Start()
    {
        
        GameObject newObject = Instantiate(myPrefab, new Vector3(-13, 25, 0), Quaternion.identity);
        newObject.transform.parent = parentTransform;
    }

}
