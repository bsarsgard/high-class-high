using UnityEngine;
using System.Collections;

public class DudeSprite: FSprite {
	protected static float JUMP_SPEED = 12f;
	protected static float JUMP_DECAY = 1.2f;
	
	protected FAtlasElement _standingElement;
	protected FAtlasElement[] _walkingElements;
	protected FAtlasElement[] _punchingElements;
	protected FAtlasElement[] _jumpingElements;
	
	protected float _frameCount = 0;
	protected float _animationSpeed = 0.15f;
	protected float _jumpAnimationSpeed = 0.05f;
	protected float _jumpFrameCount = 0;
	protected int _frameIndex = 0;
	protected float _jumpXVelocity = 0;
	protected float _jumpZVelocity = 0;
	
	public enum Poses {
		Walking,
		Punching,
		Jumping,
	};
	
	protected Poses? _pose = null;
	public Poses? Pose {
		get {
			return _pose;
		}
		set {
			if (_pose != value) {
				if (value == Poses.Jumping) {
					_jumpZVelocity = JUMP_SPEED;
					if (facingRight) {
						_jumpXVelocity = 2f;
					} else {
						_jumpXVelocity = -2f;
					}
				}
				_frameCount = _animationSpeed;
				_frameIndex = 0;
				_pose = value;
			}
		}
	}
	public bool Busy {
		get {
			return Pose == Poses.Punching || Pose == Poses.Jumping;
		}
	}
	public bool facingRight = true;
	public float speed = 200.0f;
	public float realX = 0;
	public float realY = 0;
	public float realZ = 0;
	public float hp = 100;
	
	public DudeSprite(string elementName): base(elementName) {
		_standingElement = this.element;
	}
	
	override public void Redraw(bool shouldForceDirty, bool shouldUpdateDepth) {
		if (Pose == null) {
			this.element = _standingElement;
		} else if (Pose == Poses.Walking) {
			if(_frameCount > _animationSpeed)
			{
				_frameCount = 0;
				if (_frameIndex >= _walkingElements.Length) {
					_frameIndex = 0;
				}
				this.element = _walkingElements[_frameIndex++];
			}
		} else if (Pose == Poses.Punching) {
			if(_frameCount > _animationSpeed)
			{
				_frameCount = 0;
				if (_frameIndex >= _punchingElements.Length) {
					_frameIndex = 0;
					Pose = null;
				} else {
					this.element = _punchingElements[_frameIndex++];
				}
			}
		} else if (Pose == Poses.Jumping) {
			if(_frameCount > _animationSpeed)
			{
				_frameCount = 0;
				if (_frameIndex < _jumpingElements.Length) {
					// still winding up the jump
					this.element = _jumpingElements[_frameIndex++];
				}
			}
			if (_jumpFrameCount > _jumpAnimationSpeed) {
				// process the jump
				realZ += _jumpZVelocity;
				realX += _jumpXVelocity;
				_jumpZVelocity -= JUMP_DECAY;
				if (realZ <= 0) {
					_jumpXVelocity = 0;
					_jumpZVelocity = 0;
					realZ = 0;
					Pose = null;
				}
			}
		}
	
		if (facingRight) {
			this.scaleX = 1;
		} else {
			this.scaleX = -1;
		}
		
		float delta = Time.deltaTime;
		_frameCount += delta;
		_jumpFrameCount += delta;
		base.Redraw(shouldForceDirty, shouldUpdateDepth);
	}
}