
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
  nodes.Add(new Node(0,new Point(9,1310),20));
  nodes.Add(new Node(1,new Point(40,239),37));
  nodes.Add(new Node(2,new Point(45,1100),37));
  nodes.Add(new Node(3,new Point(52,1199),41));
  nodes.Add(new Node(4,new Point(55,961),29));
  nodes.Add(new Node(5,new Point(55,1315),26));
  nodes.Add(new Node(6,new Point(58,782),16));
  nodes.Add(new Node(7,new Point(58,980),19));
  nodes.Add(new Node(8,new Point(60,959),25));
  nodes.Add(new Node(9,new Point(61,959),24));
  nodes.Add(new Node(10,new Point(61,1123),40));
  nodes.Add(new Node(11,new Point(61,1285),23));
  nodes.Add(new Node(12,new Point(61,1326),25));
  nodes.Add(new Node(13,new Point(62,455),21));
  nodes.Add(new Node(14,new Point(62,595),22));
  nodes.Add(new Node(15,new Point(62,1327),25));
  nodes.Add(new Node(16,new Point(64,1383),27));
  nodes.Add(new Node(17,new Point(67,861),24));
  nodes.Add(new Node(18,new Point(69,534),9));
  nodes.Add(new Node(19,new Point(69,719),23));
  nodes.Add(new Node(20,new Point(73,570),9));
  nodes.Add(new Node(21,new Point(75,517),9));
  nodes.Add(new Node(22,new Point(77,545),9));
  nodes.Add(new Node(23,new Point(79,1034),30));
  nodes.Add(new Node(24,new Point(82,603),15));
  nodes.Add(new Node(25,new Point(83,908),20));
  nodes.Add(new Node(26,new Point(85,734),23));
  nodes.Add(new Node(27,new Point(87,1233),49));
  nodes.Add(new Node(28,new Point(88,762),22));
  nodes.Add(new Node(29,new Point(89,436),25));
  nodes.Add(new Node(30,new Point(89,915),21));
  nodes.Add(new Node(31,new Point(93,435),27));
  nodes.Add(new Node(32,new Point(94,1029),36));
  nodes.Add(new Node(33,new Point(99,270),19));
  nodes.Add(new Node(34,new Point(100,324),31));
  nodes.Add(new Node(35,new Point(101,206),14));
  nodes.Add(new Node(36,new Point(103,816),20));
  nodes.Add(new Node(37,new Point(107,41),119));
  nodes.Add(new Node(38,new Point(107,619),20));
  nodes.Add(new Node(39,new Point(109,226),20));
  nodes.Add(new Node(40,new Point(110,677),24));
  nodes.Add(new Node(41,new Point(111,962),22));
  nodes.Add(new Node(42,new Point(112,678),23));
  nodes.Add(new Node(43,new Point(113,664),16));
  nodes.Add(new Node(44,new Point(114,811),24));
  nodes.Add(new Node(45,new Point(116,550),27));
  nodes.Add(new Node(46,new Point(116,684),20));
  nodes.Add(new Node(47,new Point(116,718),11));
  nodes.Add(new Node(48,new Point(117,518),26));
  nodes.Add(new Node(49,new Point(117,555),29));
  nodes.Add(new Node(50,new Point(117,556),28));
  nodes.Add(new Node(51,new Point(118,557),29));
  nodes.Add(new Node(52,new Point(119,181),14));
  nodes.Add(new Node(53,new Point(119,1369),31));
  nodes.Add(new Node(54,new Point(120,561),29));
  nodes.Add(new Node(55,new Point(121,232),25));
  nodes.Add(new Node(56,new Point(122,798),27));
  nodes.Add(new Node(57,new Point(124,798),27));
  nodes.Add(new Node(58,new Point(125,480),38));
  nodes.Add(new Node(59,new Point(125,797),26));
  nodes.Add(new Node(60,new Point(129,481),39));
  nodes.Add(new Node(61,new Point(132,174),20));
  nodes.Add(new Node(62,new Point(138,148),30));
  nodes.Add(new Node(63,new Point(146,223),30));
  nodes.Add(new Node(64,new Point(146,356),63));
  nodes.Add(new Node(65,new Point(148,636),22));
  nodes.Add(new Node(66,new Point(149,607),39));
  nodes.Add(new Node(67,new Point(155,737),30));
  nodes.Add(new Node(68,new Point(155,849),18));
  nodes.Add(new Node(69,new Point(158,737),31));
  nodes.Add(new Node(70,new Point(159,589),48));
  nodes.Add(new Node(71,new Point(159,690),13));
  nodes.Add(new Node(72,new Point(159,737),32));
  nodes.Add(new Node(73,new Point(162,665),18));
  nodes.Add(new Node(74,new Point(165,795),11));
  nodes.Add(new Node(75,new Point(167,670),20));
  nodes.Add(new Node(76,new Point(168,845),22));
  nodes.Add(new Node(77,new Point(171,991),73));
  nodes.Add(new Node(78,new Point(176,1126),88));
  nodes.Add(new Node(79,new Point(178,1126),89));
  nodes.Add(new Node(80,new Point(180,1126),91));
  nodes.Add(new Node(81,new Point(183,1126),94));
  nodes.Add(new Node(82,new Point(184,822),14));
  nodes.Add(new Node(83,new Point(184,1126),95));
  nodes.Add(new Node(84,new Point(185,1237),103));
  nodes.Add(new Node(85,new Point(189,1278),94));
  nodes.Add(new Node(86,new Point(190,863),20));
  nodes.Add(new Node(87,new Point(191,1026),102));
  nodes.Add(new Node(88,new Point(192,673),23));
  nodes.Add(new Node(89,new Point(192,1027),103));
  nodes.Add(new Node(90,new Point(193,743),42));
  nodes.Add(new Node(91,new Point(199,1031),107));
  nodes.Add(new Node(92,new Point(206,1126),114));
  nodes.Add(new Node(93,new Point(207,462),85));
  nodes.Add(new Node(94,new Point(207,741),48));
  nodes.Add(new Node(95,new Point(209,422),81));
  nodes.Add(new Node(96,new Point(209,423),81));
  nodes.Add(new Node(97,new Point(209,424),80));
  nodes.Add(new Node(98,new Point(210,355),105));
  nodes.Add(new Node(99,new Point(211,1051),112));
  nodes.Add(new Node(100,new Point(211,1058),111));
  nodes.Add(new Node(101,new Point(215,1355),107));
  nodes.Add(new Node(102,new Point(217,889),18));
  nodes.Add(new Node(103,new Point(219,1171),117));
  nodes.Add(new Node(104,new Point(230,843),23));
  nodes.Add(new Node(105,new Point(235,555),89));
  nodes.Add(new Node(106,new Point(235,556),89));
  nodes.Add(new Node(107,new Point(236,515),90));
  nodes.Add(new Node(108,new Point(238,863),16));
  nodes.Add(new Node(109,new Point(239,966),45));
  nodes.Add(new Node(110,new Point(248,609),65));
  nodes.Add(new Node(111,new Point(248,792),60));
  nodes.Add(new Node(112,new Point(249,791),60));
  nodes.Add(new Node(113,new Point(266,772),79));
  nodes.Add(new Node(114,new Point(272,875),13));
  nodes.Add(new Node(115,new Point(278,174),100));
  nodes.Add(new Node(116,new Point(278,175),101));
  nodes.Add(new Node(117,new Point(278,868),15));
  nodes.Add(new Node(118,new Point(279,176),102));
  nodes.Add(new Node(119,new Point(280,268),143));
  nodes.Add(new Node(120,new Point(284,937),22));
  nodes.Add(new Node(121,new Point(287,658),60));
  nodes.Add(new Node(122,new Point(289,769),89));
  nodes.Add(new Node(123,new Point(293,1180),75));
  nodes.Add(new Node(124,new Point(305,904),17));
  nodes.Add(new Node(125,new Point(309,245),159));
  nodes.Add(new Node(126,new Point(311,244),160));
  nodes.Add(new Node(127,new Point(312,244),161));
  nodes.Add(new Node(128,new Point(319,240),164));
  nodes.Add(new Node(129,new Point(320,756),103));
  nodes.Add(new Node(130,new Point(329,1514),237));
  nodes.Add(new Node(131,new Point(331,238),164));
  nodes.Add(new Node(132,new Point(337,998),53));
  nodes.Add(new Node(133,new Point(341,484),55));
  nodes.Add(new Node(134,new Point(341,998),54));
  nodes.Add(new Node(135,new Point(347,771),97));
  nodes.Add(new Node(136,new Point(347,1076),11));
  nodes.Add(new Node(137,new Point(356,74),40));
  nodes.Add(new Node(138,new Point(357,1146),50));
  nodes.Add(new Node(139,new Point(358,1146),50));
  nodes.Add(new Node(140,new Point(361,1002),49));
  nodes.Add(new Node(141,new Point(363,1070),9));
  nodes.Add(new Node(142,new Point(375,1049),16));
  nodes.Add(new Node(143,new Point(375,1050),16));
  nodes.Add(new Node(144,new Point(375,1051),15));
  nodes.Add(new Node(145,new Point(376,1050),15));
  nodes.Add(new Node(146,new Point(385,1290),75));
  nodes.Add(new Node(147,new Point(386,1082),7));
  nodes.Add(new Node(148,new Point(386,1180),23));
  nodes.Add(new Node(149,new Point(392,499),62));
  nodes.Add(new Node(150,new Point(393,1193),14));
  nodes.Add(new Node(151,new Point(399,522),46));
  nodes.Add(new Node(152,new Point(404,165),128));
  nodes.Add(new Node(153,new Point(413,986),42));
  nodes.Add(new Node(154,new Point(415,845),91));
  nodes.Add(new Node(155,new Point(415,1116),38));
  nodes.Add(new Node(156,new Point(421,1356),141));
  nodes.Add(new Node(157,new Point(422,647),11));
  nodes.Add(new Node(158,new Point(422,1068),18));
  nodes.Add(new Node(159,new Point(426,1186),11));
  nodes.Add(new Node(160,new Point(430,607),23));
  nodes.Add(new Node(161,new Point(430,1080),30));
  nodes.Add(new Node(162,new Point(431,1111),44));
  nodes.Add(new Node(163,new Point(435,1200),13));
  nodes.Add(new Node(164,new Point(436,1480),244));
  nodes.Add(new Node(165,new Point(443,593),29));
  nodes.Add(new Node(166,new Point(443,594),30));
  nodes.Add(new Node(167,new Point(443,999),32));
  nodes.Add(new Node(168,new Point(444,599),32));
  nodes.Add(new Node(169,new Point(451,860),97));
  nodes.Add(new Node(170,new Point(451,861),97));
  nodes.Add(new Node(171,new Point(451,1441),222));
  nodes.Add(new Node(172,new Point(454,493),31));
  nodes.Add(new Node(173,new Point(457,995),27));
  nodes.Add(new Node(174,new Point(457,1130),38));
  nodes.Add(new Node(175,new Point(458,995),27));
  nodes.Add(new Node(176,new Point(459,995),27));
  nodes.Add(new Node(177,new Point(468,499),22));
  nodes.Add(new Node(178,new Point(472,679),37));
  nodes.Add(new Node(179,new Point(475,845),83));
  nodes.Add(new Node(180,new Point(487,1160),26));
  nodes.Add(new Node(181,new Point(488,1161),26));
  nodes.Add(new Node(182,new Point(493,1173),30));
  nodes.Add(new Node(183,new Point(494,1057),14));
  nodes.Add(new Node(184,new Point(494,1174),30));
  nodes.Add(new Node(185,new Point(498,360),97));
  nodes.Add(new Node(186,new Point(502,1010),27));
  nodes.Add(new Node(187,new Point(507,377),90));
  nodes.Add(new Node(188,new Point(516,513),20));
  nodes.Add(new Node(189,new Point(516,529),19));
  nodes.Add(new Node(190,new Point(519,697),67));
  nodes.Add(new Node(191,new Point(521,107),146));
  nodes.Add(new Node(192,new Point(526,938),6));
  nodes.Add(new Node(193,new Point(536,987),12));
  nodes.Add(new Node(194,new Point(537,612),56));
  nodes.Add(new Node(195,new Point(537,1110),34));
  nodes.Add(new Node(196,new Point(538,1110),34));
  nodes.Add(new Node(197,new Point(546,1214),67));
  nodes.Add(new Node(198,new Point(553,298),58));
  nodes.Add(new Node(199,new Point(558,1062),48));
  nodes.Add(new Node(200,new Point(561,259),52));
  nodes.Add(new Node(201,new Point(562,1023),52));
  nodes.Add(new Node(202,new Point(563,917),3));
  nodes.Add(new Node(203,new Point(564,1020),52));
  nodes.Add(new Node(204,new Point(574,847),3));
  nodes.Add(new Node(205,new Point(574,896),17));
  nodes.Add(new Node(206,new Point(578,454),64));
  nodes.Add(new Node(207,new Point(586,814),2));
  nodes.Add(new Node(208,new Point(593,996),56));
  nodes.Add(new Node(209,new Point(595,2),187));
  nodes.Add(new Node(210,new Point(604,470),59));
  nodes.Add(new Node(211,new Point(607,263),12));
  nodes.Add(new Node(212,new Point(607,990),57));
  nodes.Add(new Node(213,new Point(608,907),32));
  nodes.Add(new Node(214,new Point(609,821),2));
  nodes.Add(new Node(215,new Point(613,777),5));
  nodes.Add(new Node(216,new Point(618,655),8));
  nodes.Add(new Node(217,new Point(619,657),8));
  nodes.Add(new Node(218,new Point(621,798),2));
  nodes.Add(new Node(219,new Point(623,706),2));
  nodes.Add(new Node(220,new Point(629,703),3));
  nodes.Add(new Node(221,new Point(630,238),7));
  nodes.Add(new Node(222,new Point(633,660),3));
  nodes.Add(new Node(223,new Point(640,282),19));
  nodes.Add(new Node(224,new Point(641,677),4));
  nodes.Add(new Node(225,new Point(642,683),3));
  nodes.Add(new Node(226,new Point(643,717),3));
  nodes.Add(new Node(227,new Point(648,864),54));
  nodes.Add(new Node(228,new Point(650,1926),257));
  nodes.Add(new Node(229,new Point(651,629),18));
  nodes.Add(new Node(230,new Point(652,628),19));
  nodes.Add(new Node(231,new Point(652,722),2));
  nodes.Add(new Node(232,new Point(657,737),6));
  nodes.Add(new Node(233,new Point(658,245),11));
  nodes.Add(new Node(234,new Point(660,708),2));
  nodes.Add(new Node(235,new Point(660,1889),241));
  nodes.Add(new Node(236,new Point(663,589),47));
  nodes.Add(new Node(237,new Point(667,161),23));
  nodes.Add(new Node(238,new Point(669,608),42));
  nodes.Add(new Node(239,new Point(669,666),10));
  nodes.Add(new Node(240,new Point(669,667),10));
  nodes.Add(new Node(241,new Point(670,265),30));
  nodes.Add(new Node(242,new Point(670,601),47));
  nodes.Add(new Node(243,new Point(670,694),3));
  nodes.Add(new Node(244,new Point(670,995),65));
  nodes.Add(new Node(245,new Point(672,724),3));
  nodes.Add(new Node(246,new Point(673,208),13));
  nodes.Add(new Node(247,new Point(674,854),70));
  nodes.Add(new Node(248,new Point(675,645),16));
  nodes.Add(new Node(249,new Point(676,853),71));
  nodes.Add(new Node(250,new Point(676,1761),214));
  nodes.Add(new Node(251,new Point(677,853),72));
  nodes.Add(new Node(252,new Point(678,736),11));
  nodes.Add(new Node(253,new Point(679,852),73));
  nodes.Add(new Node(254,new Point(680,706),3));
  nodes.Add(new Node(255,new Point(680,852),74));
  nodes.Add(new Node(256,new Point(684,851),77));
  nodes.Add(new Node(257,new Point(686,805),57));
  nodes.Add(new Node(258,new Point(689,808),61));
  nodes.Add(new Node(259,new Point(690,809),62));
  nodes.Add(new Node(260,new Point(690,847),80));
  nodes.Add(new Node(261,new Point(691,809),63));
  nodes.Add(new Node(262,new Point(692,810),64));
  nodes.Add(new Node(263,new Point(693,810),65));
  nodes.Add(new Node(264,new Point(694,811),66));
  nodes.Add(new Node(265,new Point(695,811),67));
  nodes.Add(new Node(266,new Point(695,812),68));
  nodes.Add(new Node(267,new Point(695,813),68));
  nodes.Add(new Node(268,new Point(696,813),69));
  nodes.Add(new Node(269,new Point(697,814),70));
  nodes.Add(new Node(270,new Point(698,814),71));
  nodes.Add(new Node(271,new Point(699,815),72));
  nodes.Add(new Node(272,new Point(700,815),73));
  nodes.Add(new Node(273,new Point(703,818),77));
  nodes.Add(new Node(274,new Point(704,818),78));
  nodes.Add(new Node(275,new Point(705,819),79));
  nodes.Add(new Node(276,new Point(707,266),52));
  nodes.Add(new Node(277,new Point(709,822),84));
  nodes.Add(new Node(278,new Point(711,823),86));
  nodes.Add(new Node(279,new Point(714,245),46));
  nodes.Add(new Node(280,new Point(717,828),94));
  nodes.Add(new Node(281,new Point(730,611),43));
  nodes.Add(new Node(282,new Point(741,375),93));
  nodes.Add(new Node(283,new Point(747,343),83));
  nodes.Add(new Node(284,new Point(748,383),89));
  nodes.Add(new Node(285,new Point(752,573),23));
  nodes.Add(new Node(286,new Point(754,534),24));
  nodes.Add(new Node(287,new Point(759,461),55));
  nodes.Add(new Node(288,new Point(765,431),69));
  nodes.Add(new Node(289,new Point(766,327),66));
  nodes.Add(new Node(290,new Point(767,326),65));
  nodes.Add(new Node(291,new Point(782,1939),137));
  nodes.Add(new Node(292,new Point(783,1939),136));
  nodes.Add(new Node(293,new Point(784,1938),135));
  nodes.Add(new Node(294,new Point(785,1939),135));
  nodes.Add(new Node(295,new Point(786,1939),134));
  nodes.Add(new Node(296,new Point(788,1939),132));
  nodes.Add(new Node(297,new Point(790,1939),130));
  nodes.Add(new Node(298,new Point(795,804),141));
  nodes.Add(new Node(299,new Point(796,804),141));
  nodes.Add(new Node(300,new Point(798,803),143));
  nodes.Add(new Node(301,new Point(801,1940),121));
  nodes.Add(new Node(302,new Point(802,1940),120));
  nodes.Add(new Node(303,new Point(804,1940),118));
  nodes.Add(new Node(304,new Point(805,541),24));
  nodes.Add(new Node(305,new Point(805,1940),117));
  nodes.Add(new Node(306,new Point(807,1941),116));
  nodes.Add(new Node(307,new Point(812,801),153));
  nodes.Add(new Node(308,new Point(812,1941),111));
  nodes.Add(new Node(309,new Point(816,524),16));
  nodes.Add(new Node(310,new Point(818,581),32));
  nodes.Add(new Node(311,new Point(833,374),24));
  nodes.Add(new Node(312,new Point(834,373),23));
  nodes.Add(new Node(313,new Point(845,455),17));
  nodes.Add(new Node(314,new Point(855,503),19));
  nodes.Add(new Node(315,new Point(860,1947),76));
  nodes.Add(new Node(316,new Point(861,508),19));
  nodes.Add(new Node(317,new Point(861,521),13));
  nodes.Add(new Node(318,new Point(861,1948),76));
  nodes.Add(new Node(319,new Point(862,582),40));
  nodes.Add(new Node(320,new Point(873,824),181));
  nodes.Add(new Node(321,new Point(882,954),162));
  nodes.Add(new Node(322,new Point(892,811),170));
  nodes.Add(new Node(323,new Point(893,810),169));
  nodes.Add(new Node(324,new Point(894,514),9));
  nodes.Add(new Node(325,new Point(900,810),162));
  nodes.Add(new Node(326,new Point(901,810),161));
  nodes.Add(new Node(327,new Point(904,810),159));
  nodes.Add(new Node(328,new Point(909,1843),8));
  nodes.Add(new Node(329,new Point(917,288),61));
  nodes.Add(new Node(330,new Point(919,291),62));
  nodes.Add(new Node(331,new Point(920,292),63));
  nodes.Add(new Node(332,new Point(924,1817),11));
  nodes.Add(new Node(333,new Point(927,418),73));
  nodes.Add(new Node(334,new Point(938,369),73));
  nodes.Add(new Node(335,new Point(938,370),73));
  nodes.Add(new Node(336,new Point(938,1817),9));
  nodes.Add(new Node(337,new Point(940,328),85));
  nodes.Add(new Node(338,new Point(940,362),76));
  nodes.Add(new Node(339,new Point(945,1770),24));
  nodes.Add(new Node(340,new Point(948,334),89));
  nodes.Add(new Node(341,new Point(958,1873),24));
  nodes.Add(new Node(342,new Point(960,1878),26));
  nodes.Add(new Node(343,new Point(966,1041),125));
  nodes.Add(new Node(344,new Point(970,1993),69));
  nodes.Add(new Node(345,new Point(980,487),70));
  nodes.Add(new Node(346,new Point(981,1801),28));
  nodes.Add(new Node(347,new Point(990,1862),10));
  nodes.Add(new Node(348,new Point(998,567),88));
  nodes.Add(new Node(349,new Point(998,600),101));
  nodes.Add(new Node(350,new Point(999,618),100));
  nodes.Add(new Node(351,new Point(1004,584),99));
  nodes.Add(new Node(352,new Point(1004,1809),35));
  nodes.Add(new Node(353,new Point(1014,2037),108));
  nodes.Add(new Node(354,new Point(1029,1820),32));
  nodes.Add(new Node(355,new Point(1030,1821),32));
  nodes.Add(new Node(356,new Point(1052,1210),45));
  nodes.Add(new Node(357,new Point(1053,1777),8));
  nodes.Add(new Node(358,new Point(1053,1840),44));
  nodes.Add(new Node(359,new Point(1057,653),55));
  nodes.Add(new Node(360,new Point(1060,1053),97));
  nodes.Add(new Node(361,new Point(1073,1214),43));
  nodes.Add(new Node(362,new Point(1083,685),43));
  nodes.Add(new Node(363,new Point(1088,1186),22));
  nodes.Add(new Node(364,new Point(1088,1187),22));
  nodes.Add(new Node(365,new Point(1093,459),34));
  nodes.Add(new Node(366,new Point(1094,460),35));
  nodes.Add(new Node(367,new Point(1096,255),134));
  nodes.Add(new Node(368,new Point(1108,1755),5));
  nodes.Add(new Node(369,new Point(1109,1057),100));
  nodes.Add(new Node(370,new Point(1112,482),46));
  nodes.Add(new Node(371,new Point(1121,1864),94));
  nodes.Add(new Node(372,new Point(1122,1865),95));
  nodes.Add(new Node(373,new Point(1123,1866),96));
  nodes.Add(new Node(374,new Point(1124,408),11));
  nodes.Add(new Node(375,new Point(1124,1866),97));
  nodes.Add(new Node(376,new Point(1132,408),10));
  nodes.Add(new Node(377,new Point(1139,773),56));
  nodes.Add(new Node(378,new Point(1139,1874),109));
  nodes.Add(new Node(379,new Point(1142,765),55));
  nodes.Add(new Node(380,new Point(1145,1884),115));
  nodes.Add(new Node(381,new Point(1150,1794),46));
  nodes.Add(new Node(382,new Point(1150,1795),47));
  nodes.Add(new Node(383,new Point(1150,1796),47));
  nodes.Add(new Node(384,new Point(1151,742),54));
  nodes.Add(new Node(385,new Point(1154,1785),40));
  nodes.Add(new Node(386,new Point(1156,1116),66));
  nodes.Add(new Node(387,new Point(1156,1118),64));
  nodes.Add(new Node(388,new Point(1156,1119),64));
  nodes.Add(new Node(389,new Point(1159,1064),108));
  nodes.Add(new Node(390,new Point(1160,1617),5));
  nodes.Add(new Node(391,new Point(1168,164),219));
  nodes.Add(new Node(392,new Point(1168,1233),63));
  nodes.Add(new Node(393,new Point(1170,1485),13));
  nodes.Add(new Node(394,new Point(1173,728),43));
  nodes.Add(new Node(395,new Point(1194,602),72));
  nodes.Add(new Node(396,new Point(1198,124),261));
  nodes.Add(new Node(397,new Point(1198,1255),71));
  nodes.Add(new Node(398,new Point(1202,1586),37));
  nodes.Add(new Node(399,new Point(1211,1574),44));
  nodes.Add(new Node(400,new Point(1212,1518),52));
  nodes.Add(new Node(401,new Point(1212,1542),54));
  nodes.Add(new Node(402,new Point(1212,1544),55));
  nodes.Add(new Node(403,new Point(1217,1497),52));
  nodes.Add(new Node(404,new Point(1217,1718),46));
  nodes.Add(new Node(405,new Point(1218,1495),52));
  nodes.Add(new Node(406,new Point(1218,1496),53));
  nodes.Add(new Node(407,new Point(1227,1707),42));
  nodes.Add(new Node(408,new Point(1234,1628),14));
  nodes.Add(new Node(409,new Point(1238,1417),71));
  nodes.Add(new Node(410,new Point(1241,829),35));
  nodes.Add(new Node(411,new Point(1242,1360),65));
  nodes.Add(new Node(412,new Point(1245,1428),70));
  nodes.Add(new Node(413,new Point(1245,1821),27));
  nodes.Add(new Node(414,new Point(1251,1225),57));
  nodes.Add(new Node(415,new Point(1252,828),33));
  nodes.Add(new Node(416,new Point(1260,220),192));
  nodes.Add(new Node(417,new Point(1267,1130),72));
  nodes.Add(new Node(418,new Point(1268,1183),56));
  nodes.Add(new Node(419,new Point(1270,582),118));
  nodes.Add(new Node(420,new Point(1275,790),23));
  nodes.Add(new Node(421,new Point(1283,437),111));
  nodes.Add(new Node(422,new Point(1284,528),110));
  nodes.Add(new Node(423,new Point(1284,530),110));
  nodes.Add(new Node(424,new Point(1284,531),110));
  nodes.Add(new Node(425,new Point(1284,1342),44));
  nodes.Add(new Node(426,new Point(1286,609),110));
  nodes.Add(new Node(427,new Point(1287,515),110));
  nodes.Add(new Node(428,new Point(1289,1338),42));
  nodes.Add(new Node(429,new Point(1289,1778),15));
  nodes.Add(new Node(430,new Point(1290,1777),15));
  nodes.Add(new Node(431,new Point(1295,331),140));
  nodes.Add(new Node(432,new Point(1295,350),133));
  nodes.Add(new Node(433,new Point(1300,1754),24));
  nodes.Add(new Node(434,new Point(1301,1753),25));
  nodes.Add(new Node(435,new Point(1305,1712),35));
  nodes.Add(new Node(436,new Point(1306,1649),32));
  nodes.Add(new Node(437,new Point(1307,1259),28));
  nodes.Add(new Node(438,new Point(1309,1337),29));
  nodes.Add(new Node(439,new Point(1316,790),34));
  nodes.Add(new Node(440,new Point(1316,1426),35));
  nodes.Add(new Node(441,new Point(1317,921),61));
  nodes.Add(new Node(442,new Point(1318,1384),5));
  nodes.Add(new Node(443,new Point(1319,2017),32));
  nodes.Add(new Node(444,new Point(1333,870),62));
  nodes.Add(new Node(445,new Point(1333,1260),30));
  nodes.Add(new Node(446,new Point(1337,888),66));
  nodes.Add(new Node(447,new Point(1338,1930),42));
  nodes.Add(new Node(448,new Point(1341,1363),3));
  nodes.Add(new Node(449,new Point(1345,1921),45));
  nodes.Add(new Node(450,new Point(1347,1966),14));
  nodes.Add(new Node(451,new Point(1350,1319),28));
  nodes.Add(new Node(452,new Point(1351,1007),41));
  nodes.Add(new Node(453,new Point(1351,1561),36));
  nodes.Add(new Node(454,new Point(1352,1214),27));
  nodes.Add(new Node(455,new Point(1353,890),54));
  nodes.Add(new Node(456,new Point(1360,1223),35));
  nodes.Add(new Node(457,new Point(1369,1361),23));
  nodes.Add(new Node(458,new Point(1369,1836),59));
  nodes.Add(new Node(459,new Point(1374,1601),28));
  nodes.Add(new Node(460,new Point(1375,1407),39));
  nodes.Add(new Node(461,new Point(1377,1070),32));
  nodes.Add(new Node(462,new Point(1390,640),53));
  nodes.Add(new Node(463,new Point(1397,1792),47));
  nodes.Add(new Node(464,new Point(1400,33),130));
  nodes.Add(new Node(465,new Point(1401,34),129));
  nodes.Add(new Node(466,new Point(1402,34),128));
  nodes.Add(new Node(467,new Point(1403,34),127));
  nodes.Add(new Node(468,new Point(1403,35),127));
  nodes.Add(new Node(469,new Point(1405,35),125));
  nodes.Add(new Node(470,new Point(1406,35),124));
  nodes.Add(new Node(471,new Point(1409,1699),38));
  nodes.Add(new Node(472,new Point(1411,1008),25));
  nodes.Add(new Node(473,new Point(1414,246),42));
  nodes.Add(new Node(474,new Point(1415,1648),33));
  nodes.Add(new Node(475,new Point(1416,1322),10));
  nodes.Add(new Node(476,new Point(1419,1041),7));
  nodes.Add(new Node(477,new Point(1419,1309),11));
  nodes.Add(new Node(478,new Point(1421,851),16));
  nodes.Add(new Node(479,new Point(1422,1000),22));
  nodes.Add(new Node(480,new Point(1424,1570),23));
  nodes.Add(new Node(481,new Point(1427,1709),33));
  nodes.Add(new Node(482,new Point(1432,1639),30));
  nodes.Add(new Node(483,new Point(1433,464),37));
  nodes.Add(new Node(484,new Point(1434,464),38));
  nodes.Add(new Node(485,new Point(1438,464),41));
  nodes.Add(new Node(486,new Point(1438,771),60));
  nodes.Add(new Node(487,new Point(1442,770),62));
  nodes.Add(new Node(488,new Point(1445,1117),58));
  nodes.Add(new Node(489,new Point(1445,1773),44));
  nodes.Add(new Node(490,new Point(1449,868),13));
  nodes.Add(new Node(491,new Point(1452,750),55));
  nodes.Add(new Node(492,new Point(1453,1580),32));
  nodes.Add(new Node(493,new Point(1454,1580),32));
  nodes.Add(new Node(494,new Point(1454,1958),76));
  nodes.Add(new Node(495,new Point(1455,1073),34));
  nodes.Add(new Node(496,new Point(1462,864),20));
  nodes.Add(new Node(497,new Point(1462,926),19));
  nodes.Add(new Node(498,new Point(1465,1659),17));
  nodes.Add(new Node(499,new Point(1466,1681),14));
  nodes.Add(new Node(500,new Point(1474,829),25));
  nodes.Add(new Node(501,new Point(1476,1653),14));
  nodes.Add(new Node(502,new Point(1476,1783),49));
  nodes.Add(new Node(503,new Point(1477,1784),49));
  nodes.Add(new Node(504,new Point(1482,655),35));
  nodes.Add(new Node(505,new Point(1482,810),39));
  nodes.Add(new Node(506,new Point(1486,1317),32));
  nodes.Add(new Node(507,new Point(1496,1705),12));
  nodes.Add(new Node(508,new Point(1496,1935),86));
  nodes.Add(new Node(509,new Point(1497,1704),12));
  nodes.Add(new Node(510,new Point(1501,1719),13));
  nodes.Add(new Node(511,new Point(1502,607),54));
  nodes.Add(new Node(512,new Point(1502,1625),13));
  nodes.Add(new Node(513,new Point(1503,528),95));
  nodes.Add(new Node(514,new Point(1503,1919),85));
  nodes.Add(new Node(515,new Point(1504,543),86));
  nodes.Add(new Node(516,new Point(1504,544),85));
  nodes.Add(new Node(517,new Point(1504,545),85));
  nodes.Add(new Node(518,new Point(1504,1917),85));
  nodes.Add(new Node(519,new Point(1504,1918),85));
  nodes.Add(new Node(520,new Point(1510,1881),84));
  nodes.Add(new Node(521,new Point(1513,435),97));
  nodes.Add(new Node(522,new Point(1513,504),106));
  nodes.Add(new Node(523,new Point(1515,305),70));
  nodes.Add(new Node(524,new Point(1515,384),79));
  nodes.Add(new Node(525,new Point(1515,385),80));
  nodes.Add(new Node(526,new Point(1515,386),80));
  nodes.Add(new Node(527,new Point(1515,1357),37));
  nodes.Add(new Node(528,new Point(1515,1956),67));
  nodes.Add(new Node(529,new Point(1516,346),73));
  nodes.Add(new Node(530,new Point(1516,1370),38));
  nodes.Add(new Node(531,new Point(1517,816),23));
  nodes.Add(new Node(532,new Point(1519,470),114));
  nodes.Add(new Node(533,new Point(1519,955),15));
  nodes.Add(new Node(534,new Point(1519,1960),62));
  nodes.Add(new Node(535,new Point(1520,1723),12));
  nodes.Add(new Node(536,new Point(1521,263),62));
  nodes.Add(new Node(537,new Point(1521,287),69));
  nodes.Add(new Node(538,new Point(1521,957),16));
  nodes.Add(new Node(539,new Point(1522,286),69));
  nodes.Add(new Node(540,new Point(1522,476),116));
  nodes.Add(new Node(541,new Point(1522,865),22));
  nodes.Add(new Node(542,new Point(1522,957),17));
  nodes.Add(new Node(543,new Point(1522,1847),83));
  nodes.Add(new Node(544,new Point(1522,1965),60));
  nodes.Add(new Node(545,new Point(1525,1736),15));
  nodes.Add(new Node(546,new Point(1528,803),9));
  nodes.Add(new Node(547,new Point(1529,802),8));
  nodes.Add(new Node(548,new Point(1531,741),24));
  nodes.Add(new Node(549,new Point(1534,845),4));
  nodes.Add(new Node(550,new Point(1534,916),14));
  nodes.Add(new Node(551,new Point(1535,904),11));
  nodes.Add(new Node(552,new Point(1535,1641),31));
  nodes.Add(new Node(553,new Point(1536,902),12));
  nodes.Add(new Node(554,new Point(1536,903),12));
  nodes.Add(new Node(555,new Point(1536,1681),25));
  nodes.Add(new Node(556,new Point(1536,1682),25));
  nodes.Add(new Node(557,new Point(1537,1650),35));
  nodes.Add(new Node(558,new Point(1540,884),19));
  nodes.Add(new Node(559,new Point(1547,760),33));
  nodes.Add(new Node(560,new Point(1549,158),53));
  nodes.Add(new Node(561,new Point(1551,1665),39));
  nodes.Add(new Node(562,new Point(1553,1412),16));
  nodes.Add(new Node(563,new Point(1553,1515),32));
  nodes.Add(new Node(564,new Point(1554,1506),31));
  nodes.Add(new Node(565,new Point(1556,1520),30));
  nodes.Add(new Node(566,new Point(1560,1534),23));
  nodes.Add(new Node(567,new Point(1563,1441),22));
  nodes.Add(new Node(568,new Point(1563,1578),31));
  nodes.Add(new Node(569,new Point(1566,765),39));
  nodes.Add(new Node(570,new Point(1567,987),16));
  nodes.Add(new Node(571,new Point(1569,680),39));
  nodes.Add(new Node(572,new Point(1570,851),24));
  nodes.Add(new Node(573,new Point(1573,678),39));
  nodes.Add(new Node(574,new Point(1573,955),17));
  nodes.Add(new Node(575,new Point(1577,1265),50));
  nodes.Add(new Node(576,new Point(1578,1748),42));
  nodes.Add(new Node(577,new Point(1579,677),39));
  nodes.Add(new Node(578,new Point(1579,1392),22));
  nodes.Add(new Node(579,new Point(1581,1327),29));
  nodes.Add(new Node(580,new Point(1581,1391),23));
  nodes.Add(new Node(581,new Point(1582,1444),21));
  nodes.Add(new Node(582,new Point(1584,1588),29));
  nodes.Add(new Node(583,new Point(1587,121),59));
  nodes.Add(new Node(584,new Point(1588,120),59));
  nodes.Add(new Node(585,new Point(1588,339),10));
  nodes.Add(new Node(586,new Point(1589,119),60));
  nodes.Add(new Node(587,new Point(1589,1327),36));
  nodes.Add(new Node(588,new Point(1589,1469),18));
  nodes.Add(new Node(589,new Point(1592,1543),8));
  nodes.Add(new Node(590,new Point(1593,1676),53));
  nodes.Add(new Node(591,new Point(1594,106),63));
  nodes.Add(new Node(592,new Point(1595,781),50));
  nodes.Add(new Node(593,new Point(1595,1518),10));
  nodes.Add(new Node(594,new Point(1596,104),65));
  nodes.Add(new Node(595,new Point(1596,105),65));
  nodes.Add(new Node(596,new Point(1596,1728),46));
  nodes.Add(new Node(597,new Point(1598,623),26));
  nodes.Add(new Node(598,new Point(1598,1579),21));
  nodes.Add(new Node(599,new Point(1598,1671),51));
  nodes.Add(new Node(600,new Point(1601,1545),7));
  nodes.Add(new Node(601,new Point(1602,818),53));
  nodes.Add(new Node(602,new Point(1603,1342),38));
  nodes.Add(new Node(603,new Point(1604,1577),16));
  nodes.Add(new Node(604,new Point(1607,1370),40));
  nodes.Add(new Node(605,new Point(1607,1374),40));
  nodes.Add(new Node(606,new Point(1608,831),57));
  nodes.Add(new Node(607,new Point(1611,55),77));
  nodes.Add(new Node(608,new Point(1612,1468),7));
  nodes.Add(new Node(609,new Point(1614,925),51));
  nodes.Add(new Node(610,new Point(1618,360),13));
  nodes.Add(new Node(611,new Point(1620,384),15));
  nodes.Add(new Node(612,new Point(1621,119),39));
  nodes.Add(new Node(613,new Point(1622,2012),36));
  nodes.Add(new Node(614,new Point(1623,120),37));
  nodes.Add(new Node(615,new Point(1623,2013),37));
  nodes.Add(new Node(616,new Point(1625,1950),32));
  nodes.Add(new Node(617,new Point(1626,1439),12));
  nodes.Add(new Node(618,new Point(1626,1443),14));
  nodes.Add(new Node(619,new Point(1627,294),35));
  nodes.Add(new Node(620,new Point(1627,696),43));
  nodes.Add(new Node(621,new Point(1628,748),36));
  nodes.Add(new Node(622,new Point(1629,854),58));
  nodes.Add(new Node(623,new Point(1629,1742),30));
  nodes.Add(new Node(624,new Point(1630,855),58));
  nodes.Add(new Node(625,new Point(1630,1415),20));
  nodes.Add(new Node(626,new Point(1631,312),36));
  nodes.Add(new Node(627,new Point(1632,313),36));
  nodes.Add(new Node(628,new Point(1632,314),36));
  nodes.Add(new Node(629,new Point(1632,1501),24));
  nodes.Add(new Node(630,new Point(1635,337),30));
  nodes.Add(new Node(631,new Point(1637,463),78));
  nodes.Add(new Node(632,new Point(1638,1472),15));
  nodes.Add(new Node(633,new Point(1639,324),39));
  nodes.Add(new Node(634,new Point(1639,1929),41));
  nodes.Add(new Node(635,new Point(1640,1462),16));
  nodes.Add(new Node(636,new Point(1643,1513),25));
  nodes.Add(new Node(637,new Point(1643,1566),4));
  nodes.Add(new Node(638,new Point(1644,1645),44));
  nodes.Add(new Node(639,new Point(1644,1737),20));
  nodes.Add(new Node(640,new Point(1646,1534),29));
  nodes.Add(new Node(641,new Point(1646,1788),23));
  nodes.Add(new Node(642,new Point(1647,1316),16));
  nodes.Add(new Node(643,new Point(1649,276),17));
  nodes.Add(new Node(644,new Point(1656,1934),33));
  nodes.Add(new Node(645,new Point(1657,1586),19));
  nodes.Add(new Node(646,new Point(1661,1563),20));
  nodes.Add(new Node(647,new Point(1662,1847),38));
  nodes.Add(new Node(648,new Point(1663,1548),26));
  nodes.Add(new Node(649,new Point(1664,918),91));
  nodes.Add(new Node(650,new Point(1666,466),84));
  nodes.Add(new Node(651,new Point(1671,814),8));
  nodes.Add(new Node(652,new Point(1672,813),8));
  nodes.Add(new Node(653,new Point(1675,332),36));
  nodes.Add(new Node(654,new Point(1675,665),47));
  nodes.Add(new Node(655,new Point(1675,1926),21));
  nodes.Add(new Node(656,new Point(1677,414),42));
  nodes.Add(new Node(657,new Point(1678,413),42));
  nodes.Add(new Node(658,new Point(1679,1342),25));
  nodes.Add(new Node(659,new Point(1679,1357),30));
  nodes.Add(new Node(660,new Point(1680,325),30));
  nodes.Add(new Node(661,new Point(1680,341),33));
  nodes.Add(new Node(662,new Point(1681,279),4));
  nodes.Add(new Node(663,new Point(1683,307),17));
  nodes.Add(new Node(664,new Point(1683,1361),29));
  nodes.Add(new Node(665,new Point(1684,1416),25));
  nodes.Add(new Node(666,new Point(1684,1491),17));
  nodes.Add(new Node(667,new Point(1686,1448),25));
  nodes.Add(new Node(668,new Point(1691,750),30));
  nodes.Add(new Node(669,new Point(1691,937),106));
  nodes.Add(new Node(670,new Point(1692,1979),31));
  nodes.Add(new Node(671,new Point(1694,1433),33));
  nodes.Add(new Node(672,new Point(1699,814),16));
  nodes.Add(new Node(673,new Point(1703,221),34));
  nodes.Add(new Node(674,new Point(1703,1429),28));
  nodes.Add(new Node(675,new Point(1703,1976),28));
  nodes.Add(new Node(676,new Point(1708,625),78));
  nodes.Add(new Node(677,new Point(1708,1924),15));
  nodes.Add(new Node(678,new Point(1710,595),92));
  nodes.Add(new Node(679,new Point(1715,1847),51));
  nodes.Add(new Node(680,new Point(1716,1529),34));
  nodes.Add(new Node(681,new Point(1719,1298),39));
  nodes.Add(new Node(682,new Point(1719,1359),17));
  nodes.Add(new Node(683,new Point(1720,766),44));
  nodes.Add(new Node(684,new Point(1720,783),33));
  nodes.Add(new Node(685,new Point(1721,762),46));
  nodes.Add(new Node(686,new Point(1721,765),45));
  nodes.Add(new Node(687,new Point(1726,85),45));
  nodes.Add(new Node(688,new Point(1726,86),44));
  nodes.Add(new Node(689,new Point(1728,257),37));
  nodes.Add(new Node(690,new Point(1728,1363),15));
  nodes.Add(new Node(691,new Point(1730,757),47));
  nodes.Add(new Node(692,new Point(1731,362),23));
  nodes.Add(new Node(693,new Point(1732,259),38));
  nodes.Add(new Node(694,new Point(1733,1379),15));
  nodes.Add(new Node(695,new Point(1737,980),146));
  nodes.Add(new Node(696,new Point(1738,261),38));
  nodes.Add(new Node(697,new Point(1739,982),148));
  nodes.Add(new Node(698,new Point(1741,983),150));
  nodes.Add(new Node(699,new Point(1742,984),150));
  nodes.Add(new Node(700,new Point(1744,985),152));
  nodes.Add(new Node(701,new Point(1746,1666),85));
  nodes.Add(new Node(702,new Point(1747,282),32));
  nodes.Add(new Node(703,new Point(1747,541),119));
  nodes.Add(new Node(704,new Point(1747,1667),86));
  nodes.Add(new Node(705,new Point(1748,537),120));
  nodes.Add(new Node(706,new Point(1751,319),34));
  nodes.Add(new Node(707,new Point(1754,172),19));
  nodes.Add(new Node(708,new Point(1759,191),12));
  nodes.Add(new Node(709,new Point(1763,219),11));
  nodes.Add(new Node(710,new Point(1764,724),55));
  nodes.Add(new Node(711,new Point(1765,1674),101));
  nodes.Add(new Node(712,new Point(1766,348),43));
  nodes.Add(new Node(713,new Point(1773,1106),216));
  nodes.Add(new Node(714,new Point(1780,1400),35));
  nodes.Add(new Node(715,new Point(1783,1409),40));
  nodes.Add(new Node(716,new Point(1783,1738),106));
  nodes.Add(new Node(717,new Point(1785,1425),44));
  nodes.Add(new Node(718,new Point(1786,1105),227));
  nodes.Add(new Node(719,new Point(1787,1104),227));
  nodes.Add(new Node(720,new Point(1791,1103),230));
  nodes.Add(new Node(721,new Point(1792,1437),45));
  nodes.Add(new Node(722,new Point(1797,216),23));
  nodes.Add(new Node(723,new Point(1797,1037),211));
  nodes.Add(new Node(724,new Point(1801,155),27));
  nodes.Add(new Node(725,new Point(1807,1539),90));
  nodes.Add(new Node(726,new Point(1807,1547),94));
  nodes.Add(new Node(727,new Point(1808,1286),85));
  nodes.Add(new Node(728,new Point(1809,1127),220));
  nodes.Add(new Node(729,new Point(1809,1283),88));
  nodes.Add(new Node(730,new Point(1810,522),123));
  nodes.Add(new Node(731,new Point(1810,1127),220));
  nodes.Add(new Node(732,new Point(1810,1281),90));
  nodes.Add(new Node(733,new Point(1811,1279),92));
  nodes.Add(new Node(734,new Point(1812,260),22));
  nodes.Add(new Node(735,new Point(1814,221),32));
  nodes.Add(new Node(736,new Point(1816,1838),127));
  nodes.Add(new Node(737,new Point(1825,1582),106));
  nodes.Add(new Node(738,new Point(1826,1583),106));
  nodes.Add(new Node(739,new Point(1830,371),73));
  nodes.Add(new Node(740,new Point(1831,211),36));
  nodes.Add(new Node(741,new Point(1838,1635),130));
  nodes.Add(new Node(742,new Point(1838,1636),130));
  nodes.Add(new Node(743,new Point(1838,1637),131));
  nodes.Add(new Node(744,new Point(1838,1695),163));
  nodes.Add(new Node(745,new Point(1841,1157),206));
  nodes.Add(new Node(746,new Point(1842,211),36));
  nodes.Add(new Node(747,new Point(1849,1195),179));
  nodes.Add(new Node(748,new Point(1850,1720),173));
  nodes.Add(new Node(749,new Point(1856,340),72));
  nodes.Add(new Node(750,new Point(1857,107),62));
  nodes.Add(new Node(751,new Point(1857,199),33));
  nodes.Add(new Node(752,new Point(1857,339),71));
  nodes.Add(new Node(753,new Point(1859,1376),34));
  nodes.Add(new Node(754,new Point(1866,43),97));
  nodes.Add(new Node(755,new Point(1867,73),81));
  nodes.Add(new Node(756,new Point(1868,175),34));
  nodes.Add(new Node(757,new Point(1874,301),43));
  nodes.Add(new Node(758,new Point(1885,742),115));
  nodes.Add(new Node(759,new Point(1886,743),116));
  nodes.Add(new Node(760,new Point(1887,742),116));
  nodes.Add(new Node(761,new Point(1888,742),117));
  nodes.Add(new Node(762,new Point(1889,742),118));
  nodes.Add(new Node(763,new Point(1891,743),120));
  nodes.Add(new Node(764,new Point(1894,744),123));
  nodes.Add(new Node(765,new Point(1895,744),124));
  nodes.Add(new Node(766,new Point(1909,185),16));
  nodes.Add(new Node(767,new Point(1913,1326),102));
  nodes.Add(new Node(768,new Point(1915,1333),100));
  nodes.Add(new Node(769,new Point(1915,1334),99));
  nodes.Add(new Node(770,new Point(1917,215),15));
  nodes.Add(new Node(771,new Point(1918,207),15));
  nodes.Add(new Node(772,new Point(1919,1492),11));
  nodes.Add(new Node(773,new Point(1920,1808),225));
  nodes.Add(new Node(774,new Point(1921,352),43));
  nodes.Add(new Node(775,new Point(1927,875),171));
  nodes.Add(new Node(776,new Point(1929,873),172));
  nodes.Add(new Node(777,new Point(1930,872),173));
  nodes.Add(new Node(778,new Point(1931,262),14));
  nodes.Add(new Node(779,new Point(1931,871),173));
  nodes.Add(new Node(780,new Point(1932,261),14));
  nodes.Add(new Node(781,new Point(1932,870),174));
  nodes.Add(new Node(782,new Point(1937,1375),90));
  nodes.Add(new Node(783,new Point(1941,173),13));
  nodes.Add(new Node(784,new Point(1941,245),18));
  nodes.Add(new Node(785,new Point(1941,1379),88));
  nodes.Add(new Node(786,new Point(1942,172),14));
  nodes.Add(new Node(787,new Point(1944,168),16));
  nodes.Add(new Node(788,new Point(1952,596),156));
  nodes.Add(new Node(789,new Point(1961,1466),29));
  nodes.Add(new Node(790,new Point(1963,1396),79));
  nodes.Add(new Node(791,new Point(1964,387),41));
  nodes.Add(new Node(792,new Point(1968,164),16));
  nodes.Add(new Node(793,new Point(1968,408),40));
  nodes.Add(new Node(794,new Point(1970,321),14));
  nodes.Add(new Node(795,new Point(1973,275),16));
  nodes.Add(new Node(796,new Point(1974,1523),37));
  nodes.Add(new Node(797,new Point(1975,1531),39));
  nodes.Add(new Node(798,new Point(1985,279),20));
  nodes.Add(new Node(799,new Point(1985,1539),36));
  nodes.Add(new Node(800,new Point(1989,283),20));
  nodes.Add(new Node(801,new Point(1994,250),10));
  nodes.Add(new Node(802,new Point(2000,125),37));
  nodes.Add(new Node(803,new Point(2005,357),27));
  nodes.Add(new Node(804,new Point(2013,212),26));
  nodes.Add(new Node(805,new Point(2014,1865),283));
  nodes.Add(new Node(806,new Point(2027,776),251));


	
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

public void Main(string argument, UpdateType updateSource)
{
    // The main entry point of the script, invoked every time
    // one of the programmable block's Run actions are invoked,
    // or the script updates itself. The updateSource argument
    // describes where the update came from.
    // 
    // The method itself is required, but the arguments above
    // can be removed if not needed.
	
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