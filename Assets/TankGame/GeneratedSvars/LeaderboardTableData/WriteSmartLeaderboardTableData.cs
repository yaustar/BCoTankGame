using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmartData.Abstract;

namespace SmartData.SmartLeaderboardTableData.Components {
	/// <summary>
	/// Serialised write access to a SmartLeaderboardTableData.
	/// </summary>
	[AddComponentMenu("SmartData/TankGame.LeaderboardTableData/Write Smart TankGame.LeaderboardTableData", 1)]
	public class WriteSmartLeaderboardTableData : WriteSmartBase<TankGame.LeaderboardTableData, LeaderboardTableDataWriter> {}
}