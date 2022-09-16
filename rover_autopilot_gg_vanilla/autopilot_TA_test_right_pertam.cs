
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
 string nodesStringRight = @"09ku0005
0E3L010x0D
0Jhc020a0n
0QiL030a0r1e1f1g1h1j
0Tf104070809
0Tkz05000b0c0f
0Wce060s0A
0Wfk07040n0w
0Ye_0804090u0F
0Ze_0904080u0F
0Zhz0a02031e1f1g1h1j
0Zk50b050r1l
0ZkK0c050f0g0R
0-770d0t0W
0-9j0e0k0o
0-kL0f050c0g0R
10lD0g0c0f0R
13dt0h0p0A
158m0i0l0m
15bf0j0q0E
198W0k0e0m0N0O0P
1b850l0i0M
1d8x0m0i0k0J
1fga0n02070w
1i9r0o0e0C0S
1jec0p0h0u
1lbu0q0j0s0L
1njh0r030b1k
1obW0s060q0U
1p6Q0t0d0v
1pej0u08090p0F
1t6P0v0t0W10
1ug50w070n1n1p
1z4e0x010y0T
1A540y0x10
1B3e0z0D0Q
1DcM0A060h0I
1H0F0B0-1P1Q1S
1H9H0C0o0H12
1J3y0D010z0T
1KaB0E0j0G0H
1Lf20F08090u1d
1MaC0G0E0K19
1Nao0H0C0E11
1OcH0I0A0U14
1Q8C0J0m0M0N0O0P
1QaI0K0G0L17
1Qbe0L0q0K13
1R860M0l0J0Y
1R8H0N0k0J0O0P0S
1R8I0O0k0J0N0P0S
1S8J0P0k0J0N0O0S
1T2R0Q0z0Z
1Tlp0R0c0f0g1B
1U8N0S0o0N0O0P16
1V3E0T0x0D0_
1Wcu0U0s0I0V
1Ycu0V0U0X1a
1Z7w0W0d0v0Y
1Zct0X0V1315
217x0Y0M0W1t
242K0Z0Q0-0_
2a2k0-0B0Z1Z1-1_
2i3v0_0T0Z1T
2i5A100v0y1y
2k9Y110H1219
2l9v120C1116
2rbx130L0X15
2rdh140I1c1d
2ubx150X1318
2v9d160S121G
2vaO170K181b
2vbx1815171q
2yap190G111b
2Bcr1a0V1i1q
2Dau1b17191o
2Edd1c141i1m
2Hfv1d0F141n1p
2MhC1e030a1f1g1h1j1s
2OhC1f030a1e1g1h1j1s
2QhC1g030a1e1f1h1j1s
2ThC1h030a1e1f1g1j1s
2UcS1i1a1c1L1M
2UhC1j030a1e1f1g1h1s
2Vjl1k0r1l1D
2Zj-1l0b1k1B
2-dv1m1c1C1E
2_g21n0w1d1p1r
30ax1o1b1u1K
30g31p0w1d1n1r
31bD1q181a1u
37g71r1n1p1z1J
3ehC1s1e1f1g1h1j1A1D
3f7e1t0Y1v1w1x1H
3fbB1u1o1q1N
3h6C1v1t1w1x1y25
3h6D1w1t1v1x1y25
3h6E1x1t1v1w1y25
3i5z1y101v1w1x1T
3jgr1z1r1A24
3jgy1A1s1z28
3nlb1B0R1l22
3pdV1C1m1I1J
3rij1D1k1s1X
3Cdb1E1m1I1L1M
3H8H1F1G1H1V
3H8I1G161F1K
3I831H1t1F25
3Kdv1I1C1E1O
3Lf61J1r1C1U
3U9x1K1o1G1V
3Uco1L1i1E1M1N
3Vcn1M1i1E1L1N
4ac41N1u1L1M1W
4gdH1O1I1R1Y
4m2K1P0B1Q1S20
4m2L1Q0B1P1S20
4mdA1R1O1W1Y
4n2M1S0B1P1Q20
4o4c1T0_1y1Z1-1_
4seF1U1J1Y24
4vai1V1F1K21
4xc11W1N1R21
4Bis1X1D2a2i
4Ne81Y1O1R1U
4R3R1Z0-1T1-1_20
4T3Q1-0-1T1Z1_20
4U3Q1_0-1T1Z1-20
4_3M201P1Q1S1Z1-1_23
50bQ211V1W27
59nG221B2A
5b3K23202o2V
5hfC241z1U26
5l7A251v1w1x1H2l
5lfC26242c2q
5rc327212q2O
5rgQ281A2a2d
5A1a292o
5BhW2a1X282b
5ChW2b2a2k2r
5FfG2c262e2p
5HgK2d282f2g2j
5Tgp2e2c2f2g2h2u
5Tgq2f2d2e2g2h2j
5Tgr2g2d2e2f2h2j
5Ugq2h2e2f2g2j2u
61ka2i1X2m2s
62gW2j2d2f2g2h2r
62is2k2b2m2v
687P2l252n2B2C2E2I
69iF2m2i2k2v
6f8a2n2l2B2C2E
6k2B2o23292_
6tfq2p2c2D2G
6vdd2q26272F
6vhs2r2b2j2y
6Blc2s2i2z2H
6Ca72t2w2E2O
6CgI2u2e2h2x2D
6Giy2v2k2m2z
6K9v2w2t2E2O
6KgU2x2u2y2T
6Lhn2y2r2x2K
6PiM2z2s2v2S2U
6Qn82A222H3W
6X9h2B2l2n2C2E
6X9i2C2l2n2B2E
6XfD2D2p2u2J2L2M
6Y9n2E2l2n2t2w2B2C2O32
73ds2F2q2G2P
73dt2G2p2F3a
73mx2H2s2A35
767J2I2l2N2X
79fz2J2D2L2M2W31
79hG2K2y2Q2R33
7afz2L2D2J2M2W31
7bfz2M2D2J2L2W31
7k7P2N2I2Y2Z
7oaD2O272t2w2E2-
7rdd2P2F2-3c
7Di82Q2K2R2S2U33
7Ei92R2K2Q2S2U33
7Jil2S2z2Q2R2U35
7Kgx2T2x2W37
7Kim2U2z2Q2R2S35
7O5E2V232X36
7SfO2W2J2L2M2T39
7X5V2X2I2V3e
84812Y2N2Z3e
848h2Z2N2Y32
87aV2-2O2P32
891H2_2o383h
8eeG30313a3g
8ofr312J2L2M303b
8p9A322E2Z2-
8phm332K2Q2R34
8qhm34333537
8yi-352H2S2U34
8F4G362V383v
8KgC372T3439
8N43382_363j
8Of_392W373b
8Pel3a2G303d
8QfY3b31393g
8-df3c2P3d3f
8-e03d3a3c3l
92763e2X2Y3i
9acK3f3c3m3z
9hfA3g303b3k
9j023h2_3J
9s7m3i3e3I4q
9v473j383t3v
9vfu3k3g3l3Q
9web3l3d3k3z
9xcR3m3f3q3T3V3X3Z3_
9Bc93n3q3r41424345464748494a4b4c4d4e4f4g4h4i4j4l4m
9Gaf3o3p3B3C3I
9Hah3p3o3r3u
9Jcu3q3m3n44
9Lb23r3n3p3s
9Ra_3s3r3x3y
9S3K3t3j3F3J
9Vak3u3p3w3B3C
a04q3v363j3N
a1aB3w3u3x3L3M
a2aH3x3s3w3y
a3bd3y3s3x3D
a8dw3z3f3l3T3V3X3Z3_
aau63A3H4z4A4B4C4D4E4F
ab9R3B3o3u3C3K
ac9Q3C3o3u3B3K
acbi3D3y3E3G
ahbx3E3D3Y41424345464748494a4b4c4d4e4f4g4h4i4j4l4m
ai3R3F3t3N3S
akb43G3D3P3R
aktx3H3A3W58
an9d3I3i3o3O
ar2x3J3h3t3S
at9w3K3B3C3O3U
ataq3L3w3M3P3U
atar3M3w3L3P3U
au493N3v3F4k
au9p3O3I3K4p
auaS3P3G3L3M3-
aufz3Q3k4051
awbk3R3G3Y3-
ax3g3S3F3J4n
aydm3T3m3z3V3X3Z3_40
aza53U3K3L3M3-
aAdl3V3m3z3T3X3Z3_40
aArx3W2A3H5j
aBdl3X3m3z3T3V3Z3_40
aCbw3Y3E3R4G4H4I
aDdk3Z3m3z3T3V3X3_40
aEb23-3P3R3U
aEdk3_3m3z3T3V3X3Z40
aIdj403Q3T3V3X3Z3_44
aKcB413n3E424345464748494a4b4c4d4e4f4g4h4i4j4l4m4o
aNcE423n3E414345464748494a4b4c4d4e4f4g4h4i4j4l4m4o
aOcF433n3E414245464748494a4b4c4d4e4f4g4h4i4j4l4m4o
aOdf443q404o
aPcF453n3E414243464748494a4b4c4d4e4f4g4h4i4j4l4m4o
aQcG463n3E414243454748494a4b4c4d4e4f4g4h4i4j4l4m4o
aRcG473n3E414243454648494a4b4c4d4e4f4g4h4i4j4l4m4o
aScH483n3E414243454647494a4b4c4d4e4f4g4h4i4j4l4m4o
aTcH493n3E414243454647484a4b4c4d4e4f4g4h4i4j4l4m4o
aTcI4a3n3E41424345464748494b4c4d4e4f4g4h4i4j4l4m4o
aTcJ4b3n3E41424345464748494a4c4d4e4f4g4h4i4j4l4m4o
aUcJ4c3n3E41424345464748494a4b4d4e4f4g4h4i4j4l4m4o
aVcK4d3n3E41424345464748494a4b4c4e4f4g4h4i4j4l4m4o
aWcK4e3n3E41424345464748494a4b4c4d4f4g4h4i4j4l4m4o
aXcL4f3n3E41424345464748494a4b4c4d4e4g4h4i4j4l4m4o
aYcL4g3n3E41424345464748494a4b4c4d4e4f4h4i4j4l4m4o
a_cO4h3n3E41424345464748494a4b4c4d4e4f4g4i4j4l4m4o
b0cO4i3n3E41424345464748494a4b4c4d4e4f4g4h4j4l4m4o
b1cP4j3n3E41424345464748494a4b4c4d4e4f4g4h4i4l4m4o
b34a4k3N4n4r
b5cS4l3n3E41424345464748494a4b4c4d4e4f4g4h4i4j4m4o
b7cT4m3n3E41424345464748494a4b4c4d4e4f4g4h4i4j4l4o
ba3R4n3S4k4x4y
bdcY4o4142434445464748494a4b4c4d4e4f4g4h4i4j4l4m4G4H4I
bq9z4p3O4t4P
bB5T4q3i4r4s
bH5n4r4k4q4x4y
bI5_4s4q4w4T4U
bM8Z4t4p4u4S
bO8m4u4t4v4M
bT7d4v4u4w4R
bZ6L4w4s4v4V
b-574x4n4r4y595a5b
b_564y4n4r4x595a5b
ceuj4z3A4A4B4C4D4E4F4J4K4L4N4O4Q
cfuj4A3A4z4B4C4D4E4F4J4K4L4N4O4Q
cgui4B3A4z4A4C4D4E4F4J4K4L4N4O4Q
chuj4C3A4z4A4B4D4E4F4J4K4L4N4O4Q
ciuj4D3A4z4A4B4C4E4F4J4K4L4N4O4Q
ckuj4E3A4z4A4B4C4D4F4J4K4L4N4O4Q
cmuj4F3A4z4A4B4C4D4E4J4K4L4N4O4Q
crcA4G3Y4o4H4I4P
cscA4H3Y4o4G4I4P
cucz4I3Y4o4G4H4P
cxuk4J4z4A4B4C4D4E4F4K4L4N4O4Q4X4-5m5o5x
cyuk4K4z4A4B4C4D4E4F4J4L4N4O4Q4X4-5m5o5x
cAuk4L4z4A4B4C4D4E4F4J4K4N4O4Q4X4-5m5o5x
cB8t4M4u4R4S
cBuk4N4z4A4B4C4D4E4F4J4K4L4O4Q4X4-5m5o5x
cDul4O4z4A4B4C4D4E4F4J4K4L4N4Q4X4-5m5o5x
cIcx4P4p4G4H4I50
cIul4Q4z4A4B4C4D4E4F4J4K4L4N4O4X4-5m5o5x
cM8c4R4v4M4W
cO954S4t4M4_
d15S4T4s4U5e5f5h5i
d25R4U4s4T5e5f5h5i
dd774V4w4W5d
dn7T4W4R4V4Y
dsur4X4J4K4L4N4O4Q4-5o5x
dt7Y4Y4W4Z54
dt894Z4Y4_54
dtus4-4J4K4L4N4O4Q4X5o5x
du964_4S4Z5t
dFcU504P515253
dOeW513Q505n6g6h
dYcH5250535556575u5V5X
dZcG5350525556575u5V5X
d-82544Y4Z5s
e4cG55525356575V5X
e5cG56525355575V5X
e8cG57525355565V5X
edsP583H5c5l
el4w594x4y5a5b5h
en4z5a4x4y595b5h
eo4A5b4x4y595a5h
essp5c585g5j
ev6y5d4V5e5f5i5p
eG5N5e4T4U5d5f5i5k
eG5O5f4T4U5d5e5i5k
eGsp5g5c5l5q
eI585h4T4U595a5b5k
eI5G5i4T4U5d5e5f5k
eNrG5j3W5c5q
eQ5e5k5e5f5h5i5L
e-th5l585g5m
f0tm5m4J4K4L4N4O4Q5l5r
f6gh5n515A5E6g6h
fav95o4J4K4L4N4O4Q4X4-5x
fk7D5p5d5s5J5K
fls95q5g5j5w
fut65r5m5w5C
fC8T5s545p5v
fC9o5t4_5u5v
fD9G5u52535t5D
fI985v5s5t5O
fIsh5w5q5r5y5z
fSvR5x4J4K4L4N4O4Q4X4-5o5Y
g5ss5y5w5z5B5C
g6st5z5w5y5B5C
gsiW5A5n5E5F
gtrN5B5y5z5M5P5Q5R5T
gtsM5C5r5y5z5P5Q5R5T
gxad5D5u5G6b
gAgt5E5n5A5N
gNi-5F5A5H5I68
gXaJ5G5D5X60
h0iy5H5F5I5N626364
h0iz5I5F5H5N626364
h57b5J5p5K5O5S
h67c5K5p5J5O5S
h83_5L5k5S67
hkrr5M5B5Z5-5_61
hlgx5N5E5H5I65
ho7y5O5v5J5K5U
hxt85P5B5C5Q5R5T5W
hyt95Q5B5C5P5R5T5W
hzta5R5B5C5P5Q5T5W
hA6o5S5J5K5L5U
hAta5T5B5C5P5Q5R5W
hI6o5U5O5S67
hPc55V52535556575X
hPti5W5P5Q5R5T5Y5Z5-5_
hSbZ5X52535556575G5V60
hVts5Y5x5W6t6J6K6N6O
h-s25Z5M5W5-5_61
h-s35-5M5W5Z5_61
h-s45_5M5W5Z5-61
h_bC605G5X6a
i2rV615M5Z5-5_6k
i4hs625H5I63646568
i4hu635H5I62646568
i4hv645H5I62636568
i7gE655N6263646x
i8ph666e6f6i
ig2A675L5U6c
igjh685F6263646d
iind696g6j6l6m6p
ilbo6a606b6G
iG9q6b5D6a6z
iK1Y6c676w
iKjD6d686r6u
iOoO6e666f6i
iXoC6f666e6h6i6o
iYnK6g515n696h6j6l6m
iYo66h515n6f6g6i6o6Q
iYo86i666e6f6h6o
j1np6j696g6l6m6s
j1qS6k616n6N6O
j2nn6l696g6j6m6s
j2no6m696g6j6l6s
jbqH6n6k6o6P
jips6o6f6h6i6n6Q
jmm96p696r6s
jpcZ6q6v6A6V
jqlg6r6d6p6F
jtmk6s6j6l6m6p6U
jtst6t5Y6J6K6N6O
jzj96u6d6y6R
jAcY6v6q6A6Y
jI3s6w6c6L7p
jPhG6x656y76
jQiv6y6u6x76
jS966z6b6C6D6E6G
jXcm6A6q6v6T
k36R6B6H6M7z7A7B
k48g6C6z6D6E6H81
k48i6D6z6C6E6H81
k48j6E6z6C6D6H81
k4k-6F6r6I6W
k69x6G6a6z7e
k7836H6B6C6D6E7z7A7B
k9kW6I6F6R6S
k9rO6J5Y6t6K6N6O
karN6K5Y6t6J6N6O
kf5b6L6w6M8c8d8e
kf5u6M6B6L89
kkrq6N5Y6k6t6J6K6O6P
klrp6O5Y6k6t6J6K6N6P
kpqM6P6n6N6O6Q
kqpN6Q6h6o6P
krjH6R6u6I6Z
ktkV6S6I7073
kAcm6T6A6Y7C
kAmi6U6s6W7c
kBep6V6q6-74
kClE6W6F6U70
kDvx6X6_72
kRdC6Y6v6T6-
kRjI6Z6R7378
kVdU6-6V6Y77
kWua6_6X7172
kZlj706S6W79
l1u1716_7a7K
l3uK726X6_
l6kD736S6Z79
l7fL746V7d7o
l7op757b7w8i
l8i-766x6y78
l9dW776-7u7v
lgj7786Z767c7r7t
lplh7970737c
lpsI7a717f88
lup17b757q7w
lvl_7c6U78797r7t
lxgK7d747s7E
lKa07e6G7U838485
lRs07f7a7n7F
lU0x7g7h7i7j7k7l7m8M
lV0y7h7g7i7j7k7l7m8M
lW0y7i7g7h7j7k7l7m8M
lX0y7j7g7h7i7k7l7m9f9i9j9v
lX0z7k7g7h7i7j7l7m8M9f9i9j
lZ0z7l7g7h7i7j7k7m9f9i9j9v
l-0z7m7g7h7i7j7k7l9f9i9j9v
m1qz7n7f7q7x
m3fM7o747s7v
m63S7p6w8b8o
m7pM7q7b7n7y
m8kG7r787c7t
mbgh7s7d7o7L
mbkt7t787c7r
mddj7u777C7G
mefE7v777o7L
mgoy7w757b7I
mjqJ7x7n7F7P
mopD7y7q7I7O
mp7g7z6B6H7A7B8k
mq7g7A6B6H7z7B8k
mu7g7B6B6H7z7A8k
muc37C6T7u7D
myc27D7C7H7V
mBht7E7d7L7W
mBrJ7F7f7x7S
mFdA7G7u7M7N
mIbK7H7D7U8A
mJoI7I7w7y7J
mKoI7J7I808P
mKuC7K717Y
mLgN7L7s7v7E
mSdw7M7G7N7Q
mSeu7N7G7M8l8q8u
mVpX7O7y7P7R
mWqh7P7x7O7X7Z
n2cZ7Q7M7V8t
n4pR7R7O808J
n4rT7S7F7T7-
n5rU7T7S8v8x
naaf7U7e7H7_
nacG7V7D7Q8j
nekB7W7E8f8_
noqF7X7P7Z7-8H8I
nouf7Y7K8286878g8m8w
npqE7Z7P7X7-8H8I
ntqT7-7S7X7Z8n
nu9v7_7U8384858Z
nupp807J7R8E
nv8g816C6D6E8384858a
nvt_827Y8687889E
nw8v837e7_818485
nw8w847e7_818385
nw8x857e7_818384
nwtZ867Y8287889E
nwt-877Y8286889E
nCtp887a8286878v
nF6P896M8c8d8e8k
nF7U8a818s9l
nH4N8b7p8h8p8r
nH608c6L898d8e8h
nH618d6L898c8e8h
nH628e6L898c8d8h
nHld8f7W8i93
nHuA8g7Y8m8w9B9D
nI5q8h8b8c8d8e99
nIlq8i758f8O
nJcM8j7V8y8z8B
nL7m8k7z7A7B898s
nLeX8l7N8q8u8C8W
nLuE8m7Y8g8w9B9D
nMqX8n7-8x8H8I
nN478o7p8p8r8M
nN4v8p8b8o8r9H
nNeZ8q7N8l8u8C8W
nO4u8r8b8o8p9H
nO7s8s8a8k9T
nOdx8t7Q8B8K
nOeZ8u7N8l8q8C8W
nOsT8v7T8890
nOuJ8w7Y8g8m9B9D
nRr88x7T8n90
nUcz8y8j8z8L9p
nVcy8z8j8y8L9p
nXbB8A7H8L8X
n-dd8B8j8t8Y
n-ek8C8l8q8u8D8F8G8-
n_e88D8C8F8G8K9x
n_pF8E808J8U
o0e68F8C8D8G8K9x
o0e78G8C8D8F8K9x
o0qh8H7X7Z8n8I8N
o0qi8I7X7Z8n8H8N
o1pO8J7R8E8N
o4dQ8K8t8D8F8G8Y
obbU8L8y8z8A8V
od2u8M7g7h7i7k8o97989a
ofq18N8H8I8J9e
ohm48O8i8T9294
ohnH8P7J8Q8R
oiny8Q8P8T9c
oknM8R8P8S9h
oon-8S8R8U9d
ormx8T8O8Q95
oroG8U8E8S96
oubZ8V8L8X9g
ovfr8W8l8q8u8-b9
oxaE8X8A8V8Z
oydj8Y8B8K9u
oBaC8Z7_8X91
oBeX8-8C8W9x
oFjN8_7W9bb9
oGrk908v8x9k
oHaB918Z9l9I
oHlM928O93949t
oJkL938f92949b
oJlL948O92939t
oKmA958T9c9F
oMoQ968U9m9n
oP1V978M989a9f9j9A9C
oQ1U988M979a9f9j9A9C
oQ5j998h9y9O9P9Q
oR1T9a8M97989f9j9A9C
oRkL9b8_939q
oRmZ9c8Q959w
oUo79d8S9m9o
oVqc9e8N9k9n
oW1G9f7j7k7l7m97989a9i9j
oXcd9g8V9p9J
oXnK9h8R9o9R
oY1E9i7j7k7l7m9f9j9v
oY1F9j7j7k7l7m97989a9f9i9v9A9C
oYr09k909e9L
o-9L9l8a91aC
o-oH9m969d9r9Za6
o-q79n969e9-
p1o99o9d9ha0
p2cO9p8y8z9g9u
p3k-9q9b9sa2
p4oF9r9m9Za6
p7lq9s9q9taj
p7lu9t92949s9N
p8c_9u8Y9p9K9M
pb0T9v7j7l7m9i9j
pcmY9w9c9G9U
peet9x8D8F8G8-a9
pi5E9y999z9S
pk609z9y9S9T
pl1T9A97989a9j9CaLaM
pmvs9B8g8m8w9D9E
pn1U9C97989a9j9AaLaM
pnvt9D8g8m8w9B9E
ppuu9E8286879B9D9W
pqmv9F959G9N
pqmz9G9w9F9X
pr4C9H8p8r9O9P9Qa3
praU9I919Jae
psbI9J9g9Ias
ptdm9K9u9Ma9abac
ptre9L9k9_a1
pudn9M9u9Ka9abac
pum79N9t9Fap
pv4U9O999H9P9Q9V
pw4V9P999H9O9Q9V
pw4W9Q999H9O9P9V
pwnt9R9h9U9Y
pz5h9S9y9z9V
pB7f9T8s9zaa
pCn09U9w9R9X
pD549V9O9P9Q9Sad
pDu99W9Ea4a7
pEmS9X9G9Uar
pHnF9Y9Ra0aq
pHou9Z9m9ra6
pIpJ9-9na5aZb0
pIr99_9Lb7bc
pKn-a09o9Ya8
pKrYa19La7bc
pLkAa29qaibebf
pN4ka39Hamax
pUuea49Wafau
pVoOa59-a6aZb0
pZora69m9r9Za5a8
p-sTa79Wa1aD
p_oca8a0a6aE
q0ema99x9K9Mat
q27iaa9Tagahb1
q7cKab9K9MacawaHaK
q8cJac9K9MabawaHaK
qb5cad9Vakal
qbapae9IasaA
qbu6afa4aBaD
qd6uagaaahalaQ
qe6tahaaagalaQ
qfk-aia2ajaF
qfldaj9saiao
qg55akadanb2
qg5laladagahaQ
qh4nama3anaN
qj4PanakamaR
qjlhaoajapaG
qkm8ap9Naoav
qknjaq9YaraE
qmmEar9Xaqav
qrbKas9JaeaJ
qreFata9awaTaVaWaXaY
qsuXaua4az
qumpavaparay
qzcKawabacataI
qD3taxa3aNb3
qDmlayavaSbd
qDuUazauaB
qI9NaAaeaCb6
qIu4aBafazbw
qK9jaC9laAa_
qPsTaDa7afbw
qQnVaEa8aqbm
qTkiaFaiaGbobr
qTlfaGaoaFaO
qUb-aHabacaIaJaK
qUcfaIawaHaKaTaVaWaXaY
qVbWaJasaHaKaP
qVbZaKabacaHaIaJ
q-1laL9A9CaMbO
q-1maM9A9CaLbO
r041aNamaxaR
r0ljaOaGaSbnbpbsbt
r2bRaPaJb6c7c8c9cbcd
r35GaQagahalb8
r443aRanaNaU
r5lzaSayaOba
r9fkaTataIaVaWaXaYbj
ra45aUaRa-b5
rbfmaVataIaTaWaXaYbj
rdfnaWataIaTaVaXaYbj
refoaXataIaTaVaWaYbj
rgfpaYataIaTaVaWaXbj
riq2aZ9-a5b0b7
rj4qa-aUb2bu
rj8ta_aCb1bSbTbUbVbWbXbYbZ
rjq3b09-a5aZb7
rk8pb1aaa_bq
rn4_b2aka-b8
rq2Ib3axb4bk
rv2_b4b3b5bi
rz3rb5aUb4bi
rAbkb6aAaPbSbTbUbVbWbXbYbZ
rBqab79_aZb0bE
rC5sb8aQb2bz
rJhib98W8_bebf
rQlUbaaSbbbnbpbsbt
rTm1bbbabdbN
rTrabc9_a1bI
rVmhbdaybbbh
rWhhbea2b9bfbg
rXhgbfa2b9bebg
r_hfbgbebfbjbobr
s0mtbhbdblbN
s53obib4b5bv
s5gdbjaTaVaWaXaYbgc7c8c9cbcd
s92rbkb3bAbK
sfo3blbhbmce
sfobbmaEblbxby
sgk6bnaObabpbsbtbH
shhDboaFbgbrbF
shk3bpaObabnbsbtbH
si8abqb1bzck
sihDbraFbgbobF
sik1bsaObabnbpbtbH
sjj_btaObabnbpbsbH
sk44bua-bvbJ
sm3tbvbibubA
sosKbwaBaDc5
sxoKbxbmbybBbCbDc4
syoLbybmbxbBbCbDc4
sC5Pbzb8bqbJ
sD3jbAbkbvbG
sKpzbBbxbybCbDbEct
sKpAbCbxbybBbDbEct
sKpBbDbxbybBbCbEct
sKqvbEb7bBbCbDbI
sNi5bFbobrbH
sO3jbGbAbLbR
sViHbHbnbpbsbtbFb_
sWqUbIbcbEc5
t05kbJbubzbM
t11HbKbkbPbQ
t137bLbGbQc2
t15jbMbJbRc6
t3lwbNbbbhc0c1
ta0HbOaLaMbP
tb19bPbKbOcj
tc2LbQbKbLb-
ti4JbRbGbMcacc
ttbCbSa_b6bTbUbVbWbXbYbZcC
tubDbTa_b6bSbUbVbWbXbYbZcC
tvbCbUa_b6bSbTbVbWbXbYbZcC
twbCbVa_b6bSbTbUbWbXbYbZcC
txbCbWa_b6bSbTbUbVbXbYbZcC
tzbDbXa_b6bSbTbUbVbWbYbZcC
tCbEbYa_b6bSbTbUbVbWbXbZcC
tDbEbZa_b6bSbTbUbVbWbXbYcC
tR2Vb-bQc3cfci
tVkKb_bHc0c1
tXkRc0bNb_c1ce
tXkSc1bNb_c0ce
tZ3nc2bLc3cg
t-3fc3b-c2cfci
t_nkc4bxbychcl
u0sgc5bwbIcB
u15wc6bMcncq
u7dHc7aPbjc8c9cbcdcC
u9dFc8aPbjc7c9cbcdcC
uadEc9aPbjc7c8cbcdcC
ub46cabRcccgcr
ubdDcbaPbjc7c8c9cdcC
uc45ccbRcacgcr
ucdCcdaPbjc7c8c9cbcC
uhlvceblc0c1ch
ul2Jcfb-c3cicj
ul3Rcgc2cacccr
ullzchc4cecm
um2Icib-c3cfcj
uo2EcjbPcfcico
uw9kckbqcp
uFmWclc4cmcs
uHlQcmchcl
uI63cnc6cpcz
uM2AcocjcycA
uM6ocpckcn
uO51cqc6cwcz
uR4jcrcacccgcu
uSnPcsclct
uTnXctbBbCbDcscv
v14ncucrcwcx
v1o3cvctcB
v54rcwcqcu
va3WcxcucA
vg1Zcyco
vl5Bczcncq
vt3kcAcocx
vut9cBc5cv
vHc8cCbSbTbUbVbWbXbYbZc7c8c9cbcd";


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

 
// // Point startPointGoal = new Point(1081,1031);//ok
// Point startPointGoal = new Point(50,50);//crash
 // // Point startPointGoal = new Point(1794,1913);
 // // Point startPointGoal = new Point(1102,1791);//crash ? bug358
 // // Point startPointGoal = new Point(1425,1783);
// //Point startPointGoal = new Point(1950,1664);
// // Point startPointGoal = new Point(1800,1664);//2node
// //Point startPointGoal = new Point(0,1664);//crash
// // Point startPointGoal = new Point(1081,1786);//crash
// Point finalPointGoal = new Point(2043,1664);


Point startPointGoal  = new Point(2043,1664);
Point finalPointGoal = new Point(50,50);//crash


// Point startPointGoal  = new Point(1101,1791);
// Point finalPointGoal = new Point(586,1265);

// Point startPointGoal  = new Point(1871,2019);
// Point finalPointGoal = new Point(1733,1852);




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
			
			Dictionary<Node, double> NodeFscore = new Dictionary<Node, double>();
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
					NodeFscore[neighbor] = fscore[neighbor];
					//listHeapNodes.Add(neighbor);
					 // Echo("here2");
				}
				
			}
			
			foreach(KeyValuePair<Node,double> entry in NodeFscore.OrderByDescending(key => key.Value)){
				//Echo("entry.Key:"+entry.Key);
				listHeapNodes.Add(entry.Key);
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
		
		// toCustomData = toCustomData +"displayLarger(["+pathNode.position.X +","+pathNode.position.Y + "])" + '\n';
		
		// //2027 776
		// if(gps_number == 15){
			// Echo("pathNode.position 15:"+pathNode.position);
			// Echo("pathNode.index 15:"+pathNode.index);
		// }
		// // 1952 596
		// if(gps_number == 56){
			// Echo("pathNode.position 56:"+pathNode.position);
			// Echo("pathNode.index 56:"+pathNode.index);
		// }
		
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


