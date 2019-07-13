using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmartData.SmartLeaderboardTableData.Data;
using SmartData.Abstract;
using SmartData.Interfaces;
using Sigtrap.Relays;

namespace SmartData.SmartLeaderboardTableData.Data {
	/// <summary>
	/// Dynamic collection of LeaderboardTableDataVar assets.
	/// </summary>
	[CreateAssetMenu(menuName="SmartData/TankGame.LeaderboardTableData/TankGame.LeaderboardTableData Multi", order=1)]
	public class LeaderboardTableDataMulti: SmartMulti<TankGame.LeaderboardTableData, LeaderboardTableDataVar>, ISmartMulti<TankGame.LeaderboardTableData, LeaderboardTableDataVar> {
		#if UNITY_EDITOR
		const string VALUETYPE = "TankGame.LeaderboardTableData";
		const string DISPLAYTYPE = "TankGame.LeaderboardTableData Multi";
		#endif
	}
}

namespace SmartData.SmartLeaderboardTableData {
	/// <summary>
	/// Indexed reference into a LeaderboardTableDataMulti (read-only access).
	/// For write access make a reference to LeaderboardTableDataMultiRefWriter.
	/// </summary>
	[System.Serializable]
	public class LeaderboardTableDataMultiReader : SmartDataMultiRef<LeaderboardTableDataMulti, TankGame.LeaderboardTableData, LeaderboardTableDataVar>  {
		public static implicit operator TankGame.LeaderboardTableData(LeaderboardTableDataMultiReader r){
            return r.value;
		}
		
		[SerializeField]
		Data.LeaderboardTableDataVar.LeaderboardTableDataEvent _onUpdate;
		
		protected override System.Action<TankGame.LeaderboardTableData> GetUnityEventInvoke(){
			return _onUpdate.Invoke;
		}
	}
	/// <summary>
	/// Indexed reference into a LeaderboardTableDataMulti, with a built-in UnityEvent.
	/// For read-only access make a reference to LeaderboardTableDataMultiRef.
	/// UnityEvent disabled by default. If enabled, remember to disable at end of life.
	/// </summary>
	[System.Serializable]
	public class LeaderboardTableDataMultiWriter : SmartDataMultiRefWriter<LeaderboardTableDataMulti, TankGame.LeaderboardTableData, LeaderboardTableDataVar> {
		public static implicit operator TankGame.LeaderboardTableData(LeaderboardTableDataMultiWriter r){
            return r.value;
		}
		
		[SerializeField]
		Data.LeaderboardTableDataVar.LeaderboardTableDataEvent _onUpdate;
		
		protected override System.Action<TankGame.LeaderboardTableData> GetUnityEventInvoke(){
			return _onUpdate.Invoke;
		}
		protected sealed override void InvokeUnityEvent(TankGame.LeaderboardTableData value){
			_onUpdate.Invoke(value);
		}
	}
}