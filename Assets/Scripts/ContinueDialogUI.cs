﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueDialogUI : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Clear"))
        {
            GetComponent<DialogUI>().Next();
        }
    }
}
