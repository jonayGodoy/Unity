using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ControlPlayer
{
	private ManagerAnimationPlayer callPlayer;
	private string tag;

	private Dictionary<string,ExecuteCall []> inputsMethods;

	private delegate void ExecuteCall(); 

	public ControlPlayer(ManagerAnimationPlayer callPlayer,string tag){
		this.callPlayer = callPlayer;
		this.tag = tag;

		inputsMethods= new Dictionary<string,ExecuteCall []>();
		inputsMethods.Add ("Jump_"+tag, new ExecuteCall[]{new ExecuteCall(callPlayer.Jump)});
		inputsMethods.Add ("PUNCH_R_"+tag, new ExecuteCall[]{new ExecuteCall(callPlayer.Punch_R)});
		inputsMethods.Add ("Horizontal_"+tag, new ExecuteCall[]{
			new ExecuteCall(callPlayer.Front),new ExecuteCall(callPlayer.Back)});
		inputsMethods.Add ("Vertical_"+tag, new ExecuteCall[]{
			new ExecuteCall(callPlayer.FeintLeft),new ExecuteCall(callPlayer.FeintRight)});
	}

	public void UpdateInputs(){
		foreach(KeyValuePair<string, ExecuteCall []> inputMethod in inputsMethods){
			UpdateInput (inputMethod);
		}
	}

	private void UpdateInput(KeyValuePair<string, ExecuteCall []> inputMethod){
		if (inputMethod.Value.Length == 1) {
			ExecuteInputWithoutOposite (inputMethod);
		} else {
			if (inputMethod.Value.Length == 2) {
				ExecuteInputWithOposite (inputMethod);
			}
		}
	}

	private void ExecuteInputWithoutOposite(KeyValuePair<string, ExecuteCall []> inputMethod){
		if(Input.GetAxis (inputMethod.Key) != 0){
			inputMethod.Value[0] ();
		}
	}

	private void ExecuteInputWithOposite(KeyValuePair<string, ExecuteCall []> inputMethod){
		float n = Input.GetAxis (inputMethod.Key);
		if (n != 0) {
			if (n > 0) {
				inputMethod.Value [0] ();
			} else {
				inputMethod.Value [1] ();
			}
		}
	}
		
}

