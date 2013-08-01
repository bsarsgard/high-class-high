using UnityEngine;
using System.Collections;

public class Door {
	public Rect Portal { get; set; }
	public Room Destination { get; set; }
	public Vector2 DropOff { get; set; }
	
	public Door(Rect portal, Room destination, Vector2 dropOff) {
		Portal = portal;
		Destination = destination;
		DropOff = dropOff;
	}
}

public class Room {
	public FSprite Background { get; set; }
	public Door[] Doors { get; set; }
	public bool ShowHero { get; set; }
	public Rect Boundaries { get; set; }
	public Dialog Dialog { get; set; }
	
	public Room(string backgroundSprite, bool showHero, Rect boundaries) {
		Background = new FSprite(backgroundSprite);
		Background.scale = 1f;
		Background.x = 0;
		Background.y = 0;
		ShowHero = showHero;
		Boundaries = boundaries;
	}
	
	public Door GetDoor(float x, float y) {
		//Debug.Log ("x" + x.ToString() + y" + y.ToString());
		foreach (Door door in Doors) {
			if (door.Portal.Contains(new Vector2(x, y))) {
				return door;
			}
		}
		return null;
	}
}