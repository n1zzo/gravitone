using UnityEngine;
using System.Collections;

public class Drum : Subscriber {

	public GameObject star;
	string fireKey="m";
	protected float scaleStep=0.5f;
	protected int currentSlot = 0;
	protected int lastSlot = -1;
	protected bool lastBeat = false;
  int beatsPerBar = 4;
  int subBeatsPerBeat = 4;
	protected int granularity = 0;
	protected AudioSource sound;
	protected bool[] slots = new bool[64];
	protected float progress = 0f;
	public bool[] targetDrumArray = new bool[64];
	bool isActive=false;

	string currentState = "drumPlay";

	// Use this for initialization
	void Start () {
		star.GetComponent<BeatGen>().Subscribe(this);
		beatsPerBar = star.GetComponent<BeatGen>().beatsPerBar;
		subBeatsPerBeat = star.GetComponent<BeatGen>().subBeatsPerBeat;
		granularity = beatsPerBar * subBeatsPerBeat;

		// Loads the drum clip
		sound = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {

		// Gets the current progress from the star
		progress = star.GetComponent<BeatGen>().progress;

		if(!isActive){
			widenEffect(0.95f);
			UpdatePlay();
		}
		else{

			if(currentState=="drumPlay")
				UpdatePlay();

			else if ( currentState=="drumPreview")
				UpdatePreview();

		}

	}

	public void UpdatePlay() {

		// This is executed at every beat.
		if (lastBeat && currentSlot != lastSlot) {
			if (slots[currentSlot])
				PlayDrum();
			lastSlot=currentSlot;
			lastBeat = false;
		}

	}

	public void UpdatePreview() {

		// This is executed at every beat.
		if (lastBeat && currentSlot != lastSlot) {
			if (targetDrumArray[currentSlot])
				PlayDrum();
			lastSlot = currentSlot;
			lastBeat = false;
		}
	}

	public int UpdateRecord() {


			PlayDrum();

			int index = Mathf.RoundToInt(progress * (float) granularity);

			// If it's divided in N, then the Nth beat is the initial 0
			if(index == granularity)
				index = 0;

			slots[index] = true;

			return index;
	}

	// This method is called for each beat
	public override void Beat(int currentSlot) {
		this.currentSlot = currentSlot;
		lastBeat = true;
	}

	public void ToggleRecord() {
		// If state is record, becomes play,
		// in all other cases it becomes record.
		if (currentState == "drumRecord")
			currentState = "drumPlay";
		else
			currentState = "drumRecord";
	}

	public bool CheckFire(){

		if(isActive){
			//check if our current system info equals a desktop
		 if(SystemInfo.deviceType == DeviceType.Desktop)
		     //we are on a desktop device, so don't use touch
		     return Input.GetKeyDown(fireKey);

		 //if it isn't a desktop, lets see if our device is a handheld device aka a mobile device
		 else if(SystemInfo.deviceType == DeviceType.Handheld)

		  //we are on a mobile device, so lets use touch input
			for (int i = 0; i < Input.touchCount; ++i)
				if(Input.GetTouch(i).phase == TouchPhase.Began) {
					Vector2 position = Input.GetTouch(i).position;

					if(position.x > Screen.width*15/100 && position.x < (Screen.width-Screen.width*15/100))
						return true;
				}
		}
		return false;
	}

	protected void PlayDrum(){
		sound.Play();
		transform.localScale = new Vector3(1f,1f,1f);
	}

	public void Cancel(){
		// This erases the recorded Rhythm
			for(int i=0; i<slots.Length; i++)
						slots[i]=false;
	}

	public void PlayPreview() {

		lastSlot=-1;


		// in all cases it becomes preview.
			currentState = "drumPreview";
	}

	public void StopPreview() {
		currentState = "drumPlay";
	}

	public void widenEffect(float correctness){

		if ( correctness==0)
			correctness=0.10f;

		// If the scale is not at its default state, change it.
		if(transform.localScale.x>correctness){
			float scaleFactor = scaleStep * Time.deltaTime;
			transform.localScale -= new Vector3(scaleFactor, scaleFactor, 0);
		}
	}

	// Get a deep copy of the slots array
	public bool[] GetDrumArray() {
		bool[] copy = new bool[64];
		System.Array.Copy(slots, copy, 64);
		return copy;
	}

	public string GetCurrentState(){
		return currentState;
	}

	public void SetCurrentState(string newState){
		currentState=newState;
	}

	public void SetActiveness(bool activeness){
		isActive=activeness;
	}

	public void Autocomplete(){
		slots = targetDrumArray;
	}

}