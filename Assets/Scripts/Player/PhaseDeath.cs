using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseDeath : MonoBehaviour
{
    [SerializeField]
    private float margin = 0.5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D[] contacts = new ContactPoint2D[collision.contactCount];
        collision.GetContacts(contacts);

        foreach (ContactPoint2D contact in contacts)
        {
            if (contact.separation < -margin)
            {
                StartCoroutine(GetComponent<HealthManager>().Kill());
            }
        }
    }
}
