// SMARTTYPE TankGame.ScoreEntryData
// Do not move or delete the above line

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SmartData.SmartScoreEntryData.Data;
using SmartData.Abstract;
using SmartData.Interfaces;
using Sigtrap.Relays;

namespace SmartData.SmartScoreEntryData.Data {
	/// <summary>
	/// ScriptableObject data which fires a Relay on data change.
	/// <summary>
	[CreateAssetMenu(menuName="SmartData/TankGame.ScoreEntryData/TankGame.ScoreEntryData Variable", order=0)]
	public partial class ScoreEntryDataVar : SmartVar<TankGame.ScoreEntryData>, ISmartVar<TankGame.ScoreEntryData> {	// partial to allow overrides that don't get overwritten on regeneration
		#if UNITY_EDITOR
		const string VALUETYPE = "TankGame.ScoreEntryData";
		const string DISPLAYTYPE = "TankGame.ScoreEntryData";
		#endif

		[System.Serializable]
		public class ScoreEntryDataEvent : UnityEvent<TankGame.ScoreEntryData>{}
	}
}

namespace SmartData.SmartScoreEntryData {
	/// <summary>
	/// Read-only access to SmartScoreEntryData or TankGame.ScoreEntryData, with built-in UnityEvent.
	/// For write access make a ScoreEntryDataRefWriter reference.
	/// UnityEvent disabled by default. If enabled, remember to disable at end of life.
	/// </summary>
	[System.Serializable]
	public class ScoreEntryDataReader : SmartDataRefBase<TankGame.ScoreEntryData, ScoreEntryDataVar, ScoreEntryDataConst, ScoreEntryDataMulti> {
		[SerializeField]
		Data.ScoreEntryDataVar.ScoreEntryDataEvent _onUpdate;
		
		protected sealed override System.Action<TankGame.ScoreEntryData> GetUnityEventInvoke(){
			return _onUpdate.Invoke;
		}
	}
	/// <summary>
	/// Write access to SmartScoreEntryDataWriter or TankGame.ScoreEntryData, with built-in UnityEvent.
	/// For read-only access make a ScoreEntryDataRef reference.
	/// UnityEvent disabled by default. If enabled, remember to disable at end of life.
	/// </summary>
	[System.Serializable]
	public class ScoreEntryDataWriter : SmartDataRefWriter<TankGame.ScoreEntryData, ScoreEntryDataVar, ScoreEntryDataConst, ScoreEntryDataMulti> {
		[SerializeField]
		Data.ScoreEntryDataVar.ScoreEntryDataEvent _onUpdate;
		
		protected sealed override System.Action<TankGame.ScoreEntryData> GetUnityEventInvoke(){
			return _onUpdate.Invoke;
		}
		protected sealed override void InvokeUnityEvent(TankGame.ScoreEntryData value){
			_onUpdate.Invoke(value);
		}
	}
}