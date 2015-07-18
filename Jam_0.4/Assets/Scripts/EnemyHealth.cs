using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

	public int MaxHealth;
	private int Health;
	public Transform _player;

	public Canvas _Canvas;
	public Image imgHealth;

	void Start () 
	{
		Health = MaxHealth;
		imgHealth.fillAmount = 1;
	}

	void LateUpdate()
	{
		_Canvas.transform.LookAt(_player.position);
	}

	public void ReceiveDamage(int damage)
	{
		Health -= damage;
		if(Health <= 0)
		{
			Health = 0;
			Death();
		}
		imgHealth.fillAmount = (float)Health/(float)MaxHealth;

	}

	void Death()
	{
		gameObject.SetActive(false);
	}

}
