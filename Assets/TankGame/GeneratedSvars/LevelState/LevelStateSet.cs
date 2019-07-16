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
	/// ScriptableObject data set which fires a Relay on data addition/removal.
	/// <summary>
	[CreateAssetMenu(menuName="SmartData/TankGame.LevelState/TankGame.LevelState Set", order=2)]
	public class LevelStateSet : SmartSet<TankGame.LevelState>, ISmartDataSet<TankGame.LevelState> {
		#if UNITY_EDITOR
		const string VALUETYPE = "TankGame.LevelState";
		const string DISPLAYTYPE = "TankGame.LevelState Set";
		#endif
	}
}

namespace SmartData.SmartLevelState {
	/// <summary>
	/// Read-only access to LevelStateSet or List<0>, with built-in UnityEvent.
	/// For write access make a LevelStateSetWriter reference.
	/// UnityEvent disabled by default. If enabled, remember to disable at end of life.
	/// </summary>
	[System.Serializable]
	public class LevelStateSetReader : SmartSetRefBase<TankGame.LevelState, LevelStateSet>, ISmartSetRefReader<TankGame.LevelState> {
		[SerializeField]
		Data.LevelStateVar.LevelStateEvent _onAdd;
		[SerializeField]
		Data.LevelStateVar.LevelStateEvent _onRemove;
		
		protected override System.Action<TankGame.LevelState, bool> GetUnityEventInvoke(){
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
	/// Write access to LevelStateSet or List<TankGame.LevelState>, with built-in UnityEvent.
	/// For read-only access make a LevelStateSetRef reference.
	/// UnityEvent disabled by default. If enabled, remember to disable at end of life.
	/// </summary>
	[System.Serializable]
	public class LevelStateSetWriter : SmartSetRefWriterBase<TankGame.LevelState, LevelStateSet>, ISmartSetRefReader<TankGame.LevelState> {
		[SerializeField]
		Data.LevelStateVar.LevelStateEvent _onAdd;
		[SerializeField]
		Data.LevelStateVar.LevelStateEvent _onRemove;
		
		protected override System.Action<TankGame.LevelState, bool> GetUnityEventInvoke(){
			return (e,a)=>{
				if (a){
					_onAdd.Invoke(e);
				} else {
					_onRemove.Invoke(e);
				}
			};
		}
		
		protected sealed override void InvokeUnityEvent(TankGame.LevelState value, bool added){
			if (added){
				_onAdd.Invoke(value);
			} else {
				_onRemove.Invoke(value);
			}
		}
		
	}
}