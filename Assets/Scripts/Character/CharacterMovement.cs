﻿using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

	[HideInInspector]
	public bool moving;
	private SpriteRenderer sRenderer;
	private Color startColor;
	private Color endColor = new Color(1,1,1,0);
	//move to area position function
	public float fadeSpeed = 2;

	void Start()
	{
//		GetComponent<SpriteRenderer> ().sortingLayerName = "Character3";
		sRenderer = GetComponentInChildren<SpriteRenderer> ();

		startColor = sRenderer.color;
	}
	public IEnumerator FadeOut(Vector2 pos, int nr, GameObject stanceObj)
	{
		moving = true;
		float mTime = 0;
		bool onOff = true;
		while (onOff) 
		{
			if(mTime < 1)
			{
				mTime += Time.deltaTime * fadeSpeed;
				sRenderer.color = Color.Lerp(startColor, endColor, mTime);
			}
			else
			{
				SetPosition(pos, nr, stanceObj); 
				onOff = false;
				yield break;
			}
			yield return null;
		}
	}

	void SetPosition(Vector2 pos, int nr, GameObject stanceObj)
	{
		this.transform.position = pos;
		Destroy (this.transform.GetChild (0).gameObject);
		GameObject obj = Instantiate (stanceObj, Vector3.zero, Quaternion.identity) as GameObject;
		obj.transform.SetParent (this.transform);
		obj.transform.localPosition = Vector3.zero;
		sRenderer = obj.GetComponent<SpriteRenderer> ();
		sRenderer.sortingLayerName = "Character" + nr.ToString ();

		StartCoroutine (FadeIn ());
	}
	public IEnumerator FadeIn()
	{
		float mTime = 0;
		bool onOff = true;
		while (onOff) 
		{
			if(mTime < 1)
			{
				mTime += Time.deltaTime * fadeSpeed;
				sRenderer.color = Color.Lerp(endColor, startColor, mTime);
			}
			else
			{
//				SetArea();
				moving = false;
				onOff = false;
				yield break;
			}
			yield return null;
		}
	}
//	void SetArea()
//	{
//		moving = true;
//	}
}
