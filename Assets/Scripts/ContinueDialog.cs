using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueDialog : MonoBehaviour
{ 
    void Update()
    {
        if (Input.GetButtonDown("Clear"))
        {
            GetComponent<Dialog>().Clear();
        }
    }
}
