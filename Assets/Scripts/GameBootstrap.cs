using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    public Material groundMat, wallMat, roofMat, treeMat, trunkMat, playerMat, enemyMat;
    void Start()
    {
        Application.targetFrameRate = 60;
        CreateMaterials();
        BuildIsland();
        var player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        player.name = "Player";
        player.transform.position = new Vector3(0, 1, 0);
        player.GetComponent<Renderer>().material = playerMat;
        player.AddComponent<PlayerCombat>();
        player.AddComponent<PlayerMotor>();
        var cam = Camera.main;
        if (!cam) { var c = new GameObject("Main Camera"); cam = c.AddComponent<Camera>(); c.tag = "MainCamera"; }
        cam.gameObject.AddComponent<ThirdPersonCamera>();
        cam.gameObject.GetComponent<ThirdPersonCamera>().target = player.transform;
        for (int i = 0; i < 8; i++)
        {
            Vector2 p = Random.insideUnitCircle.normalized * Random.Range(13f, 29f);
            var bot = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            bot.name = "Enemy Bot " + (i + 1);
            bot.transform.position = new Vector3(p.x, 1, p.y);
            bot.GetComponent<Renderer>().material = enemyMat;
            var ai = bot.AddComponent<EnemyBot>();
            ai.target = player.transform;
        }
        var gm = new GameObject("Game Manager");
        gm.AddComponent<GameManager>().player = player.GetComponent<PlayerCombat>();
        var ui = new GameObject("Mobile HUD");
        ui.AddComponent<MobileHUD>();
    }

    void CreateMaterials()
    {
        groundMat = Make("Island Grass", new Color(.25f,.42f,.19f));
        wallMat = Make("House Sandstone", new Color(.72f,.66f,.48f));
        roofMat = Make("Roof", new Color(.25f,.16f,.12f));
        treeMat = Make("Tree Leaves", new Color(.08f,.28f,.10f));
        trunkMat = Make("Tree Trunk", new Color(.28f,.16f,.08f));
        playerMat = Make("Player Blue", new Color(.08f,.35f,.65f));
        enemyMat = Make("Enemy Red", new Color(.65f,.12f,.10f));
    }
    Material Make(string n, Color c) { var m = new Material(Shader.Find("Standard")); m.name=n; m.color=c; return m; }

    void BuildIsland()
    {
        var ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.name="Island"; ground.transform.localScale=new Vector3(10,1,10);
        ground.GetComponent<Renderer>().material=groundMat;
        for(int i=0;i<26;i++)
        {
            Vector2 p=Random.insideUnitCircle*43;
            if(p.magnitude<10) continue;
            var trunk=GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            trunk.transform.position=new Vector3(p.x,1.1f,p.y);
            trunk.transform.localScale=new Vector3(.35f,1.1f,.35f);
            trunk.GetComponent<Renderer>().material=trunkMat;
            var crown=GameObject.CreatePrimitive(PrimitiveType.Sphere);
            crown.transform.position=new Vector3(p.x,2.7f,p.y);
            crown.transform.localScale=Vector3.one*2.5f;
            crown.GetComponent<Renderer>().material=treeMat;
        }
        MakeHouse(new Vector3(-13,0,12)); MakeHouse(new Vector3(16,0,-13));
        MakeHouse(new Vector3(-22,0,-17)); MakeHouse(new Vector3(23,0,19));
        RenderSettings.ambientLight=new Color(.65f,.68f,.72f);
        var sun=new GameObject("Sun").AddComponent<Light>();
        sun.type=LightType.Directional; sun.intensity=1.15f; sun.transform.rotation=Quaternion.Euler(48,-32,0);
    }
    void MakeHouse(Vector3 p)
    {
        var body=GameObject.CreatePrimitive(PrimitiveType.Cube);
        body.name="House"; body.transform.position=p+Vector3.up*1.5f; body.transform.localScale=new Vector3(6,3,5);
        body.GetComponent<Renderer>().material=wallMat;
        var roof=GameObject.CreatePrimitive(PrimitiveType.Cube);
        roof.transform.position=p+Vector3.up*3.25f; roof.transform.localScale=new Vector3(6.5f,.5f,5.5f);
        roof.GetComponent<Renderer>().material=roofMat;
    }
}