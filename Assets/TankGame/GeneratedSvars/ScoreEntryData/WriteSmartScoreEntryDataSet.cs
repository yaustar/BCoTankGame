using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmartData.Abstract;

namespace SmartData.SmartScoreEntryData.Components {
	/// <summary>
	/// Serialised write access to a SmartScoreEntryDataSet.
	/// </summary>
	[AddComponentMenu("SmartData/TankGame.ScoreEntryData/Write Smart TankGame.ScoreEntryData Set", 3)]
	public class WriteSmartScoreEntryDataSet : WriteSetBase<TankGame.ScoreEntryData, ScoreEntryDataSetWriter> {}
}