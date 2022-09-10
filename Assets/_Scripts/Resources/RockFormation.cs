using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockFormation : MonoBehaviour, IDamageable
{
    [SerializeField]
    public GameObject[] visualRockFragments;

    private int _maxHP;
    [SerializeField]
    private int _currentHP;

    public GameObject stone;
    public void Damage(int amount)
    {
        _currentHP -= amount;
        Instantiate(stone, transform.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y - 45f, transform.rotation.z));
        visualRockFragments[_currentHP].SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        _maxHP = 8;
        _currentHP = _maxHP; 
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentHP <= 0)
            Destroy(this.gameObject);
    }
}
