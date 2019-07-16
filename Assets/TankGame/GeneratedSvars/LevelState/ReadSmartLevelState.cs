using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmartData.Abstract;

namespace SmartData.SmartLevelState.Components {
	/// <summary>
	/// Automatically listens to a <cref>SmartLevelState</cref> and fires a <cref>UnityEvent<TankGame.LevelState></cref> when data changes.
	/// </summary>
	[AddComponentMenu("SmartData/TankGame.LevelState/Read Smart TankGame.LevelState", 0)]
	public class ReadSmartLevelState : ReadSmartBase<LevelStateReader> {}
}