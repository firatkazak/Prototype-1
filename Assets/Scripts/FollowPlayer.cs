using UnityEngine;
public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    //Arabayı sürükleyip bırakarak takip ettireceğimiz için player adında GameObject yarattık.
    [SerializeField] Vector3 offset = new Vector3(0, 5, -7);
    //Normalde kamera arabayı 0,0,0 şeklinde takip ediyor. Bu bizim istediğimiz açı değil.
    //Yukarıdaki açıyı standart açıya(0,0,0) ekleyince istediğimiz açıda takip ediyor.
    private void LateUpdate()
    {
     transform.position = player.transform.position + offset;
     //Aracın konumunu transform.position'a ata dedik. Bu sayede araç hareket ettiğinde kamera da onu takip eder.
     //Sürekli takip edeceği için Update fonksiyonuna yazdık. LateUpdate kamera takibi için daha iyi bir fonksiyon.
    }
}
