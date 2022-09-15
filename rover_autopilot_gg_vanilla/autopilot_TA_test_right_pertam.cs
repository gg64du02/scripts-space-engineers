
//public PID PowerController = new PID(2, 0, .1, 1);
public string Animation = "=|=";
public List<IMyMotorSuspension> Wheels = new List<IMyMotorSuspension>();
public IMyRemoteControl RemoteControl;
public IMySensorBlock Sensor;

public Vector3D myTerrainTarget = new Vector3D(0,0,0);

MyWaypointInfo myWaypointInfoTerrainTarget = new MyWaypointInfo("target", 0, 0, 0);

IMyRadioAntenna theAntenna = null;

string str_to_display = "";

List<faceRegionPolygon> faceRegionPolygonList = new List<faceRegionPolygon>();

string planetRegionPolygonsLoaded = "Pertam";
		
List<Point> testPointRegionsLinked =  new List<Point>();

List<Node> nodes = new List<Node>();

IEnumerator<bool> _stateMachine;

public Program()
{
    // The constructor, called only once every session and
    // always before any other method is called. Use it to
    // initialize your script. 
    //     
    // The constructor is optional and can be removed if not
    // needed.
    // 
    // It's recommended to set RuntimeInfo.UpdateFrequency 
    // here, which will allow your script to run itself without a 
    // timer block.
	
	var Blocks = new List<IMyTerminalBlock>();
	GridTerminalSystem.GetBlocks(Blocks);
	Wheels = Blocks.FindAll(x => x.IsSameConstructAs(Me) && x is IMyMotorSuspension).Select(x => x as IMyMotorSuspension).ToList();
	RemoteControl = Blocks.Find(x => x.IsSameConstructAs(Me) && x is IMyRemoteControl) as IMyRemoteControl;
	//Sensor = Blocks.Find(x => x.IsSameConstructAs(Me) && x is IMySensorBlock) as IMySensorBlock;

	
	theAntenna = Blocks.Find(x => x.IsSameConstructAs(Me) && x is IMyRadioAntenna) as IMyRadioAntenna;

	Runtime.UpdateFrequency = UpdateFrequency.Update10;
	
	// List<faceRegionPolygon> faceRegionPolygonList = new List<faceRegionPolygon>();
	faceRegionPolygonList = new List<faceRegionPolygon>();
	faceRegionPolygon faceRegionPolygon1 = null;
	List<Point> tmpPolygon = new List<Point>();
	//===========
	tmpPolygon = new List<Point>();
	tmpPolygon.Add(new Point((int)0,(int)2047));
	tmpPolygon.Add(new Point((int)0,(int)0));
	tmpPolygon.Add(new Point((int)2047,(int)0));
	tmpPolygon.Add(new Point((int)2047,(int)2047));
	tmpPolygon.Add(new Point((int)0,(int)2047));
	faceRegionPolygon1 = new faceRegionPolygon(5,1,new Point((int)1028.9155873215373,(int)1029.3366647002654),tmpPolygon);
	//===========
	faceRegionPolygonList.Add(faceRegionPolygon1);
	//===========
	tmpPolygon = new List<Point>();
	tmpPolygon.Add(new Point((int)0.0,(int)2047.0));
	tmpPolygon.Add(new Point((int)0.0,(int)0.0));
	tmpPolygon.Add(new Point((int)1121.01,(int)0.0));
	tmpPolygon.Add(new Point((int)1101.01,(int)61.0));
	tmpPolygon.Add(new Point((int)1104.01,(int)125.0));
	tmpPolygon.Add(new Point((int)1118.0,(int)187.99));
	tmpPolygon.Add(new Point((int)1135.01,(int)250.0));
	tmpPolygon.Add(new Point((int)1095.01,(int)300.0));
	tmpPolygon.Add(new Point((int)1091.01,(int)364.0));
	tmpPolygon.Add(new Point((int)1112.0,(int)424.99));
	tmpPolygon.Add(new Point((int)1120.01,(int)489.0));
	tmpPolygon.Add(new Point((int)1088.01,(int)545.0));
	tmpPolygon.Add(new Point((int)1072.01,(int)607.0));
	tmpPolygon.Add(new Point((int)1016.01,(int)640.0));
	tmpPolygon.Add(new Point((int)969.0,(int)684.01));
	tmpPolygon.Add(new Point((int)943.0,(int)742.99));
	tmpPolygon.Add(new Point((int)935.01,(int)807.0));
	tmpPolygon.Add(new Point((int)927.01,(int)871.0));
	tmpPolygon.Add(new Point((int)905.01,(int)932.0));
	tmpPolygon.Add(new Point((int)897.01,(int)996.0));
	tmpPolygon.Add(new Point((int)928.01,(int)1052.0));
	tmpPolygon.Add(new Point((int)976.01,(int)1095.0));
	tmpPolygon.Add(new Point((int)1030.99,(int)1128.0));
	tmpPolygon.Add(new Point((int)1063.01,(int)1184.0));
	tmpPolygon.Add(new Point((int)1061.01,(int)1248.0));
	tmpPolygon.Add(new Point((int)1066.0,(int)1311.99));
	tmpPolygon.Add(new Point((int)1064.0,(int)1375.99));
	tmpPolygon.Add(new Point((int)1079.01,(int)1439.0));
	tmpPolygon.Add(new Point((int)1111.01,(int)1495.0));
	tmpPolygon.Add(new Point((int)1091.01,(int)1556.0));
	tmpPolygon.Add(new Point((int)1082.01,(int)1620.0));
	tmpPolygon.Add(new Point((int)1096.01,(int)1683.0));
	tmpPolygon.Add(new Point((int)1104.01,(int)1747.0));
	tmpPolygon.Add(new Point((int)1078.01,(int)1806.0));
	tmpPolygon.Add(new Point((int)1071.01,(int)1870.0));
	tmpPolygon.Add(new Point((int)1070.0,(int)1933.99));
	tmpPolygon.Add(new Point((int)1052.0,(int)1995.99));
	tmpPolygon.Add(new Point((int)0.0,(int)2047.0));
	faceRegionPolygon1 = new faceRegionPolygon(0,2,new Point((int)529.5453943056494,(int)1038.7349670487329),tmpPolygon);
	//===========
	faceRegionPolygonList.Add(faceRegionPolygon1);
	//===========
	tmpPolygon = new List<Point>();
	tmpPolygon.Add(new Point((int)1043.0,(int)2047.0));
	tmpPolygon.Add(new Point((int)1075.99,(int)1992.0));
	tmpPolygon.Add(new Point((int)1083.99,(int)1928.0));
	tmpPolygon.Add(new Point((int)1083.0,(int)1863.01));
	tmpPolygon.Add(new Point((int)1094.99,(int)1800.0));
	tmpPolygon.Add(new Point((int)1125.99,(int)1744.0));
	tmpPolygon.Add(new Point((int)1118.0,(int)1680.01));
	tmpPolygon.Add(new Point((int)1094.99,(int)1620.0));
	tmpPolygon.Add(new Point((int)1104.99,(int)1556.0));
	tmpPolygon.Add(new Point((int)1127.99,(int)1496.0));
	tmpPolygon.Add(new Point((int)1100.0,(int)1438.01));
	tmpPolygon.Add(new Point((int)1090.0,(int)1374.01));
	tmpPolygon.Add(new Point((int)1081.0,(int)1310.01));
	tmpPolygon.Add(new Point((int)1089.99,(int)1246.0));
	tmpPolygon.Add(new Point((int)1081.0,(int)1182.01));
	tmpPolygon.Add(new Point((int)1043.99,(int)1129.0));
	tmpPolygon.Add(new Point((int)1000.0,(int)1081.01));
	tmpPolygon.Add(new Point((int)947.0,(int)1045.01));
	tmpPolygon.Add(new Point((int)922.0,(int)986.01));
	tmpPolygon.Add(new Point((int)947.0,(int)927.01));
	tmpPolygon.Add(new Point((int)938.0,(int)863.01));
	tmpPolygon.Add(new Point((int)945.0,(int)799.01));
	tmpPolygon.Add(new Point((int)955.0,(int)735.01));
	tmpPolygon.Add(new Point((int)988.0,(int)680.01));
	tmpPolygon.Add(new Point((int)1048.99,(int)659.0));
	tmpPolygon.Add(new Point((int)1087.99,(int)608.0));
	tmpPolygon.Add(new Point((int)1114.0,(int)549.01));
	tmpPolygon.Add(new Point((int)1130.99,(int)487.0));
	tmpPolygon.Add(new Point((int)1128.0,(int)423.01));
	tmpPolygon.Add(new Point((int)1104.99,(int)363.0));
	tmpPolygon.Add(new Point((int)1112.99,(int)299.0));
	tmpPolygon.Add(new Point((int)1159.99,(int)254.0));
	tmpPolygon.Add(new Point((int)1223.0,(int)266.99));
	tmpPolygon.Add(new Point((int)1261.0,(int)318.99));
	tmpPolygon.Add(new Point((int)1319.99,(int)292.0));
	tmpPolygon.Add(new Point((int)1369.99,(int)252.0));
	tmpPolygon.Add(new Point((int)1382.99,(int)189.0));
	tmpPolygon.Add(new Point((int)1403.0,(int)127.99));
	tmpPolygon.Add(new Point((int)1383.0,(int)67.01));
	tmpPolygon.Add(new Point((int)1344.0,(int)16.01));
	tmpPolygon.Add(new Point((int)2047.0,(int)0.0));
	tmpPolygon.Add(new Point((int)2047.0,(int)2047.0));
	tmpPolygon.Add(new Point((int)1043.0,(int)2047.0));
	faceRegionPolygon1 = new faceRegionPolygon(0,3,new Point((int)648.8865636587907,(int)1061.6263019142325),tmpPolygon);
	//===========
	faceRegionPolygonList.Add(faceRegionPolygon1);
	//===========
	tmpPolygon = new List<Point>();
	tmpPolygon.Add(new Point((int)1275.0,(int)0.0));
	tmpPolygon.Add(new Point((int)1324.0,(int)42.99));
	tmpPolygon.Add(new Point((int)1375.0,(int)81.99));
	tmpPolygon.Add(new Point((int)1372.01,(int)146.0));
	tmpPolygon.Add(new Point((int)1351.01,(int)207.0));
	tmpPolygon.Add(new Point((int)1327.01,(int)267.0));
	tmpPolygon.Add(new Point((int)1269.0,(int)295.01));
	tmpPolygon.Add(new Point((int)1227.0,(int)245.01));
	tmpPolygon.Add(new Point((int)1168.01,(int)219.0));
	tmpPolygon.Add(new Point((int)1131.99,(int)166.0));
	tmpPolygon.Add(new Point((int)1115.0,(int)104.01));
	tmpPolygon.Add(new Point((int)1131.0,(int)42.01));
	tmpPolygon.Add(new Point((int)1275.0,(int)0.0));
	faceRegionPolygon1 = new faceRegionPolygon(0,4,new Point((int)137.41628772219926,(int)132.52623254309182),tmpPolygon);
	//===========
	faceRegionPolygonList.Add(faceRegionPolygon1);
	//===========
	tmpPolygon = new List<Point>();
	tmpPolygon.Add(new Point((int)0,(int)2047));
	tmpPolygon.Add(new Point((int)0,(int)0));
	tmpPolygon.Add(new Point((int)2047,(int)0));
	tmpPolygon.Add(new Point((int)2047,(int)2047));
	tmpPolygon.Add(new Point((int)0,(int)2047));
	faceRegionPolygon1 = new faceRegionPolygon(1,5,new Point((int)1026.8545143752206,(int)1023.7872403211838),tmpPolygon);
	//===========
	faceRegionPolygonList.Add(faceRegionPolygon1);
	//===========
	tmpPolygon = new List<Point>();
	tmpPolygon.Add(new Point((int)0,(int)2047));
	tmpPolygon.Add(new Point((int)0,(int)0));
	tmpPolygon.Add(new Point((int)2047,(int)0));
	tmpPolygon.Add(new Point((int)2047,(int)2047));
	tmpPolygon.Add(new Point((int)0,(int)2047));
	faceRegionPolygon1 = new faceRegionPolygon(4,6,new Point((int)1026.48198125909,(int)1017.539811192641),tmpPolygon);
	//===========
	faceRegionPolygonList.Add(faceRegionPolygon1);
	//===========
	tmpPolygon = new List<Point>();
	tmpPolygon.Add(new Point((int)160,(int)160));
	faceRegionPolygon1 = new faceRegionPolygon(4,7,new Point((int)348.52316448198604,(int)391.51294069171536),tmpPolygon);
	//===========
	faceRegionPolygonList.Add(faceRegionPolygon1);
	//===========
	tmpPolygon = new List<Point>();
	tmpPolygon.Add(new Point((int)0.0,(int)2047.0));
	tmpPolygon.Add(new Point((int)0.0,(int)0.0));
	tmpPolygon.Add(new Point((int)1030.01,(int)0.0));
	tmpPolygon.Add(new Point((int)1057.01,(int)59.0));
	tmpPolygon.Add(new Point((int)1066.01,(int)123.0));
	tmpPolygon.Add(new Point((int)1072.01,(int)187.0));
	tmpPolygon.Add(new Point((int)1080.01,(int)251.0));
	tmpPolygon.Add(new Point((int)1104.01,(int)311.0));
	tmpPolygon.Add(new Point((int)1095.01,(int)375.0));
	tmpPolygon.Add(new Point((int)1079.01,(int)437.0));
	tmpPolygon.Add(new Point((int)1090.01,(int)501.0));
	tmpPolygon.Add(new Point((int)1108.01,(int)563.0));
	tmpPolygon.Add(new Point((int)1077.01,(int)619.0));
	tmpPolygon.Add(new Point((int)1064.01,(int)682.0));
	tmpPolygon.Add(new Point((int)1064.0,(int)746.01));
	tmpPolygon.Add(new Point((int)1062.0,(int)809.99));
	tmpPolygon.Add(new Point((int)1072.01,(int)874.0));
	tmpPolygon.Add(new Point((int)1105.0,(int)928.99));
	tmpPolygon.Add(new Point((int)1142.0,(int)981.99));
	tmpPolygon.Add(new Point((int)1195.0,(int)1018.99));
	tmpPolygon.Add(new Point((int)1228.0,(int)1073.99));
	tmpPolygon.Add(new Point((int)1234.01,(int)1138.0));
	tmpPolygon.Add(new Point((int)1280.0,(int)1182.99));
	tmpPolygon.Add(new Point((int)1338.0,(int)1211.99));
	tmpPolygon.Add(new Point((int)1373.0,(int)1157.99));
	tmpPolygon.Add(new Point((int)1415.0,(int)1206.99));
	tmpPolygon.Add(new Point((int)1421.0,(int)1270.99));
	tmpPolygon.Add(new Point((int)1435.01,(int)1334.0));
	tmpPolygon.Add(new Point((int)1431.01,(int)1398.0));
	tmpPolygon.Add(new Point((int)1391.01,(int)1448.0));
	tmpPolygon.Add(new Point((int)1338.0,(int)1410.01));
	tmpPolygon.Add(new Point((int)1275.0,(int)1398.01));
	tmpPolygon.Add(new Point((int)1212.0,(int)1386.01));
	tmpPolygon.Add(new Point((int)1150.0,(int)1402.01));
	tmpPolygon.Add(new Point((int)1106.01,(int)1449.0));
	tmpPolygon.Add(new Point((int)1063.0,(int)1401.01));
	tmpPolygon.Add(new Point((int)999.01,(int)1393.0));
	tmpPolygon.Add(new Point((int)960.01,(int)1444.0));
	tmpPolygon.Add(new Point((int)935.01,(int)1504.0));
	tmpPolygon.Add(new Point((int)902.01,(int)1560.0));
	tmpPolygon.Add(new Point((int)852.0,(int)1520.01));
	tmpPolygon.Add(new Point((int)797.0,(int)1553.01));
	tmpPolygon.Add(new Point((int)760.01,(int)1606.0));
	tmpPolygon.Add(new Point((int)715.01,(int)1652.0));
	tmpPolygon.Add(new Point((int)687.01,(int)1710.0));
	tmpPolygon.Add(new Point((int)691.0,(int)1773.99));
	tmpPolygon.Add(new Point((int)736.01,(int)1820.0));
	tmpPolygon.Add(new Point((int)734.01,(int)1884.0));
	tmpPolygon.Add(new Point((int)713.01,(int)1945.0));
	tmpPolygon.Add(new Point((int)718.01,(int)2009.0));
	tmpPolygon.Add(new Point((int)0.0,(int)2047.0));
	faceRegionPolygon1 = new faceRegionPolygon(3,8,new Point((int)545.8768435332956,(int)973.7314479533823),tmpPolygon);
	//===========
	faceRegionPolygonList.Add(faceRegionPolygon1);
	//===========
	tmpPolygon = new List<Point>();
	tmpPolygon.Add(new Point((int)701.0,(int)2047.0));
	tmpPolygon.Add(new Point((int)1030.01,(int)0.0));
	tmpPolygon.Add(new Point((int)1057.01,(int)59.0));
	tmpPolygon.Add(new Point((int)1066.01,(int)123.0));
	tmpPolygon.Add(new Point((int)1072.01,(int)187.0));
	tmpPolygon.Add(new Point((int)1080.01,(int)251.0));
	tmpPolygon.Add(new Point((int)1104.01,(int)311.0));
	tmpPolygon.Add(new Point((int)1095.01,(int)375.0));
	tmpPolygon.Add(new Point((int)1079.01,(int)437.0));
	tmpPolygon.Add(new Point((int)1090.01,(int)501.0));
	tmpPolygon.Add(new Point((int)1108.01,(int)563.0));
	tmpPolygon.Add(new Point((int)1077.01,(int)619.0));
	tmpPolygon.Add(new Point((int)1064.01,(int)682.0));
	tmpPolygon.Add(new Point((int)1064.0,(int)746.01));
	tmpPolygon.Add(new Point((int)1062.0,(int)809.99));
	tmpPolygon.Add(new Point((int)1072.01,(int)874.0));
	tmpPolygon.Add(new Point((int)1105.0,(int)928.99));
	tmpPolygon.Add(new Point((int)1142.0,(int)981.99));
	tmpPolygon.Add(new Point((int)1195.0,(int)1018.99));
	tmpPolygon.Add(new Point((int)1228.0,(int)1073.99));
	tmpPolygon.Add(new Point((int)1234.01,(int)1138.0));
	tmpPolygon.Add(new Point((int)1280.0,(int)1182.99));
	tmpPolygon.Add(new Point((int)1338.0,(int)1211.99));
	tmpPolygon.Add(new Point((int)1373.0,(int)1157.99));
	tmpPolygon.Add(new Point((int)1415.0,(int)1206.99));
	tmpPolygon.Add(new Point((int)1421.0,(int)1270.99));
	tmpPolygon.Add(new Point((int)1435.01,(int)1334.0));
	tmpPolygon.Add(new Point((int)1431.01,(int)1398.0));
	tmpPolygon.Add(new Point((int)1391.01,(int)1448.0));
	tmpPolygon.Add(new Point((int)1338.0,(int)1410.01));
	tmpPolygon.Add(new Point((int)1275.0,(int)1398.01));
	tmpPolygon.Add(new Point((int)1212.0,(int)1386.01));
	tmpPolygon.Add(new Point((int)1150.0,(int)1402.01));
	tmpPolygon.Add(new Point((int)1106.01,(int)1449.0));
	tmpPolygon.Add(new Point((int)1063.0,(int)1401.01));
	tmpPolygon.Add(new Point((int)999.01,(int)1393.0));
	tmpPolygon.Add(new Point((int)960.01,(int)1444.0));
	tmpPolygon.Add(new Point((int)935.01,(int)1504.0));
	tmpPolygon.Add(new Point((int)902.01,(int)1560.0));
	tmpPolygon.Add(new Point((int)852.0,(int)1520.01));
	tmpPolygon.Add(new Point((int)797.0,(int)1553.01));
	tmpPolygon.Add(new Point((int)760.01,(int)1606.0));
	tmpPolygon.Add(new Point((int)715.01,(int)1652.0));
	tmpPolygon.Add(new Point((int)687.01,(int)1710.0));
	tmpPolygon.Add(new Point((int)691.0,(int)1773.99));
	tmpPolygon.Add(new Point((int)736.01,(int)1820.0));
	tmpPolygon.Add(new Point((int)734.01,(int)1884.0));
	tmpPolygon.Add(new Point((int)713.01,(int)1945.0));
	tmpPolygon.Add(new Point((int)718.01,(int)2009.0));
	tmpPolygon.Add(new Point((int)1344.99,(int)2047.0));
	tmpPolygon.Add(new Point((int)1329.99,(int)1984.0));
	tmpPolygon.Add(new Point((int)1331.0,(int)1920.01));
	tmpPolygon.Add(new Point((int)1330.99,(int)1856.0));
	tmpPolygon.Add(new Point((int)1347.0,(int)1794.01));
	tmpPolygon.Add(new Point((int)1356.99,(int)1730.0));
	tmpPolygon.Add(new Point((int)1412.99,(int)1698.0));
	tmpPolygon.Add(new Point((int)1474.0,(int)1677.99));
	tmpPolygon.Add(new Point((int)1477.99,(int)1614.0));
	tmpPolygon.Add(new Point((int)1440.0,(int)1562.01));
	tmpPolygon.Add(new Point((int)1396.0,(int)1515.01));
	tmpPolygon.Add(new Point((int)1421.99,(int)1456.0));
	tmpPolygon.Add(new Point((int)1465.99,(int)1409.0));
	tmpPolygon.Add(new Point((int)1459.99,(int)1345.0));
	tmpPolygon.Add(new Point((int)1435.0,(int)1286.01));
	tmpPolygon.Add(new Point((int)1430.99,(int)1222.0));
	tmpPolygon.Add(new Point((int)1443.99,(int)1159.0));
	tmpPolygon.Add(new Point((int)1391.0,(int)1121.01));
	tmpPolygon.Add(new Point((int)1350.01,(int)1171.0));
	tmpPolygon.Add(new Point((int)1286.01,(int)1168.0));
	tmpPolygon.Add(new Point((int)1247.0,(int)1116.01));
	tmpPolygon.Add(new Point((int)1282.99,(int)1063.0));
	tmpPolygon.Add(new Point((int)1314.99,(int)1007.0));
	tmpPolygon.Add(new Point((int)1321.99,(int)943.0));
	tmpPolygon.Add(new Point((int)1308.0,(int)880.01));
	tmpPolygon.Add(new Point((int)1286.99,(int)819.0));
	tmpPolygon.Add(new Point((int)1302.99,(int)757.0));
	tmpPolygon.Add(new Point((int)1303.99,(int)693.0));
	tmpPolygon.Add(new Point((int)1295.0,(int)629.01));
	tmpPolygon.Add(new Point((int)1236.0,(int)602.01));
	tmpPolygon.Add(new Point((int)1177.0,(int)576.01));
	tmpPolygon.Add(new Point((int)1124.0,(int)538.01));
	tmpPolygon.Add(new Point((int)1102.99,(int)477.0));
	tmpPolygon.Add(new Point((int)1101.99,(int)413.0));
	tmpPolygon.Add(new Point((int)1117.99,(int)351.0));
	tmpPolygon.Add(new Point((int)1116.99,(int)287.0));
	tmpPolygon.Add(new Point((int)1090.0,(int)228.01));
	tmpPolygon.Add(new Point((int)1085.99,(int)164.0));
	tmpPolygon.Add(new Point((int)1083.99,(int)100.0));
	tmpPolygon.Add(new Point((int)1065.0,(int)38.01));
	tmpPolygon.Add(new Point((int)1344.0,(int)2047.0));
	tmpPolygon.Add(new Point((int)701.0,(int)2047.0));
	faceRegionPolygon1 = new faceRegionPolygon(3,9,new Point((int)472.9548859955224,(int)1183.7082535989894),tmpPolygon);
	//===========
	faceRegionPolygonList.Add(faceRegionPolygon1);
	//===========
	tmpPolygon = new List<Point>();
	tmpPolygon.Add(new Point((int)707.0,(int)2047.0));
	tmpPolygon.Add(new Point((int)735.0,(int)1988.99));
	tmpPolygon.Add(new Point((int)743.99,(int)1925.0));
	tmpPolygon.Add(new Point((int)756.0,(int)1862.01));
	tmpPolygon.Add(new Point((int)750.0,(int)1798.01));
	tmpPolygon.Add(new Point((int)705.0,(int)1751.01));
	tmpPolygon.Add(new Point((int)750.99,(int)1706.0));
	tmpPolygon.Add(new Point((int)768.0,(int)1644.01));
	tmpPolygon.Add(new Point((int)786.99,(int)1582.0));
	tmpPolygon.Add(new Point((int)842.99,(int)1551.0));
	tmpPolygon.Add(new Point((int)903.0,(int)1575.99));
	tmpPolygon.Add(new Point((int)950.99,(int)1533.0));
	tmpPolygon.Add(new Point((int)974.0,(int)1472.99));
	tmpPolygon.Add(new Point((int)1000.99,(int)1414.0));
	tmpPolygon.Add(new Point((int)1064.0,(int)1429.99));
	tmpPolygon.Add(new Point((int)1126.0,(int)1447.99));
	tmpPolygon.Add(new Point((int)1184.99,(int)1421.0));
	tmpPolygon.Add(new Point((int)1249.0,(int)1429.99));
	tmpPolygon.Add(new Point((int)1307.99,(int)1405.0));
	tmpPolygon.Add(new Point((int)1348.0,(int)1454.99));
	tmpPolygon.Add(new Point((int)1383.01,(int)1509.0));
	tmpPolygon.Add(new Point((int)1406.0,(int)1569.99));
	tmpPolygon.Add(new Point((int)1457.0,(int)1608.99));
	tmpPolygon.Add(new Point((int)1456.01,(int)1673.0));
	tmpPolygon.Add(new Point((int)1396.01,(int)1698.0));
	tmpPolygon.Add(new Point((int)1343.01,(int)1734.0));
	tmpPolygon.Add(new Point((int)1321.01,(int)1795.0));
	tmpPolygon.Add(new Point((int)1319.0,(int)1858.99));
	tmpPolygon.Add(new Point((int)1313.01,(int)1923.0));
	tmpPolygon.Add(new Point((int)1317.01,(int)1987.0));
	tmpPolygon.Add(new Point((int)707.0,(int)2047.0));
	faceRegionPolygon1 = new faceRegionPolygon(3,10,new Point((int)377.41486417253014,(int)343.1128606377722),tmpPolygon);
	//===========
	faceRegionPolygonList.Add(faceRegionPolygon1);
	//===========
	tmpPolygon = new List<Point>();
	tmpPolygon.Add(new Point((int)1345.0,(int)2047.0));
	tmpPolygon.Add(new Point((int)1329.99,(int)1984.0));
	tmpPolygon.Add(new Point((int)1331.0,(int)1920.01));
	tmpPolygon.Add(new Point((int)1330.99,(int)1856.0));
	tmpPolygon.Add(new Point((int)1347.0,(int)1794.01));
	tmpPolygon.Add(new Point((int)1356.99,(int)1730.0));
	tmpPolygon.Add(new Point((int)1412.99,(int)1698.0));
	tmpPolygon.Add(new Point((int)1474.0,(int)1677.99));
	tmpPolygon.Add(new Point((int)1477.99,(int)1614.0));
	tmpPolygon.Add(new Point((int)1440.0,(int)1562.01));
	tmpPolygon.Add(new Point((int)1396.0,(int)1515.01));
	tmpPolygon.Add(new Point((int)1421.99,(int)1456.0));
	tmpPolygon.Add(new Point((int)1465.99,(int)1409.0));
	tmpPolygon.Add(new Point((int)1459.99,(int)1345.0));
	tmpPolygon.Add(new Point((int)1435.0,(int)1286.01));
	tmpPolygon.Add(new Point((int)1430.99,(int)1222.0));
	tmpPolygon.Add(new Point((int)1443.99,(int)1159.0));
	tmpPolygon.Add(new Point((int)1391.0,(int)1121.01));
	tmpPolygon.Add(new Point((int)1350.01,(int)1171.0));
	tmpPolygon.Add(new Point((int)1286.01,(int)1168.0));
	tmpPolygon.Add(new Point((int)1247.0,(int)1116.01));
	tmpPolygon.Add(new Point((int)1282.99,(int)1063.0));
	tmpPolygon.Add(new Point((int)1314.99,(int)1007.0));
	tmpPolygon.Add(new Point((int)1321.99,(int)943.0));
	tmpPolygon.Add(new Point((int)1308.0,(int)880.01));
	tmpPolygon.Add(new Point((int)1286.99,(int)819.0));
	tmpPolygon.Add(new Point((int)1302.99,(int)757.0));
	tmpPolygon.Add(new Point((int)1303.99,(int)693.0));
	tmpPolygon.Add(new Point((int)1295.0,(int)629.01));
	tmpPolygon.Add(new Point((int)1236.0,(int)602.01));
	tmpPolygon.Add(new Point((int)1177.0,(int)576.01));
	tmpPolygon.Add(new Point((int)1124.0,(int)538.01));
	tmpPolygon.Add(new Point((int)1102.99,(int)477.0));
	tmpPolygon.Add(new Point((int)1101.99,(int)413.0));
	tmpPolygon.Add(new Point((int)1117.99,(int)351.0));
	tmpPolygon.Add(new Point((int)1116.99,(int)287.0));
	tmpPolygon.Add(new Point((int)1090.0,(int)228.01));
	tmpPolygon.Add(new Point((int)1085.99,(int)164.0));
	tmpPolygon.Add(new Point((int)1083.99,(int)100.0));
	tmpPolygon.Add(new Point((int)1065.0,(int)38.01));
	tmpPolygon.Add(new Point((int)2047.0,(int)0.0));
	tmpPolygon.Add(new Point((int)2047.0,(int)2047.0));
	tmpPolygon.Add(new Point((int)1345.0,(int)2047.0));
	faceRegionPolygon1 = new faceRegionPolygon(3,11,new Point((int)610.857566581086,(int)978.9165251292854),tmpPolygon);
	//===========
	faceRegionPolygonList.Add(faceRegionPolygon1);
	//===========
	tmpPolygon = new List<Point>();
	tmpPolygon.Add(new Point((int)160,(int)160));
	faceRegionPolygon1 = new faceRegionPolygon(3,12,new Point((int)116.29726278468841,(int)250.41804879514453),tmpPolygon);
	//===========
	faceRegionPolygonList.Add(faceRegionPolygon1);
	//===========
	tmpPolygon = new List<Point>();
	tmpPolygon.Add(new Point((int)160,(int)160));
	faceRegionPolygon1 = new faceRegionPolygon(3,13,new Point((int)254.42292544500816,(int)181.88698942299425),tmpPolygon);
	//===========
	faceRegionPolygonList.Add(faceRegionPolygon1);
	//===========
	tmpPolygon = new List<Point>();
	tmpPolygon.Add(new Point((int)0,(int)2047));
	tmpPolygon.Add(new Point((int)0,(int)0));
	tmpPolygon.Add(new Point((int)2047,(int)0));
	tmpPolygon.Add(new Point((int)2047,(int)2047));
	tmpPolygon.Add(new Point((int)0,(int)2047));
	faceRegionPolygon1 = new faceRegionPolygon(2,14,new Point((int)1023.6554673114691,(int)1025.6056052487309),tmpPolygon);
	//===========
	faceRegionPolygonList.Add(faceRegionPolygon1);

	

	testPointRegionsLinked =  new List<Point>();
	// testPointRegionsLinked.Add((int)1016.01,(int)640.0);

	testPointRegionsLinked.Add(new Point(2,1));
	testPointRegionsLinked.Add(new Point(5,3));
	testPointRegionsLinked.Add(new Point(6,1));
	testPointRegionsLinked.Add(new Point(6,5));
	testPointRegionsLinked.Add(new Point(6,3));
	testPointRegionsLinked.Add(new Point(6,2));
	testPointRegionsLinked.Add(new Point(8,1));
	testPointRegionsLinked.Add(new Point(8,2));
	testPointRegionsLinked.Add(new Point(11,3));
	testPointRegionsLinked.Add(new Point(11,9));
	testPointRegionsLinked.Add(new Point(11,5));
	testPointRegionsLinked.Add(new Point(14,1));
	testPointRegionsLinked.Add(new Point(14,8));
	testPointRegionsLinked.Add(new Point(14,11));
	testPointRegionsLinked.Add(new Point(14,6));
	// ================
// ================
 string nodesStringRight = @"9,1310,20
40,239,37
45,1100,37
52,1199,41
55,961,29
55,1315,26
58,782,16
58,980,19
60,959,25
61,959,24
61,1123,40
61,1285,23
61,1326,25
62,455,21
62,595,22
62,1327,25
64,1383,27
67,861,24
69,534,9
69,719,23
73,570,9
75,517,9
77,545,9
79,1034,30
82,603,15
83,908,20
85,734,23
87,1233,49
88,762,22
89,436,25
89,915,21
93,435,27
94,1029,36
99,270,19
100,324,31
101,206,14
103,816,20
107,41,119
107,619,20
109,226,20
110,677,24
111,962,22
112,678,23
113,664,16
114,811,24
116,550,27
116,684,20
116,718,11
117,518,26
117,555,29
117,556,28
118,557,29
119,181,14
119,1369,31
120,561,29
121,232,25
122,798,27
124,798,27
125,480,38
125,797,26
129,481,39
132,174,20
138,148,30
146,223,30
146,356,63
148,636,22
149,607,39
155,737,30
155,849,18
158,737,31
159,589,48
159,690,13
159,737,32
162,665,18
165,795,11
167,670,20
168,845,22
171,991,73
176,1126,88
178,1126,89
180,1126,91
183,1126,94
184,822,14
184,1126,95
185,1237,103
189,1278,94
190,863,20
191,1026,102
192,673,23
192,1027,103
193,743,42
199,1031,107
206,1126,114
207,462,85
207,741,48
209,422,81
209,423,81
209,424,80
210,355,105
211,1051,112
211,1058,111
215,1355,107
217,889,18
219,1171,117
230,843,23
235,555,89
235,556,89
236,515,90
238,863,16
239,966,45
248,609,65
248,792,60
249,791,60
266,772,79
272,875,13
278,174,100
278,175,101
278,868,15
279,176,102
280,268,143
284,937,22
287,658,60
289,769,89
293,1180,75
305,904,17
309,245,159
311,244,160
312,244,161
319,240,164
320,756,103
329,1514,237
331,238,164
337,998,53
341,484,55
341,998,54
347,771,97
347,1076,11
356,74,40
357,1146,50
358,1146,50
361,1002,49
363,1070,9
375,1049,16
375,1050,16
375,1051,15
376,1050,15
385,1290,75
386,1082,7
386,1180,23
392,499,62
393,1193,14
399,522,46
404,165,128
413,986,42
415,845,91
415,1116,38
421,1356,141
422,647,11
422,1068,18
426,1186,11
430,607,23
430,1080,30
431,1111,44
435,1200,13
436,1480,244
443,593,29
443,594,30
443,999,32
444,599,32
451,860,97
451,861,97
451,1441,222
454,493,31
457,995,27
457,1130,38
458,995,27
459,995,27
468,499,22
472,679,37
475,845,83
487,1160,26
488,1161,26
493,1173,30
494,1057,14
494,1174,30
498,360,97
502,1010,27
507,377,90
516,513,20
516,529,19
519,697,67
521,107,146
526,938,6
536,987,12
537,612,56
537,1110,34
538,1110,34
546,1214,67
553,298,58
558,1062,48
561,259,52
562,1023,52
563,917,3
564,1020,52
574,847,3
574,896,17
578,454,64
586,814,2
593,996,56
595,2,187
604,470,59
607,263,12
607,990,57
608,907,32
609,821,2
613,777,5
618,655,8
619,657,8
621,798,2
623,706,2
629,703,3
630,238,7
633,660,3
640,282,19
641,677,4
642,683,3
643,717,3
648,864,54
650,1926,257
651,629,18
652,628,19
652,722,2
657,737,6
658,245,11
660,708,2
660,1889,241
663,589,47
667,161,23
669,608,42
669,666,10
669,667,10
670,265,30
670,601,47
670,694,3
670,995,65
672,724,3
673,208,13
674,854,70
675,645,16
676,853,71
676,1761,214
677,853,72
678,736,11
679,852,73
680,706,3
680,852,74
684,851,77
686,805,57
689,808,61
690,809,62
690,847,80
691,809,63
692,810,64
693,810,65
694,811,66
695,811,67
695,812,68
695,813,68
696,813,69
697,814,70
698,814,71
699,815,72
700,815,73
703,818,77
704,818,78
705,819,79
707,266,52
709,822,84
711,823,86
714,245,46
717,828,94
730,611,43
741,375,93
747,343,83
748,383,89
752,573,23
754,534,24
759,461,55
765,431,69
766,327,66
767,326,65
782,1939,137
783,1939,136
784,1938,135
785,1939,135
786,1939,134
788,1939,132
790,1939,130
795,804,141
796,804,141
798,803,143
801,1940,121
802,1940,120
804,1940,118
805,541,24
805,1940,117
807,1941,116
812,801,153
812,1941,111
816,524,16
818,581,32
833,374,24
834,373,23
845,455,17
855,503,19
860,1947,76
861,508,19
861,521,13
861,1948,76
862,582,40
873,824,181
882,954,162
892,811,170
893,810,169
894,514,9
900,810,162
901,810,161
904,810,159
909,1843,8
917,288,61
919,291,62
920,292,63
924,1817,11
927,418,73
938,369,73
938,370,73
938,1817,9
940,328,85
940,362,76
945,1770,24
948,334,89
958,1873,24
960,1878,26
966,1041,125
970,1993,69
980,487,70
981,1801,28
990,1862,10
998,567,88
998,600,101
999,618,100
1004,584,99
1004,1809,35
1014,2037,108
1029,1820,32
1030,1821,32
1052,1210,45
1053,1777,8
1053,1840,44
1057,653,55
1060,1053,97
1073,1214,43
1083,685,43
1088,1186,22
1088,1187,22
1093,459,34
1094,460,35
1096,255,134
1108,1755,5
1109,1057,100
1112,482,46
1121,1864,94
1122,1865,95
1123,1866,96
1124,408,11
1124,1866,97
1132,408,10
1139,773,56
1139,1874,109
1142,765,55
1145,1884,115
1150,1794,46
1150,1795,47
1150,1796,47
1151,742,54
1154,1785,40
1156,1116,66
1156,1118,64
1156,1119,64
1159,1064,108
1160,1617,5
1168,164,219
1168,1233,63
1170,1485,13
1173,728,43
1194,602,72
1198,124,261
1198,1255,71
1202,1586,37
1211,1574,44
1212,1518,52
1212,1542,54
1212,1544,55
1217,1497,52
1217,1718,46
1218,1495,52
1218,1496,53
1227,1707,42
1234,1628,14
1238,1417,71
1241,829,35
1242,1360,65
1245,1428,70
1245,1821,27
1251,1225,57
1252,828,33
1260,220,192
1267,1130,72
1268,1183,56
1270,582,118
1275,790,23
1283,437,111
1284,528,110
1284,530,110
1284,531,110
1284,1342,44
1286,609,110
1287,515,110
1289,1338,42
1289,1778,15
1290,1777,15
1295,331,140
1295,350,133
1300,1754,24
1301,1753,25
1305,1712,35
1306,1649,32
1307,1259,28
1309,1337,29
1316,790,34
1316,1426,35
1317,921,61
1318,1384,5
1319,2017,32
1333,870,62
1333,1260,30
1337,888,66
1338,1930,42
1341,1363,3
1345,1921,45
1347,1966,14
1350,1319,28
1351,1007,41
1351,1561,36
1352,1214,27
1353,890,54
1360,1223,35
1369,1361,23
1369,1836,59
1374,1601,28
1375,1407,39
1377,1070,32
1390,640,53
1397,1792,47
1400,33,130
1401,34,129
1402,34,128
1403,34,127
1403,35,127
1405,35,125
1406,35,124
1409,1699,38
1411,1008,25
1414,246,42
1415,1648,33
1416,1322,10
1419,1041,7
1419,1309,11
1421,851,16
1422,1000,22
1424,1570,23
1427,1709,33
1432,1639,30
1433,464,37
1434,464,38
1438,464,41
1438,771,60
1442,770,62
1445,1117,58
1445,1773,44
1449,868,13
1452,750,55
1453,1580,32
1454,1580,32
1454,1958,76
1455,1073,34
1462,864,20
1462,926,19
1465,1659,17
1466,1681,14
1474,829,25
1476,1653,14
1476,1783,49
1477,1784,49
1482,655,35
1482,810,39
1486,1317,32
1496,1705,12
1496,1935,86
1497,1704,12
1501,1719,13
1502,607,54
1502,1625,13
1503,528,95
1503,1919,85
1504,543,86
1504,544,85
1504,545,85
1504,1917,85
1504,1918,85
1510,1881,84
1513,435,97
1513,504,106
1515,305,70
1515,384,79
1515,385,80
1515,386,80
1515,1357,37
1515,1956,67
1516,346,73
1516,1370,38
1517,816,23
1519,470,114
1519,955,15
1519,1960,62
1520,1723,12
1521,263,62
1521,287,69
1521,957,16
1522,286,69
1522,476,116
1522,865,22
1522,957,17
1522,1847,83
1522,1965,60
1525,1736,15
1528,803,9
1529,802,8
1531,741,24
1534,845,4
1534,916,14
1535,904,11
1535,1641,31
1536,902,12
1536,903,12
1536,1681,25
1536,1682,25
1537,1650,35
1540,884,19
1547,760,33
1549,158,53
1551,1665,39
1553,1412,16
1553,1515,32
1554,1506,31
1556,1520,30
1560,1534,23
1563,1441,22
1563,1578,31
1566,765,39
1567,987,16
1569,680,39
1570,851,24
1573,678,39
1573,955,17
1577,1265,50
1578,1748,42
1579,677,39
1579,1392,22
1581,1327,29
1581,1391,23
1582,1444,21
1584,1588,29
1587,121,59
1588,120,59
1588,339,10
1589,119,60
1589,1327,36
1589,1469,18
1592,1543,8
1593,1676,53
1594,106,63
1595,781,50
1595,1518,10
1596,104,65
1596,105,65
1596,1728,46
1598,623,26
1598,1579,21
1598,1671,51
1601,1545,7
1602,818,53
1603,1342,38
1604,1577,16
1607,1370,40
1607,1374,40
1608,831,57
1611,55,77
1612,1468,7
1614,925,51
1618,360,13
1620,384,15
1621,119,39
1622,2012,36
1623,120,37
1623,2013,37
1625,1950,32
1626,1439,12
1626,1443,14
1627,294,35
1627,696,43
1628,748,36
1629,854,58
1629,1742,30
1630,855,58
1630,1415,20
1631,312,36
1632,313,36
1632,314,36
1632,1501,24
1635,337,30
1637,463,78
1638,1472,15
1639,324,39
1639,1929,41
1640,1462,16
1643,1513,25
1643,1566,4
1644,1645,44
1644,1737,20
1646,1534,29
1646,1788,23
1647,1316,16
1649,276,17
1656,1934,33
1657,1586,19
1661,1563,20
1662,1847,38
1663,1548,26
1664,918,91
1666,466,84
1671,814,8
1672,813,8
1675,332,36
1675,665,47
1675,1926,21
1677,414,42
1678,413,42
1679,1342,25
1679,1357,30
1680,325,30
1680,341,33
1681,279,4
1683,307,17
1683,1361,29
1684,1416,25
1684,1491,17
1686,1448,25
1691,750,30
1691,937,106
1692,1979,31
1694,1433,33
1699,814,16
1703,221,34
1703,1429,28
1703,1976,28
1708,625,78
1708,1924,15
1710,595,92
1715,1847,51
1716,1529,34
1719,1298,39
1719,1359,17
1720,766,44
1720,783,33
1721,762,46
1721,765,45
1726,85,45
1726,86,44
1728,257,37
1728,1363,15
1730,757,47
1731,362,23
1732,259,38
1733,1379,15
1737,980,146
1738,261,38
1739,982,148
1741,983,150
1742,984,150
1744,985,152
1746,1666,85
1747,282,32
1747,541,119
1747,1667,86
1748,537,120
1751,319,34
1754,172,19
1759,191,12
1763,219,11
1764,724,55
1765,1674,101
1766,348,43
1773,1106,216
1780,1400,35
1783,1409,40
1783,1738,106
1785,1425,44
1786,1105,227
1787,1104,227
1791,1103,230
1792,1437,45
1797,216,23
1797,1037,211
1801,155,27
1807,1539,90
1807,1547,94
1808,1286,85
1809,1127,220
1809,1283,88
1810,522,123
1810,1127,220
1810,1281,90
1811,1279,92
1812,260,22
1814,221,32
1816,1838,127
1825,1582,106
1826,1583,106
1830,371,73
1831,211,36
1838,1635,130
1838,1636,130
1838,1637,131
1838,1695,163
1841,1157,206
1842,211,36
1849,1195,179
1850,1720,173
1856,340,72
1857,107,62
1857,199,33
1857,339,71
1859,1376,34
1866,43,97
1867,73,81
1868,175,34
1874,301,43
1885,742,115
1886,743,116
1887,742,116
1888,742,117
1889,742,118
1891,743,120
1894,744,123
1895,744,124
1909,185,16
1913,1326,102
1915,1333,100
1915,1334,99
1917,215,15
1918,207,15
1919,1492,11
1920,1808,225
1921,352,43
1927,875,171
1929,873,172
1930,872,173
1931,262,14
1931,871,173
1932,261,14
1932,870,174
1937,1375,90
1941,173,13
1941,245,18
1941,1379,88
1942,172,14
1944,168,16
1952,596,156
1961,1466,29
1963,1396,79
1964,387,41
1968,164,16
1968,408,40
1970,321,14
1973,275,16
1974,1523,37
1975,1531,39
1985,279,20
1985,1539,36
1989,283,20
1994,250,10
2000,125,37
2005,357,27
2013,212,26
2014,1865,283
2027,776,251";


	//string s = "You win some. You lose some.";
	string s = nodesStringRight;

	string[] subs  = s.Split('\n');

	int indexNumber = 0;
	
	foreach (var sub in subs)
	{
		//string[] subs = s.Split('\n');
		// Echo(sub);
		string[] subsub = sub.Split(',');
		// Echo(subsub[0]);
		// Echo(subsub[1]);
		// Echo(subsub[2]);
		Point position = new Point(int.Parse(subsub[0]),int.Parse(subsub[1]));
		int radius = int.Parse(subsub[2]);
		nodes.Add(new Node(indexNumber,position, radius));
		indexNumber = indexNumber + 1;
	}
	


    // Initialize our state machine
    _stateMachine = RunStuffOverTime();
	
    Runtime.UpdateFrequency |= UpdateFrequency.Once;
	
}

public void Save()
{
    // Called when the program needs to save its state. Use
    // this method to save your state to the Storage field
    // or some other means. 
    // 
    // This method is optional and can be removed if not
    // needed.
}



// ***MARKER: Coroutine Execution
public void RunStateMachine()
{
    // If there is an active state machine, run its next instruction set.
    if (_stateMachine != null) 
    {
		Echo("bool hasMoreSteps");
        // machine.
        bool hasMoreSteps = _stateMachine.MoveNext();

		Echo("bool hasMoreSteps2");
        // If there are no more instructions, we stop and release the state machine.
        if (hasMoreSteps)
        {
            // The state machine still has more work to do, so signal another run again, 
            // just like at the beginning.
            Runtime.UpdateFrequency |= UpdateFrequency.Once;
        } 
        else 
        {
            _stateMachine.Dispose();

            _stateMachine = null;
        }
    }
}

// ***MARKER: Coroutine Example
// The return value (bool in this case) is not important for this example. It is not
// actually in use.
public IEnumerator<bool> RunStuffOverTime() 
{
    // // For the very first instruction set, we will just switch on the light.
    // _panelLight.Enabled = true;
	yield return true;
	
	
    // while (true) 
    // {
        // yield return true;
    // }
	
	
	foreach(Node node1 in nodes){
		foreach(Node node2 in nodes){
			if(node1 != node2){
				Point diffPos = new Point(node1.position.X-node2.position.X,node1.position.Y-node2.position.Y);
				int distSq = diffPos.X*diffPos.X + diffPos.Y*diffPos.Y;
				int radius = node1.radius;
				if(radius*radius > distSq){
					// Echo("node2.index"+node2.index);
					nodes[nodes.IndexOf(node1)].neighborsNodesIndex.Add(node2.index);
					//nodes[nodes.IndexOf(node2)].neighborsNodesIndex.Add(node1.index);
				}
				
			}
		}
		Echo("nodes.IndexOf(node1):"+nodes.IndexOf(node1));
		yield return true;
	}
	graphRegened = true;

    // yield return true;

    // int i = 0;
	
    // while (true) 
    // {
        // // _textPanel.WriteText(i.ToString());
        // // i++;
		
        // yield return true;
    // }
}

 
// Point startPointGoal = new Point(1081,1031);//crash
 // Point startPointGoal = new Point(1794,1913);
 // Point startPointGoal = new Point(1102,1791);//crash ? bug358
 // Point startPointGoal = new Point(1425,1783);
//Point startPointGoal = new Point(1950,1664);
Point startPointGoal = new Point(1800,1664);//2node
//Point startPointGoal = new Point(0,1664);//crash
// Point startPointGoal = new Point(1081,1786);//crash
Point finalPointGoal = new Point(2043,1664);

public int closestNodeToPoint(Point thisPoint){
	List<int> indexNodes = new List<int>();
	List<double> indexRadiusSq = new List<double>();
	foreach(Node node in nodes){
		
		Point diffPos = new Point(node.position.X-thisPoint.X,node.position.Y-thisPoint.Y);
		int distSq = diffPos.X*diffPos.X + diffPos.Y*diffPos.Y;
		int radius = node.radius;
		if(radius*radius > distSq){
			// Echo("node.index"+node.index);
			// Echo("nodes.IndexOf(node):"+nodes.IndexOf(node));
			indexNodes.Add(nodes.IndexOf(node));
			indexRadiusSq.Add(distSq);
		}
	}
	
	int minIndexRadius = indexRadiusSq.IndexOf(indexRadiusSq.Min());
	
	// Echo("minIndexRadius:"+minIndexRadius);
	
	int indexOrClosestNode = indexNodes[minIndexRadius];
	Echo("indexOrClosestNode:"+indexOrClosestNode);
	
	return indexOrClosestNode;
		
}

public double heuristic(Point a, Point b){
	
    return (b.X - a.X)*(b.X - a.X) + (b.Y - a.Y)*(b.Y - a.Y);
}

public double distanceSquarred(Point a, Point b){
	
    return (b.X - a.X)*(b.X - a.X) + (b.Y - a.Y)*(b.Y - a.Y);
}

bool graphRegened = false;


public void Main(string argument, UpdateType updateSource)
{
    // The main entry point of the script, invoked every time
    // one of the programmable block's Run actions are invoked,
    // or the script updates itself. The updateSource argument
    // describes where the update came from.
    // 
    // The method itself is required, but the arguments above
    // can be removed if not needed.
	
	
	
	
	// foreach(int index in Range(0, nodes.Count)){
		// nodeGvalue.Add(-1);
	// }
	
	
	
	
	Echo("nodes.Count"+nodes.Count);
	
	
					
    if ((updateSource & UpdateType.Once) == UpdateType.Once)
    {
		Echo("oi1");
        RunStateMachine();
		Echo("oi2");
    }
	
	if( graphRegened ==false){
		return;
	}
	Echo("oi3");
	
	Echo("startPointGoal:"+startPointGoal);
	Echo("finalPointGoal:"+finalPointGoal);
	
	//TODO: trouver le bon node de start pour avoir l'heuristique correspondant
	int startingIndex = closestNodeToPoint(startPointGoal);
	
	Node nodeStarting = nodes[startingIndex];
	
	Echo("nodeStarting.position"+nodeStarting.position);
	
	//1 make an openlist containing only the starting node
	List<Node> openlist = new List<Node> ();
	// openlist.Add(nodes[3]);
	openlist.Add(nodeStarting);
	
	//2 make an empty closed list
	List<Node> closelist = new List<Node> ();
	
	int endingIndex = closestNodeToPoint(finalPointGoal);
	
	List<double> nodeGvalue = new List<double>();
	
	// Node ourDestinationNode = nodes[50];
	Node ourDestinationNode = nodes[endingIndex];
	// Node node = null;
	Node node = nodeStarting;
	
	List<double> testHeuristicFunction = heuristicFunction(ourDestinationNode.position);
	// // or
	// List<double> testHeuristicFunction = heuristicFunction(finalPointGoal);
	
	
	Dictionary<Node, double> gscore = new Dictionary<Node, double>();
	Dictionary<Node, double> fscore = new Dictionary<Node, double>();
	
	Dictionary<Node, Node> came_from = new Dictionary<Node, Node>();
	
	
	// is 0 because it does not cost anything to move from starting node
	gscore.Add(node,0);
	// fscore.Add(node,gscore[node]+heuristic(node.position,ourDestinationNode.position));
	fscore.Add(node,gscore[node]+Math.Sqrt(distanceSquarred(node.position,ourDestinationNode.position)));
	
	Echo("nodeStarting.index:"+nodeStarting.index);
	
	
	Echo("oi4");
	
	int debugCount = 0;
	
	
    //py heappush(oheap, (fscore[start], start))
	List<Node> listHeapNodes = new List<Node>();
	listHeapNodes.Add(nodeStarting);
	
	
	
	Echo("start.position:"+node.position);
	Echo("goal.position:"+ourDestinationNode.position);
	
	while(true){
		
		Echo("heap.C:"+listHeapNodes.Count);
		node = listHeapNodes[listHeapNodes.Count-1];
		listHeapNodes.RemoveAt(listHeapNodes.Count-1);
		
		// Echo("debugCount=====================:");
		// Echo("fscore["+node.index+"]:"+fscore[node]);
		// Echo("gscore["+node.index+"]:"+gscore[node]);
		// Echo("h:"+heuristic(node.position,ourDestinationNode.position));
		// Echo("node.position:"+node.position);
		Echo(""+Math.Sqrt(distanceSquarred(node.position,ourDestinationNode.position)));
			
		if(ourDestinationNode == node){
			Echo("goal reached");
			break;
		}
		else{		
			if(closelist.Contains(node)==false){
				closelist.Add(node);
			}
			List<Node> neighbors = new List<Node>();
			// Echo("nodes.Count:"+nodes.Count);
			Echo("node.neighborsNodesIndex.Count:"+node.neighborsNodesIndex.Count);
			foreach(int index in node.neighborsNodesIndex){
				if(closelist.Contains(nodes[index])==false){
					neighbors.Add(nodes[index]);
				}
			}
			Echo("neighbors.Count:"+neighbors.Count);
			foreach(Node neighbor in neighbors){
				
					 // Echo("here11");
				double tentative_g_score = gscore[node] + Math.Sqrt(heuristic(node.position, neighbor.position));
				if(closelist.Contains(neighbor)==true){
					double gscoreTmp = gscore.ContainsKey(neighbor) ? gscore[neighbor] : 0;
					if( tentative_g_score >=gscoreTmp){
						continue;
					}
				}
				
				double gscoreTmp2 = gscore.ContainsKey(neighbor) ? gscore[neighbor] : 0;
				if(tentative_g_score < gscoreTmp2 || listHeapNodes.Contains(neighbor)==false){
					 // py came_from[neighbor] = current
					 // Echo("here1");
					// gscore.Add(neighbor, tentative_g_score);
					// fscore.Add(neighbor, tentative_g_score + heuristic(neighbor.position,ourDestinationNode.position));
					came_from[neighbor] = node;
					gscore[neighbor] = tentative_g_score;
					//fscore[neighbor] = tentative_g_score + heuristic(neighbor.position,ourDestinationNode.position);
					fscore[neighbor] = tentative_g_score + Math.Sqrt(heuristic(neighbor.position,ourDestinationNode.position));
					listHeapNodes.Add(neighbor);
					 // Echo("here2");
				}
			}
				
		}
		
		
		
		if(debugCount ==245){
			break;
		}
		
		
		debugCount = debugCount + 1;
	}
	
	// List<Node> data = new List<Node>();
	
	// while(came_from.ContainsKey(node)){
		// Echo("data.Add(node);");
		// Echo("node.position:"+node.position);
		// Echo(""+Math.Sqrt(distanceSquarred(node.position,ourDestinationNode.position)));
		// data.Add(node);
		// node = came_from[node];
	// }
	
	
	
	// ==============================================================================
	
	if(theAntenna != null){
		theAntenna.HudText = str_to_display;
	}
	
    //var targetGpsString = "";
    //Echo("targetGpsString:" + targetGpsString);
    MyWaypointInfo myWaypointInfoTarget = new MyWaypointInfo("lol", 0, 0, 0);
    //MyWaypointInfo.TryParse("GPS:/// #4:53590.85:-26608.05:11979.08:", out myWaypointInfoTarget);

    if (argument != null)
    {
        if (argument != "")
        {
            Echo("argument:" + argument);
            if (argument.Contains(":#") == true)
            {
                Echo("if (argument.Contains(:#) == true)");
                MyWaypointInfo.TryParse(argument.Substring(0, argument.Length - 10), out myWaypointInfoTarget);
                // MyWaypointInfo.TryParse(argument.Substring(0, argument.Length - 10), out myWaypointInfoTerrainTarget);
            }
            else
            {
                Echo("not if (argument.Contains(:#) == true)");
                MyWaypointInfo.TryParse(argument, out myWaypointInfoTarget);
                // MyWaypointInfo.TryParse(argument, out myWaypointInfoTerrainTarget);
            }
            if (myWaypointInfoTarget.Coords != new Vector3D(0, 0, 0))
            {
                //x,y,z coords is global to remember between each loop
                myTerrainTarget = myWaypointInfoTarget.Coords;
				myWaypointInfoTerrainTarget = myWaypointInfoTarget;
            }
        }
    }

    if (myTerrainTarget == new Vector3D(0, 0, 0))
    {
        // //using the expected remote control to give us the center of the current planet
        // flightIndicatorsShipController.TryGetPlanetPosition(out myTerrainTarget);
    }
	
	Echo("myTerrainTarget:"+Vector3D.Round(myTerrainTarget,3));
	
	
	float SLerror = (float) (RemoteControl.SpeedLimit - RemoteControl.GetShipSpeed());
	
	if(myTerrainTarget== new Vector3D(0,0,0)){
		
		foreach (IMyMotorSuspension Wheel in Wheels)
		{
			Wheel.SetValue<Single>("Steer override", 0);
			Wheel.SetValue<float>("Propulsion override", 0);
			Wheel.Brake = true;
			
			RemoteControl.HandBrake = true;
			
			/*
			float MultiplierPO = (float) Vector3D.Dot(Wheel.WorldMatrix.Up, RemoteControl.WorldMatrix.Right);
			
			// str_to_display = ""+"MultiplierPO:"+Math.Round(MultiplierPO,3);
			// Echo(str_to_display);
			//SLerror = -0.2f;
			
			MyShipVelocities myShipVel = RemoteControl.GetShipVelocities();
			Vector3D linearSpeedsShip = myShipVel.LinearVelocity;
			
			
			//SLerror =(float) (0 - RemoteControl.GetShipSpeed());
			SLerror = (float) (-linearSpeedsShip.Dot(RemoteControl.WorldMatrix.Forward));
			
			float localPO = -MultiplierPO * SLerror;
			
			str_to_display = ""+"localPO:"+Math.Round(localPO,3);
				
			if(RemoteControl.GetShipSpeed()<1){
				
				Wheel.SetValue<float>("Propulsion override", 0);
			}
			else{
				Wheel.SetValue<float>("Propulsion override", 0.25f*localPO);
			}
			*/
		}
		
	}
	else
	{
		int facenumberCalculated = -1;
		Point pixelPosCalculated = new Point(0,0);
		
		faceAndPointOnPlanetsCalculated( RemoteControl,out facenumberCalculated,out pixelPosCalculated,false,new Vector3D(0,0,0));
		
		Echo("facenumberMain1:"+facenumberCalculated);
		Echo("pixelPosMain1:"+pixelPosCalculated);

		whichFileShouldIlook(facenumberCalculated);
		
		Vector3D targetV3Dabs = myWaypointInfoTerrainTarget.Coords;
		
		Echo("targetV3Dabs:"+Vector3D.Round(targetV3Dabs,3));
		
		
		
		int facenumberCalculatedTarget = -1;
		Point pixelPosCalculatedTarget = new Point(0,0);
		
		faceAndPointOnPlanetsCalculated( RemoteControl,out facenumberCalculatedTarget,out pixelPosCalculatedTarget,true,targetV3Dabs);
		
		
		// Echo("facenumberCalculatedTarget:"+facenumberCalculatedTarget);
		Echo("FNCalculatedTarget:"+facenumberCalculatedTarget);
		Echo("pixelPosCalcTarget:"+pixelPosCalculatedTarget);
		
		whichFileShouldIlook(facenumberCalculatedTarget);
		
		bool targetIsOnTheSameFace = false;
		if(facenumberCalculatedTarget==facenumberCalculated){
			targetIsOnTheSameFace = true;
		}
		else{
			targetIsOnTheSameFace = false;
		}
		Echo("targetIsOnTheSameFace:"+targetIsOnTheSameFace);
		
		
		// isThisPointInThisRegion(int roverCurrentFaceNumber, Point currentRoverPosition, faceRegionPolygon fRP)
		
		int currentRegionN = -1;
		int targetRegionN = -1;
		
		Echo("faceRegionPolyList.Count:"+faceRegionPolygonList.Count);
		foreach(faceRegionPolygon faceRegionPolygonCT in faceRegionPolygonList){
			int RegionNumber = faceRegionPolygonCT.regionNumber;
			bool testedRover = isThisPointInThisRegion(facenumberCalculated, pixelPosCalculated, faceRegionPolygonCT);
			if(testedRover==true){
				Echo("testedRover:RegNumber:"+RegionNumber);
				currentRegionN = RegionNumber;
			}
			bool testedTarget = isThisPointInThisRegion(facenumberCalculatedTarget, pixelPosCalculatedTarget, faceRegionPolygonCT);
			if(testedTarget==true){
				Echo("testedTarget:RegNumber:"+RegionNumber);
				targetRegionN = RegionNumber;
			}
		}
		Echo("currentRegionN:"+currentRegionN);
		Echo("targetRegionN:"+targetRegionN);
		
		Echo("If any of the two is -1 the script won't run");
		if(currentRegionN==-1||targetRegionN==-1){
			str_to_display = "target or rover not in region";
			return;
		}
		
		bool targetIsOnTheSameRegion = false;
		if(currentRegionN==targetRegionN){
			targetIsOnTheSameRegion = true;
		}
		else{
			targetIsOnTheSameRegion = false;
		}
		Echo("targetIsSameRegion:"+targetIsOnTheSameRegion);
		
		bool isThereADirectNeighbor = false;
		
		List<int> testSpaceRegionNumberCommon = new List<int>();
		
		int intermediateRegionNumber = -1;
		
		if(targetIsOnTheSameRegion==false){
			List<Point> testNeighrover = getAllConnectedRegions(currentRegionN);
			
			List<Point> testNeightarget = getAllConnectedRegions(targetRegionN);
			
			Echo("testNeighrover.C:"+testNeighrover.Count);
			Echo("testNeightarget.C:"+testNeightarget.Count);
			
			
			foreach(Point neigborRegRover in testNeighrover){
				Echo("neigborRegRover:"+neigborRegRover);
				if(areThoseRegionsConnected(neigborRegRover,targetRegionN,currentRegionN)==true){
					isThereADirectNeighbor = true;
				}
				if(neigborRegRover.X != currentRegionN){
					testSpaceRegionNumberCommon.Add(neigborRegRover.X);
				}
				if(neigborRegRover.Y != currentRegionN){
					testSpaceRegionNumberCommon.Add(neigborRegRover.Y);
				}
			}
			foreach(Point neigborRegTarget in testNeightarget){
				Echo("neigborRegTarget:"+neigborRegTarget);
				if(areThoseRegionsConnected(neigborRegTarget,targetRegionN,currentRegionN)==true){
					isThereADirectNeighbor = true;
				}
				if(neigborRegTarget.X != targetRegionN){
					testSpaceRegionNumberCommon.Add(neigborRegTarget.X);
				}
				if(neigborRegTarget.Y != targetRegionN){
					testSpaceRegionNumberCommon.Add(neigborRegTarget.Y);
				}
			}
			// Echo("testSpaceRegionNumberCommon.C:"+testSpaceRegionNumberCommon.Count);
			foreach(int regionNumber in testSpaceRegionNumberCommon){
				// Echo("regionNumber:"+regionNumber);
				int count = testSpaceRegionNumberCommon.Where(x => x.Equals(regionNumber)).Count();
				// Echo("count:"+count);
				if(count>1){
					intermediateRegionNumber = regionNumber;
				}
			}
			
			Echo("intermediateRegionNumber:"+intermediateRegionNumber);
			Echo("isThereADirectNeighbor:"+isThereADirectNeighbor);
		}
		// TODO:implement
		// what to do when the target and rover are not in the same region
		
		
		//getting vectors to help with angles proposals
		Vector3D shipForwardVector = RemoteControl.WorldMatrix.Forward;
		Vector3D shipLeftVector = RemoteControl.WorldMatrix.Left;
		Vector3D shipDownVector = RemoteControl.WorldMatrix.Down;
		
		double steerOverride = 0;
		// double steerOverride = shipForwardVector.Dot(Vector3D.Normalize(targetV3Dabs));
		// steerOverride*=100;
		// Echo("steerOverride:"+Math.Round(steerOverride,3))
		// ;
		Point testGetCen1 = getCentroidPointForThisRegion(currentRegionN);
		Echo("testGetCen1:"+testGetCen1);
		
		if(isThereADirectNeighbor==true){
			//TODO:do nothing ?
		}
		else{
			if(targetIsOnTheSameRegion==false){
				//TODO: implement this
				//getting centroid from any region
				Point testGetCen2 = getCentroidPointForThisRegion(currentRegionN);
				Echo("testGetCen2:"+testGetCen2);
				
				
				Point testGetCenInter = getCentroidPointForThisRegion(intermediateRegionNumber);
				Echo("testGetCenInter:"+testGetCenInter);
				
				int facenumberCalculatedIntermediate = -1;
				
				foreach(faceRegionPolygon faceRegionPolygon3 in faceRegionPolygonList){
					if(faceRegionPolygon3.regionNumber == intermediateRegionNumber){
						facenumberCalculatedIntermediate = faceRegionPolygon3.faceNumber;
					}
				}
				
				
				//facenumberCalculatedIntermediate ?
				if(facenumberCalculatedIntermediate!=-1){
					Vector3D rconvertPointToV3D = convertPointToV3D(RemoteControl , facenumberCalculatedIntermediate, testGetCenInter);
					Echo("rconvertPointToV3D:"+rconvertPointToV3D);
					
					MyWaypointInfo tmpWPI  = new MyWaypointInfo("inter", rconvertPointToV3D);
					
					Me.CustomData = tmpWPI.ToString();
					
					targetV3Dabs = rconvertPointToV3D;
				}
				
				// faceAndPointOnPlanetsCalculated(sc,?,testGetCenInter, true, 
				//TODO:implement here, convert point to gps
			}
			else{
				//do nothing
			}
		}
		
		
		Vector3D targetV3Drel = RemoteControl.GetPosition()-targetV3Dabs;
		
		Vector3D crossForwardTT = shipForwardVector.Cross((targetV3Drel));
		// Vector3D crossForwardTT = shipForwardVector.Cross(Vector3D.Normalize(targetV3Dabs));
		double turnRightOrLeft = crossForwardTT.Dot(shipDownVector);
		
		Echo("turnRightOrLeft:"+Math.Round(turnRightOrLeft,3));
		
		// str_to_display = ""+"turnRightOrLeft:"+Math.Round(turnRightOrLeft,3);
		
		
		steerOverride = turnRightOrLeft/crossForwardTT.Length();
		
		Echo("targetV3Drel.L:"+targetV3Drel.Length());
		
		if(targetV3Drel.Length()>10000){
			steerOverride *=4;
		}
		
		if(Math.Abs(steerOverride)<.98){
			steerOverride*=0.25;
		}
		// steerOverride*=0.25;
		
		steerOverride*=-1;
		// str_to_display = ""+"steerOverride:"+Math.Round(steerOverride,3);
		Echo("steerOverride:"+Math.Round(steerOverride,3));
		
		
		steerOverride = MyMath.Clamp(Convert.ToSingle(steerOverride), Convert.ToSingle(-1), Convert.ToSingle(1));
		
		
		foreach (IMyMotorSuspension Wheel in Wheels)
		{
			double areThisFrontWheel = shipForwardVector.Dot(Wheel.GetPosition() - RemoteControl.GetPosition());
			Echo("areThisFrontWheel:"+Math.Round(areThisFrontWheel,3));
			
			float MultiplierPO = (float) Vector3D.Dot(Wheel.WorldMatrix.Up, RemoteControl.WorldMatrix.Right);
			
			// str_to_display = ""+"MultiplierPO:"+Math.Round(MultiplierPO,3);
			// Echo(str_to_display);
			//SLerror = -0.2f;
			
			float localPO = -MultiplierPO * SLerror;
			
			str_to_display = ""+"localPO:"+Math.Round(localPO,3);
				
			if(areThisFrontWheel>0){
				Wheel.SetValue<Single>("Steer override", Convert.ToSingle(steerOverride));
				Wheel.SetValue<float>("Propulsion override", localPO);
				
			}
			else{
				// Wheel.SetValue<Single>("Steer override", Convert.ToSingle(-steerOverride));
				Wheel.SetValue<float>("Propulsion override", localPO);
			}
			
		}
		
		// //stop when destination is reached
		// if(targetV3Drel.Length()<5){
			// myTerrainTarget = new Vector3D(0, 0, 0);
		// }
		
		if(facenumberCalculated==facenumberCalculatedTarget){
			if(Math.Abs(pixelPosCalculated.X-pixelPosCalculatedTarget.X)<=1){
				if(Math.Abs(pixelPosCalculated.Y-pixelPosCalculatedTarget.Y)<=1){
					myTerrainTarget = new Vector3D(0, 0, 0);
				}
			}
		}
		
		
		
	}
	
	Echo("regionLinkCount:" + testPointRegionsLinked.Count );
	
	Echo("planetRegionPolynsLd:"+planetRegionPolygonsLoaded);
	
	

	
	// if (!RemoteControl.IsAutoPilotEnabled) {
	// }
}



public void faceAndPointOnPlanetsCalculated(IMyRemoteControl sc,out int facenumber,out Point pixelPos,bool debugMode,Vector3D testedV3D){
	
	// Echo(Me.GetPosition()+"");
	Vector3D myPos = sc.GetPosition();
	if(debugMode==true){
		myPos = testedV3D;
	}
	
	// foreach	(Point point in tmpTestNextPoints){
		// Echo("point"+point);
	// }
	
	Vector3D centerFacePositionOffset = new Vector3D(0,0,0);
	double planet_radius = 60000;
	
	Vector3D planetCenter = new Vector3D(0,0,0);

	bool planetDetected = sc.TryGetPlanetPosition(out planetCenter);
	
	Echo("planetCenter:"+planetCenter);
	
	// planet_radius = (int) (planetCenter-myPos).Length();
	planet_radius = (int) (myPos-planetCenter).Length();
	
	Echo("planet_radius:"+planet_radius);
	
	Vector3D myPosRelToCenter = (myPos-planetCenter);
	
	double myPosXAbs = Math.Abs(myPosRelToCenter.X);
	double myPosYAbs = Math.Abs(myPosRelToCenter.Y);
	double myPosZAbs = Math.Abs(myPosRelToCenter.Z);
	
	Vector3D projectedSphereVector  = new Vector3D(0,0,0);
	
	int faceNumber = -1;
	
	double pixelScalingToIGW = (2*planet_radius/2048);
	
	//shorter names formulas
	double intX = 0;
	double intY = 0;
	double intZ = 0;
	
	Point extractedPoint = new Point(0,0);
	double extractionX_pointRL = 0;
	double extractionY_pointRL = 0;
	
	if(myPosXAbs>myPosYAbs){
		if(myPosXAbs>myPosZAbs){
			projectedSphereVector = (planet_radius/myPosXAbs)*myPosRelToCenter;
			intY = projectedSphereVector.Y;
			intZ = projectedSphereVector.Z;
			if(myPosRelToCenter.X>0){
				faceNumber = 3;
				extractionX_pointRL = planet_radius - intY;
				extractionY_pointRL = planet_radius - intZ;
			}
			else{
				faceNumber = 4;
				extractionX_pointRL = planet_radius - intY;
				extractionY_pointRL = planet_radius + intZ;
			}
		}
	}
	
	if(myPosYAbs>myPosXAbs){
		if(myPosYAbs>myPosZAbs){
			projectedSphereVector = (planet_radius/myPosYAbs)*myPosRelToCenter;
			intX = projectedSphereVector.X;
			intZ = projectedSphereVector.Z;
			if(myPosRelToCenter.Y>0){
				faceNumber = 5;
				extractionY_pointRL = planet_radius - intX;
				extractionX_pointRL = planet_radius - intZ;
			}
			else{
				faceNumber = 1;
				extractionY_pointRL = planet_radius + intX ;
				extractionX_pointRL = planet_radius - intZ ;
			}
		}
	}
	
	if(myPosZAbs>myPosXAbs){
		if(myPosZAbs>myPosYAbs){
			projectedSphereVector = (planet_radius/myPosZAbs)*myPosRelToCenter;
			intX = projectedSphereVector.X;
			intY = projectedSphereVector.Y;
			if(myPosRelToCenter.Z>0){
				faceNumber = 0;
				extractionY_pointRL = planet_radius + intX;
				extractionX_pointRL = planet_radius - intY;
			}
			else{
				faceNumber = 2;
				extractionY_pointRL = planet_radius - intX;
				extractionX_pointRL =  planet_radius - intY;
			}
		}
	}
	
	if(extractionX_pointRL==0){
		//out-ing
		facenumber =faceNumber;
		pixelPos = new Point(0,0);
		
		return;}
	
	if(extractionY_pointRL==0){
		
		//out-ing
		facenumber =faceNumber;
		pixelPos = new Point(0,0);
		return;}
	
	double tmpCalcX = extractionX_pointRL / pixelScalingToIGW;
	double tmpCalcY = extractionY_pointRL / pixelScalingToIGW;
	
	extractedPoint = new Point((int)tmpCalcX,(int)tmpCalcY);
	
	//Echo("extractedPoint:"+extractedPoint);
	//Echo("faceNumber:"+faceNumber);
	//Echo("projectedSphereVector:"+projectedSphereVector);
	
	Point calculatedPoint = new Point(-1,-1);
	
	
	//out-ing
	facenumber =faceNumber;
	pixelPos =extractedPoint;
	
}

public void whichFileShouldIlook(int facenumber){
	
	string tmpStr = ""+facenumber+" is ";
	
	if(facenumber==0){
		tmpStr += "back";
	}
	if(facenumber==1){
		tmpStr += "down";
	}
	
	if(facenumber==2){
		tmpStr += "front";
	}
	if(facenumber==3){
		tmpStr += "left";
	}
	
	if(facenumber==4){
		tmpStr += "right";
	}
	if(facenumber==5){
		tmpStr += "up";
	}
	
	Echo(tmpStr);
	
	// 0 is back
	// 1 is down

	// 2 is front
	// 3 is left

	// 4 is right
	// 5 is up
}



public class Node {
	// voronoi vertex
	public int index;
	public Point position;
	public int radius;
	public List<int> neighborsNodesIndex;
	
	public Node(int index,Point position, int radius){
		this.index = index;
		this.position = position;
		this.radius = radius;
		this.neighborsNodesIndex = new List<int>();
	}
	
	public String toString(){
		return "index is:"+index + "\n" + "position is:" + position + "\n"
		+ "radius is:" + radius;
	}
	
}
	



public class faceRegionPolygon {
	
	public int faceNumber;
	public int regionNumber;
	public List<Point> polygon;
	public Point regionCentroid;
	
	public faceRegionPolygon(int faceNumber,int regionNumber,Point regionCentroid,
	List<Point> polygon
	){
		this.faceNumber = faceNumber;
		this.regionNumber = regionNumber;
		this.polygon = polygon;
		this.regionCentroid = regionCentroid;
	}
	
	public String toString(){
		return "face is:"+faceNumber + "\n" + "regionNumber is:" + regionNumber + "\n"
		+ "regionCentroid is:" + regionCentroid;
	}
	
}
	
public bool isThisPointInThisRegion(int roverCurrentFaceNumber, Point currentPointT, faceRegionPolygon fRP){
	if(roverCurrentFaceNumber != fRP.faceNumber){
		return false;
	}
	// TODO:implement this
	
	// Echo("fRP.polygon.Count:"+fRP.polygon.Count);
	Echo("currentPointT:"+currentPointT);
	bool testResultTmp = InsidePolygon(fRP.polygon, fRP.polygon.Count, currentPointT);
	// Echo("testResultTmp:"+testResultTmp);
	return testResultTmp;
}

// eecs umich insidepoly
public bool InsidePolygon(List<Point> polygon,int N,Point p)
{
  int counter = 0;
  int i;
  double xinters;
  Point p1,p2;
  

  p1 = polygon[0];
  for (i=1;i<=N;i++) {
    p2 = polygon[i % N];
    if (p.Y > Math.Min(p1.Y,p2.Y)) {
      if (p.Y <= Math.Max(p1.Y,p2.Y)) {
        if (p.X <= Math.Max(p1.X,p2.X)) {
          if (p1.Y != p2.Y) {
            xinters = (p.Y-p1.Y)*(p2.X-p1.X)/(p2.Y-p1.Y)+p1.X;
            if (p1.X == p2.X || p.X <= xinters)
              counter++;
          }
        }
      }
    }
    p1 = p2;
  }
  
  // Echo("counter:"+counter);

  if (counter % 2 == 0)
    return(false);
  else
    return(true);
}

public List<Point> getAllConnectedRegions(int regionNumber){
	List<Point> resultNodes = new List<Point>();
	//TODO:implement
	foreach(Point node in testPointRegionsLinked){
		if(node.X == regionNumber){
			resultNodes.Add(node);
		}
		if(node.Y == regionNumber){
			resultNodes.Add(node);
		}
	}
	return resultNodes;
}


public bool areThoseRegionsConnected(Point node, int node1reg, int node2reg){
	bool tmpNode = false;
	if((node.X == node2reg) && (node.Y == node1reg)){
		tmpNode = true;
	}
	if((node.X == node1reg) && (node.Y == node2reg)){
		tmpNode = true;
	}
	return tmpNode;
}

public Point getCentroidPointForThisRegion(int regionNumberPar){
	Point tmpPoint = new Point(-1,-1);
	foreach( faceRegionPolygon faceRegionPolygon2 in faceRegionPolygonList){
		if(faceRegionPolygon2.regionNumber == regionNumberPar){
			tmpPoint = faceRegionPolygon2.regionCentroid;
		}
	}
	return tmpPoint;
}

public Vector3D convertPointToV3D(IMyRemoteControl sc, int faceNumber, Point pointToV3D){
	Vector3D resultV3D = new Vector3D(0,0,0);
	
	// Vector3D cubeCenter = detectedPlanet;
	
	double intX = 0;
	double intY = 0;
	double intZ = 0;
	
	Vector3D generated_gps_point_on_cube = new Vector3D(0,0,0);
					
	
	Vector3D centerFacePositionOffset = new Vector3D(0,0,0);
	double planet_radius = 60000;
	
	
    //Get the PB Position:
    Vector3D myPos = Me.GetPosition();
	
	Vector3D planetCenter = new Vector3D(0,0,0);

	bool planetDetected = sc.TryGetPlanetPosition(out planetCenter);
	
	Vector3D cubeCenter = planetCenter;
	
	double distanceToCenter = (cubeCenter - myPos).Length();
	
	planet_radius = distanceToCenter;
	
	Point surface_face_offset = new Point(0,0);
	
	// surface_face_offset.X = Convert.ToSingle((int)(pointToV3D.X * 2*planet_radius/2048));
	// surface_face_offset.Y = Convert.ToSingle((int)(pointToV3D.Y * 2*planet_radius/2048));
	surface_face_offset.X = (int)(pointToV3D.X * 2*planet_radius/2048);
	surface_face_offset.Y = (int)(pointToV3D.Y * 2*planet_radius/2048);
	
	

	if(faceNumber==0){
		intX = 1*(- planet_radius+surface_face_offset.Y*1);
		intY = -1*(- planet_radius+surface_face_offset.X*1);
		//intZ = planet_radius * (centroid_surface_lack[1]-2048/2) * planet_radius;
		generated_gps_point_on_cube = new Vector3D(intX, intY,planet_radius);
	}
	if(faceNumber==1){
		intX = 1*(- planet_radius+surface_face_offset.Y*1);
		//intY = -1*(- planet_radius+surface_face_offset.X*1);
		intZ = -1*(- planet_radius+surface_face_offset.X*1);
		generated_gps_point_on_cube = new Vector3D(intX,-planet_radius, intZ);
	}
	if(faceNumber==2){
		intX = -1*(- planet_radius+surface_face_offset.Y*1);
		intY = -1*(- planet_radius+surface_face_offset.X*1);
		//intZ = planet_radius * (centroid_surface_lack[1]-2048/2) * planet_radius;
		generated_gps_point_on_cube = new Vector3D(intX, intY,-planet_radius);	
	}
	if(faceNumber==3){
		// intX = 1*(- planet_radius+surface_face_offset.Y*1);
		intY = -1*(- planet_radius+surface_face_offset.X*1);
		intZ = -1*(- planet_radius+surface_face_offset.Y*1);
		generated_gps_point_on_cube = new Vector3D(planet_radius,intY, intZ);
	}
	if(faceNumber==4){
		//intX = 1*(- planet_radius+surface_face_offset.Y*1);
		intY = -1*(- planet_radius+surface_face_offset.X*1);
		intZ = 1*(- planet_radius+surface_face_offset.Y*1);
		generated_gps_point_on_cube = new Vector3D(-planet_radius,intY, intZ);
	}
	if(faceNumber==5){
		intX = -1*(- planet_radius+surface_face_offset.Y*1);
		// intY = -1*(- planet_radius+surface_face_offset.X*1);
		intZ = -1*(- planet_radius+surface_face_offset.X*1);
		//generated_gps_point_on_cube = arr.array('d', [intX,planet_radius, intZ,]+center_of_planet);
		generated_gps_point_on_cube = new Vector3D(intX,planet_radius, intZ);
	}

	Vector3D generated_gps_point_on_planet = new Vector3D(0,0,0);
	
	//Echo("generated_gps_point_on_cube"+generated_gps_point_on_cube);
	
	
	Vector3D generated_gps_point_on_cube_norm = Vector3D.Normalize(generated_gps_point_on_cube);
	
	
	generated_gps_point_on_planet =  planet_radius * Vector3D.Normalize(generated_gps_point_on_cube_norm)+ cubeCenter;
	
	generated_gps_point_on_planet = Vector3D.Round(generated_gps_point_on_planet,1);
					
	resultV3D = generated_gps_point_on_planet;
	
	return resultV3D;
}



public List<double> heuristicFunction(Point iPG){

	List<double> heuristicTmp = new List<double>();
	
	foreach(Node node in nodes){
		Point diffPos = new Point(node.position.X-iPG.X,node.position.Y-iPG.Y);
		int distSq = diffPos.X*diffPos.X + diffPos.Y*diffPos.Y;
		double dist = Math.Sqrt(distSq);
		heuristicTmp.Add(dist);
		// Echo("dist:"+dist);
		// if(nodes.IndexOf(node)==10){
			// break;
		// }
	}
	
	return heuristicTmp;
}
	