using UnityEngine;
using System.Collections;
using System.Linq;

public class CompletionStep
{
	public static int TotalSteps
	{
		get
		{
			if (totalSteps == 0)
				totalSteps = (int)System.Enum.GetValues(typeof(Completion)).Cast<Completion>().Max(); //http://stackoverflow.com/questions/1747212/finding-the-highest-value-in-an-enumeration
			return totalSteps;
		}
	}
	private static int totalSteps;

	public Completion StepToStart;
	public HouseItemType HouseItemRequired;
	public Action ActionOnCompletion;
	public int ActionArgument;
	
	public delegate void Action(int i);
	
	public CompletionStep(Completion stepToStart, HouseItemType houseItemRequired, int actionArgument)
	{
		this.StepToStart = stepToStart;
		this.HouseItemRequired = houseItemRequired;
		this.ActionArgument = actionArgument;
	}
}