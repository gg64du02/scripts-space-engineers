
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
 string nodesStringRight = @"09ku0004
0E3L010w0C
0Jhc02090m
0Tf103060708
0Tkz04000a0b0e
0Wce050r0z
0Wfk06030m0v
0Ye_07030t0E
0Ze_08030t0E
0Zhz09021d1e1f1g1i
0Zk50a040q1k
0ZkK0b040f0Q
0-770c0s0V
0-9j0d0j0n
0-kL0e040f0Q
10lD0f0b0e0Q
13dt0g0o0z
158m0h0k0l
15bf0i0p0D
198W0j0d0l0M0N0O
1b850k0h0L
1d8x0l0h0j0I
1fga0m02060v
1i9r0n0d0B0R
1jec0o0g0t
1lbu0p0i0r0K
1njh0q0a1j
1obW0r050p0T
1p6Q0s0c0u
1pej0t07080o0E
1t6P0u0s0V0_
1ug50v060m1m1o
1z4e0w010x0S
1A540x0w0_
1B3e0y0C0P
1DcM0z050g0H
1H0F0A0Z1O1P1R
1H9H0B0n0G11
1J3y0C010y0S
1KaB0D0i0F0G
1Lf20E07080t1c
1MaC0F0D0J18
1Nao0G0B0D10
1OcH0H0z0T13
1Q8C0I0l0L0M0N0O
1QaI0J0F0K16
1Qbe0K0p0J12
1R860L0k0I0X
1R8H0M0j0I0R
1R8I0N0j0I0R
1S8J0O0j0I0R
1T2R0P0y0Y
1Tlp0Q0b0e0f1A
1U8N0R0n0M0N0O15
1V3E0S0w0C0-
1Wcu0T0r0H0U
1Ycu0U0T0W19
1Z7w0V0c0u0X
1Zct0W0U1214
217x0X0L0V1s
242K0Y0P0Z0-
2a2k0Z0A0Y1Y1Z1-
2i3v0-0S0Y1S
2i5A0_0u0x1x
2k9Y100G1118
2l9v110B1015
2rbx120K0W14
2rdh130H1b1c
2ubx140W1217
2v9d150R111F
2vaO160J171a
2vbx1714161p
2yap180F101a
2Bcr190U1h1p
2Dau1a16181n
2Edd1b131h1l
2Hfv1c0E131m1o
2MhC1d091r
2OhC1e091r
2QhC1f091r
2ThC1g091r
2UcS1h191b1K1L
2UhC1i091r
2Vjl1j0q1k1C
2Zj-1k0a1j1A
2-dv1l1b1B1D
2_g21m0v1c1q
30ax1n1a1t1J
30g31o0v1c1q
31bD1p17191t
37g71q1m1o1y1I
3ehC1r1d1e1f1g1i1z1C
3f7e1s0X1u1v1w1G
3fbB1t1n1p1M
3h6C1u1s1x24
3h6D1v1s1x24
3h6E1w1s1x24
3i5z1x0_1u1v1w1S
3jgr1y1q1z23
3jgy1z1r1y27
3nlb1A0Q1k21
3pdV1B1l1H1I
3rij1C1j1r1W
3Cdb1D1l1H1K1L
3H8H1E1F1G1U
3H8I1F151E1J
3I831G1s1E24
3Kdv1H1B1D1N
3Lf61I1q1B1T
3U9x1J1n1F1U
3Uco1K1h1D1M
3Vcn1L1h1D1M
4ac41M1t1K1L1V
4gdH1N1H1Q1X
4m2K1O0A1_
4m2L1P0A1_
4mdA1Q1N1V1X
4n2M1R0A1_
4o4c1S0-1x1Y1Z1-
4seF1T1I1X23
4vai1U1E1J20
4xc11V1M1Q20
4Bis1W1C292h
4Ne81X1N1Q1T
4R3R1Y0Z1S1_
4T3Q1Z0Z1S1_
4U3Q1-0Z1S1_
4_3M1_1O1P1R1Y1Z1-22
50bQ201U1V26
59nG211A2z
5b3K221_2n2U
5hfC231y1T25
5l7A241u1v1w1G2k
5lfC25232b2p
5rc326202p2N
5rgQ271z292c
5A1a282n
5BhW291W272a
5ChW2a292j2q
5FfG2b252d2o
5HgK2c272e2f2i
5Tgp2d2b2e2f2g2t
5Tgq2e2c2d2g2i
5Tgr2f2c2d2g2i
5Ugq2g2d2e2f2i2t
61ka2h1W2l2r
62gW2i2c2e2f2g2q
62is2j2a2l2u
687P2k242m2A2B2D2H
69iF2l2h2j2u
6f8a2m2k2D
6k2B2n22282-
6tfq2o2b2C2F
6vdd2p25262E
6vhs2q2a2i2x
6Blc2r2h2y2G
6Ca72s2D2N
6CgI2t2d2g2w2C
6Giy2u2j2l2y
6K9v2v2D2N
6KgU2w2t2x2S
6Lhn2x2q2w2J
6PiM2y2r2u2R2T
6Qn82z212G3V
6X9h2A2k2D
6X9i2B2k2D
6XfD2C2o2t2I2K2L
6Y9n2D2k2m2s2v2A2B2N31
73ds2E2p2F2O
73dt2F2o2E39
73mx2G2r2z34
767J2H2k2M2W
79fz2I2C2V30
79hG2J2x2P2Q32
7afz2K2C2V30
7bfz2L2C2V30
7k7P2M2H2X2Y
7oaD2N262s2v2D2Z
7rdd2O2E2Z3b
7Di82P2J2R2T32
7Ei92Q2J2R2T32
7Jil2R2y2P2Q34
7Kgx2S2w2V36
7Kim2T2y2P2Q34
7O5E2U222W35
7SfO2V2I2K2L2S38
7X5V2W2H2U3d
84812X2M2Y3d
848h2Y2M2X31
87aV2Z2N2O31
891H2-2n373g
8eeG2_30393f
8ofr302I2K2L2_3a
8p9A312D2Y2Z
8phm322J2P2Q33
8qhm33323436
8yi-342G2R2T33
8F4G352U373u
8KgC362S3338
8N43372-353i
8Of_382V363a
8Pel392F2_3c
8QfY3a30383f
8-df3b2O3c3e
8-e03c393b3k
92763d2W2X3h
9acK3e3b3l3y
9hfA3f2_3a3j
9j023g2-3I
9s7m3h3d3H4p
9v473i373s3u
9vfu3j3f3k3P
9web3k3c3j3y
9xcR3l3e3p3S3U3W3Y3-
9Bc93m3p3q4041424445464748494a4b4c4d4e4f4g4h4i4k4l
9Gaf3n3o3A3B3H
9Hah3o3n3q3t
9Jcu3p3l3m43
9Lb23q3m3o3r
9Ra_3r3q3w3x
9S3K3s3i3E3I
9Vak3t3o3v3A3B
a04q3u353i3M
a1aB3v3t3w3K3L
a2aH3w3r3v3x
a3bd3x3r3w3C
a8dw3y3e3k3S3U3W3Y3-
aau63z3G4y4z4A4B4C4D4E
ab9R3A3n3t3J
ac9Q3B3n3t3J
acbi3C3x3D3F
ahbx3D3C3X4041424445464748494a4b4c4d4e4f4g4h4i4k4l
ai3R3E3s3M3R
akb43F3C3O3Q
aktx3G3z3V57
an9d3H3h3n3N
ar2x3I3g3s3R
at9w3J3A3B3N3T
ataq3K3v3O3T
atar3L3v3O3T
au493M3u3E4j
au9p3N3H3J4o
auaS3O3F3K3L3Z
aufz3P3j3_50
awbk3Q3F3X3Z
ax3g3R3E3I4m
aydm3S3l3y3_
aza53T3J3K3L3Z
aAdl3U3l3y3_
aArx3V2z3G5i
aBdl3W3l3y3_
aCbw3X3D3Q4F4G4H
aDdk3Y3l3y3_
aEb23Z3O3Q3T
aEdk3-3l3y3_
aIdj3_3P3S3U3W3Y3-43
aKcB403m3D4n
aNcE413m3D4n
aOcF423m3D4n
aOdf433p3_4n
aPcF443m3D4n
aQcG453m3D4n
aRcG463m3D4n
aScH473m3D4n
aTcH483m3D4n
aTcI493m3D4n
aTcJ4a3m3D4n
aUcJ4b3m3D4n
aVcK4c3m3D4n
aWcK4d3m3D4n
aXcL4e3m3D4n
aYcL4f3m3D4n
a_cO4g3m3D4n
b0cO4h3m3D4n
b1cP4i3m3D4n
b34a4j3M4m4q
b5cS4k3m3D4n
b7cT4l3m3D4n
ba3R4m3R4j4w4x
bdcY4n404142434445464748494a4b4c4d4e4f4g4h4i4k4l4F4G4H
bq9z4o3N4s4O
bB5T4p3h4q4r
bH5n4q4j4p4w4x
bI5_4r4p4v4S4T
bM8Z4s4o4t4R
bO8m4t4s4u4L
bT7d4u4t4v4Q
bZ6L4v4r4u4U
b-574w4m4q58595a
b_564x4m4q58595a
ceuj4y3z4I4J4K4M4N4P
cfuj4z3z4I4J4K4M4N4P
cgui4A3z4I4J4K4M4N4P
chuj4B3z4I4J4K4M4N4P
ciuj4C3z4I4J4K4M4N4P
ckuj4D3z4I4J4K4M4N4P
cmuj4E3z4I4J4K4M4N4P
crcA4F3X4n4O
cscA4G3X4n4O
cucz4H3X4n4O
cxuk4I4y4z4A4B4C4D4E4W4Z5l5n5w
cyuk4J4y4z4A4B4C4D4E4W4Z5l5n5w
cAuk4K4y4z4A4B4C4D4E4W4Z5l5n5w
cB8t4L4t4Q4R
cBuk4M4y4z4A4B4C4D4E4W4Z5l5n5w
cDul4N4y4z4A4B4C4D4E4W4Z5l5n5w
cIcx4O4o4F4G4H4_
cIul4P4y4z4A4B4C4D4E4W4Z5l5n5w
cM8c4Q4u4L4V
cO954R4s4L4-
d15S4S4r5d5e5g5h
d25R4T4r5d5e5g5h
dd774U4v4V5c
dn7T4V4Q4U4X
dsur4W4I4J4K4M4N4P5w
dt7Y4X4V4Y53
dt894Y4X4-53
dtus4Z4I4J4K4M4N4P5w
du964-4R4Y5s
dFcU4_4O505152
dOeW503P4_5m6f6g
dYcH514_5455565t5U5W
dZcG524_5455565t5U5W
d-82534X4Y5r
e4cG5451525W
e5cG5551525W
e8cG5651525W
edsP573G5b5k
el4w584w4x5g
en4z594w4x5g
eo4A5a4w4x5g
essp5b575f5i
ev6y5c4U5d5e5h5o
eG5N5d4S4T5c5j
eG5O5e4S4T5c5j
eGsp5f5b5k5p
eI585g4S4T58595a5j
eI5G5h4S4T5c5j
eNrG5i3V5b5p
eQ5e5j5d5e5g5h5K
e-th5k575f5l
f0tm5l4I4J4K4M4N4P5k5q
f6gh5m505z5D6f6g
fav95n4I4J4K4M4N4P5w
fk7D5o5c5r5I5J
fls95p5f5i5v
fut65q5l5v5B
fC8T5r535o5u
fC9o5s4-5t5u
fD9G5t51525s5C
fI985u5r5s5N
fIsh5v5p5q5x5y
fSvR5w4I4J4K4M4N4P4W4Z5n5X
g5ss5x5v5A5B
g6st5y5v5A5B
gsiW5z5m5D5E
gtrN5A5x5y5L5O5P5Q5S
gtsM5B5q5x5y5O5P5Q5S
gxad5C5t5F6a
gAgt5D5m5z5M
gNi-5E5z5G5H67
gXaJ5F5C5W5_
h0iy5G5E5M616263
h0iz5H5E5M616263
h57b5I5o5N5R
h67c5J5o5N5R
h83_5K5j5R66
hkrr5L5A5Y5Z5-60
hlgx5M5D5G5H64
ho7y5N5u5I5J5T
hxt85O5A5B5V
hyt95P5A5B5V
hzta5Q5A5B5V
hA6o5R5I5J5K5T
hAta5S5A5B5V
hI6o5T5N5R66
hPc55U51525W
hPti5V5O5P5Q5S5X5Y5Z5-
hSbZ5W51525455565F5U5_
hVts5X5w5V6s6I6J6M6N
h-s25Y5L5V60
h-s35Z5L5V60
h-s45-5L5V60
h_bC5_5F5W69
i2rV605L5Y5Z5-6j
i4hs615G5H6467
i4hu625G5H6467
i4hv635G5H6467
i7gE645M6162636w
i8ph656e6h
ig2A665K5T6b
igjh675E6162636c
iind686f6i6k6l6o
ilbo695_6a6F
iG9q6a5C696y
iK1Y6b666v
iKjD6c676q6t
iOoO6d6e6h
iXoC6e656d6g6n
iYnK6f505m686g6i6k6l
iYo66g505m6e6f6h6n6P
iYo86h656d6g6n
j1np6i686f6r
j1qS6j606m6M6N
j2nn6k686f6r
j2no6l686f6r
jbqH6m6j6n6O
jips6n6e6g6h6m6P
jmm96o686q6r
jpcZ6p6u6z6U
jqlg6q6c6o6E
jtmk6r6i6k6l6o6T
jtst6s5X6M6N
jzj96t6c6x6Q
jAcY6u6p6z6X
jI3s6v6b6K7o
jPhG6w646x75
jQiv6x6t6w75
jS966y6a6B6C6D6F
jXcm6z6p6u6S
k36R6A6G6L7y7z7A
k48g6B6y6G80
k48i6C6y6G80
k48j6D6y6G80
k4k-6E6q6H6V
k69x6F696y7d
k7836G6A6B6C6D7y7z7A
k9kW6H6E6Q6R
k9rO6I5X6M6N
karN6J5X6M6N
kf5b6K6v6L8b8c8d
kf5u6L6A6K88
kkrq6M5X6j6s6I6J6O
klrp6N5X6j6s6I6J6O
kpqM6O6m6M6N6P
kqpN6P6g6n6O
krjH6Q6t6H6Y
ktkV6R6H6_72
kAcm6S6z6X7B
kAmi6T6r6V7b
kBep6U6p6Z73
kClE6V6E6T6_
kDvx6W6-71
kRdC6X6u6S6Z
kRjI6Y6Q7277
kVdU6Z6U6X76
kWua6-6W7071
kZlj6_6R6V78
l1u1706-797J
l3uK716W6-
l6kD726R6Y78
l7fL736U7c7n
l7op747a7v8h
l8i-756w6x77
l9dW766Z7t7u
lgj7776Y757b7q7s
lplh786_727b
lpsI79707e87
lup17a747p7v
lvl_7b6T77787q7s
lxgK7c737r7D
lKa07d6F7T828384
lRs07e797m7E
lU0x7f7i7j7k7l8L
lV0y7g7i7j7k7l8L
lW0y7h7i7j7k7l8L
lX0y7i7f7g7h7j9e9h9t
lX0z7j7f7g7h7i7k7l8L9e9h
lZ0z7k7f7g7h7j9e9h9t
l-0z7l7f7g7h7j9e9h9t
m1qz7m7e7p7w
m3fM7n737r7u
m63S7o6v8a8n
m7pM7p7a7m7x
m8kG7q777b
mbgh7r7c7n7K
mbkt7s777b
mddj7t767B7F
mefE7u767n7K
mgoy7v747a7H
mjqJ7w7m7E7O
mopD7x7p7H7N
mp7g7y6A6G8j
mq7g7z6A6G8j
mu7g7A6A6G8j
muc37B6S7t7C
myc27C7B7G7U
mBht7D7c7K7V
mBrJ7E7e7w7R
mFdA7F7t7L7M
mIbK7G7C7T8z
mJoI7H7v7x7I
mKoI7I7H7_8O
mKuC7J707X
mLgN7K7r7u7D
mSdw7L7F7M7P
mSeu7M7F7L8k8p8t
mVpX7N7x7O7Q
mWqh7O7w7N7W7Y
n2cZ7P7L7U8s
n4pR7Q7N7_8I
n4rT7R7E7S7Z
n5rU7S7R8u8w
naaf7T7d7G7-
nacG7U7C7P8i
nekB7V7D8e8-
noqF7W7O7Z8G8H
nouf7X7J8185868f8l8v
npqE7Y7O7Z8G8H
ntqT7Z7R7W7Y8m
nu9v7-7T8283848Y
nupp7_7I7Q8D
nv8g806B6C6D82838489
nvt_817X879C
nw8v827d7-80
nw8w837d7-80
nw8x847d7-80
nwtZ857X879C
nwt-867X879C
nCtp87798185868u
nF6P886L8b8c8d8j
nF7U89808r9j
nH4N8a7o8g8o8q
nH608b6K888g
nH618c6K888g
nH628d6K888g
nHld8e7V8h92
nHuA8f7X9z9B
nI5q8g8a8b8c8d98
nIlq8h748e8N
nJcM8i7U8x8y8A
nL7m8j7y7z7A888r
nLeX8k7M8B8V
nLuE8l7X9z9B
nMqX8m7Z8w8G8H
nN478n7o8o8q8L
nN4v8o8a8n9F
nNeZ8p7M8B8V
nO4u8q8a8n9F
nO7s8r898j9R
nOdx8s7P8A8J
nOeZ8t7M8B8V
nOsT8u7S878_
nOuJ8v7X9z9B
nRr88w7S8m8_
nUcz8x8i8K9n
nVcy8y8i8K9n
nXbB8z7G8K8W
n-dd8A8i8s8X
n-ek8B8k8p8t8C8E8F8Z
n_e88C8B8J9v
n_pF8D7_8I8T
o0e68E8B8J9v
o0e78F8B8J9v
o0qh8G7W7Y8m8M
o0qi8H7W7Y8m8M
o1pO8I7Q8D8M
o4dQ8J8s8C8E8F8X
obbU8K8x8y8z8U
od2u8L7f7g7h7j8n969799
ofq18M8G8H8I9d
ohm48N8h8S9193
ohnH8O7I8P8Q
oiny8P8O8S9b
oknM8Q8O8R9g
oon-8R8Q8T9c
ormx8S8N8P94
oroG8T8D8R95
oubZ8U8K8W9f
ovfr8V8k8p8t8Zb7
oxaE8W8z8U8Y
oydj8X8A8J9s
oBaC8Y7-8W90
oBeX8Z8B8V9v
oFjN8-7V9ab7
oGrk8_8u8w9i
oHaB908Y9j9G
oHlM918N929r
oJkL928e91939a
oJlL938N929r
oKmA948S9b9D
oMoQ958T9k9l
oP1V968L9e9y9A
oQ1U978L9e9y9A
oQ5j988g9w9M9N9O
oR1T998L9e9y9A
oRkL9a8-929o
oRmZ9b8P949u
oUo79c8R9k9m
oVqc9d8M9i9l
oW1G9e7i7j7k7l9697999h
oXcd9f8U9n9H
oXnK9g8Q9m9P
oY1E9h7i7j7k7l9e9t
oYr09i8_9d9J
o-9L9j8990aA
o-oH9k959c9p9Xa4
o-q79l959d9Y
p1o99m9c9g9-
p2cO9n8x8y9f9s
p3k-9o9a9qa0
p4oF9p9ka4
p7lq9q9o9rah
p7lu9r91939q9L
p8c_9s8X9n9I9K
pb0T9t7i7k7l9h
pcmY9u9b9E9S
peet9v8C8E8F8Za7
pi5E9w989x9Q
pk609x9w9Q9R
pl1T9y969799aJaK
pmvs9z8f8l8v9C
pn1U9A969799aJaK
pnvt9B8f8l8v9C
ppuu9C8185869z9B9U
pqmv9D949E9L
pqmz9E9u9D9V
pr4C9F8o8q9M9N9Oa1
praU9G909Hac
psbI9H9f9Gaq
ptdm9I9sa7a9aa
ptre9J9i9Z9_
pudn9K9sa7a9aa
pum79L9r9Dan
pv4U9M989F9T
pw4V9N989F9T
pw4W9O989F9T
pwnt9P9g9S9W
pz5h9Q9w9x9T
pB7f9R8r9xa8
pCn09S9u9P9V
pD549T9M9N9O9Qab
pDu99U9Ca2a5
pEmS9V9E9Sap
pHnF9W9P9-ao
pHou9X9ka4
pIpJ9Y9la3aXa-
pIr99Z9Jb5ba
pKn-9-9m9Wa6
pKrY9_9Ja5ba
pLkAa09oagbcbd
pN4ka19Fakav
pUuea29Uadas
pVoOa39Ya4aXa-
pZora49k9p9Xa3a6
p-sTa59U9_aB
p_oca69-a4aC
q0ema79v9I9Kar
q27ia89Raeafa_
q7cKa99I9KauaFaI
q8cJaa9I9KauaFaI
qb5cab9Taiaj
qbapac9Gaqay
qbu6ada2azaB
qd6uaea8ajaO
qe6tafa8ajaO
qfk-aga0ahaD
qfldah9qagam
qg55aiabalb0
qg5lajabaeafaO
qh4naka1alaL
qj4PalaiakaP
qjlhamahanaE
qkm8an9Lamat
qknjao9WapaC
qmmEap9Vaoat
qrbKaq9HacaH
qreFara7auaRaTaUaVaW
qsuXasa2ax
qumpatanapaw
qzcKaua9aaaraG
qD3tava1aLb1
qDmlawataQbb
qDuUaxasaz
qI9NayacaAb4
qIu4azadaxbu
qK9jaA9jayaZ
qPsTaBa5adbu
qQnVaCa6aobk
qTkiaDagaEbmbp
qTlfaEamaDaM
qUb-aFa9aaaGaH
qUcfaGauaFaIaRaTaUaVaW
qVbWaHaqaFaIaN
qVbZaIa9aaaGaH
q-1laJ9y9AbM
q-1maK9y9AbM
r041aLakavaP
r0ljaMaEaQblbnbqbr
r2bRaNaHb4c5c6c7c9cb
r35GaOaeafajb6
r443aPalaLaS
r5lzaQawaMb8
r9fkaRaraGbh
ra45aSaPaYb3
rbfmaTaraGbh
rdfnaUaraGbh
refoaVaraGbh
rgfpaWaraGbh
riq2aX9Ya3b5
rj4qaYaSb0bs
rj8taZaAa_bQbRbSbTbUbVbWbX
rjq3a-9Ya3b5
rk8pa_a8aZbo
rn4_b0aiaYb6
rq2Ib1avb2bi
rv2_b2b1b3bg
rz3rb3aSb2bg
rAbkb4ayaNbQbRbSbTbUbVbWbX
rBqab59ZaXa-bC
rC5sb6aOb0bx
rJhib78V8-bcbd
rQlUb8aQb9blbnbqbr
rTm1b9b8bbbL
rTraba9Z9_bG
rVmhbbawb9bf
rWhhbca0b7be
rXhgbda0b7be
r_hfbebcbdbhbmbp
s0mtbfbbbjbL
s53obgb2b3bt
s5gdbhaRaTaUaVaWbec5c6c7c9cb
s92rbib1bybI
sfo3bjbfbkcc
sfobbkaCbjbvbw
sgk6blaMb8bF
shhDbmaDbebD
shk3bnaMb8bF
si8aboa_bxci
sihDbpaDbebD
sik1bqaMb8bF
sjj_braMb8bF
sk44bsaYbtbH
sm3tbtbgbsby
sosKbuazaBc3
sxoKbvbkbzbAbBc2
syoLbwbkbzbAbBc2
sC5Pbxb6bobH
sD3jbybibtbE
sKpzbzbvbwbCcr
sKpAbAbvbwbCcr
sKpBbBbvbwbCcr
sKqvbCb5bzbAbBbG
sNi5bDbmbpbF
sO3jbEbybJbP
sViHbFblbnbqbrbDbZ
sWqUbGbabCc3
t05kbHbsbxbK
t11HbIbibNbO
t137bJbEbOc0
t15jbKbHbPc4
t3lwbLb9bfb-b_
ta0HbMaJaKbN
tb19bNbIbMch
tc2LbObIbJbY
ti4JbPbEbKc8ca
ttbCbQaZb4cA
tubDbRaZb4cA
tvbCbSaZb4cA
twbCbTaZb4cA
txbCbUaZb4cA
tzbDbVaZb4cA
tCbEbWaZb4cA
tDbEbXaZb4cA
tR2VbYbOc1cdcg
tVkKbZbFb-b_
tXkRb-bLbZcc
tXkSb_bLbZcc
tZ3nc0bJc1ce
t-3fc1bYc0cdcg
t_nkc2bvbwcfcj
u0sgc3bubGcz
u15wc4bKclco
u7dHc5aNbhcA
u9dFc6aNbhcA
uadEc7aNbhcA
ub46c8bPcecp
ubdDc9aNbhcA
uc45cabPcecp
ucdCcbaNbhcA
uhlvccbjb-b_cf
ul2JcdbYc1ch
ul3Rcec0c8cacp
ullzcfc2ccck
um2IcgbYc1ch
uo2EchbNcdcgcm
uw9kcibocn
uFmWcjc2ckcq
uHlQckcfcj
uI63clc4cncx
uM2Acmchcwcy
uM6ocncicl
uO51coc4cucx
uR4jcpc8cacecs
uSnPcqcjcr
uTnXcrbzbAbBcqct
v14ncscpcucv
v1o3ctcrcz
v54rcucocs
va3Wcvcscy
vg1Zcwcm
vl5Bcxclco
vt3kcycmcv
vut9czc3ct
vHc8cAbQbRbSbTbUbVbWbXc5c6c7c9cb";


	//string s = "You win some. You lose some.";
	string s = nodesStringRight;

	string[] subs  = s.Split('\n');
	
	// Echo("a:"+decodeAsCharNumberMax64('a'));
	// Echo("aa:"+decodeStr__NumberMax4095("aa"));
	

	
	foreach (string sub in subs)
	{
		//string[] subs = s.Split('\n');
		Echo(sub);
		
		Echo("sub.Length:"+sub.Length);
		// string encodedIndexes = sub.Substring(5,sub.Length-3);
		int end = sub.Length-1;
		
		// Echo("end:"+end);
		
		// string encodedIndexes = sub.Substring(5,sub.Length-1);
		string encodedIndexes = sub.Substring(4);
		// string encodedIndexes = sub.Substring(5,sub.Length);
		Echo(encodedIndexes);
		
		string encodedNeighborsIndexes = encodedIndexes.Substring(2);
		Echo("encodedNeighborsIndexes:"+encodedNeighborsIndexes);
		
		int currentNodeIndexDecoded = decodeStr__NumberMax4095(encodedIndexes.Substring(0,2));
		Echo("currentNodeIndexDecoded:"+currentNodeIndexDecoded);
		
		int xNodeInit = decodeStr__NumberMax4095(sub.Substring(0,2));
		int yNodeInit = decodeStr__NumberMax4095(sub.Substring(2,2));
		
		Point position = new Point(xNodeInit,yNodeInit);
		//int radius = 0;
		//TODO: encode this
		int radius = 500;
		// int indexNumber = int.Parse(subsub[2]);
		int indexNumber = currentNodeIndexDecoded;
		nodes.Add(new Node(indexNumber,position, radius));
		
		int numberOfSubstringNeighbors = encodedNeighborsIndexes.Length/2;
		Echo("numberOfSubstringNeighbors:"+numberOfSubstringNeighbors);
		
		foreach(int tmpIndex in Enumerable.Range(0,numberOfSubstringNeighbors)){
			string tmpNeighborStr = encodedNeighborsIndexes.Substring(2*tmpIndex,2);
			int tmpNeighborInt = decodeStr__NumberMax4095(tmpNeighborStr);
			nodes[indexNumber].neighborsNodesIndex.Add(tmpNeighborInt);
		}
		
		
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


public int decodeStr__NumberMax4095(string strMax4095){


    int resultInt = decodeAsCharNumberMax64(strMax4095[0]) * 64 + decodeAsCharNumberMax64(strMax4095[1]) * 1;

    // # resultInt = 0
    return resultInt;
}

public int decodeAsCharNumberMax64(char character){
    // # print("number:"+str(number))

    // # print("character:",character)
    // int numberToProcess = (int) Char.GetNumericValue(character);
    int numberToProcess = (int) character;
    // # print("numberToProcess:",numberToProcess)
    // Echo("character:"+character);
    // Echo("numberToProcess:"+numberToProcess);

    int resultNumberUnder64 = 0;


    if(character=='-'){
        resultNumberUnder64 = 62;
        return resultNumberUnder64;
	}
    if(character=='_'){
        resultNumberUnder64 = 63;
        return resultNumberUnder64;
	}

    // # "0" "9" 48 58     0 9       58= 48 +10
    // # "A" "Z" 65 90     36 62     91= 65 + 26
    // # "a" "z" 97 122    10 35     122

    if(numberToProcess<58){
        // # 48 is "0"
        resultNumberUnder64 = numberToProcess - 48;
        return resultNumberUnder64;
	}
    if(numberToProcess<(90+1)){
        // # 97 is "A";
        resultNumberUnder64 = numberToProcess - (90+1) + 26 + 36;
        return resultNumberUnder64;
	}
    if(numberToProcess<(122+1)){
        // # 97 is "a";
        resultNumberUnder64 = numberToProcess - (122+1) + 10 + 26;
        return resultNumberUnder64;
	}




    return resultNumberUnder64;
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

 
// Point startPointGoal = new Point(1081,1031);//ok
Point startPointGoal = new Point(50,50);//crash
 // Point startPointGoal = new Point(1794,1913);
 // Point startPointGoal = new Point(1102,1791);//crash ? bug358
 // Point startPointGoal = new Point(1425,1783);
//Point startPointGoal = new Point(1950,1664);
// Point startPointGoal = new Point(1800,1664);//2node
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
	
	
					
    // if ((updateSource & UpdateType.Once) == UpdateType.Once)
    // {
		// Echo("oi1");
        // RunStateMachine();
		// Echo("oi2");
    // }
	
	graphRegened = true;
	
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
	
	List<Node> data = new List<Node>();
	
	while(came_from.ContainsKey(node)){
		Echo("data.Add(node);");
		Echo("node.position:"+node.position);
		Echo(""+Math.Sqrt(distanceSquarred(node.position,ourDestinationNode.position)));
		data.Add(node);
		node = came_from[node];
	}
	
	
	string toCustomData = "";
	
	int gps_number = 0;
	
	foreach(Node pathNode in data){
		// public Vector3D convertPointToV3D(IMyRemoteControl sc, int faceNumber, Point pointToV3D){
		//toCustomData = toCustomData + pathNode.position;
		Vector3D nodeConverted = convertPointToV3D(RemoteControl, 4, pathNode.position);
		
		// MyWaypointInfo tmpWPINode  = new MyWaypointInfo("inter", nodeConverted);
		MyWaypointInfo tmpWPINode  = new MyWaypointInfo(gps_number.ToString(), nodeConverted);
		
		toCustomData = toCustomData + tmpWPINode.ToString() + '\n';
		
		gps_number = gps_number + 1;
	}
	
	Me.CustomData = toCustomData;
	
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


