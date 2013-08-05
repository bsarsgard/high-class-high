using UnityEngine;
using System.Collections;

public class NpcSprite : DudeSprite {
	public NpcSprite(string elementName, Room room, float x, float y): base(elementName, room, x, y) {
		this._pose = Poses.Walking;
	}
}
