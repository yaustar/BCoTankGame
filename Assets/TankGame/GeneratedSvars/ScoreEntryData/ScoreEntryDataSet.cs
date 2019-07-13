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
	/// ScriptableObject data set which fires a Relay on data addition/removal.
	/// <summary>
	[CreateAssetMenu(menuName="SmartData/TankGame.ScoreEntryData/TankGame.ScoreEntryData Set", order=2)]
	public class ScoreEntryDataSet : SmartSet<TankGame.ScoreEntryData>, ISmartDataSet<TankGame.ScoreEntryData> {
		#if UNITY_EDITOR
		const string VALUETYPE = "TankGame.ScoreEntryData";
		const string DISPLAYTYPE = "TankGame.ScoreEntryData Set";
		#endif
	}
}

namespace SmartData.SmartScoreEntryData {
	/// <summary>
	/// Read-only access to ScoreEntryDataSet or List<0>, with built-in UnityEvent.
	/// For write access make a ScoreEntryDataSetWriter reference.
	/// UnityEvent disabled by default. If enabled, remember to disable at end of life.
	/// </summary>
	[System.Serializable]
	public class ScoreEntryDataSetReader : SmartSetRefBase<TankGame.ScoreEntryData, ScoreEntryDataSet>, ISmartSetRefReader<TankGame.ScoreEntryData> {
		[SerializeField]
		Data.ScoreEntryDataVar.ScoreEntryDataEvent _onAdd;
		[SerializeField]
		Data.ScoreEntryDataVar.ScoreEntryDataEvent _onRemove;
		
		protected override System.Action<TankGame.ScoreEntryData, bool> GetUnityEventInvoke(){
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
	/// Write access to ScoreEntryDataSet or List<TankGame.ScoreEntryData>, with built-in UnityEvent.
	/// For read-only access make a ScoreEntryDataSetRef reference.
	/// UnityEvent disabled by default. If enabled, remember to disable at end of life.
	/// </summary>
	[System.Serializable]
	public class ScoreEntryDataSetWriter : SmartSetRefWriterBase<TankGame.ScoreEntryData, ScoreEntryDataSet>, ISmartSetRefReader<TankGame.ScoreEntryData> {
		[SerializeField]
		Data.ScoreEntryDataVar.ScoreEntryDataEvent _onAdd;
		[SerializeField]
		Data.ScoreEntryDataVar.ScoreEntryDataEvent _onRemove;
		
		protected override System.Action<TankGame.ScoreEntryData, bool> GetUnityEventInvoke(){
			return (e,a)=>{
				if (a){
					_onAdd.Invoke(e);
				} else {
					_onRemove.Invoke(e);
				}
			};
		}
		
		protected sealed override void InvokeUnityEvent(TankGame.ScoreEntryData value, bool added){
			if (added){
				_onAdd.Invoke(value);
			} else {
				_onRemove.Invoke(value);
			}
		}
		
	}
}