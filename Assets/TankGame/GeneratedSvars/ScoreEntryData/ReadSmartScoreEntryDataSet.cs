using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmartData.Abstract;

namespace SmartData.SmartScoreEntryData.Components {
	/// <summary>
	/// Automatically listens to a <cref>SmartScoreEntryDataSet</cref> and fires a <cref>UnityEvent<TankGame.ScoreEntryData></cref> when data changes.
	/// </summary>
	[AddComponentMenu("SmartData/TankGame.ScoreEntryData/Read Smart TankGame.ScoreEntryData Set", 2)]
	public class ReadSmartScoreEntryDataSet : ReadSmartBase<ScoreEntryDataSetReader> {}
}