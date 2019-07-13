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
	/// ScriptableObject data set which fires a Relay on data addition/removal.
	/// <summary>
	[CreateAssetMenu(menuName="SmartData/TankGame.LeaderboardTableData/TankGame.LeaderboardTableData Set", order=2)]
	public class LeaderboardTableDataSet : SmartSet<TankGame.LeaderboardTableData>, ISmartDataSet<TankGame.LeaderboardTableData> {
		#if UNITY_EDITOR
		const string VALUETYPE = "TankGame.LeaderboardTableData";
		const string DISPLAYTYPE = "TankGame.LeaderboardTableData Set";
		#endif
	}
}

namespace SmartData.SmartLeaderboardTableData {
	/// <summary>
	/// Read-only access to LeaderboardTableDataSet or List<0>, with built-in UnityEvent.
	/// For write access make a LeaderboardTableDataSetWriter reference.
	/// UnityEvent disabled by default. If enabled, remember to disable at end of life.
	/// </summary>
	[System.Serializable]
	public class LeaderboardTableDataSetReader : SmartSetRefBase<TankGame.LeaderboardTableData, LeaderboardTableDataSet>, ISmartSetRefReader<TankGame.LeaderboardTableData> {
		[SerializeField]
		Data.LeaderboardTableDataVar.LeaderboardTableDataEvent _onAdd;
		[SerializeField]
		Data.LeaderboardTableDataVar.LeaderboardTableDataEvent _onRemove;
		
		protected override System.Action<TankGame.LeaderboardTableData, bool> GetUnityEventInvoke(){
			return (e,a)=>{
				if (a){
					_onAdd.Invoke(e);
				} else {
					_onRemove.Invoke(e);
				}
			};
		}
	}
	/// <summary>
	/// Write access to LeaderboardTableDataSet or List<TankGame.LeaderboardTableData>, with built-in UnityEvent.
	/// For read-only access make a LeaderboardTableDataSetRef reference.
	/// UnityEvent disabled by default. If enabled, remember to disable at end of life.
	/// </summary>
	[System.Serializable]
	public class LeaderboardTableDataSetWriter : SmartSetRefWriterBase<TankGame.LeaderboardTableData, LeaderboardTableDataSet>, ISmartSetRefReader<TankGame.LeaderboardTableData> {
		[SerializeField]
		Data.LeaderboardTableDataVar.LeaderboardTableDataEvent _onAdd;
		[SerializeField]
		Data.LeaderboardTableDataVar.LeaderboardTableDataEvent _onRemove;
		
		protected override System.Action<TankGame.LeaderboardTableData, bool> GetUnityEventInvoke(){
			return (e,a)=>{
				if (a){
					_onAdd.Invoke(e);
				} else {
					_onRemove.Invoke(e);
				}
			};
		}
		
		protected sealed override void InvokeUnityEvent(TankGame.LeaderboardTableData value, bool added){
			if (added){
				_onAdd.Invoke(value);
			} else {
				_onRemove.Invoke(value);
			}
		}
		
	}
}