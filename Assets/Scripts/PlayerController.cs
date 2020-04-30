using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 20; // 動く速さ
    public Text scoreText;
    public Text winText;
    public Text timerText;
    
    public GameObject itemList;
    private int countDown;

    private Rigidbody rb; // Rididbody
    private int score;
    private float time;
    private float slowTime;
    private bool isStart;
    
    private Material playerColor;
    
    AudioSource audioSource;

    public AudioClip getSound;
    public AudioClip wetSound;
    public AudioClip hitSound;
    public AudioClip clearSound;

    void Start()
    {
        // Rigidbody を取得
        rb = GetComponent<Rigidbody>();
        
        playerColor = GetComponent<Renderer>().material;
        
        countDown = itemList.transform.childCount;
        score = 0;
        time = 0.00f;
        SetCountText();
        TimerText();
        
        isStart = false;
        StartCoroutine("GameStart");
        
        audioSource = GetComponent<AudioSource>();
        
    }

    void Update()
    {
        if(isStart)
        {
            // カーソルキーの入力を取得
            var moveHorizontal = Input.GetAxis("Horizontal");
            var moveVertical = Input.GetAxis("Vertical");
    
            // カーソルキーの入力に合わせて移動方向を設定
            var movement = new Vector3(moveHorizontal, 0, moveVertical);
    
            // Ridigbody に力を与えて玉を動かす
            rb.AddForce(movement * speed);
            if(countDown > 0)
            {
                time += Time.deltaTime;
                TimerText();
            }
            if(slowTime > 0f)
            {
                slowTime -= Time.deltaTime;
            }
            else
            {
                speed = 20;
                playerColor.color = Color.white;
            }
        }
    }
    
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Damage") && countDown > 0 && score > 0)
        {
            score -= 5;
            SetCountText();
            audioSource.clip = hitSound;
            audioSource.Play();
        }
    }
    
    // 玉が他のオブジェクトにぶつかった時に呼び出される
    void OnTriggerEnter(Collider other)
    {
        // ぶつかったオブジェクトが収集アイテムだった場合
        if (other.gameObject.CompareTag("Item"))
        {
            // その収集アイテムを非表示にします
            other.gameObject.SetActive(false);
            
            countDown--;
            score += 10;
            SetCountText();
            if(countDown > 0){
                 audioSource.clip = getSound;
                 audioSource.Play();
            }

        }
        
        if (other.gameObject.CompareTag("Shield"))
        {
            playerColor.color = Color.gray;
            slowTime = 5f;
            speed = 5f;
            audioSource.clip = wetSound;
            audioSource.Play();
        }
    }
    // UI の表示を更新する
    void SetCountText()
    {     
        // すべての収集アイテムを獲得した場合
        if (countDown <= 0)
        {
            // リザルトの表示を更新
            winText.text = "You Win!";
            score += 100 - ((int)time%60);
            audioSource.clip = clearSound;
            audioSource.Play();
        }
        // スコアの表示を更新
        scoreText.text = "Score: " + score.ToString();
    }
    
    void TimerText()
    {
        var second = (int)time%60;
        timerText.text = "Time: " + second.ToString();
    }
    
    private IEnumerator GameStart()
    {
        winText.text = "3";
        yield return new WaitForSeconds(1f);
        winText.text = "2";
        yield return new WaitForSeconds(1f);
        winText.text = "1";
        yield return new WaitForSeconds(1f);
        winText.text = "GO!";
        isStart = true;
        yield return new WaitForSeconds(1f);
        winText.text = "";
    }
}
