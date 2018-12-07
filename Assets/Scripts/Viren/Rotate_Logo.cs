using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Logo : MonoBehaviour
{
    

    public float spinSpeed = 10f;


    void Update()
    {
        transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
    }
}
