using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyMeter : MonoBehaviour
{
	public int startEnergy = 500;
	public int currentEnergy;
	public bool canPhase = false;

	public Slider energySlider;

    // Start is called before the first frame update
    void Start()
    {
		currentEnergy = startEnergy;
		energySlider.value = startEnergy;
    }

    // Update is called once per frame
    void Update()
    {
		canPhase = false;
		if (Input.GetButton("Phase")) 
		{
			currentEnergy -= 1;
			if (currentEnergy > 0){
				canPhase = true;
			}
		}
		energySlider.value = currentEnergy;
    }
		
}
