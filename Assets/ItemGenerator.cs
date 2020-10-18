using System.Collections;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public GameObject carPrefub;
    public GameObject coinPrefub;
    public GameObject conePrefub;
    //アイテム作成用のチェックポイント
    public GameObject checkpointPrefab;
    private GameObject unitychan;
    private int startPos = 80;
    private int goalPos = 360;
    private float posRange = 3.4f;

    // Start is called before the first frame update
    void Start()
    {   
        //ユニティちゃんのオブジェクトを取得
        this.unitychan = GameObject.Find("unitychan");   
        //チェックポイントを15m毎に作成
        for (int i = startPos; i < goalPos; i += 15)
        {
            GameObject checkPoint = Instantiate(checkpointPrefab);
            checkPoint.transform.position = new Vector3(0, checkPoint.transform.position.y, i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //アイテム作成用のオブジェクトをユニティちゃんの40m前に走らせる
        this.transform.position = new Vector3(0, this.transform.position.y, this.unitychan.transform.position.z + 40);
    }

    //アイテム作成用オブジェクトとチェックポイントが接触した際にアイテムを生成
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CheckPointTag")
        {
            //どのアイテムを出すのかをランダムに設定
            int num = Random.Range(1, 11);
            if (num <= 2)
            {
                //コーンをx軸に生成
                for (float j = -1; j <= 1; j += 0.4f)
                {
                    GameObject cone = Instantiate(conePrefub);
                    cone.transform.position = new Vector3(4 * j, cone.transform.position.y, this.transform.position.z);
                }
            }
            else
            {
                //レーンごとにアイテムを生成
                for (int j = -1; j <= 1; j++)
                {
                    //アイテムの種類を決める
                    int item = Random.Range(1, 11);
                    //アイテムを置くz座標のオフセットをランダムに設定
                    int offsetZ = Random.Range(-5, 6);
                    //60%コイン配置、30%車配置、10%何もなし
                    if (1 <= item && item <= 6)
                    {
                        //コインを生成
                        GameObject coin = Instantiate(coinPrefub);
                        coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, this.transform.position.z + offsetZ);
                    }
                    else if (7 <= item && item <= 9)
                    {
                        //車を生成
                        GameObject car = Instantiate(carPrefub);
                        car.transform.position = new Vector3(posRange * j, car.transform.position.y, this.transform.position.z + offsetZ);
                    }
                }
            }
        }
    }
}
