using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerController : MonoBehaviour
{
    [SerializeField] float horsePower = 60000;//Aracın beygir gücü.
    [SerializeField] float turnSpeed = 50.0f;//Sağa sola dönme hızı.
    private float horizontalInput;//Horizontal çıkış almak için.
    private float verticalInput;//Vertical çıkış almak için.
    private Rigidbody playerRb;// Rigidbody komponentine erişmek için.
    [SerializeField] GameObject centerOfMass;//Aracın "Kütle Merkezi"ni ayarlamak için.
    [SerializeField] TextMeshProUGUI mphText;//MPH türünde hız Text'i için.
    [SerializeField] TextMeshProUGUI rpmText;//Dakikada devir sayısı Text'i için.
    [SerializeField] float mphSpeed;//Mil türünde hız için.
    [SerializeField] float rpmSpeed;//Dakikada devir sayısı. 
    [SerializeField] List<WheelCollider> allwheels;//Arabanın 4 tekerini de tanıttığımız bir liste.
    [SerializeField] int wheelsOnGround;//4 tekerin de yere değip değmediğini kontrol etmek için.
    private void Start()
    {
     playerRb = GetComponent<Rigidbody>();//Başlangıçta arabanın RigidBody'sini komponentini playerRb'ye atadık.
     playerRb.centerOfMass = centerOfMass.transform.position;
     //Mevcut kütle merkezinin pozisyonunu RigidBody'nin kütle merkezine atadık.
    }
    private void FixedUpdate()
    {
     verticalInput = Input.GetAxis("Vertical");//İleri geri için çıkış atadık.
     horizontalInput = Input.GetAxis("Horizontal");//Sağa sola dönmek için çıkış atadık.
     if(IsOnGround())//Eğer aşağıdaki IsOnGround fonksiyonu true dönüyorsa aşağıdaki kodlar çalışacak.
     //Yani 4 teker de yere temas ettiğinde hız ve devir göstergesi artıp azalacak, araç ileri geri, sağa sola gidecek.
     //4 teker de yere değmediğinde bunları yapamayacak. Mantıken de olması gereken bu.
     {
      //transform.Translate(Vector3.forward * Time.deltaTime * horsePower * verticalInput);
      //Normalde yukarıdaki kod ile ileri geri gidiliyordu.
      playerRb.AddRelativeForce(Vector3.forward * horsePower * verticalInput);
      //Fakat araba gibi gerçekçi olsun diye AddRelativeForce fonksiyonunu kullandık.
      transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);
      //Sağa sola dönüş için.
      mphSpeed = Mathf.Round(playerRb.velocity.magnitude * 2.237f);
      //Oyunun normal hızını, daha gerçekçi olsun diye yukarıdaki hesaplama ile Mil türüne çevirdik.
      mphText.SetText(mphSpeed + " Mph");
      //Ve bunu ekranın sol üst köşesine yazdırdık.
      rpmSpeed = Mathf.Round((mphSpeed % 30) * 40);
      //Yine matematik işlemi ile mil'den devir sayısını hesapladık.
      rpmText.SetText(rpmSpeed + " Rpm");
      //Ve bunu da ekranın sol üst köşesine mph text'inin altına yazdırdık.
     }
    }
    private bool IsOnGround()//Yerde mi? adında tekerlerin yere temasını sorguladığımız mantıksal bir fonksiyon yazdık.
    {
     wheelsOnGround = 0;//Yukarıda wheelsOnGround isimli integer bir değişken tanımlamıştık.Başlangıçta bu 0 olarak başladı.
     foreach(WheelCollider wheel in allwheels)//Yukarıda allwheels isminde bir liste tanımlamıştık. 
     //allwheels listesinden wheel isimli WheelCollider tanımladık. foreach yani herbiri için;
     {
      if(wheel.isGrounded)//Eğer tekerleklerden herhangi 1 tanesi yere temas ediyorsa.
      {
       wheelsOnGround++;//wheelsOnGround'ı 1 arttır. 0'sa 1, 1'se 2, 2 ise 3, 3 ise 4 olacak.
      }
     }
      if(wheelsOnGround == 4)//Eğer 4 teker de yere değiyorsa
      {
       return true;//true döndür.
      }
      else//Değilse
      {
       return false;//false döndür.
      }
    }
}
