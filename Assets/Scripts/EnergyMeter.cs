using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyMeter : MonoBehaviour
{
	public int startEnergy = 100;
	private int currentEnergy;
	//public float degradeSpeed = 2.5f;

	public Slider energySlider;

    // Start is called before the first frame update
    void Start()
    {
		currentEnergy = startEnergy;
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetButton("Phase")) {
			currentEnergy -= 1;
		}
		energySlider.value = currentEnergy;
    }
}
