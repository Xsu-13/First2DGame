using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKelli : MonoBehaviour
{
    #region Inspector
    public string horizontalAxis = "Horizontal";

    public KelliModel model;

    private void OnValidate()
    {
        if (model == null)
            model = GetComponent<KelliModel>();
    }
    #endregion

    
    void Update()
    {
        if (model == null) return;

        float currentHorizontal = Input.GetAxisRaw(horizontalAxis);
        model.TryMove(currentHorizontal);

        //TO DO
        if (Input.GetMouseButtonDown(0))
            model.TryAttack();

        if (Input.GetKeyDown(KeyCode.Space))
            model.TryJump();

        if (Input.GetKeyDown(KeyCode.S))
            model.TrySqade();
    }
}
