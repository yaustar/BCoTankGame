using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmartData.Abstract;

namespace SmartData.SmartLeaderboardTableData.Components {
	/// <summary>
	/// Automatically listens to a <cref>SmartLeaderboardTableData</cref> and fires a <cref>UnityEvent<TankGame.LeaderboardTableData></cref> when data changes.
	/// </summary>
	[AddComponentMenu("SmartData/TankGame.LeaderboardTableData/Read Smart TankGame.LeaderboardTableData", 0)]
	public class ReadSmartLeaderboardTableData : ReadSmartBase<LeaderboardTableDataReader> {}
}