using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmartData.Abstract;

namespace SmartData.SmartLevelState.Components {
	/// <summary>
	/// Serialised write access to a SmartLevelStateSet.
	/// </summary>
	[AddComponentMenu("SmartData/TankGame.LevelState/Write Smart TankGame.LevelState Set", 3)]
	public class WriteSmartLevelStateSet : WriteSetBase<TankGame.LevelState, LevelStateSetWriter> {}
}