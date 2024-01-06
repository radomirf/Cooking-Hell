using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LookAtCamera : MonoBehaviour
{


    private void LateUpdate()
    {
               transform.forward = Camera.main.transform.forward;

        }
       
    }

