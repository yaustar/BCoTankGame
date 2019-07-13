using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmartData.Abstract;

namespace SmartData.SmartScoreEntryData.Components {
	/// <summary>
	/// Serialised write access to a SmartScoreEntryData.
	/// </summary>
	[AddComponentMenu("SmartData/TankGame.ScoreEntryData/Write Smart TankGame.ScoreEntryData", 1)]
	public class WriteSmartScoreEntryData : WriteSmartBase<TankGame.ScoreEntryData, ScoreEntryDataWriter> {}
}