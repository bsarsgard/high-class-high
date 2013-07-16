using UnityEngine;
using System.Collections;

public class HeroSprite: DudeSprite {
	public HeroSprite(): base("Guy_standing") {
		_walkingElements = new FAtlasElement[4];
		_punchingElements = new FAtlasElement[2];
		_jumpingElements = new FAtlasElement[4];
		
		FAtlasManager am = Futile.atlasManager;
		//of course there are way smarter ways to do this, but this is fast
		//it's a ping ponging animation, which is why I did it this way, it's not a straight loop
		_walkingElements[0] = am.GetElementWithName("Guy_test1");
		_walkingElements[1] = am.GetElementWithName("Guy_test2");
		_walkingElements[2] = am.GetElementWithName("Guy_test3");
		_walkingElements[3] = am.GetElementWithName("Guy_test4");
		
		_punchingElements[0] = am.GetElementWithName("Guy_test1");
		_punchingElements[1] = am.GetElementWithName("Guy_test2");
		
		_jumpingElements[0] = am.GetElementWithName("Guy_test1");
		_jumpingElements[1] = am.GetElementWithName("Guy_test2");
		_jumpingElements[2] = am.GetElementWithName("Guy_test3");
		_jumpingElements[2] = am.GetElementWithName("Guy_test4");
	}
}
