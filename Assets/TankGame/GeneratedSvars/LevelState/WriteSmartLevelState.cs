using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmartData.Abstract;

namespace SmartData.SmartLevelState.Components {
	/// <summary>
	/// Serialised write access to a SmartLevelState.
	/// </summary>
	[AddComponentMenu("SmartData/TankGame.LevelState/Write Smart TankGame.LevelState", 1)]
	public class WriteSmartLevelState : WriteSmartBase<TankGame.LevelState, LevelStateWriter> {}
}