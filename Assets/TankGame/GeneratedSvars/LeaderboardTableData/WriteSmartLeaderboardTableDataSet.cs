using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmartData.Abstract;

namespace SmartData.SmartLeaderboardTableData.Components {
	/// <summary>
	/// Serialised write access to a SmartLeaderboardTableDataSet.
	/// </summary>
	[AddComponentMenu("SmartData/TankGame.LeaderboardTableData/Write Smart TankGame.LeaderboardTableData Set", 3)]
	public class WriteSmartLeaderboardTableDataSet : WriteSetBase<TankGame.LeaderboardTableData, LeaderboardTableDataSetWriter> {}
}