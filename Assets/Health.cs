using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    public int maxHealth;

    public MonoBehaviour Script;
    public string DeathFunction;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        if (health == 0)
            Script.Invoke(DeathFunction, 0f);
    }
}
