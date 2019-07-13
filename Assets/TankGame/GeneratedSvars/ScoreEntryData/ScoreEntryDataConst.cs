using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmartData.Abstract;
using SmartData.Interfaces;

namespace SmartData.SmartScoreEntryData.Data {
	/// <summary>
	/// ScriptableObject constant TankGame.ScoreEntryData.
	/// </summary>
	[CreateAssetMenu(menuName="SmartData/TankGame.ScoreEntryData/TankGame.ScoreEntryData Const", order=3)]
	public class ScoreEntryDataConst : SmartConst<TankGame.ScoreEntryData>, ISmartConst<TankGame.ScoreEntryData> {
		#if UNITY_EDITOR
		const string VALUETYPE = "TankGame.ScoreEntryData";
		const string DISPLAYTYPE = "TankGame.ScoreEntryData Const";
		#endif
	}
}