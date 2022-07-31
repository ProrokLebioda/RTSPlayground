using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGrowing : MonoBehaviour, IDamageable
{
    private int _maxHP;
    [SerializeField]
    private int _currentHP;

    public GameObject treelog;

    public int growthStage;

    public bool IsInUse { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        _maxHP = 2;
        _currentHP = _maxHP;
        growthStage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentHP <= 0)
        {            
            TreeFell();
        }
    }

    private void TreeFell()
    {
        Debug.Log("Tree cut");

        // Spawn log here
        Instantiate(treelog, transform.position, Quaternion.Euler(transform.rotation.x , transform.rotation.y - 45f, transform.rotation.z));


        Destroy(gameObject);
    }

    public void Damage(int amount)
    {
        _currentHP-=amount;
    }
}
