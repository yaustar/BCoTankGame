using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmartData.Abstract;
using SmartData.Interfaces;

namespace SmartData.SmartLevelState.Data {
	/// <summary>
	/// ScriptableObject constant TankGame.LevelState.
	/// </summary>
	[CreateAssetMenu(menuName="SmartData/TankGame.LevelState/TankGame.LevelState Const", order=3)]
	public class LevelStateConst : SmartConst<TankGame.LevelState>, ISmartConst<TankGame.LevelState> {
		#if UNITY_EDITOR
		const string VALUETYPE = "TankGame.LevelState";
		const string DISPLAYTYPE = "TankGame.LevelState Const";
		#endif
	}
}