using UnityEngine;
using System.Collections;

public class CompletionStep
{
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