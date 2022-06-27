using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Rodrigo Chiale
public class Collector : MonoBehaviour
{

    [SerializeField] float rotationSpeed = 40;


    private void Update()
    {
        transform.localEulerAngles += new Vector3(0, 1, 0) * rotationSpeed * Time.deltaTime;
    }
}
