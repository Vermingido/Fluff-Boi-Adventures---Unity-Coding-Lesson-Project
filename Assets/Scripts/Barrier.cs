using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public GameObject barrier;

    public void Activate()
    {
        barrier.SetActive(true);
    }
}
