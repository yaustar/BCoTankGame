using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmartData.SmartScoreEntryData.Data;
using SmartData.Abstract;
using SmartData.Interfaces;
using Sigtrap.Relays;

namespace SmartData.SmartScoreEntryData.Data {
	/// <summary>
	/// Dynamic collection of ScoreEntryDataVar assets.
	/// </summary>
	[CreateAssetMenu(menuName="SmartData/TankGame.ScoreEntryData/TankGame.ScoreEntryData Multi", order=1)]
	public class ScoreEntryDataMulti: SmartMulti<TankGame.ScoreEntryData, ScoreEntryDataVar>, ISmartMulti<TankGame.ScoreEntryData, ScoreEntryDataVar> {
		#if UNITY_EDITOR
		const string VALUETYPE = "TankGame.ScoreEntryData";
		const string DISPLAYTYPE = "TankGame.ScoreEntryData Multi";
		#endif
	}
}

namespace SmartData.SmartScoreEntryData {
	/// <summary>
	/// Indexed reference into a ScoreEntryDataMulti (read-only access).
	/// For write access make a reference to ScoreEntryDataMultiRefWriter.
	/// </summary>
	[System.Serializable]
	public class ScoreEntryDataMultiReader : SmartDataMultiRef<ScoreEntryDataMulti, TankGame.ScoreEntryData, ScoreEntryDataVar>  {
		public static implicit operator TankGame.ScoreEntryData(ScoreEntryDataMultiReader r){
            return r.value;
		}
		
		[SerializeField]
		Data.ScoreEntryDataVar.ScoreEntryDataEvent _onUpdate;
		
		protected override System.Action<TankGame.ScoreEntryData> GetUnityEventInvoke(){
			return _onUpdate.Invoke;
		}
	}
	/// <summary>
	/// Indexed reference into a ScoreEntryDataMulti, with a built-in UnityEvent.
	/// For read-only access make a reference to ScoreEntryDataMultiRef.
	/// UnityEvent disabled by default. If enabled, remember to disable at end of life.
	/// </summary>
	[System.Serializable]
	public class ScoreEntryDataMultiWriter : SmartDataMultiRefWriter<ScoreEntryDataMulti, TankGame.ScoreEntryData, ScoreEntryDataVar> {
		public static implicit operator TankGame.ScoreEntryData(ScoreEntryDataMultiWriter r){
            return r.value;
		}
		
		[SerializeField]
		Data.ScoreEntryDataVar.ScoreEntryDataEvent _onUpdate;
		
		protected override System.Action<TankGame.ScoreEntryData> GetUnityEventInvoke(){
			return _onUpdate.Invoke;
		}
		protected sealed override void InvokeUnityEvent(TankGame.ScoreEntryData value){
			_onUpdate.Invoke(value);
		}
	}
}