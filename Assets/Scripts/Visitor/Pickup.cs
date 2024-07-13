//using UnityEngine;

//public class Pickup : MonoBehaviour
//{
//    public PowerUp PowerUp;

//    private void OnTriggerEnter(Collider other)
//    {
//        IVisitable visitable = other.GetComponent<IVisitable>();
//        if (visitable != null)
//        {
//            visitable.Accept(PowerUp);
//            Destroy(other.gameObject);
//        }
//    }
//}