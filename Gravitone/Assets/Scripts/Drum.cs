using UnityEngine;
using System.Collections;

public class Drum : Subscriber {

	public GameObject star;
	public string fireKey="";
	protected float scaleStep=0.5f;
	protected int currentSlot = 0;
	protected int lastSlot = -1;
	protected bool lastBeat = false;
	protected int beatsPerBar = 0;
	protected int subBeatsPerBeat = 0;
	protected int granularity = 0;
	protected AudioSource sound;
	protected bool[] slots = new bool[64];
	protected bool[] prev = new bool[64];
	protected float progress = 0f;

	Drum currentState;
	Drum drumRecord;
	Drum drumPlay;
	Drum drumPreview;

	public Drum() {
		drumRecord = new DrumRecord();
		drumPlay = new DrumPlay();
		drumPreview = new DrumPreview();
		currentState = drumPlay;
	}

	// is true if we consider the right part of the screen for the touch
	public bool isRight=false;

	// Use this for initialization
	void Start () {
		star.GetComponent<BeatGen>().Subscribe(this);
		beatsPerBar = star.GetComponent<BeatGen>().beatsPerBar;
		subBeatsPerBeat = star.GetComponent<BeatGen>().subBeatsPerBeat;

		granularity = beatsPerBar * subBeatsPerBeat;

		// Loads the drum clip
		sound = GetComponent<AudioSource>();

		prev = GetComponent<DrumCompare>().get();
	}

	// Update is called once per frame
	void Update () {

		// If the scale is not at its default state, change it.
		if(transform.localScale.x>0.95){
			float scaleFactor = scaleStep * Time.deltaTime;
			transform.localScale -= new Vector3(scaleFactor, scaleFactor, 0);
		}

		// Gets the current progress from the star
		progress = star.GetComponent<BeatGen>().progress;

		currentState.Update();

		// This is executed at every beat.
		if (lastBeat && currentSlot != lastSlot) {
			if (prev[currentSlot])
				playDrum();
			lastSlot=currentSlot;
			lastBeat = false;
		}

	}

	// This method is called for each beat
	public override void Beat(int currentSlot) {
		this.currentSlot = currentSlot;
		lastBeat = true;
	}

	public void toggleRecord() {
		// If state is record, becomes play,
		// in all other cases it becomes record.
		if (currentState == drumRecord)
			currentState = drumPlay;
		else
			currentState = drumRecord;
	}

	// check if the user touches the right position
	private bool checkPosition(Vector2 pos){
		if(!isRight)
			return pos.x<Screen.width/2;
		else
			return pos.x>Screen.width/2;
	}

	protected bool checkFire(){
			//check if our current system info equals a desktop
		 if(SystemInfo.deviceType == DeviceType.Desktop){
		     //we are on a desktop device, so don't use touch
		     return Input.GetKeyDown(fireKey);
		 }
		 //if it isn't a desktop, lets see if our device is a handheld device aka a mobile device
		 else if(SystemInfo.deviceType == DeviceType.Handheld){
		     //we are on a mobile device, so lets use touch input
				 bool checkInput=false;
			 		for (int i = 0; i < Input.touchCount; ++i)
			 			if(Input.GetTouch(i).phase == TouchPhase.Began &&
			 					checkPosition(Input.GetTouch(i).position))
									checkInput=true;
					return checkInput;
		 }
		 else
		 	return false;
	}

	protected void playDrum(){
		sound.Play();
		transform.localScale = new Vector3(1, 1, 1);
	}

	public void cancel(){
		// This erases the recorded Rhythm
			for(int i=0; i<slots.Length; i++)
						slots[i]=false;
	}

	public void playPreview() {
		star.GetComponent<BeatGen>().progress=0;
		lastSlot=-1;
		// If state is preview, becomes play,
		// in all other cases it becomes preview.
		if (currentState == drumPreview)
			currentState = drumPlay;
		else
			currentState = drumPreview;
	}

	public void stopPreview() {
		currentState = drumPlay;
	}

	// Get a deep copy of the slots array
	public bool[] getDrumArray() {
		bool[] copy = new bool[64];
		System.Array.Copy(slots, copy, 64);
		return copy;
	}

}
