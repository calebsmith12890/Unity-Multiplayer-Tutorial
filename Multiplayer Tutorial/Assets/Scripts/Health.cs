using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour 
{
	public RectTransform healthBar;
	public const int maxHealth = 100;

	[SyncVar (hook = "OnChangeHealth")]
	public int currentHealth = maxHealth;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TakeDamage (int amount)
	{

		if (!isServer)
		{
			return;
		}

		currentHealth -= amount;
		
		if (currentHealth <= 0)
		{
			currentHealth = maxHealth;
			RpcRespawn ();
		}
	}

	void OnChangeHealth (int currentHealth)
	{
		healthBar.sizeDelta = new Vector2 (
			currentHealth,
			healthBar.sizeDelta.y
		);
	}

	[ClientRpc]
	void RpcRespawn ()
	{
		if (isLocalPlayer)
		{
			// Move back to spawn location
			transform.position = Vector3.zero;
		}
	}
}
