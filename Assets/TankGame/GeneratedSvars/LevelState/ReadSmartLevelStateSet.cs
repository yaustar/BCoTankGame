using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmartData.Abstract;

namespace SmartData.SmartLevelState.Components {
	/// <summary>
	/// Automatically listens to a <cref>SmartLevelStateSet</cref> and fires a <cref>UnityEvent<TankGame.LevelState></cref> when data changes.
	/// </summary>
	[AddComponentMenu("SmartData/TankGame.LevelState/Read Smart TankGame.LevelState Set", 2)]
	public class ReadSmartLevelStateSet : ReadSmartBase<LevelStateSetReader> {}
}