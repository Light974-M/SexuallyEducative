using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UPDB.Data.VisualNovelManager
{
	[CreateAssetMenu(fileName = "new ActionData", menuName = "ScriptableObjects/VisualNovelManager/Action Data")]
	public class ActionData : ScriptableObject
	{
		public virtual void OnActionLaunch()
		{

		}
	} 
}
