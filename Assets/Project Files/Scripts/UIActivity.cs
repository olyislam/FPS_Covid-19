using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActivity
{
   /*
    * this class use for ui transiction 
    * make global instance of this class and pass new page to control ui
    */


    [Tooltip("Hold Current Activity that is still active on the screen")]
    private GameObject CurrentActivity;/***********/


    /// <summary>
    /// Active new page and deactive previous page,
    /// it will Show new Activity page on the screen 
    /// </summary>
    /// <param name="NewActivity"></param>
    public void SetActivity(GameObject NewActivity)
    {
        if (this.CurrentActivity == null)
        {
            this.CurrentActivity = NewActivity;
            this.CurrentActivity.SetActive(true);
            return;
        }

        this.CurrentActivity.SetActive(false);
        this.CurrentActivity = NewActivity;
        this.CurrentActivity.SetActive(true);
    }
}
