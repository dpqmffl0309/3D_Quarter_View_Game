using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { Melee, Range };
    public Type type;
    public int damage;
    public float rate;
    public int maxAmmo;
    public int curAmmo;

    public BoxCollider meleeArea;
    public TrailRenderer trailEffect;
    public Transform bulletPos;
    public GameObject bullet;
    public Transform bulletCasePos;
    public GameObject bulletCase;

    public void Use()
    {
        if (type == Type.Melee)
        {
            StopCoroutine("Swing");
            StartCoroutine("Swing");
        }
        else if (type == Type.Range && curAmmo > 0)
        {
            curAmmo--;
            
            StartCoroutine("Shot");
        }
    }
    IEnumerator Swing()
    {
        //1
        yield return new WaitForSeconds(0.1f); //0.1초 대기
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        //2
        yield return new WaitForSeconds(0.3f); 
        meleeArea.enabled = false;
        //3
        yield return new WaitForSeconds(0.3f); 
        trailEffect.enabled = false;

        //yield break;
        //Use() 메인루틴 -> Swing() 서브루틴 -> Use() 메인루틴
        //Use() 메인루틴 -> Swing() 코루틴 (Co-Op)
    }
    IEnumerator Shot()
    {
        //1.총알발사
        GameObject instantBullet = Instantiate(bullet, 
                                               bulletPos.position, 
                                               bulletPos.rotation);
        Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
        bulletRigid.linearVelocity = bulletPos.forward * 100;
        yield return null;

        //2.탄피 배출
        GameObject instantCase = Instantiate(bulletCase,
                                               bulletCasePos.position,
                                               bulletCasePos.rotation);
        Rigidbody CaseRigid = instantCase.GetComponent<Rigidbody>();
        Vector3 caseVec = bulletCasePos.forward * Random.Range(-3, -2) 
                                + Vector3.up * Random.Range(2, 3);

        CaseRigid.AddForce(caseVec, ForceMode.Impulse);
        CaseRigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);
        yield return null;
    }
}
    
