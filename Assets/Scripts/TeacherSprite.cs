using UnityEngine;
using System.Collections;

public class TeacherSprite: NpcSprite {
	public TeacherSprite(Room room, float x, float y): base("Teacher_standing", room, x, y) {
		//scale = 1.25f;
		speed = 100f;
		_walkingElements = new FAtlasElement[4];
		_punchingElements = new FAtlasElement[2];
		_jumpingElements = new FAtlasElement[4];
		
		FAtlasManager am = Futile.atlasManager;
		//of course there are way smarter ways to do this, but this is fast
		//it's a ping ponging animation, which is why I did it this way, it's not a straight loop
		_walkingElements[0] = am.GetElementWithName("Teacher1");
		_walkingElements[1] = am.GetElementWithName("Teacher2");
		_walkingElements[2] = am.GetElementWithName("Teacher3");
		_walkingElements[3] = am.GetElementWithName("Teacher4");
		
		_punchingElements[0] = am.GetElementWithName("Teacher1");
		_punchingElements[1] = am.GetElementWithName("Teacher2");
		
		_jumpingElements[0] = am.GetElementWithName("Teacher1");
		_jumpingElements[1] = am.GetElementWithName("Teacher2");
		_jumpingElements[2] = am.GetElementWithName("Teacher3");
		_jumpingElements[2] = am.GetElementWithName("Teacher4");
	}
}
