using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmartData.Abstract;
using SmartData.Interfaces;

namespace SmartData.SmartLeaderboardTableData.Data {
	/// <summary>
	/// ScriptableObject constant TankGame.LeaderboardTableData.
	/// </summary>
	[CreateAssetMenu(menuName="SmartData/TankGame.LeaderboardTableData/TankGame.LeaderboardTableData Const", order=3)]
	public class LeaderboardTableDataConst : SmartConst<TankGame.LeaderboardTableData>, ISmartConst<TankGame.LeaderboardTableData> {
		#if UNITY_EDITOR
		const string VALUETYPE = "TankGame.LeaderboardTableData";
		const string DISPLAYTYPE = "TankGame.LeaderboardTableData Const";
		#endif
	}
}