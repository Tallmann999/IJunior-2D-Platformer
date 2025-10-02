using System;
using System.Collections;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyNeeds : MonoBehaviour,IDamageble
{
    [SerializeField]private Need _health;

    public event Action Hit;
    public event Action<bool> Die;
    private bool IsDie;
    public bool IsAlive => _health.CurrentValue > 0;

    private void Start()
    {
        _health.CurrentValue = _health.StartValue;

    }

    public void TakeDamage(int damage)
    {
        Debug.Log($" ������� ������� ��������   {_health.CurrentValue}");
        Hit?.Invoke();
        // ����� ������� �������� �� ��������� ����� � 2 ������ .
        // ���� ��� ������� �� ������� ����� ����� �� ������� ��� �����
        _health.Decline(damage);

        if (_health.CurrentValue<=0)
        {
            IsDie = true;
            Die?.Invoke(IsDie);
        }
               
            //StartCoroutine(OnDestroyActivator());
        
    }

    //private IEnumerator OnDestroyActivator()
    //{
       
    //    yield return   new WaitForSeconds(20f);
    //    //Destroy(gameObject);
    //}
}
