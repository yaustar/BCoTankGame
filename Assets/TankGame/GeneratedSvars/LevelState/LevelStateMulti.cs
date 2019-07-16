using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmartData.SmartLevelState.Data;
using SmartData.Abstract;
using SmartData.Interfaces;
using Sigtrap.Relays;

namespace SmartData.SmartLevelState.Data {
	/// <summary>
	/// Dynamic collection of LevelStateVar assets.
	/// </summary>
	[CreateAssetMenu(menuName="SmartData/TankGame.LevelState/TankGame.LevelState Multi", order=1)]
	public class LevelStateMulti: SmartMulti<TankGame.LevelState, LevelStateVar>, ISmartMulti<TankGame.LevelState, LevelStateVar> {
		#if UNITY_EDITOR
		const string VALUETYPE = "TankGame.LevelState";
		const string DISPLAYTYPE = "TankGame.LevelState Multi";
		#endif
	}
}

namespace SmartData.SmartLevelState {
	/// <summary>
	/// Indexed reference into a LevelStateMulti (read-only access).
	/// For write access make a reference to LevelStateMultiRefWriter.
	/// </summary>
	[System.Serializable]
	public class LevelStateMultiReader : SmartDataMultiRef<LevelStateMulti, TankGame.LevelState, LevelStateVar>  {
		public static implicit operator TankGame.LevelState(LevelStateMultiReader r){
            return r.value;
		}
		
		[SerializeField]
		Data.LevelStateVar.LevelStateEvent _onUpdate;
		
		protected override System.Action<TankGame.LevelState> GetUnityEventInvoke(){
			return _onUpdate.Invoke;
		}
	}
	/// <summary>
	/// Indexed reference into a LevelStateMulti, with a built-in UnityEvent.
	/// For read-only access make a reference to LevelStateMultiRef.
	/// UnityEvent disabled by default. If enabled, remember to disable at end of life.
	/// </summary>
	[System.Serializable]
	public class LevelStateMultiWriter : SmartDataMultiRefWriter<LevelStateMulti, TankGame.LevelState, LevelStateVar> {
		public static implicit operator TankGame.LevelState(LevelStateMultiWriter r){
            return r.value;
		}
		
		[SerializeField]
		Data.LevelStateVar.LevelStateEvent _onUpdate;
		
		protected override System.Action<TankGame.LevelState> GetUnityEventInvoke(){
			return _onUpdate.Invoke;
		}
		protected sealed override void InvokeUnityEvent(TankGame.LevelState value){
			_onUpdate.Invoke(value);
		}
	}
}