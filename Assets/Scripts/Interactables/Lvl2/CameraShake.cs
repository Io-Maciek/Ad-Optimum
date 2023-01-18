using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Models;

public class CameraShake : Activatable
{
	private Transform camTransform;

	public float shakeDuration = 1f;

	public float shakeAmount = 0.4f;
	public float decreaseFactor = 0.2f;

	Vector3 originalPos;

	public override Result<object, object> SetTo(bool setValue, params object[] args)
	{
		if (setValue)
		{
			this.enabled = true;
			camTransform = GetComponent<Transform>();
			originalPos = camTransform.localPosition;
		}

		return Result<object>.Ok();
	}

	void Update()
	{
		if (shakeDuration > 0)
		{
			camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

			shakeDuration -= Time.deltaTime * decreaseFactor;

			shakeAmount -= Time.deltaTime * decreaseFactor;
			if (shakeAmount <= 0) shakeAmount = 0;
		}
		else
		{
			shakeDuration = 0f;
			camTransform.localPosition = originalPos;
			this.enabled = false;
		}
	}
}
