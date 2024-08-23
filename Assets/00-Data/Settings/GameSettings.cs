using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HappyTroll/Data/GameSettings", fileName = "GameSettings")]
public class GameSettings : ScriptableObject
{
	// Car
	public float[] movementSpeed;
	
	// Sound
	public float sfxVolume;
	public float bgmVolume;
}
