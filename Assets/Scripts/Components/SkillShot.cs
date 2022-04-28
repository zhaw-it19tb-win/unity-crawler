using UnityEngine;

public class SkillShot : MonoBehaviour {

  public GameObject player;
  public Transform Effect;
  [SerializeField] public LayerMask LayerMask;
  public float EffectSpeed = 1f;
  public float DestroyInSeconds = 3f;

  private bool _isCasting = false;
  private Vector2 _origin;
  private Vector2 _target;
  private Vector2 _moveVector;
  private Vector3 _moveVector3d;
  private Transform _skillShotTransform;
  private GameObject _skillShot;
  private FireballCollision collisionDetector;

  private void CastAbility() {
    _isCasting = true;
    _skillShotTransform = Instantiate(Effect, _origin, Quaternion.identity);
    _skillShot = _skillShotTransform.gameObject;
    Destroy(_skillShot, DestroyInSeconds);
    var collider = _skillShotTransform.gameObject.GetComponent<Collider2D>();
    _moveVector = (_target - _origin).normalized * EffectSpeed;
    _moveVector3d = new Vector3(_moveVector.x, _moveVector.y, 0);
    collisionDetector = _skillShotTransform.gameObject.GetComponent<FireballCollision>();
  }

  // Update is called once per frame
  void Update() {
    if (_skillShot == null) {
      _isCasting = false;
    }
    if (_isCasting) {
      var temp = _skillShotTransform.position + _moveVector3d * (Time.deltaTime);
      _skillShotTransform.position = new Vector3(temp.x, temp.y, -1);
      if (collisionDetector.hasCollided) {
        _isCasting = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_skillShotTransform.position, 0.2f);
        foreach (Collider2D collider in colliders) {
          collider.gameObject.GetComponent<Health>()?.TakeDamage(10);
        }
        var impact = Instantiate(Effect, _skillShotTransform.position, Quaternion.identity);
        Destroy(_skillShot);
        impact.transform.localScale += new Vector3(0.03f, 0.03f, 0.03f);
        Destroy(impact.gameObject, 3f);
      }
    }
    else if (Input.GetMouseButtonDown(0)) {
      var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      _origin = new Vector2(player.transform.position.x, player.transform.position.y);
      _target = new Vector2(mousePosition.x, mousePosition.y);
      CastAbility();
    }
  }

  // Instead of using rigidbody, for using a skillshot that has no traveltime raycasts should probably be used somehow like this:

      //float distance = Helpers.GetIsoDistance(origin, target);
      /*Debug.Log("original distance: " + distance);
      if (distance > MaxRange) {
        target.x = target.x / distance * MaxRange;
        target.y = target.y / distance * MaxRange;
        Debug.Log("Limited range: " + Helpers.GetIsoDistance(origin, target));
      }*/

      /*RaycastHit2D hit = Physics2D.Raycast(origin, target, Mathf.Infinity, layerMask: LayerMask); // add layermask
      Debug.Log("Ray from " + origin + " to: " + target);
      Debug.DrawRay(origin, target, Color.blue, 20, true);*/

      /*foreach (var particleSystem in vfx.GetComponentsInChildren<ParticleSystem>()) {
        particleSystem.Play();
      }*/



      /* if (hit) {
         Debug.Log("Hit: "+ hit.collider.gameObject.name);
         var vfx = Instantiate(Effect, new Vector3(hit.point.x, hit.point.y, 0), Quaternion.identity);

         // aoe on impact
         Collider2D[] colliders = Physics2D.OverlapCircleAll(hit.point, 1.0f);
         foreach (Collider2D collider in colliders) {
           collider.gameObject.GetComponent<Health>()?.TakeDamage(10);
         }

         if (hit.collider.CompareTag("Enemy")) {
           Debug.Log("Enemy hit");
         } else {
           Debug.Log("hit something else");
         }
         var targetPos = hit.collider.gameObject.transform.position; //Save the position of the object mouse was over

       }*/

}
