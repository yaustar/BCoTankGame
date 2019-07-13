// SMARTTYPE TankGame.LeaderboardTableData
// Do not move or delete the above line

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SmartData.SmartLeaderboardTableData.Data;
using SmartData.Abstract;
using SmartData.Interfaces;
using Sigtrap.Relays;

namespace SmartData.SmartLeaderboardTableData.Data {
	/// <summary>
	/// ScriptableObject data which fires a Relay on data change.
	/// <summary>
	[CreateAssetMenu(menuName="SmartData/TankGame.LeaderboardTableData/TankGame.LeaderboardTableData Variable", order=0)]
	public partial class LeaderboardTableDataVar : SmartVar<TankGame.LeaderboardTableData>, ISmartVar<TankGame.LeaderboardTableData> {	// partial to allow overrides that don't get overwritten on regeneration
		#if UNITY_EDITOR
		const string VALUETYPE = "TankGame.LeaderboardTableData";
		const string DISPLAYTYPE = "TankGame.LeaderboardTableData";
		#endif

		[System.Serializable]
		public class LeaderboardTableDataEvent : UnityEvent<TankGame.LeaderboardTableData>{}
	}
}

namespace SmartData.SmartLeaderboardTableData {
	/// <summary>
	/// Read-only access to SmartLeaderboardTableData or TankGame.LeaderboardTableData, with built-in UnityEvent.
	/// For write access make a LeaderboardTableDataRefWriter reference.
	/// UnityEvent disabled by default. If enabled, remember to disable at end of life.
	/// </summary>
	[System.Serializable]
	public class LeaderboardTableDataReader : SmartDataRefBase<TankGame.LeaderboardTableData, LeaderboardTableDataVar, LeaderboardTableDataConst, LeaderboardTableDataMulti> {
		[SerializeField]
		Data.LeaderboardTableDataVar.LeaderboardTableDataEvent _onUpdate;
		
		protected sealed override System.Action<TankGame.LeaderboardTableData> GetUnityEventInvoke(){
			return _onUpdate.Invoke;
		}
	}
	/// <summary>
	/// Write access to SmartLeaderboardTableDataWriter or TankGame.LeaderboardTableData, with built-in UnityEvent.
	/// For read-only access make a LeaderboardTableDataRef reference.
	/// UnityEvent disabled by default. If enabled, remember to disable at end of life.
	/// </summary>
	[System.Serializable]
	public class LeaderboardTableDataWriter : SmartDataRefWriter<TankGame.LeaderboardTableData, LeaderboardTableDataVar, LeaderboardTableDataConst, LeaderboardTableDataMulti> {
		[SerializeField]
		Data.LeaderboardTableDataVar.LeaderboardTableDataEvent _onUpdate;
		
		protected sealed override System.Action<TankGame.LeaderboardTableData> GetUnityEventInvoke(){
			return _onUpdate.Invoke;
		}
		protected sealed override void InvokeUnityEvent(TankGame.LeaderboardTableData value){
			_onUpdate.Invoke(value);
		}
	}
}