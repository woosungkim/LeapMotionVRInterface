using UnityEngine;
using System.Collections;

public class PointerSettings : MonoBehaviour {

	public MountType MountType = MountType.TableMount;

    public Color Color = new Color(1, 1, 1, 1);
    public float RadiusNormal = 0.05f;
    public float RadiusHighlighted = 0.01f;
	public float Thickness = 0.3f;
	
	public PointerType[] PointerUsed = {
		PointerType.RightIndex
	};



}
