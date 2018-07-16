using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
	public float[] scaleBound = new float[2];
	public int interval; //how many frame per call for rescale
	public int rescaleStepCount; //how many steps to finish rescale

	//for rescale
	private int step;
	private float stepVal;
	private int index;
	//transform cache
	private Transform _transform;
	private SpriteRenderer _sr;
	private Rigidbody2D _rb2d;

	IEnumerator updateScale()
	{
		while(true)
		{
			if (step == rescaleStepCount)
			{
				step = 0;
				stepVal = (scaleBound[index] - _transform.localScale.x) / rescaleStepCount;
				index = 1 - index;
			}
			else
			{
				_transform.localScale += new Vector3(stepVal, 0, 0);
				step++;
			}
			yield return null;
		}
	}

	void randomMove()
	{
		Vector3 force = new Vector3(Random.Range(-30, 30), Random.Range(-30, 30), 0);
		Debug.Log(force);
		_rb2d.AddForce(force);
	}

	Vector3 getRandomPosition()
	{
		return Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, 1), Random.Range(0, 1), Random.Range(0, 1)));
	}

	// Use this for initialization
	void Start()
	{
		_rb2d = gameObject.GetComponent<Rigidbody2D>();
		_sr = gameObject.GetComponent<SpriteRenderer>();
		_transform = gameObject.GetComponent<Transform>();
		float scaleX = _transform.localScale.x;
		scaleBound[0] *= scaleX;
		scaleBound[1] *= scaleX;

		step = 0;
		index = 1;
		stepVal = (scaleBound[0] - scaleX) / rescaleStepCount;
		StartCoroutine(updateScale());
		InvokeRepeating("randomMove", 1, 3);
	}

	// Update is called once per frame
	void Update()
	{
		if(_rb2d.velocity.x < 0) _sr.flipX = true;
		else _sr.flipX = false;
	}
}