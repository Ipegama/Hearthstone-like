using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile  : MonoBehaviour
{
    public Action Triggered { get; internal set; }

    public void LaunchAt(Transform target) 
    {
    }
}
