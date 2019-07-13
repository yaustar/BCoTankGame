using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmartData.Abstract;

namespace SmartData.SmartScoreEntryData.Components {
	/// <summary>
	/// Automatically listens to a <cref>SmartScoreEntryData</cref> and fires a <cref>UnityEvent<TankGame.ScoreEntryData></cref> when data changes.
	/// </summary>
	[AddComponentMenu("SmartData/TankGame.ScoreEntryData/Read Smart TankGame.ScoreEntryData", 0)]
	public class ReadSmartScoreEntryData : ReadSmartBase<ScoreEntryDataReader> {}
}