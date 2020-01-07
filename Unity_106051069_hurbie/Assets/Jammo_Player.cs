
using UnityEngine;

public class Jammo_Player : MonoBehaviour
{
    #region 欄位區域
    // 宣告變數 (定義欄位 Field)
    // 修飾詞 欄位類型 欄位名稱 (指定 值) 結束
    // 私人 - 隱藏 private (預設)
    // 公開 - 顯示 public 
    [Header("移動速度")]
    [Range(1, 2000)]
    public int speed = 10;             // 整數 1, 9999, -100
    [Header("旋轉速度"), Tooltip("Jammo_Player的旋轉速度"), Range(1.5f, 200f)]
    public float turn = 20.5f;         // 浮點數
    [Header("是否完成任務")]
    public bool mission;               // 布林值 true false
    [Header("玩家名稱")]
    public string _name = "Jammo_Player";      // 字串 ""
    #endregion
    public Transform tran;
    public Rigidbody rig;
    public Animator ani;
    public AudioSource aud;
    [Header("檢物品位置")]
    public Rigidbody rigCatch;

    public AudioClip soundBark;

    private void Update()
    {
        Turn();
        Run();
        jump();
        Catch();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Banana" && ani.GetCurrentAnimatorStateInfo(0).IsName("hit01"))
        {
            Physics.IgnoreCollision(other, GetComponent<Collider>());
            other.GetComponent<HingeJoint>().connectedBody = rigCatch;
        }

        if (other.name == "location" && ani.GetCurrentAnimatorStateInfo(0).IsName("hit01"))
        {
            GameObject.Find("Banana").GetComponent<HingeJoint>().connectedBody = null;
        }
    }

    #region 方法區域
    /// <summary>
    /// 跑步
    /// </summary>
    private void Run()
    {
        // 如果 動畫 為 撿東西 就 跳出
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("hit01")) return;

        float v = Input.GetAxis("Vertical");        // W 上 1、S 下 -1、沒按 0
        // rig.AddForce(0, 0, speed * v);           // 世界座標
        // tran.right   區域座標 X 軸
        // tran.up      區域座標 Y 軸
        // tran.forward 區域座標 Z 軸
        // Time.deltaTime 當下裝置一幀的時間
        rig.AddForce(tran.forward * speed * v * Time.deltaTime);     // 區域座標

        ani.SetBool("walk", v != 0);
    }

    /// <summary>
    /// 旋轉
    /// </summary>
    private void Turn()
    {
        float h = Input.GetAxis("Horizontal");    // A 左 -1、D 右 1、沒按 0
        tran.Rotate(0, turn * h * Time.deltaTime, 0);
    }

    /// <summary>
    /// 亂叫
    /// </summary>
    private void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 按下空白鍵拍翅膀
            ani.SetTrigger("jump");
            // 音源.播放一次音效(音效，音量)
           
        }
    }

    /// <summary>
    /// 撿東西
    /// </summary>
    private void Catch()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // 按下左鍵撿東西
            ani.SetTrigger("hit");
        }
    }

    /// <summary>
    /// 檢視任務
    /// </summary>
    private void Task()
    {

    }
    #endregion
}
