// SMARTTYPE TankGame.LevelState
// Do not move or delete the above line

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SmartData.SmartLevelState.Data;
using SmartData.Abstract;
using SmartData.Interfaces;
using Sigtrap.Relays;

namespace SmartData.SmartLevelState.Data {
	/// <summary>
	/// ScriptableObject data which fires a Relay on data change.
	/// <summary>
	[CreateAssetMenu(menuName="SmartData/TankGame.LevelState/TankGame.LevelState Variable", order=0)]
	public partial class LevelStateVar : SmartVar<TankGame.LevelState>, ISmartVar<TankGame.LevelState> {	// partial to allow overrides that don't get overwritten on regeneration
		#if UNITY_EDITOR
		const string VALUETYPE = "TankGame.LevelState";
		const string DISPLAYTYPE = "TankGame.LevelState";
		#endif

		[System.Serializable]
		public class LevelStateEvent : UnityEvent<TankGame.LevelState>{}
	}
}

namespace SmartData.SmartLevelState {
	/// <summary>
	/// Read-only access to SmartLevelState or TankGame.LevelState, with built-in UnityEvent.
	/// For write access make a LevelStateRefWriter reference.
	/// UnityEvent disabled by default. If enabled, remember to disable at end of life.
	/// </summary>
	[System.Serializable]
	public class LevelStateReader : SmartDataRefBase<TankGame.LevelState, LevelStateVar, LevelStateConst, LevelStateMulti> {
		[SerializeField]
		Data.LevelStateVar.LevelStateEvent _onUpdate;
		
		protected sealed override System.Action<TankGame.LevelState> GetUnityEventInvoke(){
			return _onUpdate.Invoke;
		}
	}
	/// <summary>
	/// Write access to SmartLevelStateWriter or TankGame.LevelState, with built-in UnityEvent.
	/// For read-only access make a LevelStateRef reference.
	/// UnityEvent disabled by default. If enabled, remember to disable at end of life.
	/// </summary>
	[System.Serializable]
	public class LevelStateWriter : SmartDataRefWriter<TankGame.LevelState, LevelStateVar, LevelStateConst, LevelStateMulti> {
		[SerializeField]
		Data.LevelStateVar.LevelStateEvent _onUpdate;
		
		protected sealed override System.Action<TankGame.LevelState> GetUnityEventInvoke(){
			return _onUpdate.Invoke;
		}
		protected sealed override void InvokeUnityEvent(TankGame.LevelState value){
			_onUpdate.Invoke(value);
		}
	}
}