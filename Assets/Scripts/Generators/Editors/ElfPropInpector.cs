using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// 为了方便调整参数
[CustomEditor(typeof(BaseElf))]
public class ElfPropInpector: Editor
{
	public bool isMoving = false;
	private float MoveSpeedSer = 10;

	public bool isAttack;
	public static int Attack = 0;

	public bool isAttackRange = false;
	public static float AttackRange = 0;

	public static int Life = 0;
	private int LifeSer = 0;


	private void DrawAttackProp(PlayerElf owner) {
		isAttack = EditorGUILayout.Toggle("Is Attack", isAttack);
		if (isAttack) {
			Attack = EditorGUILayout.IntField("Attack Count", Attack);
			// add type to propList

			isAttackRange = EditorGUILayout.Toggle("Is Attack Range", isAttackRange);
			if (isAttackRange) {
				AttackRange = EditorGUILayout.FloatField("Attack Range", AttackRange);
			}
		}
	}

	private void DrawMoveProp () {

	}
}
