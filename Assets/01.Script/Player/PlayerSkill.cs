using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField] LaserController laser;
    [SerializeField] float laserOffset = 4f;
    [SerializeField] Player player;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Vector3 pos = player.transform.position + Vector3.right * laserOffset * player.FacingDir;
            laser.Fire(pos);
        }
    }
}
