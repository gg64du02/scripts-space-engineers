
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

string nodesStringRight = "09ku05|0E3L0x0D|0Jhc0a0n|0QiL0a0r1e1f1g1h1j|0Tf1070809|0Tkz000b0c0f|0Wce0s0A|0Wfk040n0w|0Ye_040u0F|0Ze_040u0F|0Zhz02031e1f1g1h1j|0Zk5050r1l|0ZkK050g0R|0-770t0W|0-9j0k0o|0-kL050g0R|10lD0c0f0R|13dt0p0A|158m0l0m|15bf0q0E|198W0e0m0N0O0P|1b850i0M|1d8x0i0k0J|1fga02070w|1i9r0e0C0S|1jec0h0u|1lbu0j0s0L|1njh030b1k|1obW060q0U|1p6Q0d0v|1pej08090p0F|1t6P0t0W10|1ug5070n1n1p|1z4e010y0T|1A540x10|1B3e0D0Q|1DcM060h0I|1H0F0-1P1Q1S|1H9H0o0H12|1J3y010z0T|1KaB0j0G0H|1Lf208090u1d|1MaC0E0K19|1Nao0C0E11|1OcH0A0U14|1Q8C0m0M0N0O0P|1QaI0G0L17|1Qbe0q0K13|1R860l0J0Y|1R8H0k0J0S|1R8I0k0J0S|1S8J0k0J0S|1T2R0z0Z|1Tlp0c0f0g1B|1U8N0o0N0O0P16|1V3E0x0D0_|1Wcu0s0I0V|1Ycu0U0X1a|1Z7w0d0v0Y|1Zct0V1315|217x0M0W1t|242K0Q0-0_|2a2k0B0Z1Z1-1_|2i3v0T0Z1T|2i5A0v0y1y|2k9Y0H1219|2l9v0C1116|2rbx0L0X15|2rdh0I1c1d|2ubx0X1318|2v9d0S121G|2vaO0K181b|2vbx15171q|2yap0G111b|2Bcr0V1i1q|2Dau17191o|2Edd141i1m|2Hfv0F141n1p|2MhC030a1s|2OhC030a1s|2QhC030a1s|2ThC030a1s|2UcS1a1c1L1M|2UhC030a1s|2Vjl0r1l1D|2Zj-0b1k1B|2-dv1c1C1E|2_g20w1d1r|30ax1b1u1K|30g30w1d1r|31bD181a1u|37g71n1p1z1J|3ehC1e1f1g1h1j1A1D|3f7e0Y1v1w1x1H|3fbB1o1q1N|3h6C1t1y25|3h6D1t1y25|3h6E1t1y25|3i5z101v1w1x1T|3jgr1r1A24|3jgy1s1z28|3nlb0R1l22|3pdV1m1I1J|3rij1k1s1X|3Cdb1m1I1L1M|3H8H1G1H1V|3H8I161F1K|3I831t1F25|3Kdv1C1E1O|3Lf61r1C1U|3U9x1o1G1V|3Uco1i1E1N|3Vcn1i1E1N|4ac41u1L1M1W|4gdH1I1R1Y|4m2K0B20|4m2L0B20|4mdA1O1W1Y|4n2M0B20|4o4c0_1y1Z1-1_|4seF1J1Y24|4vai1F1K21|4xc11N1R21|4Bis1D2a2i|4Ne81O1R1U|4R3R0-1T20|4T3Q0-1T20|4U3Q0-1T20|4_3M1P1Q1S1Z1-1_23|50bQ1V1W27|59nG1B2A|5b3K202o2V|5hfC1z1U26|5l7A1v1w1x1H2l|5lfC242c2q|5rc3212q2O|5rgQ1A2a2d|5A1a2o|5BhW1X282b|5ChW2a2k2r|5FfG262e2p|5HgK282f2g2j|5Tgp2c2f2g2h2u|5Tgq2d2e2h2j|5Tgr2d2e2h2j|5Ugq2e2f2g2j2u|61ka1X2m2s|62gW2d2f2g2h2r|62is2b2m2v|687P252n2B2C2E2I|69iF2i2k2v|6f8a2l2E|6k2B23292_|6tfq2c2D2G|6vdd26272F|6vhs2b2j2y|6Blc2i2z2H|6Ca72E2O|6CgI2e2h2x2D|6Giy2k2m2z|6K9v2E2O|6KgU2u2y2T|6Lhn2r2x2K|6PiM2s2v2S2U|6Qn8222H3W|6X9h2l2E|6X9i2l2E|6XfD2p2u2J2L2M|6Y9n2l2n2t2w2B2C2O32|73ds2q2G2P|73dt2p2F3a|73mx2s2A35|767J2l2N2X|79fz2D2W31|79hG2y2Q2R33|7afz2D2W31|7bfz2D2W31|7k7P2I2Y2Z|7oaD272t2w2E2-|7rdd2F2-3c|7Di82K2S2U33|7Ei92K2S2U33|7Jil2z2Q2R35|7Kgx2x2W37|7Kim2z2Q2R35|7O5E232X36|7SfO2J2L2M2T39|7X5V2I2V3e|84812N2Z3e|848h2N2Y32|87aV2O2P32|891H2o383h|8eeG313a3g|8ofr2J2L2M303b|8p9A2E2Z2-|8phm2K2Q2R34|8qhm333537|8yi-2H2S2U34|8F4G2V383v|8KgC2T3439|8N432_363j|8Of_2W373b|8Pel2G303d|8QfY31393g|8-df2P3d3f|8-e03a3c3l|92762X2Y3i|9acK3c3m3z|9hfA303b3k|9j022_3J|9s7m3e3I4q|9v47383t3v|9vfu3g3l3Q|9web3d3k3z|9xcR3f3q3T3V3X3Z3_|9Bc93q3r41424345464748494a4b4c4d4e4f4g4h4i4j4l4m|9Gaf3p3B3C3I|9Hah3o3r3u|9Jcu3m3n44|9Lb23n3p3s|9Ra_3r3x3y|9S3K3j3F3J|9Vak3p3w3B3C|a04q363j3N|a1aB3u3x3L3M|a2aH3s3w3y|a3bd3s3x3D|a8dw3f3l3T3V3X3Z3_|aau63H4z4A4B4C4D4E4F|ab9R3o3u3K|ac9Q3o3u3K|acbi3y3E3G|ahbx3D3Y41424345464748494a4b4c4d4e4f4g4h4i4j4l4m|ai3R3t3N3S|akb43D3P3R|aktx3A3W58|an9d3i3o3O|ar2x3h3t3S|at9w3B3C3O3U|ataq3w3P3U|atar3w3P3U|au493v3F4k|au9p3I3K4p|auaS3G3L3M3-|aufz3k4051|awbk3G3Y3-|ax3g3F3J4n|aydm3m3z40|aza53K3L3M3-|aAdl3m3z40|aArx2A3H5j|aBdl3m3z40|aCbw3E3R4G4H4I|aDdk3m3z40|aEb23P3R3U|aEdk3m3z40|aIdj3Q3T3V3X3Z3_44|aKcB3n3E4o|aNcE3n3E4o|aOcF3n3E4o|aOdf3q404o|aPcF3n3E4o|aQcG3n3E4o|aRcG3n3E4o|aScH3n3E4o|aTcH3n3E4o|aTcI3n3E4o|aTcJ3n3E4o|aUcJ3n3E4o|aVcK3n3E4o|aWcK3n3E4o|aXcL3n3E4o|aYcL3n3E4o|a_cO3n3E4o|b0cO3n3E4o|b1cP3n3E4o|b34a3N4n4r|b5cS3n3E4o|b7cT3n3E4o|ba3R3S4k4x4y|bdcY4142434445464748494a4b4c4d4e4f4g4h4i4j4l4m4G4H4I|bq9z3O4t4P|bB5T3i4r4s|bH5n4k4q4x4y|bI5_4q4w4T4U|bM8Z4p4u4S|bO8m4t4v4M|bT7d4u4w4R|bZ6L4s4v4V|b-574n4r595a5b|b_564n4r595a5b|ceuj3A4J4K4L4N4O4Q|cfuj3A4J4K4L4N4O4Q|cgui3A4J4K4L4N4O4Q|chuj3A4J4K4L4N4O4Q|ciuj3A4J4K4L4N4O4Q|ckuj3A4J4K4L4N4O4Q|cmuj3A4J4K4L4N4O4Q|crcA3Y4o4P|cscA3Y4o4P|cucz3Y4o4P|cxuk4z4A4B4C4D4E4F4X4-5m5o5x|cyuk4z4A4B4C4D4E4F4X4-5m5o5x|cAuk4z4A4B4C4D4E4F4X4-5m5o5x|cB8t4u4R4S|cBuk4z4A4B4C4D4E4F4X4-5m5o5x|cDul4z4A4B4C4D4E4F4X4-5m5o5x|cIcx4p4G4H4I50|cIul4z4A4B4C4D4E4F4X4-5m5o5x|cM8c4v4M4W|cO954t4M4_|d15S4s5e5f5h5i|d25R4s5e5f5h5i|dd774w4W5d|dn7T4R4V4Y|dsur4J4K4L4N4O4Q5x|dt7Y4W4Z54|dt894Y4_54|dtus4J4K4L4N4O4Q5x|du964S4Z5t|dFcU4P515253|dOeW3Q505n6g6h|dYcH505556575u5V5X|dZcG505556575u5V5X|d-824Y4Z5s|e4cG52535X|e5cG52535X|e8cG52535X|edsP3H5c5l|el4w4x4y5h|en4z4x4y5h|eo4A4x4y5h|essp585g5j|ev6y4V5e5f5i5p|eG5N4T4U5d5k|eG5O4T4U5d5k|eGsp5c5l5q|eI584T4U595a5b5k|eI5G4T4U5d5k|eNrG3W5c5q|eQ5e5e5f5h5i5L|e-th585g5m|f0tm4J4K4L4N4O4Q5l5r|f6gh515A5E6g6h|fav94J4K4L4N4O4Q5x|fk7D5d5s5J5K|fls95g5j5w|fut65m5w5C|fC8T545p5v|fC9o4_5u5v|fD9G52535t5D|fI985s5t5O|fIsh5q5r5y5z|fSvR4J4K4L4N4O4Q4X4-5o5Y|g5ss5w5B5C|g6st5w5B5C|gsiW5n5E5F|gtrN5y5z5M5P5Q5R5T|gtsM5r5y5z5P5Q5R5T|gxad5u5G6b|gAgt5n5A5N|gNi-5A5H5I68|gXaJ5D5X60|h0iy5F5N626364|h0iz5F5N626364|h57b5p5O5S|h67c5p5O5S|h83_5k5S67|hkrr5B5Z5-5_61|hlgx5E5H5I65|ho7y5v5J5K5U|hxt85B5C5W|hyt95B5C5W|hzta5B5C5W|hA6o5J5K5L5U|hAta5B5C5W|hI6o5O5S67|hPc552535X|hPti5P5Q5R5T5Y5Z5-5_|hSbZ52535556575G5V60|hVts5x5W6t6J6K6N6O|h-s25M5W61|h-s35M5W61|h-s45M5W61|h_bC5G5X6a|i2rV5M5Z5-5_6k|i4hs5H5I6568|i4hu5H5I6568|i4hv5H5I6568|i7gE5N6263646x|i8ph6f6i|ig2A5L5U6c|igjh5F6263646d|iind6g6j6l6m6p|ilbo606b6G|iG9q5D6a6z|iK1Y676w|iKjD686r6u|iOoO6f6i|iXoC666e6h6o|iYnK515n696h6j6l6m|iYo6515n6f6g6i6o6Q|iYo8666e6h6o|j1np696g6s|j1qS616n6N6O|j2nn696g6s|j2no696g6s|jbqH6k6o6P|jips6f6h6i6n6Q|jmm9696r6s|jpcZ6v6A6V|jqlg6d6p6F|jtmk6j6l6m6p6U|jtst5Y6N6O|jzj96d6y6R|jAcY6q6A6Y|jI3s6c6L7p|jPhG656y76|jQiv6u6x76|jS966b6C6D6E6G|jXcm6q6v6T|k36R6H6M7z7A7B|k48g6z6H81|k48i6z6H81|k48j6z6H81|k4k-6r6I6W|k69x6a6z7e|k7836B6C6D6E7z7A7B|k9kW6F6R6S|k9rO5Y6N6O|karN5Y6N6O|kf5b6w6M8c8d8e|kf5u6B6L89|kkrq5Y6k6t6J6K6P|klrp5Y6k6t6J6K6P|kpqM6n6N6O6Q|kqpN6h6o6P|krjH6u6I6Z|ktkV6I7073|kAcm6A6Y7C|kAmi6s6W7c|kBep6q6-74|kClE6F6U70|kDvx6_72|kRdC6v6T6-|kRjI6R7378|kVdU6V6Y77|kWua6X7172|kZlj6S6W79|l1u16_7a7K|l3uK6X6_|l6kD6S6Z79|l7fL6V7d7o|l7op7b7w8i|l8i-6x6y78|l9dW6-7u7v|lgj76Z767c7r7t|lplh70737c|lpsI717f88|lup1757q7w|lvl_6U78797r7t|lxgK747s7E|lKa06G7U838485|lRs07a7n7F|lU0x7j7k7l7m8M|lV0y7j7k7l7m8M|lW0y7j7k7l7m8M|lX0y7g7h7i7k9f9i9j9v|lX0z7g7h7i7j7l7m8M9f9i9j|lZ0z7g7h7i7k9f9i9j9v|l-0z7g7h7i7k9f9i9j9v|m1qz7f7q7x|m3fM747s7v|m63S6w8b8o|m7pM7b7n7y|m8kG787c|mbgh7d7o7L|mbkt787c|mddj777C7G|mefE777o7L|mgoy757b7I|mjqJ7n7F7P|mopD7q7I7O|mp7g6B6H8k|mq7g6B6H8k|mu7g6B6H8k|muc36T7u7D|myc27C7H7V|mBht7d7L7W|mBrJ7f7x7S|mFdA7u7M7N|mIbK7D7U8A|mJoI7w7y7J|mKoI7I808P|mKuC717Y|mLgN7s7v7E|mSdw7G7N7Q|mSeu7G7M8l8q8u|mVpX7y7P7R|mWqh7x7O7X7Z|n2cZ7M7V8t|n4pR7O808J|n4rT7F7T7-|n5rU7S8v8x|naaf7e7H7_|nacG7D7Q8j|nekB7E8f8_|noqF7P7-8H8I|nouf7K8286878g8m8w|npqE7P7-8H8I|ntqT7S7X7Z8n|nu9v7U8384858Z|nupp7J7R8E|nv8g6C6D6E8384858a|nvt_7Y889E|nw8v7e7_81|nw8w7e7_81|nw8x7e7_81|nwtZ7Y889E|nwt-7Y889E|nCtp7a8286878v|nF6P6M8c8d8e8k|nF7U818s9l|nH4N7p8h8p8r|nH606L898h|nH616L898h|nH626L898h|nHld7W8i93|nHuA7Y9B9D|nI5q8b8c8d8e99|nIlq758f8O|nJcM7V8y8z8B|nL7m7z7A7B898s|nLeX7N8C8W|nLuE7Y9B9D|nMqX7-8x8H8I|nN477p8p8r8M|nN4v8b8o9H|nNeZ7N8C8W|nO4u8b8o9H|nO7s8a8k9T|nOdx7Q8B8K|nOeZ7N8C8W|nOsT7T8890|nOuJ7Y9B9D|nRr87T8n90|nUcz8j8L9p|nVcy8j8L9p|nXbB7H8L8X|n-dd8j8t8Y|n-ek8l8q8u8D8F8G8-|n_e88C8K9x|n_pF808J8U|o0e68C8K9x|o0e78C8K9x|o0qh7X7Z8n8N|o0qi7X7Z8n8N|o1pO7R8E8N|o4dQ8t8D8F8G8Y|obbU8y8z8A8V|od2u7g7h7i7k8o97989a|ofq18H8I8J9e|ohm48i8T9294|ohnH7J8Q8R|oiny8P8T9c|oknM8P8S9h|oon-8R8U9d|ormx8O8Q95|oroG8E8S96|oubZ8L8X9g|ovfr8l8q8u8-b9|oxaE8A8V8Z|oydj8B8K9u|oBaC7_8X91|oBeX8C8W9x|oFjN7W9bb9|oGrk8v8x9k|oHaB8Z9l9I|oHlM8O939t|oJkL8f92949b|oJlL8O939t|oKmA8T9c9F|oMoQ8U9m9n|oP1V8M9f9j9A9C|oQ1U8M9f9j9A9C|oQ5j8h9y9O9P9Q|oR1T8M9f9j9A9C|oRkL8_939q|oRmZ8Q959w|oUo78S9m9o|oVqc8N9k9n|oW1G7j7k7l7m97989a9i|oXcd8V9p9J|oXnK8R9o9R|oY1E7j7k7l7m9f9v|oY1F7j7k7l7m97989a9v9A9C|oYr0909e9L|o-9L8a91aC|o-oH969d9r9Za6|o-q7969e9-|p1o99d9ha0|p2cO8y8z9g9u|p3k-9b9sa2|p4oF9ma6|p7lq9q9taj|p7lu92949s9N|p8c_8Y9p9K9M|pb0T7j7l7m9i9j|pcmY9c9G9U|peet8D8F8G8-a9|pi5E999z9S|pk609y9S9T|pl1T97989a9jaLaM|pmvs8g8m8w9E|pn1U97989a9jaLaM|pnvt8g8m8w9E|ppuu8286879B9D9W|pqmv959G9N|pqmz9w9F9X|pr4C8p8r9O9P9Qa3|praU919Jae|psbI9g9Ias|ptdm9ua9abac|ptre9k9_a1|pudn9ua9abac|pum79t9Fap|pv4U999H9V|pw4V999H9V|pw4W999H9V|pwnt9h9U9Y|pz5h9y9z9V|pB7f8s9zaa|pCn09w9R9X|pD549O9P9Q9Sad|pDu99Ea4a7|pEmS9G9Uar|pHnF9Ra0aq|pHou9ma6|pIpJ9na5aZb0|pIr99Lb7bc|pKn-9o9Ya8|pKrY9La7bc|pLkA9qaibebf|pN4k9Hamax|pUue9Wafau|pVoO9-a6aZb0|pZor9m9r9Za5a8|p-sT9Wa1aD|p_oca0a6aE|q0em9x9K9Mat|q27i9Tagahb1|q7cK9K9MawaHaK|q8cJ9K9MawaHaK|qb5c9Vakal|qbap9IasaA|qbu6a4aBaD|qd6uaaalaQ|qe6taaalaQ|qfk-a2ajaF|qfld9saiao|qg55adanb2|qg5ladagahaQ|qh4na3anaN|qj4PakamaR|qjlhajapaG|qkm89Naoav|qknj9YaraE|qmmE9Xaqav|qrbK9JaeaJ|qreFa9awaTaVaWaXaY|qsuXa4az|qumpaparay|qzcKabacataI|qD3ta3aNb3|qDmlavaSbd|qDuUauaB|qI9NaeaCb6|qIu4afazbw|qK9j9laAa_|qPsTa7afbw|qQnVa8aqbm|qTkiaiaGbobr|qTlfaoaFaO|qUb-abacaIaJ|qUcfawaHaKaTaVaWaXaY|qVbWasaHaKaP|qVbZabacaIaJ|q-1l9A9CbO|q-1m9A9CbO|r041amaxaR|r0ljaGaSbnbpbsbt|r2bRaJb6c7c8c9cbcd|r35Gagahalb8|r443anaNaU|r5lzayaOba|r9fkataIbj|ra45aRa-b5|rbfmataIbj|rdfnataIbj|refoataIbj|rgfpataIbj|riq29-a5b7|rj4qaUb2bu|rj8taCb1bSbTbUbVbWbXbYbZ|rjq39-a5b7|rk8paaa_bq|rn4_aka-b8|rq2Iaxb4bk|rv2_b3b5bi|rz3raUb4bi|rAbkaAaPbSbTbUbVbWbXbYbZ|rBqa9_aZb0bE|rC5saQb2bz|rJhi8W8_bebf|rQlUaSbbbnbpbsbt|rTm1babdbN|rTra9_a1bI|rVmhaybbbh|rWhha2b9bg|rXhga2b9bg|r_hfbebfbjbobr|s0mtbdblbN|s53ob4b5bv|s5gdaTaVaWaXaYbgc7c8c9cbcd|s92rb3bAbK|sfo3bhbmce|sfobaEblbxby|sgk6aObabH|shhDaFbgbF|shk3aObabH|si8ab1bzck|sihDaFbgbF|sik1aObabH|sjj_aObabH|sk44a-bvbJ|sm3tbibubA|sosKaBaDc5|sxoKbmbBbCbDc4|syoLbmbBbCbDc4|sC5Pb8bqbJ|sD3jbkbvbG|sKpzbxbybEct|sKpAbxbybEct|sKpBbxbybEct|sKqvb7bBbCbDbI|sNi5bobrbH|sO3jbAbLbR|sViHbnbpbsbtbFb_|sWqUbcbEc5|t05kbubzbM|t11HbkbPbQ|t137bGbQc2|t15jbJbRc6|t3lwbbbhc0c1|ta0HaLaMbP|tb19bKbOcj|tc2LbKbLb-|ti4JbGbMcacc|ttbCa_b6cC|tubDa_b6cC|tvbCa_b6cC|twbCa_b6cC|txbCa_b6cC|tzbDa_b6cC|tCbEa_b6cC|tDbEa_b6cC|tR2VbQc3cfci|tVkKbHc0c1|tXkRbNb_ce|tXkSbNb_ce|tZ3nbLc3cg|t-3fb-c2cfci|t_nkbxbychcl|u0sgbwbIcB|u15wbMcncq|u7dHaPbjcC|u9dFaPbjcC|uadEaPbjcC|ub46bRcgcr|ubdDaPbjcC|uc45bRcgcr|ucdCaPbjcC|uhlvblc0c1ch|ul2Jb-c3cj|ul3Rc2cacccr|ullzc4cecm|um2Ib-c3cj|uo2EbPcfcico|uw9kbqcp|uFmWc4cmcs|uHlQchcl|uI63c6cpcz|uM2AcjcycA|uM6ockcn|uO51c6cwcz|uR4jcacccgcu|uSnPclct|uTnXbBbCbDcscv|v14ncrcwcx|v1o3ctcB|v54rcqcu|va3WcucA|vg1Zco|vl5Bcncq|vt3kcocx|vut9c5cv|vHc8bSbTbUbVbWbXbYbZc7c8c9cbcd";


	//string s = "You win some. You lose some.";
	string s = nodesStringRight;

	string[] subs  = s.Split('|');
	
	// Echo("a:"+decodeAsCharNumberMax64('a'));
	// Echo("aa:"+decodeStr__NumberMax4095("aa"));
	

	int indexNumber = 0;
	
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
		
		string encodedNeighborsIndexes = encodedIndexes.Substring(0);
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
		//int indexNumber = currentNodeIndexDecoded;
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


public void aStarPathFinding(Point startPoint, Point endPoint,out List<Node> listPathNode){
	listPathNode = new List<Node>();
	
	Point startPointGoal  = startPoint;
	Point finalPointGoal = endPoint;

	
	Echo("nodes.Count"+nodes.Count);
	
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
	fscore.Add(node,gscore[node]+heuristic(node.position,ourDestinationNode.position));
	
	Echo("nodeStarting.index:"+nodeStarting.index);
	
	
	int debugCount = 0;
	
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
				double tentative_g_score = gscore[node] + heuristic(node.position, neighbor.position);
				if(closelist.Contains(neighbor)==true){
					double gscoreTmp = gscore.ContainsKey(neighbor) ? gscore[neighbor] : 0;
					if( tentative_g_score >=gscoreTmp){
						continue;
					}
				}
				
				double gscoreTmp2 = gscore.ContainsKey(neighbor) ? gscore[neighbor] : 0;
				if(tentative_g_score < gscoreTmp2 || listHeapNodes.Contains(neighbor)==false){
					 // Echo("here1");
					came_from[neighbor] = node;
					gscore[neighbor] = tentative_g_score;
					fscore[neighbor] = tentative_g_score + heuristic(neighbor.position,ourDestinationNode.position);
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
		// Echo("data.Add(node);");
		// Echo("node.position:"+node.position);
		// Echo(""+Math.Sqrt(distanceSquarred(node.position,ourDestinationNode.position)));
		Echo("gscore[node]:"+gscore[node]);
		data.Add(node);
		node = came_from[node];
	}
	
	
	string toCustomData = "";
	
	int gps_number = 0;
	
	Point previousPointDebug = new Point(0,0);
	
	foreach(Node pathNode in data){
		// public Vector3D convertPointToV3D(IMyRemoteControl sc, int faceNumber, Point pointToV3D){
		//toCustomData = toCustomData + pathNode.position;
		Vector3D nodeConverted = convertPointToV3D(RemoteControl, 4, pathNode.position);
		
		// MyWaypointInfo tmpWPINode  = new MyWaypointInfo("inter", nodeConverted);
		MyWaypointInfo tmpWPINode  = new MyWaypointInfo(gps_number.ToString(), nodeConverted);
		
		// toCustomData = toCustomData + tmpWPINode.ToString() + '\n';
		
		//toCustomData = toCustomData +"displayLarger(["+pathNode.position.X +","+pathNode.position.Y + "])" + '\n';
		
		if(previousPointDebug==new Point(0,0)){
			previousPointDebug = pathNode.position;
		}
		else{
			toCustomData = toCustomData +"displayLine(["+pathNode.position.X+","+pathNode.position.Y + "],["+previousPointDebug.X+","+previousPointDebug.Y+"])" + '\n';
			previousPointDebug = pathNode.position;
		}
		
		gps_number = gps_number + 1;
	}
	Me.CustomData = toCustomData;
}



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
	
    // return heuristicZero(a,b);
    return euclideanDistance(a,b);
    // return manhattanDistance(a,b);
    // return distanceSquarred(a,b);
}

public double euclideanDistance(Point a, Point b){
	
    return Math.Sqrt((b.X - a.X)*(b.X - a.X) + (b.Y - a.Y)*(b.Y - a.Y));
}

public double manhattanDistance(Point a, Point b){
	
    return Math.Abs(b.X - a.X) + Math.Abs(b.Y - a.Y);
}

public double distanceSquarred(Point a, Point b){
	
    return (b.X - a.X)*(b.X - a.X) + (b.Y - a.Y)*(b.Y - a.Y);
}


public double heuristicZero(Point a, Point b){
	
    return 0;
}


public void Main(string argument, UpdateType updateSource)
{
    // The main entry point of the script, invoked every time
    // one of the programmable block's Run actions are invoked,
    // or the script updates itself. The updateSource argument
    // describes where the update came from.
    // 
    // The method itself is required, but the arguments above
    // can be removed if not needed.
	
	List<Node> aStarPathNodeList = new List<Node>();
	
// // ok euclidian distance going across with no circles
// Point startPointGoal  = new Point(2043,1664);
// Point finalPointGoal = new Point(50,50);

//todo: checking for simplification
// Point startPointGoal  = new Point(2043,1664);//this2
// Point finalPointGoal = new Point(429,1284);
Point finalPointGoal  = new Point(2043,1664);//this1
Point startPointGoal   = new Point(429,1284);
// Point startPointGoal  = new Point(1440,767);
// Point finalPointGoal = new Point(429,1284);
// Point finalPointGoal  = new Point(1440,767);
// Point startPointGoal  = new Point(429,1284);

// need more test, seems like path finding is jumping around the big obstacle ?
//TODO: too many links ?
// Point startPointGoal  = new Point(1101,1791);
// Point finalPointGoal = new Point(586,1265);


//ok, 3 point euclidian distance
// Point startPointGoal  = new Point(1871,2019);
// Point finalPointGoal = new Point(1733,1852);

// //testing avoiding the canyons
// Point startPointGoal  = new Point(600,2043);
// Point finalPointGoal = new Point(1600,2043);


	aStarPathFinding(startPointGoal,finalPointGoal, out aStarPathNodeList);
	
	
	
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


