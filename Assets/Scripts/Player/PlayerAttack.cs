﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack:MonoBehaviour
{
    [SerializeField] private GameObject bullet = null;

    public void BulletAttack()
    {
        Instantiate(bullet, this.transform.position, Quaternion.identity);
    }
}