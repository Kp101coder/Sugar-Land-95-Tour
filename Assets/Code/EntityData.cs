using UnityEngine;

public class EntityData : MonoBehaviour //Class used for storing data and basic entity actions such as sound
{
    public int health;
    public int healthcap;
    public int armour;
    public int armourcap;
    public AudioClip hurtSound;
    public AudioClip armourHurtSound;
    public AudioClip attackSound;
    public AudioClip growlSound;
    public AudioSource audioSys;
    public AudioClip healSound;
    public AudioClip armourSound;
    public AudioClip armourBreakSound;
    public AudioClip deathSound;
    public float volume;
    public int attackDamage;
    public Animator anim;
    public float attackRange;
    public float lastHurt;

    public void growl()
    {
        playSound(growlSound);
    }

    public void growl(float vol)
    {
        playSound(growlSound, vol);
    }

    public void attack() //Sound and Animation Only
    {
        anim.SetTrigger("OnAttack");
        playSound(attackSound);
    }

    public void dead(bool noAnim)
    {
        playSound(deathSound);
    }
    public void dead()
    {
        playSound(deathSound);
        anim.SetBool("IsDead", true);
    }

    public bool damageEntity(int dmg) //returns alive status
    {
        if (health > 0)
        {
            if (armour > 0)
            {
                armour = armour - dmg;
                #region Damage Overflow
                if (armour < 0)
                {
                    health += armour;
                    armour = 0;
                    playSound(armourBreakSound);
                    playSound(hurtSound);
                }
                else
                {
                    playSound(armourHurtSound);
                }
                #endregion
            }
            else
            {
                health -= dmg;
                if(Time.time - lastHurt > hurtSound.length) 
                {
                    playSound(hurtSound);
                    lastHurt = Time.time;
                }
            }
        }
        return health>0;
    }

    public void healEntity(int hp)
    {
        if (hp + health > healthcap)
        {
            health = healthcap;
        }
        else
        {
            health += hp;
        }
        playSound(healSound);
    }

    public void armourEntity(int arm)
    {
        if (arm + armour > armourcap)
        {
            armour = armourcap;
        }
        else
        {
            armour += arm;
        }
        playSound(armourSound);
    }

    public void playSound(AudioClip sound)
    {
        audioSys.PlayOneShot(sound, volume);
    }

    public void playSound(AudioClip sound, float vol)
    {
        audioSys.PlayOneShot(sound, vol);
    }
}
