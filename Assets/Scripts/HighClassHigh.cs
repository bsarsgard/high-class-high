using UnityEngine;
using System.Collections;

public class HighClassHigh : MonoBehaviour {
	public Game game;

	// Use this for initialization
	void Start () {
		Screen.SetResolution(800, 600, false);
		FutileParams fparams = new FutileParams(true, true, true, true);
		fparams.AddResolutionLevel(800f, 1.0f, 1.0f, ""); //iPhone
		fparams.origin = new Vector2(0.5f,0.5f);
		Futile.instance.Init(fparams);
			
	    Futile.atlasManager.LoadAtlas("Atlases/HighClassHigh");
	    Futile.atlasManager.LoadAtlas("Atlases/Entrance");
	    Futile.atlasManager.LoadAtlas("Atlases/Hallway");
	    Futile.atlasManager.LoadAtlas("Atlases/Classroom");
	    Futile.atlasManager.LoadAtlas("Atlases/Cafeteria");
	    Futile.atlasManager.LoadAtlas("Atlases/Bathroom");
	    Futile.atlasManager.LoadAtlas("Atlases/Outside");
		Futile.atlasManager.LoadFont("PressStart2P", "PressStart2P_0", "Atlases/PressStart2P", 0, 0);
		Futile.atlasManager.LoadFont("MorningCoffee", "MorningCoffee_0", "Atlases/MorningCoffee", 0, 0);
		Futile.atlasManager.LoadFont("MorningCoffee_b", "MorningCoffee_b_0", "Atlases/MorningCoffee_b", 0, 0);
		Futile.atlasManager.LoadFont("OldNewspaperTypes", "OldNewspaperTypes_0", "Atlases/OldNewspaperTypes", 0, 0);
		Futile.atlasManager.LoadFont("Skinny", "Skinny_0", "Atlases/Skinny", 0, 0);
		game = new Game();
	}
	
	// Update is called once per frame
	void Update () {
		game.Update(Time.deltaTime);
	}
}
