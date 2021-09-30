﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }

    void Update()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

}
