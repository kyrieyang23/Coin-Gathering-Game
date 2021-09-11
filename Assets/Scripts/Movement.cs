using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject coineffect;
    private Rigidbody rigid;
    private Collider col;
    public GameObject coin;
    public int maxNumCoins = 20;
    public float force = 10;
    public float jump = 1;
    public float RandSpace = 30;
    public float jumpRatio = 0.9f;
    public TMPro.TextMeshProUGUI txtscore;
    public TMPro.TextMeshProUGUI txttime;
    public GameObject gameend;
    public int score = 0;
    public float time = 60f;
    private float newtime;
    public LayerMask jumpLayers;
    private int gamecount = 0;
    // Start is called before the first frame update
    void Start()
    {
        newtime = time;
        for (int i = 0; i < maxNumCoins; i++)
        {
            CoinSpawn();
        }
        rigid = this.gameObject.GetComponent<Rigidbody>();
        col = this.gameObject.GetComponent<Collider>();
    }

    private void CoinSpawn()
    {
        Instantiate(coin, new Vector3(Random.Range(-RandSpace,RandSpace), 0f, Random.Range(-RandSpace,RandSpace)), coin.transform.rotation);
    }
    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.tag == "FuckingCoin")
        {
            Instantiate(coineffect, collision.gameObject.transform.position + new Vector3(0f, 2f, 0f), coineffect.transform.rotation);
            Destroy(collision.gameObject);
            score++;
            StartCoroutine(RespawnCoin());
        }
        if (collision.gameObject.tag == "FuckingInvisibleFloor")
        {
            this.gameObject.transform.position = new Vector3(-0.32f, 12.1f, -2.48f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        newtime -= 1 * Time.deltaTime;
        txtscore.text = "Fucking Score: " + score;
        txttime.text = newtime.ToString("0.0");
        if (newtime <= 0.04f)
        {
            newtime = 0.03f;
            if(gamecount == 0)
            {
                Instantiate(gameend, gameend.transform.position, gameend.transform.rotation);
                gamecount++;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                newtime = time;
                score = 0;
                gamecount = 0;
                GameObject[] coins = GameObject.FindGameObjectsWithTag("FuckingCoin");
                foreach(GameObject n in coins)GameObject.Destroy(n);
                GameObject[] coineffects = GameObject.FindGameObjectsWithTag("FuckingEffect");
                foreach(GameObject n in coineffects)GameObject.Destroy(n);
                for (int i = 0; i < maxNumCoins; i++)
                {
                    CoinSpawn();
                }
                this.gameObject.transform.position = new Vector3(-0.32f, 12.1f, -2.48f);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.A))
            {
                rigid.AddForce(new Vector3(-1f * force, 0f, 0f));
            }
            if (Input.GetKey(KeyCode.W))
            {
                rigid.AddForce(new Vector3(0, 0f, 1f * force));
            }
            if (Input.GetKey(KeyCode.S))
            {
                rigid.AddForce(new Vector3(0f, 0f, -1f * force));
            }
            if (Input.GetKey(KeyCode.D))
            {
                rigid.AddForce(new Vector3(1f * force, 0f, 0f));
            }
            if (Input.GetKey(KeyCode.Space) && IsGrounded())
            {
                rigid.AddForce(new Vector3(0f, 1f * jump, 0f), ForceMode.Impulse);
            }
        }
    }
    private bool IsGrounded()
    {
        return Physics.CheckBox(col.bounds.center, new Vector3(col.bounds.size.x * jumpRatio, col.bounds.size.y * jumpRatio, col.bounds.size.z * jumpRatio),
        this.gameObject.transform.rotation,jumpLayers);
    }
    IEnumerator RespawnCoin() 
    {
        yield return new WaitForSeconds(3f); 
        CoinSpawn();
    }
}