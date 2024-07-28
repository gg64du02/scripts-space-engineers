// This script is available at:
// https://steamcommunity.com/sharedfiles/filedetails/?id=2399231983

//=======================
//Script settings start here
//=======================
double rangeToCheckForOres = 2000;
//=======================
//Script settings end here
//=======================

List<String> stringList = new List<String>();
List<String> stringListOres = new List<String>();

List<String> generatedGPSs = new List<String>();

List<Vector2D> oreCoords2DSubPattern = new List<Vector2D>();

//width constant of sub pattern
int subPatternSize = 0;

public void addEarthLikeAlienTritonSubPattern(){
	
    stringList.Add("5.28571,50.85714,SiNiMg");
    stringList.Add("7.26666,17.66666,SiIceMg");
    stringList.Add("8.66666,88.125,IceMgSi");
    stringList.Add("10.78571,112.1428,AgAu");
    stringList.Add("23.43478,49.0869,IceAuFe");
    stringList.Add("26.78571,76.1428,AgAu");
    stringList.Add("34.2285,16.68571,MgSiNi");
    stringList.Add("36.66666,109.125,IceMgSi");
    stringList.Add("42.83333,42.33333,FeNiCo");
    stringList.Add("51.2285,70.6857,MgSiNi");
    stringList.Add("60.5666,7.03333,FeNiCo");
    stringList.Add("62.8823,104.3235,FeAuCo");
    stringList.Add("69.8823,29.32352,FeAuCo");
    stringList.Add("70.7857,55.14285,AgAu");
    stringList.Add("78.2666,79.6666,SiIceMg");
    stringList.Add("88.7857,108.1428,AgAu");
    stringList.Add("90.6666,13.125,IceMgSi");
    stringList.Add("92.2857,53.85714,SiNiMg");
    stringList.Add("96.6666,82.125,IceMgSi");
    stringList.Add("112.8333,18.33333,FeNiCo");
    stringList.Add("114.7083,83.6666,IceAgCo");
    stringList.Add("116.4166,52.08333,IceAgCo");
    stringList.Add("115.2666,112.6666,SiIceMg");

    foreach(var str in stringList)
    {

        // using the method 
        String[] strlist = str.Split(',');
        /*Echo("str" + str);
        Echo(strlist[0]);
        Echo(strlist[1]);
        Echo(strlist[2]);*/
        float tmpx = float.Parse(strlist[0]);
        float tmpy = float.Parse(strlist[1]);
        /*int tmpx = 0;
        int tmpy = 0;*/
        //string ores = "lol";
        string ores = strlist[2];
		stringListOres.Add(ores);
        
        oreCoords2DSubPattern.Add(new Vector2D(tmpx, tmpy));
    }
    Echo("-"+oreCoords2DSubPattern.Count+" ore spot pattern loaded for the current planet\n");
}


public void addMarsSubPattern(){
	
	stringList.Add("5.28571,50.85714,SiNiMg");
	stringList.Add("7.26666,17.66666,SiIceMg");
	stringList.Add("8.66666,88.125,IceMgSi");
	stringList.Add("9.0,68.7142,Ice");
	stringList.Add("10.78571,112.1428,AgAu");
	stringList.Add("12.875,7.875,Ice");
	stringList.Add("23.43478,49.0869,IceAuFe");
	stringList.Add("23.0,96.0,Ice");
	stringList.Add("26.78571,76.1428,AgAu");
	stringList.Add("34.2285,16.68571,MgSiNi");
	stringList.Add("34.125,60.875,Ice");
	stringList.Add("36.66666,109.125,IceMgSi");
	stringList.Add("42.83333,42.33333,FeNiCo");
	stringList.Add("51.2285,70.6857,MgSiNi");
	stringList.Add("48.8461,90.7692,Ice");
	stringList.Add("50.4,20.2,Ice");
	stringList.Add("60.5666,7.03333,FeNiCo");
	stringList.Add("62.8823,104.3235,FeAuIce");
	stringList.Add("63.0,43.25,Ice");
	stringList.Add("69.8823,29.32352,FeAuIce");
	stringList.Add("70.7857,55.14285,AgAu");
	stringList.Add("69.5,71.0,Ice");
	stringList.Add("78.2666,79.6666,SiIceMg");
	stringList.Add("78.125,10.875,Ice");
	stringList.Add("81.6666,95.2222,Ice");
	stringList.Add("88.7857,108.1428,AgAu");
	stringList.Add("90.6666,13.125,IceMgSi");
	stringList.Add("92.2857,53.85714,SiNiMg");
	stringList.Add("96.6666,82.125,IceMgSi");
	stringList.Add("98.8,35.6,Ice");
	stringList.Add("106.0,66.5,Ice");
	stringList.Add("106.125,99.875,Ice");
	stringList.Add("107.8,10.4,Ice");
	stringList.Add("112.8333,18.33333,FeNiCo");
	stringList.Add("114.7083,83.6666,IceAgCo");
	stringList.Add("116.4166,52.08333,IceAgCo");
	stringList.Add("115.2666,112.6666,SiIceMg");

    foreach(var str in stringList)
    {

        // using the method 
        String[] strlist = str.Split(',');
        /*Echo("str" + str);
        Echo(strlist[0]);
        Echo(strlist[1]);
        Echo(strlist[2]);*/
        float tmpx = float.Parse(strlist[0]);
        float tmpy = float.Parse(strlist[1]);
        /*int tmpx = 0;
        int tmpy = 0;*/
        //string ores = "lol";
        string ores = strlist[2];
		stringListOres.Add(ores);
        
        oreCoords2DSubPattern.Add(new Vector2D(tmpx, tmpy));
    }
    Echo("-"+oreCoords2DSubPattern.Count+" ore spot pattern loaded for the current planet\n");
}


public void addEuropaMoonTitanSubPattern(){
	
	stringList.Add("29.27272,158.8636,IceMgSi");
	stringList.Add("31.90384,53.0192,PtNi");
	stringList.Add("74.1666,215.1666,FeNiCo");
	stringList.Add("82.3904,36.6438,FeAuIce");
	stringList.Add("104.0714,144.7857,AgAu");
	stringList.Add("140.1666,55.16666,FeNiCo");
	stringList.Add("168.5,131.0975,PtNi");
	stringList.Add("204.753,42.33333,IceAgCo");
	stringList.Add("208.0714,209.2142,SiNiMg");

    foreach(var str in stringList)
    {

        // using the method 
        String[] strlist = str.Split(',');
        /*Echo("str" + str);
        Echo(strlist[0]);
        Echo(strlist[1]);
        Echo(strlist[2]);*/
        float tmpx = float.Parse(strlist[0]);
        float tmpy = float.Parse(strlist[1]);
        /*int tmpx = 0;
        int tmpy = 0;*/
        //string ores = "lol";
        string ores = strlist[2];
		stringListOres.Add(ores);
        
        oreCoords2DSubPattern.Add(new Vector2D(tmpx, tmpy));
    }
    Echo("-"+oreCoords2DSubPattern.Count+" ore spot pattern loaded for the current planet\n");
}

public void addPertamSubPattern(){
	
		/*
	stringList.Add("0.0,0.0,TL");
	stringList.Add("511.0,511.0,BR");
	stringList.Add("511.0,0.0,BL");
	stringList.Add("0.0,511.0,TR");
	*/
	
	stringList.Add("0.0,25.0,Si");
	stringList.Add("0.0,168.0,Ni");
	stringList.Add("2.0,246.0,Si");
	stringList.Add("6.0,385.0,Co");
	stringList.Add("9.0,287.0,Si");
	stringList.Add("10.0,301.0,Co");
	stringList.Add("10.0,335.0,Ni");
	stringList.Add("11.0,354.0,Ni");
	stringList.Add("14.0,175.0,Ni");
	stringList.Add("14.0,364.0,Ni");
	stringList.Add("16.0,439.0,Si");
	stringList.Add("18.0,272.0,Co");
	stringList.Add("18.0,486.0,Fe");
	stringList.Add("19.0,137.0,Au");
	stringList.Add("20.0,148.0,Si");
	stringList.Add("22.0,456.0,Fe");
	stringList.Add("23.0,484.0,Fe");
	stringList.Add("24.0,31.0,Ni");
	stringList.Add("26.0,178.0,Fe");
	stringList.Add("28.0,86.0,Fe");
	stringList.Add("29.0,441.0,Si");
	stringList.Add("29.0,492.0,Ni");
	stringList.Add("31.0,19.0,Fe");
	stringList.Add("31.0,179.0,Si");
	stringList.Add("31.0,356.0,Fe");
	stringList.Add("31.0,456.0,Ni");
	stringList.Add("33.0,426.0,Fe");
	stringList.Add("36.0,474.0,Co");
	stringList.Add("37.0,57.0,Co");
	stringList.Add("37.0,224.0,Si");
	stringList.Add("37.0,308.0,Co");
	stringList.Add("39.5,78.0,CoNi");
	stringList.Add("39.0,266.0,Ag");
	stringList.Add("39.0,385.0,Ni");
	stringList.Add("40.0,27.0,Fe");
	stringList.Add("40.0,300.0,Co");
	stringList.Add("41.0,60.0,Si");
	stringList.Add("41.0,126.0,Co");
	stringList.Add("41.0,159.0,Au");
	stringList.Add("41.0,204.0,Fe");
	stringList.Add("41.0,228.0,Si");
	stringList.Add("41.0,349.0,Ag");
	stringList.Add("42.7,384.3,CoSiNi");
	stringList.Add("44.0,211.0,Co");
	stringList.Add("44.0,363.0,Ni");
	stringList.Add("44.0,389.0,Fe");
	stringList.Add("44.0,488.0,Fe");
	stringList.Add("45.0,8.0,Fe");
	stringList.Add("45.0,107.0,Co");
	stringList.Add("45.0,110.0,Si");
	stringList.Add("46.0,125.0,Si");
	stringList.Add("47.0,131.0,Fe");
	stringList.Add("47.0,294.0,Co");
	stringList.Add("48.3,246.3,SiCoNi");
	stringList.Add("48.0,264.0,Fe");
	stringList.Add("49.0,316.0,Si");
	stringList.Add("51.0,509.0,Ni");
	stringList.Add("53.0,508.0,Si");
	stringList.Add("54.0,507.0,Fe");
	stringList.Add("56.0,244.0,Fe");
	stringList.Add("56.7,351.7,NiCoSi");
	stringList.Add("57.0,141.0,Si");
	stringList.Add("57.0,495.0,Fe");
	stringList.Add("58.0,63.0,Fe");
	stringList.Add("58.0,398.0,Ni");
	stringList.Add("59.0,198.0,Co");
	stringList.Add("59.0,432.0,Si");
	stringList.Add("61.0,106.0,Fe");
	stringList.Add("61.0,225.0,Fe");
	stringList.Add("62.0,146.0,Fe");
	stringList.Add("62.0,248.0,Fe");
	stringList.Add("62.0,379.0,Si");
	stringList.Add("63.0,460.0,Fe");
	stringList.Add("64.0,134.0,Fe");
	stringList.Add("65.0,159.0,Ni");
	stringList.Add("65.0,429.0,Si");
	stringList.Add("66.0,101.0,Fe");
	stringList.Add("66.0,106.0,Fe");
	stringList.Add("67.0,313.0,Co");
	stringList.Add("68.0,190.0,Fe");
	stringList.Add("68.0,487.0,Ni");
	stringList.Add("70.0,186.0,Fe");
	stringList.Add("70.0,191.0,Fe");
	stringList.Add("71.0,202.0,Fe");
	stringList.Add("75.0,11.0,Fe");
	stringList.Add("75.0,161.0,Ni");
	stringList.Add("75.7,425.3,CoNiFe");
	stringList.Add("76.0,360.0,Co");
	stringList.Add("77.0,391.0,Ni");
	stringList.Add("78.0,112.0,Fe");
	stringList.Add("79.0,148.0,Fe");
	stringList.Add("81.0,14.5,CoFe");
	stringList.Add("81.0,145.0,Fe");
	stringList.Add("81.0,204.5,Fe");
	stringList.Add("81.0,374.0,Fe");
	stringList.Add("83.5,310.0,CoSi");
	stringList.Add("84.0,57.0,Fe");
	stringList.Add("84.0,273.0,Si");
	stringList.Add("84.0,379.0,Si");
	stringList.Add("85.7,96.7,FeCoNi");
	stringList.Add("85.0,202.0,Ni");
	stringList.Add("85.0,221.0,Fe");
	stringList.Add("85.0,408.0,Fe");
	stringList.Add("85.0,465.0,Ni");
	stringList.Add("86.0,182.0,Fe");
	stringList.Add("86.0,449.0,Si");
	stringList.Add("87.0,235.0,Co");
	stringList.Add("87.0,406.0,Fe");
	stringList.Add("88.5,325.0,CoNi");
	stringList.Add("88.0,409.0,Fe");
	stringList.Add("89.0,15.0,Co");
	stringList.Add("91.0,339.0,Si");
	stringList.Add("91.0,462.0,Si");
	stringList.Add("94.0,98.0,Si");
	stringList.Add("94.0,170.0,Co");
	stringList.Add("94.0,272.0,Co");
	stringList.Add("95.0,97.0,Fe");
	stringList.Add("97.0,184.0,Ni");
	stringList.Add("98.0,243.0,Si");
	stringList.Add("99.0,56.0,Ni");
	stringList.Add("100.0,20.0,Fe");
	stringList.Add("100.0,99.0,Fe");
	stringList.Add("100.5,192.0,NiSi");
	stringList.Add("100.0,299.5,NiCo");
	stringList.Add("101.0,478.0,Fe");
	stringList.Add("103.0,219.0,Fe");
	stringList.Add("104.0,465.0,Si");
	stringList.Add("104.0,476.0,Fe");
	stringList.Add("105.0,99.5,SiNi");
	stringList.Add("106.0,249.0,Ni");
	stringList.Add("106.0,492.0,Co");
	stringList.Add("107.0,60.0,Si");
	stringList.Add("107.0,256.0,Si");
	stringList.Add("109.0,257.0,Ni");
	stringList.Add("109.0,342.0,Fe");
	stringList.Add("109.0,433.0,Co");
	stringList.Add("109.0,452.0,Co");
	stringList.Add("110.0,98.0,Si");
	stringList.Add("110.0,378.0,Ni");
	stringList.Add("110.0,386.0,Si");
	stringList.Add("112.0,149.0,Co");
	stringList.Add("112.0,157.0,Ni");
	stringList.Add("113.3,316.7,NiSiCo");
	stringList.Add("114.0,160.0,Fe");
	stringList.Add("116.0,174.0,Ni");
	stringList.Add("118.0,125.0,Co");
	stringList.Add("118.3,219.7,NiSiCo");
	stringList.Add("118.0,365.0,Fe");
	stringList.Add("121.0,408.0,Si");
	stringList.Add("123.0,148.0,Si");
	stringList.Add("124.0,445.0,Fe");
	stringList.Add("126.0,27.0,Co");
	stringList.Add("126.7,63.7,NiCoSi");
	stringList.Add("127.0,494.0,Fe");
	stringList.Add("128.0,2.0,Fe");
	stringList.Add("128.0,272.0,Fe");
	stringList.Add("130.0,374.0,Ni");
	stringList.Add("131.0,375.0,Fe");
	stringList.Add("132.3,52.3,SiCoNi");
	stringList.Add("132.0,479.0,Fe");
	stringList.Add("136.0,6.0,Co");
	stringList.Add("136.0,152.0,Fe");
	stringList.Add("136.0,319.0,Ni");
	stringList.Add("136.0,367.0,Co");
	stringList.Add("137.0,102.0,Fe");
	stringList.Add("137.0,144.0,Co");
	stringList.Add("137.0,151.0,Ni");
	stringList.Add("137.0,294.0,Fe");
	stringList.Add("138.0,179.0,Fe");
	stringList.Add("138.0,274.0,Mg");
	stringList.Add("138.0,301.0,Fe");
	stringList.Add("138.0,470.0,Au");
	stringList.Add("140.0,208.0,Si");
	stringList.Add("141.7,273.3,CoSiNi");
	stringList.Add("141.0,452.0,Ni");
	stringList.Add("141.0,488.0,Co");
	stringList.Add("142.0,227.0,Si");
	stringList.Add("143.0,134.0,Fe");
	stringList.Add("144.5,433.0,FeCo");
	stringList.Add("145.0,51.0,Si");
	stringList.Add("145.0,323.0,Si");
	stringList.Add("146.0,201.0,Si");
	stringList.Add("148.0,135.0,Mg");
	stringList.Add("149.0,258.0,Ni");
	stringList.Add("149.0,307.0,Fe");
	stringList.Add("149.0,320.0,Fe");
	stringList.Add("149.0,417.0,Co");
	stringList.Add("150.0,90.0,Ni");
	stringList.Add("150.0,257.0,Fe");
	stringList.Add("150.0,403.0,Co");
	stringList.Add("151.0,299.0,Ni");
	stringList.Add("153.0,69.0,Co");
	stringList.Add("153.7,356.7,NiCoSi");
	stringList.Add("153.0,486.0,Fe");
	stringList.Add("154.0,497.0,Ni");
	stringList.Add("156.5,464.0,Fe");
	stringList.Add("157.0,455.0,Fe");
	stringList.Add("158.0,354.0,Fe");
	stringList.Add("159.0,156.0,Fe");
	stringList.Add("159.3,345.3,SiCoNi");
	stringList.Add("159.0,355.0,Si");
	stringList.Add("159.0,427.0,Fe");
	stringList.Add("159.0,498.0,Si");
	stringList.Add("160.0,157.0,Ni");
	stringList.Add("160.0,246.5,CoSi");
	stringList.Add("160.0,382.0,Co");
	stringList.Add("161.0,488.0,Fe");
	stringList.Add("165.0,335.0,Fe");
	stringList.Add("167.0,89.0,Ni");
	stringList.Add("168.0,284.0,Co");
	stringList.Add("170.0,203.0,Co");
	stringList.Add("170.0,441.0,Ni");
	stringList.Add("171.0,24.0,Si");
	stringList.Add("171.0,404.0,Si");
	stringList.Add("174.0,252.0,Ni");
	stringList.Add("175.0,112.0,Ni");
	stringList.Add("176.0,23.0,Si");
	stringList.Add("176.0,31.0,Ni");
	stringList.Add("177.0,67.0,Fe");
	stringList.Add("177.0,340.0,Fe");
	stringList.Add("177.0,469.0,Co");
	stringList.Add("177.0,488.0,Co");
	stringList.Add("179.0,153.0,Si");
	stringList.Add("182.0,467.0,Ni");
	stringList.Add("184.0,266.0,Fe");
	stringList.Add("184.0,392.0,Fe");
	stringList.Add("185.5,217.0,SiNi");
	stringList.Add("186.0,109.5,CoNi");
	stringList.Add("186.0,331.0,Co");
	stringList.Add("187.0,175.0,Fe");
	stringList.Add("187.0,371.0,Si");
	stringList.Add("188.0,43.0,Fe");
	stringList.Add("188.0,166.0,Si");
	stringList.Add("189.0,396.0,Fe");
	stringList.Add("191.0,67.0,Co");
	stringList.Add("191.0,312.0,Fe");
	stringList.Add("192.0,239.0,Co");
	stringList.Add("192.0,311.0,Si");
	stringList.Add("193.0,224.0,Fe");
	stringList.Add("195.0,70.0,Si");
	stringList.Add("196.0,116.0,Fe");
	stringList.Add("196.0,301.0,Mg");
	stringList.Add("196.0,325.5,NiSi");
	stringList.Add("197.0,383.0,Fe");
	stringList.Add("197.0,394.0,Co");
	stringList.Add("198.7,10.3,CoSiNi");
	stringList.Add("198.0,430.0,Au");
	stringList.Add("199.0,3.0,Fe");
	stringList.Add("200.3,312.3,NiCoFe");
	stringList.Add("200.0,426.0,Fe");
	stringList.Add("201.0,1.0,Fe");
	stringList.Add("201.0,188.0,Fe");
	stringList.Add("201.0,456.0,Ni");
	stringList.Add("202.5,99.0,SiCo");
	stringList.Add("202.0,352.0,Fe");
	stringList.Add("203.0,345.0,Fe");
	stringList.Add("203.0,474.0,Si");
	stringList.Add("206.0,211.0,Fe");
	stringList.Add("206.0,397.0,Fe");
	stringList.Add("208.0,162.0,Ni");
	stringList.Add("210.0,459.0,Ni");
	stringList.Add("211.0,398.0,Fe");
	stringList.Add("214.0,270.0,Si");
	stringList.Add("214.0,484.0,Co");
	stringList.Add("216.0,218.0,Ni");
	stringList.Add("217.0,107.0,Ag");
	stringList.Add("218.0,337.0,Ni");
	stringList.Add("218.0,434.0,Ni");
	stringList.Add("220.0,225.0,Ni");
	stringList.Add("220.0,384.0,Ni");
	stringList.Add("221.0,55.0,Si");
	stringList.Add("221.0,250.0,Ni");
	stringList.Add("221.0,366.0,Fe");
	stringList.Add("221.0,492.0,Si");
	stringList.Add("224.0,30.0,Si");
	stringList.Add("227.0,489.0,Si");
	stringList.Add("228.0,107.0,Si");
	stringList.Add("229.3,57.3,SiCoNi");
	stringList.Add("229.0,290.0,Ni");
	stringList.Add("229.0,380.0,Si");
	stringList.Add("229.0,449.0,Ni");
	stringList.Add("231.0,337.0,Ni");
	stringList.Add("232.0,55.0,Ag");
	stringList.Add("235.0,190.0,Co");
	stringList.Add("235.0,404.0,Si");
	stringList.Add("235.0,412.0,Ni");
	stringList.Add("235.0,421.0,Co");
	stringList.Add("237.7,162.7,NiCoSi");
	stringList.Add("238.0,145.0,Fe");
	stringList.Add("239.0,115.0,Co");
	stringList.Add("240.0,284.0,Si");
	stringList.Add("241.0,0.0,Ni");
	stringList.Add("241.0,299.0,Si");
	stringList.Add("243.0,463.0,Fe");
	stringList.Add("244.0,55.0,Fe");
	stringList.Add("245.0,60.0,Ag");
	stringList.Add("245.0,205.0,Fe");
	stringList.Add("245.0,250.0,Au");
	stringList.Add("245.0,349.0,Si");
	stringList.Add("246.0,109.0,Co");
	stringList.Add("246.0,200.0,Si");
	stringList.Add("246.0,382.0,Fe");
	stringList.Add("247.0,24.0,Ni");
	stringList.Add("248.0,158.0,Fe");
	stringList.Add("249.0,185.0,Si");
	stringList.Add("249.0,248.0,Fe");
	stringList.Add("249.0,352.0,Co");
	stringList.Add("250.3,151.7,NiSiCo");
	stringList.Add("251.0,244.0,Au");
	stringList.Add("253.0,46.0,Fe");
	stringList.Add("253.0,348.5,SiNi");
	stringList.Add("253.0,373.0,Mg");
	stringList.Add("255.0,390.0,Ni");
	stringList.Add("256.0,450.0,Fe");
	stringList.Add("257.0,429.0,Ni");
	stringList.Add("257.0,480.0,Si");
	stringList.Add("258.0,95.0,Co");
	stringList.Add("261.0,119.0,Fe");
	stringList.Add("262.0,303.0,Si");
	stringList.Add("262.0,499.0,Fe");
	stringList.Add("263.0,343.0,Co");
	stringList.Add("263.0,437.0,Fe");
	stringList.Add("265.0,408.0,Fe");
	stringList.Add("267.0,207.0,Ni");
	stringList.Add("267.0,272.0,Au");
	stringList.Add("270.0,482.0,Si");
	stringList.Add("272.0,234.0,Ni");
	stringList.Add("274.0,50.0,Ni");
	stringList.Add("275.0,55.0,Ni");
	stringList.Add("276.0,108.0,Co");
	stringList.Add("278.0,138.0,Si");
	stringList.Add("278.0,270.0,Si");
	stringList.Add("279.0,233.0,Ni");
	stringList.Add("279.0,471.0,Co");
	stringList.Add("282.0,73.0,Ni");
	stringList.Add("284.0,163.0,Si");
	stringList.Add("286.0,241.0,Ni");
	stringList.Add("286.0,384.0,Si");
	stringList.Add("287.0,147.0,Ni");
	stringList.Add("287.0,214.0,Fe");
	stringList.Add("288.0,14.0,Ni");
	stringList.Add("289.0,5.0,Ni");
	stringList.Add("289.0,11.0,Fe");
	stringList.Add("289.0,260.0,Fe");
	stringList.Add("289.0,329.0,Ni");
	stringList.Add("289.0,427.5,SiCo");
	stringList.Add("290.0,6.0,Fe");
	stringList.Add("290.0,176.0,Si");
	stringList.Add("290.0,319.0,Si");
	stringList.Add("291.0,320.0,Fe");
	stringList.Add("292.0,99.0,Ni");
	stringList.Add("293.0,98.0,Fe");
	stringList.Add("295.0,177.0,Ni");
	stringList.Add("296.0,93.0,Co");
	stringList.Add("297.3,189.3,SiCoNi");
	stringList.Add("297.0,407.0,Fe");
	stringList.Add("298.0,375.0,Ni");
	stringList.Add("300.0,65.0,Co");
	stringList.Add("300.0,257.0,Co");
	stringList.Add("301.5,307.0,CoFe");
	stringList.Add("304.0,111.0,Si");
	stringList.Add("304.0,288.0,Ni");
	stringList.Add("304.0,351.0,Si");
	stringList.Add("304.0,392.0,Co");
	stringList.Add("306.0,28.0,Fe");
	stringList.Add("306.0,357.0,Fe");
	stringList.Add("307.3,400.7,NiSiCo");
	stringList.Add("307.0,447.0,Si");
	stringList.Add("308.0,222.0,Ni");
	stringList.Add("308.0,433.0,Co");
	stringList.Add("311.0,204.0,Au");
	stringList.Add("311.0,495.0,Fe");
	stringList.Add("312.0,18.0,Co");
	stringList.Add("315.0,82.0,Fe");
	stringList.Add("318.0,299.0,Fe");
	stringList.Add("319.0,300.0,Ni");
	stringList.Add("321.0,160.0,Fe");
	stringList.Add("321.0,256.0,Co");
	stringList.Add("321.0,402.0,Fe");
	stringList.Add("323.0,452.0,Fe");
	stringList.Add("323.0,492.0,Co");
	stringList.Add("328.0,266.0,Si");
	stringList.Add("329.0,107.0,Fe");
	stringList.Add("329.0,364.0,Co");
	stringList.Add("330.0,200.0,Fe");
	stringList.Add("330.7,454.3,CoSiNi");
	stringList.Add("331.0,309.0,Fe");
	stringList.Add("332.0,159.0,Fe");
	stringList.Add("332.0,413.0,Fe");
	stringList.Add("333.0,47.0,Fe");
	stringList.Add("333.0,485.0,Fe");
	stringList.Add("333.0,500.0,Ni");
	stringList.Add("335.0,311.0,Si");
	stringList.Add("335.7,357.3,CoSiNi");
	stringList.Add("336.0,208.0,Fe");
	stringList.Add("337.0,17.0,Fe");
	stringList.Add("337.0,114.0,Ni");
	stringList.Add("338.0,243.0,Si");
	stringList.Add("339.0,64.0,Si");
	stringList.Add("342.0,102.0,Si");
	stringList.Add("342.0,156.0,Ag");
	stringList.Add("343.0,182.0,Co");
	stringList.Add("343.0,289.0,Ni");
	stringList.Add("344.0,62.0,Ni");
	stringList.Add("345.0,209.0,Si");
	stringList.Add("345.0,392.0,Si");
	stringList.Add("346.0,426.0,Mg");
	stringList.Add("349.0,142.0,Fe");
	stringList.Add("350.0,106.0,Ni");
	stringList.Add("351.0,49.0,Si");
	stringList.Add("351.0,182.0,Co");
	stringList.Add("355.0,402.0,Co");
	stringList.Add("360.5,349.0,NiCo");
	stringList.Add("361.0,265.0,Fe");
	stringList.Add("362.0,439.0,Co");
	stringList.Add("363.0,41.0,Si");
	stringList.Add("363.0,225.0,Si");
	stringList.Add("363.0,479.0,Fe");
	stringList.Add("363.0,492.0,Fe");
	stringList.Add("364.0,472.0,Ni");
	stringList.Add("365.0,83.0,Fe");
	stringList.Add("365.0,348.5,NiCo");
	stringList.Add("365.0,401.0,Si");
	stringList.Add("367.0,158.0,Co");
	stringList.Add("368.0,147.5,FeCo");
	stringList.Add("368.0,300.0,Fe");
	stringList.Add("369.0,370.0,Fe");
	stringList.Add("371.0,50.0,Fe");
	stringList.Add("371.0,488.0,Ni");
	stringList.Add("373.3,248.7,FeNiCo");
	stringList.Add("373.0,299.0,Si");
	stringList.Add("373.0,314.0,Co");
	stringList.Add("374.0,1.0,Ni");
	stringList.Add("376.0,221.0,Ni");
	stringList.Add("378.0,426.0,Si");
	stringList.Add("380.0,92.0,Co");
	stringList.Add("380.0,446.0,Ni");
	stringList.Add("382.0,130.0,Co");
	stringList.Add("382.0,361.0,Co");
	stringList.Add("386.0,214.0,Fe");
	stringList.Add("386.0,509.0,Fe");
	stringList.Add("387.0,16.0,Fe");
	stringList.Add("387.0,283.0,Ni");
	stringList.Add("387.0,426.0,Fe");
	stringList.Add("389.0,46.0,Si");
	stringList.Add("391.0,99.0,Fe");
	stringList.Add("391.0,276.0,Ni");
	stringList.Add("392.0,179.0,Fe");
	stringList.Add("394.0,160.0,Si");
	stringList.Add("394.0,343.0,Co");
	stringList.Add("395.0,13.0,Fe");
	stringList.Add("395.0,217.0,Fe");
	stringList.Add("400.0,358.0,Si");
	stringList.Add("403.0,192.0,Fe");
	stringList.Add("403.0,396.0,Fe");
	stringList.Add("404.0,55.0,Co");
	stringList.Add("404.0,66.0,Co");
	stringList.Add("404.0,249.0,Si");
	stringList.Add("405.0,463.0,Co");
	stringList.Add("406.3,289.7,NiSiCo");
	stringList.Add("408.0,36.0,Co");
	stringList.Add("408.0,446.0,Si");
	stringList.Add("409.5,84.0,NiCo");
	stringList.Add("409.0,363.0,Si");
	stringList.Add("410.0,408.0,Ag");
	stringList.Add("411.0,126.0,Ni");
	stringList.Add("412.0,366.0,Co");
	stringList.Add("412.0,412.0,Fe");
	stringList.Add("413.0,200.0,Co");
	stringList.Add("416.0,248.0,Fe");
	stringList.Add("416.0,279.0,Co");
	stringList.Add("418.0,143.0,Fe");
	stringList.Add("418.0,317.0,Co");
	stringList.Add("418.0,475.0,Ni");
	stringList.Add("418.0,495.0,Si");
	stringList.Add("420.0,495.0,Si");
	stringList.Add("421.0,76.0,Fe");
	stringList.Add("422.0,188.0,Ni");
	stringList.Add("423.0,496.0,Fe");
	stringList.Add("425.0,110.0,Si");
	stringList.Add("425.0,131.0,Ni");
	stringList.Add("427.0,433.0,Ni");
	stringList.Add("429.0,14.0,Si");
	stringList.Add("429.0,39.0,Fe");
	stringList.Add("431.0,188.0,Fe");
	stringList.Add("431.0,251.0,Co");
	stringList.Add("431.0,343.0,Co");
	stringList.Add("431.0,402.0,Co");
	stringList.Add("432.0,344.0,Ni");
	stringList.Add("433.0,60.5,CoNi");
	stringList.Add("433.0,326.0,Fe");
	stringList.Add("435.0,310.0,Ni");
	stringList.Add("435.0,368.0,Si");
	stringList.Add("435.0,442.0,Fe");
	stringList.Add("439.0,339.0,Ni");
	stringList.Add("440.0,387.0,Si");
	stringList.Add("443.0,289.0,Co");
	stringList.Add("447.0,360.0,Si");
	stringList.Add("451.0,194.0,Ni");
	stringList.Add("452.0,495.0,Mg");
	stringList.Add("453.0,17.0,Si");
	stringList.Add("453.0,104.0,Si");
	stringList.Add("453.0,145.0,Co");
	stringList.Add("455.0,120.0,Ni");
	stringList.Add("456.0,253.0,Ag");
	stringList.Add("457.0,46.0,Fe");
	stringList.Add("459.0,238.0,Fe");
	stringList.Add("459.0,254.0,Si");
	stringList.Add("459.0,452.0,Fe");
	stringList.Add("460.0,166.0,Si");
	stringList.Add("461.0,295.0,Ni");
	stringList.Add("461.0,392.0,Fe");
	stringList.Add("462.0,201.0,Fe");
	stringList.Add("463.0,98.0,Si");
	stringList.Add("464.0,178.0,Fe");
	stringList.Add("465.0,362.0,Fe");
	stringList.Add("465.0,436.0,Fe");
	stringList.Add("466.0,250.0,Fe");
	stringList.Add("466.0,443.0,Ni");
	stringList.Add("466.0,508.0,Fe");
	stringList.Add("467.0,21.0,Co");
	stringList.Add("467.0,140.0,Ni");
	stringList.Add("467.0,334.0,Co");
	stringList.Add("467.0,362.0,Fe");
	stringList.Add("468.0,209.0,Fe");
	stringList.Add("468.0,284.0,Si");
	stringList.Add("469.0,45.0,Co");
	stringList.Add("469.0,302.0,Fe");
	stringList.Add("471.0,482.0,Ni");
	stringList.Add("473.0,384.0,Fe");
	stringList.Add("474.0,59.0,Mg");
	stringList.Add("474.0,91.0,Si");
	stringList.Add("475.0,429.0,Co");
	stringList.Add("475.0,469.0,Fe");
	stringList.Add("477.0,153.0,Co");
	stringList.Add("483.0,327.0,Fe");
	stringList.Add("487.0,71.0,Si");
	stringList.Add("487.0,103.0,Mg");
	stringList.Add("488.0,290.0,Fe");
	stringList.Add("490.0,192.0,Ni");
	stringList.Add("490.0,488.0,Co");
	stringList.Add("492.0,52.0,Fe");
	stringList.Add("493.0,390.0,Si");
	stringList.Add("494.0,17.0,Co");
	stringList.Add("494.0,22.0,Ni");
	stringList.Add("494.0,121.0,Ni");
	stringList.Add("494.0,141.0,Co");
	stringList.Add("494.0,312.0,Fe");
	stringList.Add("494.0,340.0,Co");
	stringList.Add("495.0,231.0,Ni");
	stringList.Add("496.5,102.0,FeCo");
	stringList.Add("497.0,496.0,Fe");
	stringList.Add("498.0,344.0,Co");
	stringList.Add("500.7,219.7,NiCoSi");
	stringList.Add("501.0,2.0,Fe");
	stringList.Add("502.0,222.0,Fe");
	stringList.Add("502.0,420.0,Si");
	stringList.Add("502.0,436.0,Fe");
	stringList.Add("504.0,116.0,Fe");
	stringList.Add("505.0,311.0,Fe");
	stringList.Add("506.0,310.0,Ni");
	stringList.Add("508.0,17.0,Co");
	stringList.Add("508.0,58.0,Si");
	stringList.Add("509.0,80.0,Ni");
	stringList.Add("509.0,149.0,Fe");
	stringList.Add("509.0,398.0,Fe");
	stringList.Add("510.0,395.0,Ni");
	stringList.Add("510.0,480.0,Ni");
	stringList.Add("511.0,195.0,Fe");
	stringList.Add("511.0,262.0,Ni");


    foreach(var str in stringList)
    {

        // using the method 
        String[] strlist = str.Split(',');
        /*Echo("str" + str);
        Echo(strlist[0]);
        Echo(strlist[1]);
        Echo(strlist[2]);*/
        float tmpx = float.Parse(strlist[0]);
        float tmpy = float.Parse(strlist[1]);
        /*int tmpx = 0;
        int tmpy = 0;*/
        //string ores = "lol";
        string ores = strlist[2];
		stringListOres.Add(ores);
        
        oreCoords2DSubPattern.Add(new Vector2D(tmpx, tmpy));
    }
    Echo("-"+oreCoords2DSubPattern.Count+" ore spot pattern loaded for the current planet\n");
}

public void clearSubPattern(){
	stringList = new List<String>();
	stringListOres = new List<String>();
	List<String> generatedGPSs = new List<String>();
	oreCoords2DSubPattern = new List<Vector2D>();
}
	

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


	//Echo("lol4");
	Echo("-Click Run to generate the GPSs ore spots in the CustomData inside the PB\n");

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


	//Echo("lol3");
	
	if(stringList.Count !=0){
		clearSubPattern();
	}
	
	string customDataStrBuild = "";

    //Get the PB Position:
    Vector3D myPos = Me.GetPosition();


	Vector3D vec3Dtarget = new Vector3D(0,0,0);
	List<IMyShipController> shipsControllers = new List<IMyShipController>();
	GridTerminalSystem.GetBlocksOfType<IMyShipController>(shipsControllers);
	if(shipsControllers.Count==0){
			Echo("This script need any kind of ship controller\n\na remote control, cockpit, ....");
			return;
	}
	IMyShipController firstController = shipsControllers[0];
    //Get any control capable block to get the planet center
	//using the expected remote control to give us the center of the current planet
	bool planetCenterDetected = firstController.TryGetPlanetPosition(out vec3Dtarget);
	
	Echo("-You pressed Run you might want to check the content of CustomData\n");
	
	Echo("-The following is for debugging purpose:\n");
	
	Echo("vec3Dtarget"+vec3Dtarget);
	
	//Echo("lol2");
	
	if(planetCenterDetected){
		
		Echo("planet detected");
		
		
		Echo("checking detected planet");
		
		
		//Every stock planets center
		List<String> listOfPlanetsGPSString = new List<String>();

		listOfPlanetsGPSString.Add("GPS:EarthLike:0.50:0.50:0.50:");
		listOfPlanetsGPSString.Add("GPS:Moon:16384.50:136384.50:-113615.50:");

		listOfPlanetsGPSString.Add("GPS:Mars:1031072.50:131072.50:1631072.50:");
		listOfPlanetsGPSString.Add("GPS:Europa:916384.50:16384.50:1616384.50:");

		listOfPlanetsGPSString.Add("GPS:Triton:-284463.50:-2434463.50:365536.50:");

		listOfPlanetsGPSString.Add("GPS:Pertam:-3967231.50:-32231.50:-767231.50:");

		listOfPlanetsGPSString.Add("GPS:Alien:131072.50:131072.50:5731072.50:");
		listOfPlanetsGPSString.Add("GPS:Titan:36384.50:226384.50:5796384.50:");
		
		// if you want to use the ship controller for some reason
		// myPos = firstController.GetPosition();
		
		Vector3D detectedPlanet = new Vector3D(0,0,0);
		MyWaypointInfo tmpTestPlanetCenter = new MyWaypointInfo("dnm", 0, 0, 0);
		
		//Echo("lol1");
		
		foreach(string str in listOfPlanetsGPSString){
			
			//MyWaypointInfo tmpTestPlanetCenter = new MyWaypointInfo("dnm", 0, 0, 0);
			tmpTestPlanetCenter = new MyWaypointInfo("dnm", 0, 0, 0);
			MyWaypointInfo.TryParse(str, out tmpTestPlanetCenter);
			//Echo("tmpTestPlanetCenter"+tmpTestPlanetCenter);
			
			Vector3D tmpVector3DplanetCenter = tmpTestPlanetCenter.Coords;
			//Echo("tmpVector3DplanetCenter"+tmpVector3DplanetCenter);
			
			 Vector3D vector3DToPlanetCenter = tmpVector3DplanetCenter - myPos;
			 
			 double distanceToPlanetCenter = vector3DToPlanetCenter.Length();
			//Echo("distanceToPlanetCenter"+distanceToPlanetCenter);
			
			if(distanceToPlanetCenter < 100000){
				detectedPlanet = tmpVector3DplanetCenter;
				customDataStrBuild += "planet's center:\n\n";
				customDataStrBuild += str;
				customDataStrBuild += "\n\n\nalign this planet's center with one in the following list:\n";
				break;
			}
		}
		
		
		int activeEarthLife = 0;
		int activeMoon      = 0;
		int activeMars      = 0;
		int activeEuropa      = 0;
		int activeTriton    = 0;
		int activePertam    = 0;
		int activeAlien     = 0;
		int activeTitan     = 0;

		
		//detect if it is a known planet
		if(detectedPlanet != new Vector3D(0,0,0)){
			//Echo("tmpTestPlanetCenter"+tmpTestPlanetCenter);
			
			string planetsName = tmpTestPlanetCenter.Name;
			
			Echo("planetsName: "+planetsName);
			
			Echo("detectedPlanet "+detectedPlanet);
			Vector3D cubeCenter = detectedPlanet;
			
			//in meters
			double planet_radius = 0;
			
			double distanceToCenter = (cubeCenter - myPos).Length();
			
			Echo("distanceToCenter"+distanceToCenter);
			
			planet_radius = distanceToCenter;
			
			
			//choose the appropriate subpattern to use for the detected planet
			//or adapt to local elevation of the firstController?
			if(planetsName == "Alien"){
				addEarthLikeAlienTritonSubPattern();
				subPatternSize = 128;
				activeAlien = 1;
			}
			if(planetsName == "EarthLike"){
				addEarthLikeAlienTritonSubPattern();
				subPatternSize = 128;
				activeEarthLife = 1;
			}
			if(planetsName == "Europa"){
				addEuropaMoonTitanSubPattern();
				subPatternSize = 256;
				activeEuropa = 1;
			}
			if(planetsName == "Mars"){
				addMarsSubPattern();
				subPatternSize = 128;
				activeMars = 1;
			}
			if(planetsName == "Moon"){
				addEuropaMoonTitanSubPattern();
				subPatternSize = 256;
				activeMoon = 1;
			}
			if(planetsName == "Pertam"){
			//	planet_radius = 30000;
				//customDataStrBuild += "Pertam is not yet supported\n";
				//Me.CustomData = customDataStrBuild;
				customDataStrBuild += "on Pertam \"flat\" areas would be most likely filled with false positives,\n";
				customDataStrBuild += "if there is nothing there seek somewhere not as even terrain wise.\n";
				customDataStrBuild += "I will make false positive disappear eventually.\n";
				addPertamSubPattern();
				subPatternSize = 512;
				activePertam = 1;
			}
			if(planetsName == "Titan"){
				addEuropaMoonTitanSubPattern();
				subPatternSize = 256;
				activeTitan = 1;
			}
			if(planetsName == "Triton"){
				addEarthLikeAlienTritonSubPattern();
				subPatternSize = 128;
				activeTriton = 1;
			}

			int allActiveButPertam = (activeEarthLife | activeMoon | activeMars | activeEuropa | activeTriton | activeAlien | activeTitan);
			
			Echo("planets-"+activeEarthLife+"-"+activeMoon+"-"+activeMars+"-"+activeEuropa+"-"+activeTriton+"-"+activeAlien+"-"+activeTitan+"-"+allActiveButPertam+"-"+activePertam);

			//Don't change unless you know what you are doing: 128 * 16 = 2048
			int constantNumbersOfSubPatternToGenerate = 16;
			constantNumbersOfSubPatternToGenerate = 2048 / subPatternSize;
			Echo("constantNumbersOfSubPatternToGenerate" + constantNumbersOfSubPatternToGenerate);

			List<int> intIndexFaces = new List<int>(6);
			intIndexFaces.Add(0);
			intIndexFaces.Add(1);
			intIndexFaces.Add(2);
			intIndexFaces.Add(3);
			intIndexFaces.Add(4);
			intIndexFaces.Add(5);
			
			Vector3D centerFacePositionOffset = new Vector3D(0,0,0);
			foreach(int intTmp in intIndexFaces){
				Echo("Checking planet face:"+intTmp);
				if(intTmp == 0)
				{
					centerFacePositionOffset = new Vector3D(0, 0, planet_radius);
				}
				if(intTmp == 1)
				{
					centerFacePositionOffset = new Vector3D(0, -planet_radius,0);
				}
				if(intTmp == 2)
				{
					centerFacePositionOffset = new Vector3D(0, 0, -planet_radius);
				}
				if(intTmp == 3)
				{
					centerFacePositionOffset = new Vector3D(planet_radius,0,0);
				}
				if(intTmp == 4)
				{
					centerFacePositionOffset = new Vector3D(-planet_radius,0,0);
				}
				if(intTmp == 5)
				{
					centerFacePositionOffset = new Vector3D(0, planet_radius,0);
				}
				Vector3D centerFacePosition = detectedPlanet + centerFacePositionOffset;
				
				int intActivatedZero  = 1      * (intTmp - 1) * (intTmp - 2) * (intTmp - 3) * (intTmp - 4) * (intTmp - 5) / -120;
				int intActivatedOne   = intTmp * 1            * (intTmp - 2) * (intTmp - 3) * (intTmp - 4) * (intTmp - 5) / 24;
				int intActivatedTwo   = intTmp * (intTmp - 1) * 1            * (intTmp - 3) * (intTmp - 4) * (intTmp - 5) / -12;
				int intActivatedThree = intTmp * (intTmp - 1) * (intTmp - 2) *  1           * (intTmp - 4) * (intTmp - 5) / 12 ;
				int intActivatedFour  = intTmp * (intTmp - 1) * (intTmp - 2) * (intTmp - 3) *  1           * (intTmp - 5) / -24;
				int intActivatedFive  = intTmp * (intTmp - 1) * (intTmp - 2) * (intTmp - 3) * (intTmp - 4) *  1           / 120;
				
				Echo(intTmp+"--"+intActivatedZero+"-"+intActivatedOne+"-"+intActivatedTwo+"-"+intActivatedThree+"-"+intActivatedFour+"-"+intActivatedFive+"-IC:" + Runtime.CurrentInstructionCount);
				
				//Echo("centerFacePosition "+centerFacePosition);
				
				Vector3D vectorToFaceCenter = myPos - centerFacePosition;
				
				//Echo("vectorToFaceCenter "+vectorToFaceCenter);
				
				double distanceToFaceCenter = vectorToFaceCenter.Length();

				//Echo("distanceToFaceCenter "+distanceToFaceCenter);

				//sqrt(2)/2 was used for a 1D case
				//Actually it would need about sqrt(2)*sqrt(1-sqrt(2)/2) in threshold for the 2D case
				//Let s simplify the logic and just use one for the 3d
				if (distanceToFaceCenter < 1 * planet_radius)
				{
						Echo("face close enough to try to generate");
					
					int intXsubPattern = 0;
					int intYsubPattern = 0;
					
					int[] subIntSubPattern = Enumerable.Range(0, constantNumbersOfSubPatternToGenerate).ToArray();
					
					double intX = 0;
					double intY = 0;
					double intZ = 0;
					
					Vector3D generated_gps_point_on_cube = new Vector3D(0,0,0);
					
					
					List<float> centroid_surface_lack_planetSized = new List<float>();
					centroid_surface_lack_planetSized.Add(0);
					centroid_surface_lack_planetSized.Add(0);
					
					
                    //centroid_surface_lack_planetSized = arr.array('d', [2*planet_radius* (centroid_surface_lack_array[0]/2048),2*planet_radius* (centroid_surface_lack_array[1]/2048)])
                    // print("centroid_surface_lack_planetSized:",centroid_surface_lack_planetSized)
					
					foreach(int subXindex in subIntSubPattern){
						//Echo("subXindex"+subXindex);
						foreach(int subYindex in subIntSubPattern){
							//Echo("subYindex"+subYindex);	
							int oreCoordSubPatternIndex = 0;
							foreach(Vector2D oreCoordSubPattern in oreCoords2DSubPattern){
								//Echo("oreCoordSubPattern"+oreCoordSubPattern);
								/*centroid_surface_lack_planetSized[0] = (128 * subXindex+oreCoordSubPattern.X) * (2*planet_radius/2048);
								centroid_surface_lack_planetSized[1] = (128 * subYindex+oreCoordSubPattern.Y) * (2*planet_radius/2048);*/
								centroid_surface_lack_planetSized[0] = Convert.ToSingle((subPatternSize * subXindex+oreCoordSubPattern.X) * (2*planet_radius/2048));
								centroid_surface_lack_planetSized[1] = Convert.ToSingle((subPatternSize * subYindex+oreCoordSubPattern.Y) * (2*planet_radius/2048));
								/*
								bool display = false;
								//TL
								if(subPatternSize * subXindex+oreCoordSubPattern.X<3    && subPatternSize * subYindex+oreCoordSubPattern.Y<3){
									display =  true;
								}
								//BR
								if(subPatternSize * subXindex+oreCoordSubPattern.X>2040 && subPatternSize * subYindex+oreCoordSubPattern.Y>2040){
									display =  true;
								}
								//TR
								if(subPatternSize * subXindex+oreCoordSubPattern.X<3    && subPatternSize * subYindex+oreCoordSubPattern.Y>2040){
									display =  true;
								}
								//BL
								if(subPatternSize * subXindex+oreCoordSubPattern.X>2040 && subPatternSize * subYindex+oreCoordSubPattern.Y<3){
									display =  true;
								}
								*/
								
								/*
								Echo("=="+intTmp);
								Echo("--"+intActivatedZero);
								Echo("--"+intActivatedOne);
								Echo("--"+intActivatedTwo);
								Echo("--"+intActivatedThree);
								Echo("--"+intActivatedFour);
								Echo("--"+intActivatedFive);
								
								Echo(intTmp+"--"+intActivatedZero+"-"+intActivatedOne+"-"+intActivatedTwo+"-"+intActivatedThree+"-"+intActivatedFour+"-"+intActivatedFive);
								Echo("IC:" + Runtime.CurrentInstructionCount);
								*/
								
								
								
								//if(intTmp==0){
									intX = 1*(- planet_radius+centroid_surface_lack_planetSized[1]*1);
									intY = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1);
									//intZ = planet_radius * (centroid_surface_lack[1]-2048/2) * planet_radius;
									Vector3D zeroV = new Vector3D(intX, intY,planet_radius);
								//}
								//if(intTmp==1){
									//original
									intX = 1*(- planet_radius+centroid_surface_lack_planetSized[1]*1);
									//intY = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1);
									intZ = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1);
									Vector3D oneV = new Vector3D(intX,-planet_radius, intZ);
									
									//pertam ok tested
									//rotating 90 clock wise
									intX = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1);
									//intY = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1);
									intZ = -1*(- planet_radius+centroid_surface_lack_planetSized[1]*1);
									Vector3D oneV2 = new Vector3D(intX,-planet_radius, intZ);
								//}
								//if(intTmp==2){
									intX = -1*(- planet_radius+centroid_surface_lack_planetSized[1]*1);
									intY = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1);
									//intZ = planet_radius * (centroid_surface_lack[1]-2048/2) * planet_radius;
									Vector3D twoV = new Vector3D(intX, intY,-planet_radius);	
								//}
								//if(intTmp==3){
									// intX = 1*(- planet_radius+centroid_surface_lack_planetSized[1]*1);
									intY = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1);
									intZ = -1*(- planet_radius+centroid_surface_lack_planetSized[1]*1);
									Vector3D threeV = new Vector3D(planet_radius,intY, intZ);
								//}
								//if(intTmp==4){
									//intX = 1*(- planet_radius+centroid_surface_lack_planetSized[1]*1);
									intY = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1);
									intZ = 1*(- planet_radius+centroid_surface_lack_planetSized[1]*1);
									Vector3D fourV = new Vector3D(-planet_radius,intY, intZ);
								//}
								//if(intTmp==5){
									intX = -1*(- planet_radius+centroid_surface_lack_planetSized[1]*1);
									// intY = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1);
									intZ = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1);
									//generated_gps_point_on_cube = arr.array('d', [intX,planet_radius, intZ,]+center_of_planet);
									Vector3D fiveV = new Vector3D(intX,planet_radius, intZ);
									
									//up petarm to be tested (need same rotation as the down face) look at 553 1176 the leaf shape as a land mark
									//rotating 90 clock wise
									intX = 1*(- planet_radius+centroid_surface_lack_planetSized[0]*1);
									// intY = -1*(- planet_radius+centroid_surface_lack_planetSized[0]*1);
									intZ = -1*(- planet_radius+centroid_surface_lack_planetSized[1]*1);
									//generated_gps_point_on_cube = arr.array('d', [intX,planet_radius, intZ,]+center_of_planet);
									Vector3D fiveV2 = new Vector3D(intX,planet_radius, intZ);
								//}
								generated_gps_point_on_cube = intActivatedZero   * zeroV ;
								generated_gps_point_on_cube += intActivatedOne   * oneV * (allActiveButPertam);
								generated_gps_point_on_cube += intActivatedTwo   * twoV;
								generated_gps_point_on_cube += intActivatedThree * threeV;
								generated_gps_point_on_cube += intActivatedFour  * fourV;
								generated_gps_point_on_cube += intActivatedFive  * fiveV * (allActiveButPertam);
								
								generated_gps_point_on_cube += intActivatedOne * oneV2 * (activePertam);
								generated_gps_point_on_cube += intActivatedFive * fiveV2 * (activePertam);
					
								Vector3D generated_gps_point_on_planet = new Vector3D(0,0,0);
								
								//Echo("generated_gps_point_on_cube"+generated_gps_point_on_cube);
								
								
								Vector3D generated_gps_point_on_cube_norm = Vector3D.Normalize(generated_gps_point_on_cube);
								
								
								generated_gps_point_on_planet =  planet_radius * Vector3D.Normalize(generated_gps_point_on_cube_norm)+ cubeCenter;
								
								generated_gps_point_on_planet = Vector3D.Round(generated_gps_point_on_planet,1);
								
								//Echo("generated_gps_point_on_planet"+generated_gps_point_on_planet);
								
								string oreNames = stringListOres[oreCoordSubPatternIndex];
								
								//Echo("oreNames"+oreNames);
								
								//MyWaypointInfo tmpWPI  = new MyWaypointInfo("test", generated_gps_point_on_planet);
								MyWaypointInfo tmpWPI  = new MyWaypointInfo(oreNames, generated_gps_point_on_planet);
								
								//Me.CustomData = "lol6";
								//Me.CustomData = tmpWPI.ToString();
								
								//enerated_gps_point_on_planet = (planet_radius+alt_adj) * (
                                //generated_gps_point_on_cube / np.linalg.norm(generated_gps_point_on_cube))+center_of_planet
								
								
								
								Vector3D vectorToGPSgenerated = tmpWPI.Coords - myPos;
								
								//Echo("vectorToGPSgenerated"+vectorToGPSgenerated);
								if(vectorToGPSgenerated.Length()<rangeToCheckForOres){
									//generatedGPSs.Add(oreCoordSubPattern.ToString());
									//generatedGPSs.Add(generated_gps_point_on_cube.ToString());
									//if(display == true){
										//if(intTmp==5){
											generatedGPSs.Add(tmpWPI.ToString());
										//}
									//}
									//generatedGPSs.Add("===========");
									Echo("one close enough");
								}
								
								
								oreCoordSubPatternIndex +=1 ;
								
								
								
								//return;
							}
							//oreCoords2DSubPattern
						}
					}
				}
				
				
			}
		}
		else {
			Echo("unidentified planet");
			return;
		}
	}
	
	foreach(string stcTmp in generatedGPSs){
		customDataStrBuild += "\n"+stcTmp;
	}
	
	
	Me.CustomData = customDataStrBuild;
	

	Echo("IC:" + Runtime.CurrentInstructionCount);

    //generate all coordinates to the cube planet

    //get the first three closest faces.
    
    //try to get closer with the first ores in the subpattern infos? (quicker to run ?)

    //then generated all ores nearby

    //sort and write those closest in the Custom Data



    //put the script output inside the customdata of the PB it is running onto
    //Me.CustomData = "";
}
