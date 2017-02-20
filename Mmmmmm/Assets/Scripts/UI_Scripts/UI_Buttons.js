#pragma strict


var nextScene : String;

function Start () {
	
}

function Update () {
	
}


function OnMouseOver()
{
   //transform.position = Vector3.x(+.3);
}

function OnMouseExit()
{
   //transform.position = Vector3.x(-.3);
}

function OnMouseDown () {
    test();
}


 function test() {
     Debug.Log("Hello");
 }


 function LoadScene () {
     Application.LoadLevel (nextScene);
 }