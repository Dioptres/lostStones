using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public int MaxHealth;
	private int Health;

	public Image imgHealth;
	public Text txtHealth;
	
	void Start () 
	{
		Health = MaxHealth;
		imgHealth.fillAmount = 1;
		txtHealth.text = Health + "/" + MaxHealth;
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
		txtHealth.text = Health + "/" + MaxHealth;
		
	}
	
	void Death()
	{
		gameObject.SetActive(false);
	}
}
