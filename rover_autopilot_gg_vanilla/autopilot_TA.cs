
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
	faceRegionPolygon1 = new faceRegionPolygon(3,2,new Point((int)545.8768435332956,(int)973.7314479533823),tmpPolygon);
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
	faceRegionPolygon1 = new faceRegionPolygon(3,3,new Point((int)472.9548859955224,(int)1183.7082535989894),tmpPolygon);
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
	faceRegionPolygon1 = new faceRegionPolygon(3,4,new Point((int)377.41486417253014,(int)343.1128606377722),tmpPolygon);
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
	faceRegionPolygon1 = new faceRegionPolygon(3,5,new Point((int)610.857566581086,(int)978.9165251292854),tmpPolygon);
	//===========
	faceRegionPolygonList.Add(faceRegionPolygon1);
	//===========
	tmpPolygon = new List<Point>();
	tmpPolygon.Add(new Point((int)50,(int)50));
	faceRegionPolygon1 = new faceRegionPolygon(3,6,new Point((int)116.29726278468841,(int)250.41804879514453),tmpPolygon);
	//===========
	faceRegionPolygonList.Add(faceRegionPolygon1);
	//===========
	tmpPolygon = new List<Point>();
	tmpPolygon.Add(new Point((int)50,(int)50));
	faceRegionPolygon1 = new faceRegionPolygon(3,7,new Point((int)254.42292544500816,(int)181.88698942299425),tmpPolygon);
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
	faceRegionPolygon1 = new faceRegionPolygon(0,8,new Point((int)529.5453943056494,(int)1038.7349670487329),tmpPolygon);
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
	faceRegionPolygon1 = new faceRegionPolygon(0,9,new Point((int)648.8865636587907,(int)1061.6263019142325),tmpPolygon);
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
	faceRegionPolygon1 = new faceRegionPolygon(0,10,new Point((int)137.41628772219926,(int)132.52623254309182),tmpPolygon);
	//===========
	faceRegionPolygonList.Add(faceRegionPolygon1);
	//===========
	tmpPolygon = new List<Point>();
	tmpPolygon.Add(new Point((int)0,(int)2047));
	tmpPolygon.Add(new Point((int)0,(int)0));
	tmpPolygon.Add(new Point((int)2047,(int)0));
	tmpPolygon.Add(new Point((int)2047,(int)2047));
	tmpPolygon.Add(new Point((int)0,(int)2047));
	faceRegionPolygon1 = new faceRegionPolygon(1,11,new Point((int)1026.8545143752206,(int)1023.7872403211838),tmpPolygon);
	//===========
	faceRegionPolygonList.Add(faceRegionPolygon1);
	//===========
	tmpPolygon = new List<Point>();
	tmpPolygon.Add(new Point((int)0,(int)2047));
	tmpPolygon.Add(new Point((int)0,(int)0));
	tmpPolygon.Add(new Point((int)2047,(int)0));
	tmpPolygon.Add(new Point((int)2047,(int)2047));
	tmpPolygon.Add(new Point((int)0,(int)2047));
	faceRegionPolygon1 = new faceRegionPolygon(2,12,new Point((int)1023.6554673114691,(int)1025.6056052487309),tmpPolygon);
	//===========
	faceRegionPolygonList.Add(faceRegionPolygon1);
	//===========
	tmpPolygon = new List<Point>();
	tmpPolygon.Add(new Point((int)0,(int)2047));
	tmpPolygon.Add(new Point((int)0,(int)0));
	tmpPolygon.Add(new Point((int)2047,(int)0));
	tmpPolygon.Add(new Point((int)2047,(int)2047));
	tmpPolygon.Add(new Point((int)0,(int)2047));
	faceRegionPolygon1 = new faceRegionPolygon(4,13,new Point((int)1026.48198125909,(int)1017.539811192641),tmpPolygon);
	//===========
	faceRegionPolygonList.Add(faceRegionPolygon1);
	//===========
	tmpPolygon = new List<Point>();
	tmpPolygon.Add(new Point((int)50,(int)50));
	faceRegionPolygon1 = new faceRegionPolygon(4,14,new Point((int)348.52316448198604,(int)391.51294069171536),tmpPolygon);
	//===========
	faceRegionPolygonList.Add(faceRegionPolygon1);

	
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
	
    //note:
    //https://github.com/KeenSoftwareHouse/SpaceEngineers/blob/master/Sources/VRage.Math/Vector3D.cs
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
		
		
		Echo("facenumberCalculatedTarget:"+facenumberCalculatedTarget);
		Echo("pixelPosCalculatedTarget:"+pixelPosCalculatedTarget);
		
		whichFileShouldIlook(facenumberCalculatedTarget);
		
		bool targetIsOnTheSameFace = false;
		if(facenumberCalculatedTarget==facenumberCalculated){
			targetIsOnTheSameFace = true;
		}
		else{
			targetIsOnTheSameFace = false;
		}
		Echo("targetIsOnTheSameFace:"+targetIsOnTheSameFace);
		
		
		//getting vectors to help with angles proposals
		Vector3D shipForwardVector = RemoteControl.WorldMatrix.Forward;
		Vector3D shipLeftVector = RemoteControl.WorldMatrix.Left;
		Vector3D shipDownVector = RemoteControl.WorldMatrix.Down;
		
		double steerOverride = 0;
		// double steerOverride = shipForwardVector.Dot(Vector3D.Normalize(targetV3Dabs));
		// steerOverride*=100;
		// Echo("steerOverride:"+Math.Round(steerOverride,3));
		
		Vector3D targetV3Drel = RemoteControl.GetPosition()-targetV3Dabs;
		
		Vector3D crossForwardTT = shipForwardVector.Cross((targetV3Drel));
		// Vector3D crossForwardTT = shipForwardVector.Cross(Vector3D.Normalize(targetV3Dabs));
		double turnRightOrLeft = crossForwardTT.Dot(shipDownVector);
		
		Echo("turnRightOrLeft:"+Math.Round(turnRightOrLeft,3));
		
		// str_to_display = ""+"turnRightOrLeft:"+Math.Round(turnRightOrLeft,3);
		
		
		steerOverride = turnRightOrLeft/crossForwardTT.Length();
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
		
		//stop when destination is reached
		if(targetV3Drel.Length()<5){
			myTerrainTarget = new Vector3D(0, 0, 0);
		}
		
	}
	





	Echo(""+faceRegionPolygonList.Count);

	// isThisPointInThisRegion(int roverCurrentFaceNumber, Point currentRoverPosition, faceRegionPolygon fRP)
	// foreach(faceRegionPolygon faceRegionPolygonCT in faceRegionPolygonList){
		// isThisPointInThisRegion(facenumberCalculated, pixelPosCalculated, faceRegionPolygonCT);
	// }
	
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
	int planet_radius = 60000;
	
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

public class faceRegionPolygon {
	
	public int faceNumber;
	int regionNumber;
	public List<Point> polygon;
	Point regionCentroid;
	
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
	
public bool isThisPointInThisRegion(int roverCurrentFaceNumber, Point currentRoverPosition, faceRegionPolygon fRP){
	if(roverCurrentFaceNumber != fRP.faceNumber){
		return false;
	}
	// TODO:implement this
	bool testResultTmp = InsidePolygon(fRP.polygon, fRP.polygon.Count, currentRoverPosition);
	Echo("testResultTmp:"+testResultTmp);
	return testResultTmp;
}

// https://www.eecs.umich.edu/courses/eecs380/HANDOUTS/PROJ2/InsidePoly.html
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

  if (counter % 2 == 0)
    return(false);
  else
    return(true);
}
	