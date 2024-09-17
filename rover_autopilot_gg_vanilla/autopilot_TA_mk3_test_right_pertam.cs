
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


string nodesStringRight = "09ku05|0E3L0x0D|0Jhc0a0n|0QiL0a0r1e1f1g1h1j|0Tf1070809|0Tkz000b0c0f|0Wce0s0A|0Wfk040n0w|0Ye_040u0F|0Ze_040u0F|0Zhz02031e1f1g1h1j|0Zk5050r1l|0ZkK050g0R|0-770t0W|0-9j0k0o|0-kL050g0R|10lD0c0f0R|13dt0p0A|158m0l0m|15bf0q0E|198W0e0m0N0O0P|1b850i0M|1d8x0i0k0J|1fga02070w|1i9r0e0C0S|1jec0h0u|1lbu0j0s0L|1njh030b1k|1obW060q0U|1p6Q0d0v|1pej08090p0F|1t6P0t0W10|1ug5070n1n1p|1z4e010y0T|1A540x10|1B3e0D0Q|1DcM060h0I|1H0F0-1P1Q1S|1H9H0o0H12|1J3y010z0T|1KaB0j0G0H|1Lf208090u1d|1MaC0E0K19|1Nao0C0E11|1OcH0A0U14|1Q8C0m0M0N0O0P|1QaI0G0L17|1Qbe0q0K13|1R860l0J0Y|1R8H0k0J0S|1R8I0k0J0S|1S8J0k0J0S|1T2R0z0Z|1Tlp0c0f0g1B|1U8N0o0N0O0P16|1V3E0x0D0_|1Wcu0s0I0V|1Ycu0U0X1a|1Z7w0d0v0Y|1Zct0V1315|217x0M0W1t|242K0Q0-0_|2a2k0B0Z1Z1-1_|2i3v0T0Z1T|2i5A0v0y1y|2k9Y0H1219|2l9v0C1116|2rbx0L0X15|2rdh0I1c1d|2ubx0X1318|2v9d0S121G|2vaO0K181b|2vbx15171q|2yap0G111b|2Bcr0V1i1q|2Dau17191o|2Edd141i1m|2Hfv0F141n1p|2MhC030a1s|2OhC030a1s|2QhC030a1s|2ThC030a1s|2UcS1a1c1L1M|2UhC030a1s|2Vjl0r1l1D|2Zj-0b1k1B|2-dv1c1C1E|2_g20w1d1r|30ax1b1u1K|30g30w1d1r|31bD181a1u|37g71n1p1z1J|3ehC1e1f1g1h1j1A1D|3f7e0Y1v1w1x1H|3fbB1o1q1N|3h6C1t1y25|3h6D1t1y25|3h6E1t1y25|3i5z101v1w1x1T|3jgr1r1A24|3jgy1s1z28|3nlb0R1l22|3pdV1m1I1J|3rij1k1s1X|3Cdb1m1I1L1M|3H8H1G1H1V|3H8I161F1K|3I831t1F25|3Kdv1C1E1O|3Lf61r1C1U|3U9x1o1G1V|3Uco1i1E1N|3Vcn1i1E1N|4ac41u1L1M1W|4gdH1I1R1Y|4m2K0B20|4m2L0B20|4mdA1O1W1Y|4n2M0B20|4o4c0_1y1Z1-1_|4seF1J1Y24|4vai1F1K21|4xc11N1R21|4Bis1D2a2i|4Ne81O1R1U|4R3R0-1T20|4T3Q0-1T20|4U3Q0-1T20|4_3M1P1Q1S1Z1-1_23|50bQ1V1W27|59nG1B2A|5b3K202o2V|5hfC1z1U26|5l7A1v1w1x1H2l|5lfC242c2q|5rc3212q2O|5rgQ1A2a2d|5A1a2o|5BhW1X282b|5ChW2a2k2r|5FfG262e2p|5HgK282f2g2j|5Tgp2c2f2g2h2u|5Tgq2d2e2h2j|5Tgr2d2e2h2j|5Ugq2e2f2g2j2u|61ka1X2m2s|62gW2d2f2g2h2r|62is2b2m2v|687P252n2B2C2E2I|69iF2i2k2v|6f8a2l2E|6k2B23292_|6tfq2c2D2G|6vdd26272F|6vhs2b2j2y|6Blc2i2z2H|6Ca72E2O|6CgI2e2h2x2D|6Giy2k2m2z|6K9v2E2O|6KgU2u2y2T|6Lhn2r2x2K|6PiM2s2v2S2U|6Qn8222H3W|6X9h2l2E|6X9i2l2E|6XfD2p2u2J2L2M|6Y9n2l2n2t2w2B2C2O32|73ds2q2G2P|73dt2p2F3a|73mx2s2A35|767J2l2N2X|79fz2D2W31|79hG2y2Q2R33|7afz2D2W31|7bfz2D2W31|7k7P2I2Y2Z|7oaD272t2w2E2-|7rdd2F2-3c|7Di82K2S2U33|7Ei92K2S2U33|7Jil2z2Q2R35|7Kgx2x2W37|7Kim2z2Q2R35|7O5E232X36|7SfO2J2L2M2T39|7X5V2I2V3e|84812N2Z3e|848h2N2Y32|87aV2O2P32|891H2o383h|8eeG313a3g|8ofr2J2L2M303b|8p9A2E2Z2-|8phm2K2Q2R34|8qhm333537|8yi-2H2S2U34|8F4G2V383v|8KgC2T3439|8N432_363j|8Of_2W373b|8Pel2G303d|8QfY31393g|8-df2P3d3f|8-e03a3c3l|92762X2Y3i|9acK3c3m3z|9hfA303b3k|9j022_3J|9s7m3e3I4q|9v47383t3v|9vfu3g3l3Q|9web3d3k3z|9xcR3f3q3T3V3X3Z3_|9Bc93q3r41424345464748494a4b4c4d4e4f4g4h4i4j4l4m|9Gaf3p3B3C3I|9Hah3o3r3u|9Jcu3m3n44|9Lb23n3p3s|9Ra_3r3x3y|9S3K3j3F3J|9Vak3p3w3B3C|a04q363j3N|a1aB3u3x3L3M|a2aH3s3w3y|a3bd3s3x3D|a8dw3f3l3T3V3X3Z3_|aau63H4z4A4B4C4D4E4F|ab9R3o3u3K|ac9Q3o3u3K|acbi3y3E3G|ahbx3D3Y41424345464748494a4b4c4d4e4f4g4h4i4j4l4m|ai3R3t3N3S|akb43D3P3R|aktx3A3W58|an9d3i3o3O|ar2x3h3t3S|at9w3B3C3O3U|ataq3w3P3U|atar3w3P3U|au493v3F4k|au9p3I3K4p|auaS3G3L3M3-|aufz3k4051|awbk3G3Y3-|ax3g3F3J4n|aydm3m3z40|aza53K3L3M3-|aAdl3m3z40|aArx2A3H5j|aBdl3m3z40|aCbw3E3R4G4H4I|aDdk3m3z40|aEb23P3R3U|aEdk3m3z40|aIdj3Q3T3V3X3Z3_44|aKcB3n3E4o|aNcE3n3E4o|aOcF3n3E4o|aOdf3q404o|aPcF3n3E4o|aQcG3n3E4o|aRcG3n3E4o|aScH3n3E4o|aTcH3n3E4o|aTcI3n3E4o|aTcJ3n3E4o|aUcJ3n3E4o|aVcK3n3E4o|aWcK3n3E4o|aXcL3n3E4o|aYcL3n3E4o|a_cO3n3E4o|b0cO3n3E4o|b1cP3n3E4o|b34a3N4n4r|b5cS3n3E4o|b7cT3n3E4o|ba3R3S4k4x4y|bdcY4142434445464748494a4b4c4d4e4f4g4h4i4j4l4m4G4H4I|bq9z3O4t4P|bB5T3i4r4s|bH5n4k4q4x4y|bI5_4q4w4T4U|bM8Z4p4u4S|bO8m4t4v4M|bT7d4u4w4R|bZ6L4s4v4V|b-574n4r595a5b|b_564n4r595a5b|ceuj3A4J4K4L4N4O4Q|cfuj3A4J4K4L4N4O4Q|cgui3A4J4K4L4N4O4Q|chuj3A4J4K4L4N4O4Q|ciuj3A4J4K4L4N4O4Q|ckuj3A4J4K4L4N4O4Q|cmuj3A4J4K4L4N4O4Q|crcA3Y4o4P|cscA3Y4o4P|cucz3Y4o4P|cxuk4z4A4B4C4D4E4F4X4-5m5o5x|cyuk4z4A4B4C4D4E4F4X4-5m5o5x|cAuk4z4A4B4C4D4E4F4X4-5m5o5x|cB8t4u4R4S|cBuk4z4A4B4C4D4E4F4X4-5m5o5x|cDul4z4A4B4C4D4E4F4X4-5m5o5x|cIcx4p4G4H4I50|cIul4z4A4B4C4D4E4F4X4-5m5o5x|cM8c4v4M4W|cO954t4M4_|d15S4s5e5f5h5i|d25R4s5e5f5h5i|dd774w4W5d|dn7T4R4V4Y|dsur4J4K4L4N4O4Q5x|dt7Y4W4Z54|dt894Y4_54|dtus4J4K4L4N4O4Q5x|du964S4Z5t|dFcU4P515253|dOeW3Q505n6g6h|dYcH505556575u5V5X|dZcG505556575u5V5X|d-824Y4Z5s|e4cG52535X|e5cG52535X|e8cG52535X|edsP3H5c5l|el4w4x4y5h|en4z4x4y5h|eo4A4x4y5h|essp585g5j|ev6y4V5e5f5i5p|eG5N4T4U5d5k|eG5O4T4U5d5k|eGsp5c5l5q|eI584T4U595a5b5k|eI5G4T4U5d5k|eNrG3W5c5q|eQ5e5e5f5h5i5L|e-th585g5m|f0tm4J4K4L4N4O4Q5l5r|f6gh515A5E6g6h|fav94J4K4L4N4O4Q5x|fk7D5d5s5J5K|fls95g5j5w|fut65m5w5C|fC8T545p5v|fC9o4_5u5v|fD9G52535t5D|fI985s5t5O|fIsh5q5r5y5z|fSvR4J4K4L4N4O4Q4X4-5o5Y|g5ss5w5B5C|g6st5w5B5C|gsiW5n5E5F|gtrN5y5z5M5P5Q5R5T|gtsM5r5y5z5P5Q5R5T|gxad5u5G6b|gAgt5n5A5N|gNi-5A5H5I68|gXaJ5D5X60|h0iy5F5N626364|h0iz5F5N626364|h57b5p5O5S|h67c5p5O5S|h83_5k5S67|hkrr5B5Z5-5_61|hlgx5E5H5I65|ho7y5v5J5K5U|hxt85B5C5W|hyt95B5C5W|hzta5B5C5W|hA6o5J5K5L5U|hAta5B5C5W|hI6o5O5S67|hPc552535X|hPti5P5Q5R5T5Y5Z5-5_|hSbZ52535556575G5V60|hVts5x5W6t6J6K6N6O|h-s25M5W61|h-s35M5W61|h-s45M5W61|h_bC5G5X6a|i2rV5M5Z5-5_6k|i4hs5H5I6568|i4hu5H5I6568|i4hv5H5I6568|i7gE5N6263646x|i8ph6f6i|ig2A5L5U6c|igjh5F6263646d|iind6g6j6l6m6p|ilbo606b6G|iG9q5D6a6z|iK1Y676w|iKjD686r6u|iOoO6f6i|iXoC666e6h6o|iYnK515n696h6j6l6m|iYo6515n6f6g6i6o6Q|iYo8666e6h6o|j1np696g6s|j1qS616n6N6O|j2nn696g6s|j2no696g6s|jbqH6k6o6P|jips6f6h6i6n6Q|jmm9696r6s|jpcZ6v6A6V|jqlg6d6p6F|jtmk6j6l6m6p6U|jtst5Y6N6O|jzj96d6y6R|jAcY6q6A6Y|jI3s6c6L7p|jPhG656y76|jQiv6u6x76|jS966b6C6D6E6G|jXcm6q6v6T|k36R6H6M7z7A7B|k48g6z6H81|k48i6z6H81|k48j6z6H81|k4k-6r6I6W|k69x6a6z7e|k7836B6C6D6E7z7A7B|k9kW6F6R6S|k9rO5Y6N6O|karN5Y6N6O|kf5b6w6M8c8d8e|kf5u6B6L89|kkrq5Y6k6t6J6K6P|klrp5Y6k6t6J6K6P|kpqM6n6N6O6Q|kqpN6h6o6P|krjH6u6I6Z|ktkV6I7073|kAcm6A6Y7C|kAmi6s6W7c|kBep6q6-74|kClE6F6U70|kDvx6_72|kRdC6v6T6-|kRjI6R7378|kVdU6V6Y77|kWua6X7172|kZlj6S6W79|l1u16_7a7K|l3uK6X6_|l6kD6S6Z79|l7fL6V7d7o|l7op7b7w8i|l8i-6x6y78|l9dW6-7u7v|lgj76Z767c7r7t|lplh70737c|lpsI717f88|lup1757q7w|lvl_6U78797r7t|lxgK747s7E|lKa06G7U838485|lRs07a7n7F|lU0x7j7k7l7m8M|lV0y7j7k7l7m8M|lW0y7j7k7l7m8M|lX0y7g7h7i7k9f9i9j9v|lX0z7g7h7i7j7l7m8M9f9i9j|lZ0z7g7h7i7k9f9i9j9v|l-0z7g7h7i7k9f9i9j9v|m1qz7f7q7x|m3fM747s7v|m63S6w8b8o|m7pM7b7n7y|m8kG787c|mbgh7d7o7L|mbkt787c|mddj777C7G|mefE777o7L|mgoy757b7I|mjqJ7n7F7P|mopD7q7I7O|mp7g6B6H8k|mq7g6B6H8k|mu7g6B6H8k|muc36T7u7D|myc27C7H7V|mBht7d7L7W|mBrJ7f7x7S|mFdA7u7M7N|mIbK7D7U8A|mJoI7w7y7J|mKoI7I808P|mKuC717Y|mLgN7s7v7E|mSdw7G7N7Q|mSeu7G7M8l8q8u|mVpX7y7P7R|mWqh7x7O7X7Z|n2cZ7M7V8t|n4pR7O808J|n4rT7F7T7-|n5rU7S8v8x|naaf7e7H7_|nacG7D7Q8j|nekB7E8f8_|noqF7P7-8H8I|nouf7K8286878g8m8w|npqE7P7-8H8I|ntqT7S7X7Z8n|nu9v7U8384858Z|nupp7J7R8E|nv8g6C6D6E8384858a|nvt_7Y889E|nw8v7e7_81|nw8w7e7_81|nw8x7e7_81|nwtZ7Y889E|nwt-7Y889E|nCtp7a8286878v|nF6P6M8c8d8e8k|nF7U818s9l|nH4N7p8h8p8r|nH606L898h|nH616L898h|nH626L898h|nHld7W8i93|nHuA7Y9B9D|nI5q8b8c8d8e99|nIlq758f8O|nJcM7V8y8z8B|nL7m7z7A7B898s|nLeX7N8C8W|nLuE7Y9B9D|nMqX7-8x8H8I|nN477p8p8r8M|nN4v8b8o9H|nNeZ7N8C8W|nO4u8b8o9H|nO7s8a8k9T|nOdx7Q8B8K|nOeZ7N8C8W|nOsT7T8890|nOuJ7Y9B9D|nRr87T8n90|nUcz8j8L9p|nVcy8j8L9p|nXbB7H8L8X|n-dd8j8t8Y|n-ek8l8q8u8D8F8G8-|n_e88C8K9x|n_pF808J8U|o0e68C8K9x|o0e78C8K9x|o0qh7X7Z8n8N|o0qi7X7Z8n8N|o1pO7R8E8N|o4dQ8t8D8F8G8Y|obbU8y8z8A8V|od2u7g7h7i7k8o97989a|ofq18H8I8J9e|ohm48i8T9294|ohnH7J8Q8R|oiny8P8T9c|oknM8P8S9h|oon-8R8U9d|ormx8O8Q95|oroG8E8S96|oubZ8L8X9g|ovfr8l8q8u8-b9|oxaE8A8V8Z|oydj8B8K9u|oBaC7_8X91|oBeX8C8W9x|oFjN7W9bb9|oGrk8v8x9k|oHaB8Z9l9I|oHlM8O939t|oJkL8f92949b|oJlL8O939t|oKmA8T9c9F|oMoQ8U9m9n|oP1V8M9f9j9A9C|oQ1U8M9f9j9A9C|oQ5j8h9y9O9P9Q|oR1T8M9f9j9A9C|oRkL8_939q|oRmZ8Q959w|oUo78S9m9o|oVqc8N9k9n|oW1G7j7k7l7m97989a9i|oXcd8V9p9J|oXnK8R9o9R|oY1E7j7k7l7m9f9v|oY1F7j7k7l7m97989a9v9A9C|oYr0909e9L|o-9L8a91aC|o-oH969d9r9Za6|o-q7969e9-|p1o99d9ha0|p2cO8y8z9g9u|p3k-9b9sa2|p4oF9ma6|p7lq9q9taj|p7lu92949s9N|p8c_8Y9p9K9M|pb0T7j7l7m9i9j|pcmY9c9G9U|peet8D8F8G8-a9|pi5E999z9S|pk609y9S9T|pl1T97989a9jaLaM|pmvs8g8m8w9E|pn1U97989a9jaLaM|pnvt8g8m8w9E|ppuu8286879B9D9W|pqmv959G9N|pqmz9w9F9X|pr4C8p8r9O9P9Qa3|praU919Jae|psbI9g9Ias|ptdm9ua9abac|ptre9k9_a1|pudn9ua9abac|pum79t9Fap|pv4U999H9V|pw4V999H9V|pw4W999H9V|pwnt9h9U9Y|pz5h9y9z9V|pB7f8s9zaa|pCn09w9R9X|pD549O9P9Q9Sad|pDu99Ea4a7|pEmS9G9Uar|pHnF9Ra0aq|pHou9ma6|pIpJ9na5aZb0|pIr99Lb7bc|pKn-9o9Ya8|pKrY9La7bc|pLkA9qaibebf|pN4k9Hamax|pUue9Wafau|pVoO9-a6aZb0|pZor9m9r9Za5a8|p-sT9Wa1aD|p_oca0a6aE|q0em9x9K9Mat|q27i9Tagahb1|q7cK9K9MawaHaK|q8cJ9K9MawaHaK|qb5c9Vakal|qbap9IasaA|qbu6a4aBaD|qd6uaaalaQ|qe6taaalaQ|qfk-a2ajaF|qfld9saiao|qg55adanb2|qg5ladagahaQ|qh4na3anaN|qj4PakamaR|qjlhajapaG|qkm89Naoav|qknj9YaraE|qmmE9Xaqav|qrbK9JaeaJ|qreFa9awaTaVaWaXaY|qsuXa4az|qumpaparay|qzcKabacataI|qD3ta3aNb3|qDmlavaSbd|qDuUauaB|qI9NaeaCb6|qIu4afazbw|qK9j9laAa_|qPsTa7afbw|qQnVa8aqbm|qTkiaiaGbobr|qTlfaoaFaO|qUb-abacaIaJ|qUcfawaHaKaTaVaWaXaY|qVbWasaHaKaP|qVbZabacaIaJ|q-1l9A9CbO|q-1m9A9CbO|r041amaxaR|r0ljaGaSbnbpbsbt|r2bRaJb6c7c8c9cbcd|r35Gagahalb8|r443anaNaU|r5lzayaOba|r9fkataIbj|ra45aRa-b5|rbfmataIbj|rdfnataIbj|refoataIbj|rgfpataIbj|riq29-a5b7|rj4qaUb2bu|rj8taCb1bSbTbUbVbWbXbYbZ|rjq39-a5b7|rk8paaa_bq|rn4_aka-b8|rq2Iaxb4bk|rv2_b3b5bi|rz3raUb4bi|rAbkaAaPbSbTbUbVbWbXbYbZ|rBqa9_aZb0bE|rC5saQb2bz|rJhi8W8_bebf|rQlUaSbbbnbpbsbt|rTm1babdbN|rTra9_a1bI|rVmhaybbbh|rWhha2b9bg|rXhga2b9bg|r_hfbebfbjbobr|s0mtbdblbN|s53ob4b5bv|s5gdaTaVaWaXaYbgc7c8c9cbcd|s92rb3bAbK|sfo3bhbmce|sfobaEblbxby|sgk6aObabH|shhDaFbgbF|shk3aObabH|si8ab1bzck|sihDaFbgbF|sik1aObabH|sjj_aObabH|sk44a-bvbJ|sm3tbibubA|sosKaBaDc5|sxoKbmbBbCbDc4|syoLbmbBbCbDc4|sC5Pb8bqbJ|sD3jbkbvbG|sKpzbxbybEct|sKpAbxbybEct|sKpBbxbybEct|sKqvb7bBbCbDbI|sNi5bobrbH|sO3jbAbLbR|sViHbnbpbsbtbFb_|sWqUbcbEc5|t05kbubzbM|t11HbkbPbQ|t137bGbQc2|t15jbJbRc6|t3lwbbbhc0c1|ta0HaLaMbP|tb19bKbOcj|tc2LbKbLb-|ti4JbGbMcacc|ttbCa_b6cC|tubDa_b6cC|tvbCa_b6cC|twbCa_b6cC|txbCa_b6cC|tzbDa_b6cC|tCbEa_b6cC|tDbEa_b6cC|tR2VbQc3cfci|tVkKbHc0c1|tXkRbNb_ce|tXkSbNb_ce|tZ3nbLc3cg|t-3fb-c2cfci|t_nkbxbychcl|u0sgbwbIcB|u15wbMcncq|u7dHaPbjcC|u9dFaPbjcC|uadEaPbjcC|ub46bRcgcr|ubdDaPbjcC|uc45bRcgcr|ucdCaPbjcC|uhlvblc0c1ch|ul2Jb-c3cj|ul3Rc2cacccr|ullzc4cecm|um2Ib-c3cj|uo2EbPcfcico|uw9kbqcp|uFmWc4cmcs|uHlQchcl|uI63c6cpcz|uM2AcjcycA|uM6ockcn|uO51c6cwcz|uR4jcacccgcu|uSnPclct|uTnXbBbCbDcscv|v14ncrcwcx|v1o3ctcB|v54rcqcu|va3WcucA|vg1Zco|vl5Bcncq|vt3kcocx|vut9c5cv|vHc8bSbTbUbVbWbXbYbZc7c8c9cbcd";

string nodesStringFront = "001i0b0i|01ff0d1F|09lV0n0Z|0lsh051w|0n520g0j|0nsh032n2r2s2N2O|0o520g0j|0oho0k0u|0p520g0j|0y4p0a0c|0R4a090c0C|0U1M000B0D|0V4u090a0j|0VfW010f0h1e1g1i|0WdI0x0P0Q|0Wg60d0y|0X5p0406080l0m0p12|0Xg70d0y|0Z1a000q0D|0Z4y0406080c0F0G0H|0ZhS070u0z|0_620g|10630g|10mq020s13|10m-0s0v|11630g0O1U|12130i0V|12jR0A141516|12mu0n0o0J|142K0B1l|15gZ070k0w|15m_0o0J0K0L|17gW0u0y0U|18cM0e0I|19gz0f0h0w0R|19iu0k0W|19ke0r10|1b2q0b0t0E|1b3_0a0F0G0H1l|1c1x0b0i0N|1d2o0B0N11|1g4D0j0C12|1h4D0j0C12|1i4D0j0C12|1icN0x0M0X|1imT0s0v181b|1knI0v0-|1lnJ0v0-|1ocR0I0P0Q1f|1p1H0D0E0S|1t9m0p0_|1vdZ0e0M1z|1wd-0e0M1z|1xgq0y1e1g1i1j|1z1G0N0T0Y|1A1H0S111x|1Ch40w191k|1D150q0Y|1FiK0z171n1p1u|1Hce0I0_1O|1J1f0S0V1J|1Jl502101a|1Jn-0K0L1c1w|1K9m0O0X2h|1Lk20A0Z141516|1N1Z0E0T1q|1N4T0g0F0G0H1T|1Pmg0n181b1h|1QjH0r1017|1QjI0r1017|1RjG0r1017|1TjA0W1415162m2p2q|1VmZ0J131c|1WhH0U1n1p1u1E|1Wlc0Z1h1I|1Wm-0J131c|1Wm_0-181b1d|1Xm_1c1C2c|1YfK0d0R1D|1-d50M1K1O|1-fJ0d0R1D|1-m7131a1m|1_fI0d0R1D|20gJ0R1k1A|20gK0U1j1o1t|243w0t0C1s|2am91h1C1I|2civ0W191R|2dgL1k1A1B|2div0W191R|2e20111r1v1x|2e3y1q1s1Q|2e3z1l1r1v1V|2egM1k1A1B|2eiu0W191R|2f3w1q1s1Q|2fpr030-21|2g1Y0T1q1y|2h1X1x1J1M|2jeo0P0Q1K1L|2jfV1j1o1t1D|2jgS1o1t1E22|2nmn1d1m24|2rfq1e1g1i1A1F|2rhp191B1N|2tfn011D1G|2ufn1F1H1L|2vfp1G1W2Q|2vli1a1m1Y1_20|2w1k0Y1y1-|2we31f1z26|2wf51z1G2X|2x1X1y1P292a|2AhA1E1S2g|2Bcz0X1f1X|2L2i1M1Q292a|2N2Y1r1v1P2C|2Nir1n1p1u1S2l|2Oib1N1R2o|2R50121U1V|2R6l0p1T2h|2X4J1s1T2Y|2Ygd1H222R2S2T|2-cu1O282z|2-la1I2325272m2p2q|2-ma232425272t|2_1b1J2k|2_la1I2325272m2p2q|30l91I2325272m2p2q|30oz1w2c3Q|31gt1B1W2b2d2e|31la1Y1Z1_202t|31mJ1C1Z2f|32la1Y1Z1_202t|33dK1K282U2V2W|33la1Y1Z1_202t|34dI1X262P|351E1M1P2j|371D1M1P2j|37gz222i2G2H|37n31d212f|38gA222i2G2H|39gB222i2G2H|39mX242c3d|3bhr1N2i2o|3c8V0_1U2I|3dhl2b2d2e2g2G2H|3f1s292a2k2C|3i1j1-2j3B|3ij71R2u2J2K2L|3ijz171Y1_202u|3irB052A2B2D4849|3jhS1S2g2J2K2L|3jjy171Y1_202u|3kjy171Y1_202u|3krB052A2B2D4849|3lrA052A2B2D4849|3ml81Z2325272F|3ojx2l2m2p2q2x|3oum2M2Z|3pun2M2Z|3qjy2u2F3C|3qun2M2Z|3rbV1X2E38|3vrC2n2r2s3h3j|3wrC2n2r2s3h3j|3y2M1Q2j2Y|3yrC2n2r2s3h3j|3SaZ2z2I3f|3WkY2t2x36|3Zg-2b2d2e2i3033|40g_2b2d2e2i3033|499q2h2E3K|4ahN2l2o3e|4bhM2l2o3e|4chM2l2o3e|4duq2v2w2y3539|4etb052-313439|4ftc052-313439|4gdT282U2V2W38|4jfx1H2R2S2T2_|4jfL1W2Q3033|4jfM1W2Q3033|4jfN1W2Q3033|4leK262P2X|4leL262P2X|4meM262P2X|4meN1L2U2V2W2_|4s3f1V2C32|4tvl2v2w2y37|4wta2N2O3g3m|4xf02Q2X3a|4xh32G2H2R2S2T3e|4xt92N2O3g3m|4y3d2Y3o3B|4yh42G2H2R2S2T3e|4yt92N2O3g3m|4zuH2M373b|4Dlm2F3d3z|4DuQ2Z353i|4Hd62z2P3f|4Iuc2M2N2O3c|4Me-2_3w3N|4Nuo353c3n|4Ouj393b3g|4PlL2f363Q|4Qhm2J2K2L30333A|4XcZ2E383M|4Yu62-31343c3s3t|53sm2A2B2D3j3l|5buV373k3n|5crP2A2B2D3h3v|5eu_3i3J|5gsM3h3m3p3q3r|5hsV2-31343l3D|5juA3b3i3s3t|5k4i323u3F|5lsD3l3v3x|5lsE3l3v3x|5lsF3l3v3x|5qu93g3n3D|5qua3g3n3D|5s4E3o3y3X3-42|5wr_3j3p3q3r3x|5zes3a3M3T|5Bs13p3q3r3v3S|5D5F3u3E3_|5HjN363C4D4E|5JhO3e3C3N|5L0D2k32|5LiC2x3z3A|5RtL3m3s3t3G3H|61653y3U3V3W3Y40|643Q3o3R3X3-41|64tW3D3L4n|64tX3D3L4n|68vk3J3Z|6avb3k3I3L|6fa12I3P464s4A4K4M4W4Z|6ruE3G3H3J3Z|6ycN3f3w3P|6ygL3a3A3O|6AgK3N3T4g|6DcJ3K3M4g|6Dnl213d4m|6H3C3F4447|6HrU3x4b4c4d4i|6KfG3w3O4g|6Q6v3E454C|6R5a3E3_45|6R6v3E454C|6S4t3u3F4142|6S6v3E454C|6SuD3I3L4x|6T4u3u3F4142|6T4P3y3V43|6T6u3E454C|6V4t3F3X3-4244|6V4u3u3X3-4143|6Y4E3_424a|72443R414h|785v3U3V3W3Y404q|7f8S3K4A4K4M4W4Z|7m0t3R4t4u4v|7opb2n2r2s4b4c4d4e4f|7ppa2n2r2s4b4c4d4e4f|7w4S434j4p|7wrk3S48494i|7wrl3S48494i|7wrm3S48494i|7yoV48494m4F|7zoT48494m4F|7CeJ3O3P3T|7O3I444j4w|7Ssw3S4b4c4d4k4l|7Y3X4a4h4o|7YsG4i4n4H|7YsH4i4n4H|7_nV3Q4e4f4z|84t53G3H4k4l4r4x4y|864d4j4p4S|8k5n4a4o4q|8k5p454p4C|8ktv4n4x|8l7y3K4A4K4M4W4Z|8o2i474w54|8o2k474w54|8o2l474w54|8q2v4h4t4u4v4R|8quv3Z4n4r4y|8sud4n4x|8Lnv4m4D4E555657585f|8R7T3K464s4B4G4K4M4W4Z|8V8C4A4G4N|8Y5K3U3W3Y404q4U|8Ymt3z4z5E|8Zms3z4z5E|8-qW4e4f4H4Q|977_4A4B4T|99rW4k4l4F4Q|9bax4J4L4O|9fbB4I4K4P|9hbM3K464s4A4J4M4W4X4Z|9ja84I4V4Z|9lcE3K464s4A4K4W4Z5u5V|9m9c4B4T4W|9maO4I4P4V|9naT4J4O4X|9rr_4F4H4_|9s3_4w4S5c5d5e|9u474o4R4-50|9u8A4G4N4W|9v5I4C4Y4-50|9yar4L4O51|9z9q3K464s4A4K4M4N4T4Z|9Bb64K4P51|9F6y4U5l5n|9F9N3K464s4A4K4L4M4W51|9G4W4S4U5t|9Gr-4Q5253|9H4V4S4U5t|9Oae4V4X4Z|a1rR4_5359|a1sn4_525h|a92T4t4u4v5a5g|a9ob4z5b5H|abod4z5b5H|agoe4z5b5H|anoi4z5b5H|anrD525b5p|ao37545c5d5e6b|apry55565758595f5x5y5z|aq3c4R5a5t|ar3d4R5a5t|ar3e4R5a5t|arok4z5b5H|au28545i5S|azsN535j5p|aB1p5g5k5w|aJsY5h5q5C|aK0A5i5r5v|aQ8F4Y5m5s|aS8K5l5o5F|aZ784Y5s5A|aZaI5m5u5Q|aZrU595h5x5y5z|aZtP5j5B5R|b20s5k5v|b37G5l5n5G5J|b63U4-505c5d5e5D|b6by4M5o6667|bd185k5r5w|bf1j5i5v5S|bgr35b5p5H|bgr45b5p5H|bgr55b5p5H|bs6E5n5I5K|butB5q5C5W|bCt45j5B60|bF405t5M6i6j6k6m|bFhI4D4E5V6l|bJ8H5m5L5N5U|bL7z5s5K5L5N|bLp0555657585f5x5y5z6y|bM6h5A5O5P5T|bM7y5s5K5L5N|bQ7t5A5G5J5X5Y5-|bS8u5F5G5J6R6Y6-|bT5d5D5T696a|bT8t5F5G5J6R6Y6-|bW6A5I5X5Y5-6d6e6g|bX6B5I5X5Y5-6d6e6g|bYa85o5Z64|bYuk5q5W5_|bZ1L5g5w6b|b-5W5I5M62|b_905F5Z6w|b_hk4M5E6F6G6H|c0u15B5R63|c27q5K5O5P6d6e6g|c47q5K5O5P6d6e6g|c49K5Q5U61|c57q5K5O5P6d6e6g|ckvI5R6f|cmsA5C636s6t6u|cq9N5Z646w|cz5H5T696a7p7q7r7s7t7u7v7w7x|cDtn5W6068|cGaw5Q6165|cQaN6466676z6A6B6C6D|cQbb5u656h|cRba5u656h|cUtq636c6f|cY455M626i6j6k6m|cZ435M626i6j6k6m|c_295a5S6n|d5tj686o6p6x|d77s5O5P5X5Y5-6R6Y6-|d97s5O5P5X5Y5-6R6Y6-|d9uP5_686v|da7s5O5P5X5Y5-6R6Y6-|dvb966676r6K|dB325D696a6q|dC315D696a6q|dD305D696a6q|dDkE5E6F6G6H6M6N6O|dE305D696a6q|dN2c6b6q|d-sp6c6s6t6u6x|d-sq6c6s6t6u6x|e32D6i6j6k6m6n6E|e4aG6h6z6A6B6C6D6K|e4q9606o6p6y|e4qa606o6p6y|e5q8606o6p6y|e8us6f6x|e98T5U616W6Z|e9tv6c6o6p6v|eepI5H6s6t6u6L|eia8656r6W6Z|eia9656r6W6Z|eja6656r6W6Z|eka4656r6W6Z|ela3656r6W6Z|es2T6q6I6V6X|eGgN5V6l6J|eHgM5V6l6J|eJgM5V6l6J|eO3i6E7c7d7D|ePgK6F6G6H6M6N6O6U|eQd66h6r6P6Q6S6T70|eYpy6y7a7e|f2gW6l6J7f7g7i7j7k|f4gY6l6J7f7g7i7j7k|f5gZ6l6J7f7g7i7j7k|fedK6K6_70717273|fgdO6K6_70717273|fh7P5L5N6d6e6g7b|fhdO6K6_70717273|fidR6K6_70717273|fkf36J747C|fo276E7D|fo8g6w6z6A6B6C6D79|fp266E7D|fp7R5L5N6d6e6g7b|fr8d6w6z6A6B6C6D79|fs7S5L5N6d6e6g7b|fs9n6P6Q6S6T737879|fse46K6P6Q6S6T7374|ft9a6P6Q6S6T737879|fu956P6Q6S6T737879|fud_6P6Q6S6T6_70717278|fueg6U70757677|fweg747z8k8l8m|fxef747z8k8l8m|fyef747z8k8l8m|fzdP6_7172737h|fA856W6Z6_71727b|fJpR6L7m7n7o7Z7-7_|fK7V6R6Y6-797y|fT5m6I7p7q7r7s7t7u7v7w7x85|fT5o6I7p7q7r7s7t7u7v7w7x85|fWm96L7l7m7n7o|g2hF6M6N6O7l8c|g2hG6M6N6O7l8c|g3cQ787E7F7G7H7I7J7K7L7M7N|g3hH6M6N6O7l8c|g4hI6M6N6O7l8c|g5hI6M6N6O7l8c|g8hL7e7f7g7i7j7k7O7Q7R7S7T|giq17a7e7Z7-7_|gjq27a7e7Z7-7_|gkq27a7e7Z7-7_|gn6N627c7d7y|gn6O627c7d7y|gp6R627c7d7y|gp6S627c7d7y|gq6U627c7d7y|gq6W627c7d7y|gr6X627c7d7y|gs6-627c7d7y|gs6_627c7d7y|gC7o7b7p7q7r7s7t7u7v7w7x7P|hncQ7576777U8e|hu0I7B7W|hv0a7A7V7W|hveS6U8f8G8K|hx1S6I6V6X7X|hza67h7H7I7J7K7L7M7N7U|hzab7h7H7I7J7K7L7M7N7U|hzac7h7H7I7J7K7L7M7N7U|hB8Y7h7E7F7G7P|hB8Z7h7E7F7G7P|hB917h7E7F7G7P|hC8T7h7E7F7G7P|hC8V7h7E7F7G7P|hC927h7E7F7G7P|hD8R7h7E7F7G7P|hJhv7l8c8E|hK7G7y7H7I7J7K7L7M7N7Y|hKhu7l8c8E|hLhu7l8c8E|hMht7l8c8E|hNht7l8c8E|hObJ7z7E7F7G8a|hU1c7B7W|hX1g7A7B7V7X878889|hX1Q7D7W84|id7H7P8182838N8O8R|ierj7a7m7n7o8s|ifrk7a7m7n7o8s|igrk7a7m7n7o8s|ihlD8d8p|ik7C7Y8oac|il7B7Y8oac|in7A7Y8oac|io2i7X8586|iC2E7c7d848g8y8H8I8J|iI1C848788898j|iK1b7W868i|iL1b7W868i|iM1b7W868i|iRcf7U8e8N8O8R|iRlf8d8p|iVfc7f7g7i7j7k7O7Q7R7S7T8f|iVmk808b8h8p8A|iYcC7z8a8k8l8m|i-f77C8c8G8K|i_2E858H8I8J|j0ml8d8p8D|ja158788898j8n|jc1f868i8H8I8J|jdd17576778e8W|jed27576778e8W|jfd37576778e8W|jg0L8i8V|jj6U8182838M8P8Qaqaras|jllp808b8d8h8q8r|jtli8p8F9g9h9i|julh8p8F9g9h9i|jysr7Z7-7_8t8u8v8w8x8Z8-8_90919293959798|jBsz8s8zaL|jBsA8s8zaL|jCsB8s8zaL|jCsC8s8zaL|jCsD8s8zaL|jJ2m858H8I8J|jJsV8t8u8v8w8x8B9y9z9A|jKnr8d8D9T9Vad|jLti8z8Cbm|jLtj8Bb1b3|jOn38h8A9l|jUg-7O7Q7R7S7T8S8T8U8X8Y9u9O9P|jVkQ8q8r8L9m|jWe67C8f8W|jX1R858g8j8y94|jX1S858g8j8y94|jX1T858g8j8y94|jXe57C8f8W|j_kQ8F999g9h9i|k3608o9nak|k3a37Y8a96|k4a47Y8a96|k55Z8o9nak|k55-8o9nak|k5a57Y8a96|k5gN8E9C9D9E9F9G9H|k5gO8E9C9D9E9F9G9H|k7gL8E9C9D9E9F9G9H|k90I8n949d|k9dT8k8l8m8G8K9j|kagI8E9C9D9E9F9G9H|kbgH8E9C9D9E9F9G9H|kfrm8s9xahaiaj|kfrn8s9xahaiaj|khrj8s9xahaiaj|kirh8s9xahaiaj|kiri8s9xahaiaj|kjrf8s9xahaiaj|kjrg8s9xahaiaj|kk1c8H8I8J8V9d|kkre8s9xahaiaj|klak8N8O8R9a9b9c9e9j|klrd8s9xahaiaj|kmrb8s9xahaiaj|kokK8L9k9w|ksam969f9K9L9M|ktan969f9K9L9M|kuan969f9K9L9M|kv0r8V94|kvao969f9K9L9M|kAao9a9b9c9eacao|kDmc8q8r8L9k|kEmd8q8r8L9k|kFmd8q8r8L9k|kGdX8W969I|kJmh999g9h9i9l|kLmn8D9k9V|kRjC8F9w9N9Q9R|kW518M8P8Q9o9p9q9r9s9B|kY4T9n9t9vam|kY4U9n9t9vam|kZ4Q9n9t9vam|kZ4R9n9t9vam|kZ4S9n9t9vam|l93X9o9p9q9r9sabaf|l9hr8E9P|la3U9o9p9q9r9sabaf|lek5999m9Q|lepr8Z8-8_909192939597989Jahaiaj|llsj8z9W9Y9Za1a3aXaYaZ|lnsi8z9W9Y9Za1a3aXaYaZ|lqsh8z9W9Y9Za1a3aXaYaZ|ly549nagaIaJaK|lyfZ8S8T8U8X8Y9D9E9F9G9H9P|lAfO8S8T8U8X8Y9C9S|lAfP8S8T8U8X8Y9C9S|lAfQ8S8T8U8X8Y9C9S|lBfM8S8T8U8X8Y9C9S|lCfL8S8T8U8X8Y9C9S|lOe19j9K9L9M9S|lOol9x9Tan|lSdQ9a9b9c9e9Ia4|lSdR9a9b9c9e9Ia4|lTdO9a9b9c9e9Ia4|lUiF9m9Q|lVh28E9P|lXgZ8E9u9C9OaD|m5kc9m9w9N9R9V|m7iM9m9Q|m8es9D9E9F9G9H9Iat|m9nP8A9J9U9Vad|mjnI9Tadav|mmmp8A9l9Q9Tad|mys29y9z9Ab5b6b7b8b9bg|mzv1b1b3bIbJ|mAs19y9z9Ab5b6b7b8b9bg|mBs19y9z9Ab5b6b7b8b9bg|mBv0b1b3bIbJ|mDu_b1b3bIbJ|mEu_b1b3bIbJ|mFs19y9z9Ab5b6b7b8b9bg|mGu-b1b3bIbJ|mHs19y9z9Ab5b6b7b8b9bg|mJcJ9K9L9MaSaTbF|mJuZb1b3bIbJ|mK28a7a8a9aaaba_|mK2sa6aaaQ|mK2ta6aaaQ|mK2ua6aaaQ|mL2ya6a7a8a9abaU|mL2B9t9va6aaaf|mM9c8182839fae|mMn08A9T9U9VaEaFaG|mX98acauaz|mY399t9vabam|mZ5W9BapaM|m-pL8Z8-8_909192939597989xan|m_pL8Z8-8_909192939597989xan|n1pL8Z8-8_909192939597989xan|n46U8M8P8Qalap|n577akaqarasbO|n73q9o9p9q9r9safaIaJaK|n7pK9JahaiajaC|n8az9fazaSaT|nb6IagakaM|nd8q8oalau|nd8r8oalau|nd8s8oalau|nef29SaDbS|nh8SaeaqarasawaxayaAaBaWblbn|njp49UaCaEaFaG|nk8Saublbn|nl8Saublbn|nn8Saublbn|nna0aeaobabcbd|no8Saublbn|np8Saublbn|nqpOanavaL|nsfl9PataV|nsnxadavaH|nunAadavaH|nvnAadavaH|nznDaEaFaGaNaOaPbr|nF4u9BamaM|nF4v9BamaM|nF4w9BamaM|nHq28t8u8v8w8xaCaXaYaZ|nK5FagapaIaJaK|nLnVaHaRbp|nLnWaHaRbp|nNnYaHaRbp|nV24a7a8a9aUa_|nWoeaNaOaPb2bhbibj|nZbka4aobabcbd|n-bka4aobabcbd|o228aaaQa-a_be|o7fUaDb0c5|oc9Waublbn|ogqk9y9z9AaLb2|ohqk9y9z9AaLb2|oiql9y9z9AaLb2|ol25aUa_|on1Ka6aQaUa-be|oogkaVbGbR|optw8C9X9-9_a0a2a5bE|orqnaRaXaYaZb4|ortv8C9X9-9_a0a2a5bE|otqob2b5b6b7b8b9bb|oyqP9W9Y9Za1a3b4bf|ozqQ9W9Y9Za1a3b4bf|ozqS9W9Y9Za1a3b4bf|oAqU9W9Y9Za1a3b4bf|oAqV9W9Y9Za1a3b4bf|oCbbazaSaTblbn|oCqjb4bkbN|oEbbazaSaTblbn|oFbbazaSaTblbn|oI1TaUa_|oUrSb5b6b7b8b9bgc6|oXsj9W9Y9Za1a3bfbm|p3oWaRbkbo|p3oXaRbkbo|p3oYaRbkbo|p4pfbbbhbibjca|p6baauawaxayaAaBaWbabcbdbF|p6sM8BbgbqbtbubvbxbybAbB|p7bbauawaxayaAaBaWbabcbdbF|p7ovbhbibjbpbQ|p8onaNaOaPbobs|p8sObmbCbDc6|p9nYaHbzbV|p9olbpbzbL|p9sPbmbCbDc6|p9sQbmbCbDc6|pasRbmbCbDc6|pbjgbGbZ|pbsSbmbCbDc6|pbsTbmbCbDc6|pcocbrbsbH|pcsUbmbCbDc6|pcsVbmbCbDc6|pdsVbqbtbubvbxbybAbBbDbXc6|pdsWbqbtbubvbxbybAbBbCbEbX|pftab1b3bDbIbJ|pobka4blbncb|pvhwb0bwbUbWbZ|pxo8bzbPc0|pEtX9X9-9_a0a2a5bEbK|pFtY9X9-9_a0a2a5bEbK|pMu7bIbJcn|pVosbsbMbQ|p_oqbLbPc3|q2qabbc1cd|q56EalbTdg|q5o7bHbMc0|q6oAbobLc3|q8gHb0bWc5|qbe0atchcs|qi5JbOcgdz|qjkrbGbZ|qjmCbrb-cc|qmhzbGbRcL|qmsebCbDc8do|qn1jcg|qnkubwbGbUb_c2|qnmobVb_cG|qolAbZb-ej|qon_bHbPc9|qppDbNc4cd|qrkrbZcIep|qrolbMbQc7|qvpjc1cick|qwfGaVbRch|qwqXbfbqbtbubvbxbybAbBbCce|qxogc3c9ca|qxrLbXcjcq|qAo0c0c7cc|qDoibkc7cl|qFbhbFcsdh|qGnXbVc9cD|qHpvbNc1cf|qKqTc6cmcu|qLpucdcicp|qO2PbTbYcCcEcFcK|qOftbSc5cO|qOpqc4cfco|qRrgc8cmcq|qUp2c4cocr|qWovcacrcN|qXqYcecjcx|qXu5bKcvcA|q_pecickcz|r0pKcfcycz|r0ric8cjcx|r1oGckclcH|r4cnbScbct|r9cqcsc-dR|raqocecwdbdcddde|rat_cncBdqdrds|rbqjcucycS|rdq_cmcqdSdTd_e0e1|repZcpcwcJ|rfpfcocpcZ|rgvAcndqdrds|ritWcvdodE|rj2AcgdudvdJ|rjnycccGcM|rk2zcgdudvdJ|rm2ycgdudvdJ|rmn9b-cDcX|rmoAcrcNcW|rniOc2cLeZ|rnpUcycPcQ|ro2xcgdudvdJ|rqifbWcIcV|rsnAcDd7da|rtoeclcHd7|rufychcRcTcU|rxpGcJcQd8|rxpZcJcPcS|ryfFcOc_d0d1d2d3d5d6dV|ryp_cwcQd4|rEfecOc-dA|rFfdcOc-dA|rFh-cLd9dj|rHoDcHcZdn|rIm-cGcYdl|rKn1cXdadp|rKoVczcWd8|rLegctcTcUdy|rLgucRd9dG|rLgvcRd9dG|rMgycRd9dG|rMgzcRd9dG|rMgBcRd9dG|rMq2cSd8dbdcddde|rNgDcRd9dG|rNgEcRd9dG|rNnZcMcNdk|rPpmcPcZd4|rQgVcVc_d0d1d2d3d5d6dP|rRnwcMcYdf|rRqrcud4dSdTe3e5|rSqscud4dSdTe3e5|rTqscud4dSdTe3e5|rUqscud4dSdTe3e5|rVnvdadmdHdIdLdMdN|r-9ubOdhdi|r-9xcbdgeq|r_9udgdxez|s2hYcVef|s3nRd7dmdn|s4mycXdpei|s9nJdfdke9|s9otcWdkewey|s9s9bXcBdU|semNcYdldHdIdLdMdN|sfvvcvcAdt|sgvwcvcAdt|shvxcvcAdt|sjvzdqdrdsdF|so1OcCcEcFcKet|sp1NcCcEcFcKet|sG5MdCdDdJdQ|sG8odidzebed|sGdVc-dAdX|sH6wbTdxdB|sJexcTcUdydK|sO6ndzdCdDeo|sP6kdwdBdZ|sP6ldwdBdZ|sPuecBdFdO|sPv2dtdEdO|sQghc_d0d1d2d3d5d6dPdV|sSnkdfdpe6|sTnkdfdpe6|sU4acCcEcFcKdwe7|sUexdAdYd-|sUnkdfdpe6|sVnldfdpe6|sWnldfdpe6|t6tIdEdFe2|tch2d9dGea|tg5ndwdZee|tjcActdWeq|tnrqcxdbdcddded_e0e1e3e5|tnrrcxdbdcddded_e0e1e3e5|tnrGdod_e0e1e2|tog3cRdGe4|tpcGdRdXex|tre4dydWdY|tre8dKdXen|ts5MdCdDdQek|tsfIdKe4ec|tsrwcxdSdTdUe3e5|ttrucxdSdTdUe3e5|ttrvcxdSdTdUe3e5|tts9dOdUeY|turtdbdcdddedSdTd_e0e1eX|tvg2dVd-e8|tvrsdbdcdddedSdTd_e0e1eX|twnzdHdIdLdMdNe9eh|tx4pdJeger|tAgae4eaeu|tBnWdme6ewey|tChbdPe8ef|tE7SdxemeD|tEfjd-eneJ|tF7RdxemeD|tJ52dQegel|tJhodjea|tK4Me7eeeC|tKm8e6ei|tNlFdlehej|tOlBb_eiep|tT5zdZeleB|tV5peeekeT|tY7tebedeoeN|u1eFdYeceJ|u26HdBemev|u2k_c2ejeZ|uaaUdhdRes|ub3De7eQeS|ucaVeqezeA|ue2jdudveF|ueghe8eGeHeIeL|ug6AeoeBe-|ujphdne9eX|ukcJdWeEf2f3f4f5|ukpidne9eX|ul8TdieseM|uoa_eseE|ur62ekeveP|uw4AegeOeS|uw8febedeMeV|uwbGexeAf2f3f4f5|ux2peteQeR|uzfteueKeL|uAfreueKeL|uBfqeueKeL|uEfjeceneK|uFfkeGeHeIeJ|uFgAeueGeHeI|uG8tezeDf1|uI7xemeVf8|uK54eCeTf0|uK5ZeBeTe-|uL34ereFeU|uM2ceFf7|uM4mereCe_|uM5releOeP|uO35eQf6f7|uT7KeDeNeW|uX7LeVf1|uZq2e3e5ewey|uZuie2|v1k1cIep|v56pevePf8|v84geSf0f6|v84SeOe_|va88eMeW|vpdfexeE|vrdgexeE|vsdhexeE|vtdhexeE|vH3SeUe_|vR2meReU|vT6ueNe-";


int previousCalculatedFace = -1;

IMyTextSurface _drawingSurface;
RectangleF _viewport;
MySpriteDrawFrame spriteFrame;

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
	Wheels = Blocks.FindAll(x => x.CubeGrid == Me.CubeGrid && x is IMyMotorSuspension).Select(x => x as IMyMotorSuspension).ToList();
	RemoteControl = Blocks.Find(x => x.CubeGrid == Me.CubeGrid && x is IMyRemoteControl) as IMyRemoteControl;
	//Sensor = Blocks.Find(x => x.CubeGrid == Me.CubeGrid && x is IMySensorBlock) as IMySensorBlock;

	
	theAntenna = Blocks.Find(x => x.CubeGrid == Me.CubeGrid && x is IMyRadioAntenna) as IMyRadioAntenna;

	Runtime.UpdateFrequency = UpdateFrequency.Update10;
	
	

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


	
    _drawingSurface = Me.GetSurface(0);
	
    // Calculate the viewport offset by centering the surface size onto the texture size
    _viewport = new RectangleF(
        (_drawingSurface.TextureSize - _drawingSurface.SurfaceSize) / 2f,
        _drawingSurface.SurfaceSize
    );
	
	
}

public bool initTheCurrentFaceNodes(int faceNumberNodesToApply){
	
	string s = "";
	//4 is right
	if(faceNumberNodesToApply == 4){
		s = nodesStringRight;
	}
	else{
		//2 is front
		if(faceNumberNodesToApply == 2){
			s = nodesStringFront;
		}
		else{
			Echo("no faces nodes init available");
			return false;
		}
	}
	

	string[] subs  = s.Split('|');
	
	// Echo("a:"+decodeAsCharNumberMax64('a'));
	// Echo("aa:"+decodeStr__NumberMax4095("aa"));
	

	int indexNumber = 0;
	
	//for the function
	nodes = new List<Node>();
	
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
	
	//Nodes got init
	return true;
	
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

//whip's code
public void DrawLine(ref MySpriteDrawFrame frame, Vector2 point1, Vector2 point2, float width, Color color)
{
    Vector2 position = 0.5f * (point1 + point2);
    Vector2 diff = point1 - point2;
    float length = diff.Length();
    if (length > 0)
        diff /= length;

    Vector2 size = new Vector2(length, width);
    float angle = (float)Math.Acos(Vector2.Dot(diff, Vector2.UnitX));
    angle *= Math.Sign(Vector2.Dot(diff, Vector2.UnitY));

    MySprite sprite = MySprite.CreateSprite("SquareSimple", position, size);
    sprite.RotationOrScale = angle;
    sprite.Color = color;
    frame.Add(sprite);
}

//TODO:
//add new heuristic based on angles?
//do heuristic change mid path ?
//check for neighbors check at the beginning
//do best path based on path combining
//link faces
//store all faces
//display the current closest point and neighbors to help with changing path
public void aStarPathFinding(Point startPoint, Point endPoint,out List<Node> listPathNode, out Dictionary<Node, double> gscoreOut){
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
		// Echo(""+Math.Sqrt(distanceSquarred(node.position,ourDestinationNode.position)));
			
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
			// Echo("node.neighborsNodesIndex.Count:"+node.neighborsNodesIndex.Count);
			foreach(int index in node.neighborsNodesIndex){
				if(closelist.Contains(nodes[index])==false){
					neighbors.Add(nodes[index]);
				}
			}
			// Echo("neighbors.Count:"+neighbors.Count);
			
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
	
	gscoreOut = gscore;
	
	List<Node> data = new List<Node>();
	
	while(came_from.ContainsKey(node)){
		// Echo("data.Add(node);");
		// Echo("node.position:"+node.position);
		// Echo(""+Math.Sqrt(distanceSquarred(node.position,ourDestinationNode.position)));
		Echo("gscore[node]:"+Math.Round(gscore[node],3));
		data.Add(node);
		node = came_from[node];
	}
	
	listPathNode = data;
	
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
	
	
	
	
	spriteFrame = _drawingSurface.DrawFrame();
	
	int facenumberCalculated = -1;
	Point pixelPosCalculated = new Point(0,0);
	
	faceAndPointOnPlanetsCalculated( RemoteControl,out facenumberCalculated,out pixelPosCalculated,false,new Vector3D(0,0,0));
	
	Echo("facenumberMain1:"+facenumberCalculated);
	Echo("pixelPosMain1:"+pixelPosCalculated);

	whichFileShouldIlook(facenumberCalculated);
	
	// add the direction of the rover on the map
	int faceNumberTipRover = -1;
	Vector3D shipForwardVectorTip = 8*1024*RemoteControl.WorldMatrix.Forward+Me.GetPosition();
		
	Point pointShipForwardVector = new Point(0,0);
	faceAndPointOnPlanetsCalculated( RemoteControl,out faceNumberTipRover,out pointShipForwardVector,true,shipForwardVectorTip);
	Echo("shipForwardVectorTip:"+Vector3D.Round(shipForwardVectorTip,3));
	Echo("pointShipForwardVector:"+pointShipForwardVector);
		
	
	if(myTerrainTarget== new Vector3D(0,0,0)){
		
		// foreach (IMyMotorSuspension Wheel in Wheels)
		// {
			// Wheel.SetValue<Single>("Steer override", 0);
			// Wheel.SetValue<float>("Propulsion override", 0);
			// Wheel.Brake = true;
			
			// RemoteControl.HandBrake = true;
			
		// }
		
	}
	else
	{
		
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
		
		if(targetIsOnTheSameFace==true){
			List<Node> aStarPathNodeList1 = new List<Node>();
			List<Node> aStarPathNodeList2 = new List<Node>();

			// // ok euclidian distance going across with no circles
			// Point startPointGoal  = new Point(2043,1664);
			// Point finalPointGoal = new Point(50,50);

			//todo: checking for simplification
			// Point startPointGoal  = new Point(2043,1664);//this2
			// Point finalPointGoal = new Point(429,1284);
			// Point finalPointGoal  = new Point(2043,1664);//this1
			// Point startPointGoal   = new Point(429,1284);
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

			// Point startPointGoal  = new Point(1440,767);
			// Point finalPointGoal = new Point(429,1284);
			Echo("nodes.Count:"+nodes.Count);
			if(previousCalculatedFace!=facenumberCalculated){
				bool faceNodesInitResult = initTheCurrentFaceNodes(facenumberCalculated);

				Echo("faceNodesInitResult:"+faceNodesInitResult);
				previousCalculatedFace=facenumberCalculated;
			}

			Point startPointGoal  = pixelPosCalculated;
			Point finalPointGoal = pixelPosCalculatedTarget;
			// Point finalPointGoal = new Point(429,-200);
			// Point finalPointGoal = new Point(1500,-200);

			// Point finalPointGoal = new Point(1500,2060);

			Dictionary<Node, double> gscore1 = new Dictionary<Node, double>();
			Dictionary<Node, double> gscore2 = new Dictionary<Node, double>();

			aStarPathFinding(startPointGoal,finalPointGoal, out aStarPathNodeList1, out gscore1);
			// aStarPathFinding(finalPointGoal,startPointGoal, out aStarPathNodeList2, out gscore2);

			Echo("aStarPathNodeList1.Count:"+aStarPathNodeList1.Count);
			Echo("aStarPathNodeList2.Count:"+aStarPathNodeList2.Count);
			if(aStarPathNodeList1.Count !=0){
				Echo("gscore1_max:"+Math.Round(gscore1[aStarPathNodeList1[0]],3));
				// Echo("aStarPathNodeList1[0].position:"+aStarPathNodeList1[0].position);
				// Echo("aStarPathNodeList1[aStarPathNodeList1.Count-1].position:"+aStarPathNodeList1[aStarPathNodeList1.Count-1].position);
				Echo("nextPointToGo:"+aStarPathNodeList1[aStarPathNodeList1.Count-1].position);
				Echo("aStarPathNodeList1.Count:"+aStarPathNodeList1.Count);
			}
			if(aStarPathNodeList2.Count !=0){
				Echo("gscore2_max:"+Math.Round(gscore2[aStarPathNodeList2[0]],3));
				// Echo("aStarPathNodeList1[0].position:"+aStarPathNodeList1[0].position);
				// Echo("aStarPathNodeList1[aStarPathNodeList1.Count-1].position:"+aStarPathNodeList1[aStarPathNodeList1.Count-1].position);
				Echo("nextPointToGo:"+aStarPathNodeList2[aStarPathNodeList2.Count-1].position);
				Echo("aStarPathNodeList2.Count:"+aStarPathNodeList2.Count);
			}
			// Point bestPositionToGo = new Point(0,0);
			// if(gscore1[aStarPathNodeList1[0]] < gscore2[aStarPathNodeList2[0]]){
			// bestPositionToGo = aStarPathNodeList1[aStarPathNodeList1.Count-1].position;
			// }
			// else{
			// if(aStarPathNodeList1.Count != 1){
			// bestPositionToGo = aStarPathNodeList2[1].position;	
			// }
			// else{
			// bestPositionToGo = aStarPathNodeList2[0].position;	
			// }
			// }

			// public Vector3D convertPointToV3D(IMyRemoteControl sc, int faceNumber, Point pointToV3D){
			if(aStarPathNodeList1.Count >2){
				targetV3Dabs  = convertPointToV3D(RemoteControl, facenumberCalculated, aStarPathNodeList1[aStarPathNodeList1.Count-1].position);
				// targetV3Dabs  = convertPointToV3D(RemoteControl, facenumberCalculated, bestPositionToGo);
			}
			else{
				targetV3Dabs= new Vector3D(0,0,0);

			}

			// DrawLine(ref spriteFrame, new Vector2(256,100), new Vector2(256,160), 30.0f, Color.DarkRed);
			Vector2 startVector2 = new Vector2(0,0);
			Vector2 endVector2 = new Vector2(0,0);
			if(aStarPathNodeList1.Count>=2){
				foreach(int indexNodeTmp in Enumerable.Range(0,aStarPathNodeList1.Count)){
					if(indexNodeTmp !=aStarPathNodeList1.Count -1){
						Echo("aStarPathNodeList1["+indexNodeTmp+"]:"+aStarPathNodeList1[indexNodeTmp]);
						startVector2 = aStarPathNodeList1[indexNodeTmp].toVector2sax()/8;
						endVector2 = aStarPathNodeList1[indexNodeTmp+1].toVector2sax()/8;
						DrawLine(ref spriteFrame, startVector2, endVector2, 3.0f, Color.DarkRed);
						// startVector2 = aStarPathNodeList1[indexNodeTmp].position;
						// endVector2 = aStarPathNodeList1[indexNodeTmp+1].position;
					}
				}
			}
			if(aStarPathNodeList1.Count==1){
				Vector2 leftLastPointVector2 = new Vector2(aStarPathNodeList1[0].position.Y - 24, aStarPathNodeList1[0].position.X)/8;
				Vector2 rightLastPointVector2 = new Vector2(aStarPathNodeList1[0].position.Y + 24, aStarPathNodeList1[0].position.X)/8;
				DrawLine(ref spriteFrame, leftLastPointVector2, rightLastPointVector2, 6.0f, Color.Green );
			}
		
		}
		
		Vector2 leftMyGoalVector2 = new Vector2((float)pixelPosCalculatedTarget.Y - 24, (float)pixelPosCalculatedTarget.X)/8;
		Vector2 rightGoalVector2 = new Vector2((float)pixelPosCalculatedTarget.Y + 24, (float)pixelPosCalculatedTarget.X)/8;
		DrawLine(ref spriteFrame, leftMyGoalVector2, rightGoalVector2, 6.0f, Color.MediumBlue   );
		
		
		
		
		
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
		
		// Echo("If any of the two is -1 the script won't run");
		// if(currentRegionN==-1||targetRegionN==-1){
			// str_to_display = "target or rover not in region";
			// return;
		// }
		
		bool targetIsOnTheSameRegion = false;
		if(currentRegionN==targetRegionN){
			targetIsOnTheSameRegion = true;
		}
		else{
			targetIsOnTheSameRegion = false;
		}
		Echo("targetIsSameRegion:"+targetIsOnTheSameRegion);
		
		
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
		
		
		
		Vector3D targetV3Drel = RemoteControl.GetPosition()-targetV3Dabs;
		
		Vector3D crossForwardTT = shipForwardVector.Cross((targetV3Drel));
		// Vector3D crossForwardTT = shipForwardVector.Cross(Vector3D.Normalize(targetV3Dabs));
		double turnRightOrLeft = crossForwardTT.Dot(shipDownVector);
		
		Echo("turnRightOrLeft:"+Math.Round(turnRightOrLeft,3));
		
		// str_to_display = ""+"turnRightOrLeft:"+Math.Round(turnRightOrLeft,3);
		
		
		steerOverride = turnRightOrLeft/crossForwardTT.Length();
		
		Echo("targetV3Drel.L:"+Math.Round(targetV3Drel.Length(),3));
		
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
		
		
		// foreach (IMyMotorSuspension Wheel in Wheels)
		// {
			// double areThisFrontWheel = shipForwardVector.Dot(Wheel.GetPosition() - RemoteControl.GetPosition());
			// Echo("areThisFrontWheel:"+Math.Round(areThisFrontWheel,3));
			
			// float MultiplierPO = (float) Vector3D.Dot(Wheel.WorldMatrix.Up, RemoteControl.WorldMatrix.Right);
			
			// // str_to_display = ""+"MultiplierPO:"+Math.Round(MultiplierPO,3);
			// // Echo(str_to_display);
			// //SLerror = -0.2f;
			
			// float localPO = -MultiplierPO * SLerror;
			
			// str_to_display = ""+"localPO:"+Math.Round(localPO,3);
				
			// if(areThisFrontWheel>0){
				// Wheel.SetValue<Single>("Steer override", Convert.ToSingle(steerOverride));
				// Wheel.SetValue<float>("Propulsion override", localPO);
				
			// }
			// else{
				// // Wheel.SetValue<Single>("Steer override", Convert.ToSingle(-steerOverride));
				// Wheel.SetValue<float>("Propulsion override", localPO);
			// }
			
		// }
		
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
		
	Vector2 leftMyPosVector2 = new Vector2((float)pixelPosCalculated.Y - 24, (float)pixelPosCalculated.X)/8;
	Vector2 rightMyPosVector2 = new Vector2((float)pixelPosCalculated.Y + 24, (float)pixelPosCalculated.X)/8;
	DrawLine(ref spriteFrame, leftMyPosVector2, rightMyPosVector2, 6.0f, Color.Green );	
	
	Vector2 leftMyRoverTipVector2 = new Vector2((float)pixelPosCalculated.Y, (float)pixelPosCalculated.X)/8;
	Vector2 rightMyRoverTipVector2 = new Vector2((float)pointShipForwardVector.Y, (float)pointShipForwardVector.X)/8;
	DrawLine(ref spriteFrame, leftMyRoverTipVector2, rightMyRoverTipVector2, 3.0f, Color.Green   );
	
	// x 0 y 0 w 256 h 256

	Echo("_viewport:"+_viewport);
	// DrawSprites(ref spriteFrame);
	spriteFrame.Dispose();
	
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
	
	public Vector2 toVector2(){
		return new Vector2(position.X,position.Y);
	}
	//swaitched axises
	public Vector2 toVector2sax(){
		return new Vector2(position.Y,position.X);
		
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


