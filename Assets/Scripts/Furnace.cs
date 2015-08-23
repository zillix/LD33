using UnityEngine;
using System.Collections;

public class Furnace : MonoBehaviour, ITriggerable{

	public int heat = 1;
	int lastPumpState = 0;
	int accumulatedPumps = 0;

	static int lowHeatPumps = 0;
	static int mediumHeatPumps = 2;
	static int highHeatPumps = 5;
	static int extremeHeatPumps = 8;

	Animator anim;


	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void startTrigger(float value)
	{
		int intVal = (int)value;
		if (intVal != lastPumpState) {
			lastPumpState = intVal;
			// Only count down pumps
			if (intVal == -1)
			{
				addPump();
			}
		}
	}

	private void addPump()
	{
		accumulatedPumps++;
		if (accumulatedPumps >= extremeHeatPumps) {
			heat = 4;
		} else if (accumulatedPumps >= highHeatPumps) {
			heat = 3;
		} else if (accumulatedPumps >= mediumHeatPumps) {
			heat = 2;
		} else {
			heat = 1;
		}

		anim.SetInteger ("heat", heat);
	}
	
	public void stopTrigger(float value)
	{
		// do nothing?
	}

	public void FixedUpdate()
	{


	}

	public void drainHeat()
	{
		heat = Mathf.Max (1, heat - 1);
	}
}
