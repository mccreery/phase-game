using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderPhaseable: MonoBehaviour
{
	private Collider2D p_collider;
	public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
		p_collider = GetComponent<Collider2D>();   
	}

    // Update is called once per frame
    void Update()
    {
		if (player.GetComponent<EnergyMeter>().canPhase)
		{
			p_collider.enabled = false;
		} 
		else 
		{
			p_collider.enabled = true;	
		}
    }


}
