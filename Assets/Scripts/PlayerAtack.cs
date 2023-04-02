using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtack : MonoBehaviour
{

    public Animator animator;
    public Transform atackPoint; // позиция откуда исходит атака
    public float atackRange = 0.5f; // дальность атаки
    public LayerMask enemyLayers; // слой врагов

    public float atackRate = 2; // количество атак в секунду
    private float nextAtackTime = 0f; // возможное время следующующей атаки 

    public int atackDamage = 0;

    void Update()
    {
        // нужно убрать
        if(Input.GetKeyDown(KeyCode.F)) //удар клавишей F в дальнейшем переделать на кнопки 
        {
            Atack();
        }
        // нужно убрать

    }

    public void Atack()
    {  
        //обнаружить врагов в зоне действия 
        //определеяет радиус вокруг позиции, создает круг, и помещаем их в массив
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(atackPoint.position, atackRange, enemyLayers); 
        
        if (Time.time >= nextAtackTime)
        {
            //анимация атаки
            animator.SetTrigger("player_atack");
            gameObject.GetComponent<Player>().PlayAudioClip(2);
            
            //нанести урон врагам 
            foreach(Collider2D enemy in hitEnemies)   
            {           
                // наносим урон
                print(gameObject);
                enemy.GetComponent<Enemy>().TakeDamage(atackDamage, gameObject); 

                // устанавливаем время следующей возможной атаки
                nextAtackTime = Time.time + 1f / atackRate; 

            }
        }
    }

    // позволяет отоброзить материал когда объект выбран
    void OnDrawGizmosSelected() 
    {
        if (atackPoint != null)
            Gizmos.DrawWireSphere(atackPoint.position, atackRange);   
    }

}

