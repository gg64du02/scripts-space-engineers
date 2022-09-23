
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

string nodesStringFront = "001i0P0b0i|01ff150d1F|09lV0T0n0Z|0lsh4j051w|0n520o0g0j|0nsh4h032n2r2s2N2O|0o520n0g0j|0oho0v0k0u|0p520m0g0j|0y4p040a0c|0R4a09090c0C|0U1M0d000B0D|0V4u0c090a0j|0VfW0v010f0h1e1g1i|0WdI0i0x0P0Q|0Wg60o0d0y|0X5p090406080l0m0p12|0Xg70o0d0y|0Z1a0g000q0D|0Z4y0e0406080c0F0G0H|0ZhS0d070u0z|0_620n0g|10630o0g|10mq0c020s13|10m-0g0s0v|11630p0g0O1U|12130f0i0V|12jR050A141516|12mu0a0n0o0J|142K0K0B1l|15gZ0p070k0w|15m_0g0o0J0K0L|17gW0o0u0y0U|18cM0K0e0I|19gz0l0f0h0w0R|19iu0u0k0W|19ke0c0r10|1b2q0x0b0t0E|1b3_0a0a0F0G0H1l|1c1x0e0b0i0N|1d2o0v0B0N11|1g4D0l0j0C12|1h4D0l0j0C12|1i4D0m0j0C12|1icN0J0x0M0X|1imT0e0s0v181b|1knI0o0v0-|1lnJ0p0v0-|1ocR0E0I0P0Q1f|1p1H0d0D0E0S|1t9m2v0p0_|1vdZ0l0e0M1z|1wd-0m0e0M1z|1xgq0f0y1e1g1i1j|1z1G0d0N0T0Y|1A1H0c0S111x|1Ch40g0w191k|1D150f0q0Y|1FiK0z0z171n1p1u|1Hce0q0I0_1O|1J1f0e0S0V1J|1Jl50v02101a|1Jn-0L0K0L1c1w|1K9m2y0O0X2h|1Lk20s0A0Z141516|1N1Z070E0T1q|1N4T0J0g0F0G0H1T|1Pmg0d0n181b1h|1QjH0z0r1017|1QjI0z0r1017|1RjG0A0r1017|1TjA0D0W1415162m2p2q|1VmZ0t0J131c|1WhH0i0U1n1p1u1E|1Wlc0r0Z1h1I|1Wm-0u0J131c|1Wm_0u0-181b1d|1Xm_0u1c1C2c|1YfK0s0d0R1D|1-d50o0M1K1O|1-fJ0u0d0R1D|1-m70c131a1m|1_fI0u0d0R1D|20gJ0k0R1k1A|20gK0l0U1j1o1t|243w0Z0t0C1s|2am9091h1C1I|2civ0r0W191R|2dgL0o1k1A1B|2div0r0W191R|2e200g111r1v1x|2e3y111q1s1Q|2e3z121l1r1v1V|2egM0p1k1A1B|2eiu0r0W191R|2f3w101q1s1Q|2fpr1X030-21|2g1Y0h0T1q1y|2h1X0h1x1J1M|2jeo0D0P0Q1K1L|2jfV0s1j1o1t1D|2jgS0p1o1t1E22|2nmn0e1d1m24|2rfq0V1e1g1i1A1F|2rhp0x191B1N|2tfn0X011D1G|2ufn0X1F1H1L|2vfp0W1G1W2Q|2vli0A1a1m1Y1_20|2w1k0k0Y1y1-|2we30w1f1z26|2wf50R1z1G2X|2x1X061y1P292a|2AhA0t1E1S2g|2Bcz0r0X1f1X|2L2i071M1Q292a|2N2Y0m1r1v1P2C|2Nir0E1n1p1u1S2l|2Oib0w1N1R2o|2R501C121U1V|2R6l1N0p1T2h|2X4J1B1s1T2Y|2Ygd0o1H222R2S2T|2-cu0H1O282z|2-la0L1I2325272m2p2q|2-ma07232425272t|2_1b0I1J2k|2_la0L1I2325272m2p2q|30l90L1I2325272m2p2q|30oz1e1w2c3Q|31gt0n1B1W2b2d2e|31la0M1Y1Z1_202t|31mJ0x1C1Z2f|32la0M1Y1Z1_202t|33dK0p1K282U2V2W|33la0N1Y1Z1_202t|34dI0p1X262P|351E0y1M1P2j|371D0A1M1P2j|37gz0o222i2G2H|37n30L1d212f|38gA0n222i2G2H|39gB0n222i2G2H|39mX0J242c3d|3bhr0b1N2i2o|3c8V2V0_1U2I|3dhl092b2d2e2g2G2H|3f1s0L292a2k2C|3i1j0R1-2j3B|3ij70N1R2u2J2K2L|3ijz0W171Y1_202u|3irB1A052A2B2D4849|3jhS0b1S2g2J2K2L|3jjy0W171Y1_202u|3kjy0X171Y1_202u|3krB1y052A2B2D4849|3lrA1x052A2B2D4849|3ml80W1Z2325272F|3ojx0-2l2m2p2q2x|3oum0U2M2Z|3pun0U2M2Z|3qjy0-2u2F3C|3qun0T2M2Z|3rbV0P1X2E38|3vrC1p2n2r2s3h3j|3wrC1o2n2r2s3h3j|3y2M0o1Q2j2Y|3yrC1m2n2r2s3h3j|3SaZ1t2z2I3f|3WkY1g2t2x36|3Zg-0F2b2d2e2i3033|40g_0I2b2d2e2i3033|499q2N2h2E3K|4ahN0B2l2o3e|4bhM0C2l2o3e|4chM0D2l2o3e|4duq0d2v2w2y3539|4etb0P052-313439|4ftc0P052-313439|4gdT0F282U2V2W38|4jfx0T1H2R2S2T2_|4jfL0P1W2Q3033|4jfM0O1W2Q3033|4jfN0O1W2Q3033|4leK0T262P2X|4leL0U262P2X|4meM0V262P2X|4meN0W1L2U2V2W2_|4s3f1b1V2C32|4tvl0p2v2w2y37|4wta0H2N2O3g3m|4xf0112Q2X3a|4xh3162G2H2R2S2T3e|4xt90G2N2O3g3m|4y3d1d2Y3o3B|4yh4172G2H2R2S2T3e|4yt90G2N2O3g3m|4zuH082M373b|4Dlm1u2F3d3z|4DuQ092Z353i|4Hd60I2z2P3f|4Iuc092M2N2O3c|4Me-0Y2_3w3N|4Nuo0a353c3n|4Ouj0b393b3g|4PlL1x2f363Q|4Qhm1g2J2K2L30333A|4XcZ0K2E383M|4Yu6042-31343c3s3t|53sm0g2A2B2D3j3l|5buV0g373k3n|5crP0c2A2B2D3h3v|5eu_0e3i3J|5gsM0C3h3m3p3q3r|5hsV0I2-31343l3D|5juA0e3b3i3s3t|5k4i0G323u3F|5lsD0t3l3v3x|5lsE0u3l3v3x|5lsF0v3l3v3x|5qu90m3g3n3D|5qua0m3g3n3D|5s4E0J3o3y3X3-42|5wr_033j3p3q3r3x|5zes0N3a3M3T|5Bs1043p3q3r3v3S|5D5F0K3u3E3_|5HjN0Y363C4D4E|5JhO1K3e3C3N|5L0D2u2k32|5LiC1z2x3z3A|5RtL0E3m3s3t3G3H|61650E3y3U3V3W3Y40|643Q083o3R3X3-41|64tW0y3D3L4n|64tX0z3D3L4n|68vk0D3J3Z|6avb0B3k3I3L|6fa12t2I3P464s4A4K4M4W4Z|6ruE0y3G3H3J3Z|6ycN1N3f3w3P|6ygL1t3a3A3O|6AgK1t3N3T4g|6DcJ1Q3K3M4g|6Dnl2A213d4m|6H3C0o3F4447|6HrU0C3x4b4c4d4i|6KfG0_3w3O4g|6Q6v0A3E454C|6R5a0s3E3_45|6R6v0z3E454C|6S4t0K3u3F4142|6S6v0z3E454C|6SuD0i3I3L4x|6T4u0L3u3F4142|6T4P0G3y3V43|6T6u0y3E454C|6V4t0M3F3X3-4244|6V4u0N3u3X3-4143|6Y4E0M3_424a|72440H3R414h|785v0d3U3V3W3Y404q|7f8S1u3K4A4K4M4W4Z|7m0t2N3R4t4u4v|7opb2W2n2r2s4b4c4d4e4f|7ppa2X2n2r2s4b4c4d4e4f|7w4S0p434j4p|7wrk1y3S48494i|7wrl1x3S48494i|7wrm1x3S48494i|7yoV3348494m4F|7zoT3548494m4F|7CeJ153O3P3T|7O3I0E444j4w|7Ssw1g3S4b4c4d4k4l|7Y3X0r4a4h4o|7YsG1g4i4n4H|7YsH1g4i4n4H|7_nV3F3Q4e4f4z|84t51j3G3H4k4l4r4x4y|864d0d4j4p4S|8k5n0Q4a4o4q|8k5p0R454p4C|8ktv1i4n4x|8l7y0e3K4A4K4M4W4Z|8o2i1q474w54|8o2k1q474w54|8o2l1p474w54|8q2v1k4h4t4u4v4R|8quv1h3Z4n4r4y|8sud1g4n4x|8Lnv3N4m4D4E555657585f|8R7T033K464s4B4G4K4M4W4Z|8V8C034A4G4N|8Y5K1k3U3W3Y404q4U|8Ymt3h3z4z5E|8Zms3h3z4z5E|8-qW0F4e4f4H4Q|977_034A4B4T|99rW094k4l4F4Q|9bax034J4L4O|9fbB0d4I4K4P|9hbM0l3K464s4A4J4M4W4X4Z|9ja8034I4V4Z|9lcE0A3K464s4A4K4W4Z5u5V|9m9c034B4T4W|9maO024I4P4V|9naT034J4O4X|9rr_0e4F4H4_|9s3_0Y4w4S5c5d5e|9u470_4o4R4-50|9u8A044G4N4W|9v5I1D4C4Y4-50|9yar024L4O51|9z9q023K464s4A4K4M4N4T4Z|9Bb6024K4P51|9F6y1c4U5l5n|9F9N033K464s4A4K4L4M4W51|9G4W1i4S4U5t|9Gr-094Q5253|9H4V1i4S4U5t|9Oae024V4X4Z|a1rR094_5359|a1sn0f4_525h|a92T0j4t4u4v5a5g|a9ob2-4z5b5H|abod2Y4z5b5H|agoe2W4z5b5H|anoi2T4z5b5H|anrD0i525b5p|ao370f545c5d5e6b|apry0h55565758595f5x5y5z|aq3c0f4R5a5t|ar3d0f4R5a5t|ar3e0f4R5a5t|arok2S4z5b5H|au280e545i5S|azsN0u535j5p|aB1p0c5g5k5w|aJsY0w5h5q5C|aK0A0b5i5r5v|aQ8F124Y5m5s|aS8K145l5o5F|aZ780j4Y5s5A|aZaI0Y5m5u5Q|aZrU0d595h5x5y5z|aZtP0k5j5B5R|b20s075k5v|b37G0l5l5n5G5J|b63U0m4-505c5d5e5D|b6by1i4M5o6667|bd180q5k5r5w|bf1j0A5i5v5S|bgr30E5b5p5H|bgr40E5b5p5H|bgr50D5b5p5H|bs6E0q5n5I5K|butB0i5q5C5W|bCt4095j5B60|bF400E5t5M6i6j6k6m|bFhI394D4E5V6l|bJ8H0F5m5L5N5U|bL7z0k5s5K5L5N|bLp02y555657585f5x5y5z6y|bM6h0D5A5O5P5T|bM7y0l5s5K5L5N|bQ7t0p5A5G5J5X5Y5-|bS8u0u5F5G5J6R6Y6-|bT5d0m5D5T696a|bT8t0t5F5G5J6R6Y6-|bW6A0k5I5X5Y5-6d6e6g|bX6B0j5I5X5Y5-6d6e6g|bYa80a5o5Z64|bYuk0D5q5W5_|bZ1L1a5g5w6b|b-5W0H5I5M62|b_900r5F5Z6w|b_hk3j4M5E6F6G6H|c0u10z5B5R63|c27q0r5K5O5P6d6e6g|c47q0r5K5O5P6d6e6g|c49K0i5Q5U61|c57q0s5K5O5P6d6e6g|ckvI1f5R6f|cmsA0A5C636s6t6u|cq9N0h5Z646w|cz5H0o5T696a7p7q7r7s7t7u7v7w7x|cDtn0y5W6068|cGaw0m5Q6165|cQaN0u6466676z6A6B6C6D|cQbb0E5u656h|cRba0E5u656h|cUtq0y636c6f|cY451a5M626i6j6k6m|cZ431c5M626i6j6k6m|c_29285a5S6n|d5tj0q686o6p6x|d77s165O5P5X5Y5-6R6Y6-|d97s175O5P5X5Y5-6R6Y6-|d9uP0G5_686v|da7s185O5P5X5Y5-6R6Y6-|dvb90h66676r6K|dB322k5D696a6q|dC312l5D696a6q|dD302m5D696a6q|dDkE025E6F6G6H6M6N6O|dE302n5D696a6q|dN2c2M6b6q|d-sp0K6c6s6t6u6x|d-sq0K6c6s6t6u6x|e32D2T6i6j6k6m6n6E|e4aG0B6h6z6A6B6C6D6K|e4q92w606o6p6y|e4qa2v606o6p6y|e5q82x606o6p6y|e8us0V6f6x|e98T1J5U616W6Z|e9tv0J6c6o6p6v|eepI2V5H6s6t6u6L|eia80_656r6W6Z|eia90_656r6W6Z|eja611656r6W6Z|eka412656r6W6Z|ela314656r6W6Z|es2T2Q6q6I6V6X|eGgN3L5V6l6J|eHgM3L5V6l6J|eJgM3M5V6l6J|eO3i2K6E7c7d7D|ePgK3P6F6G6H6M6N6O6U|eQd61K6h6r6P6Q6S6T70|eYpy2Y6y7a7e|f2gW3G6l6J7f7g7i7j7k|f4gY3F6l6J7f7g7i7j7k|f5gZ3F6l6J7f7g7i7j7k|fedK2k6K6_70717273|fgdO2n6K6_70717273|fh7P2X5L5N6d6e6g7b|fhdO2o6K6_70717273|fidR2q6K6_70717273|fkf3346J747C|fo271I6E7D|fo8g2Z6w6z6A6B6C6D79|fp261H6E7D|fp7R335L5N6d6e6g7b|fr8d316w6z6A6B6C6D79|fs7S365L5N6d6e6g7b|fs9n1Y6P6Q6S6T737879|fse42G6K6P6Q6S6T7374|ft9a286P6Q6S6T737879|fu952d6P6Q6S6T737879|fud_2B6P6Q6S6T6_70717278|fueg2N6U70757677|fweg2M747z8k8l8m|fxef2L747z8k8l8m|fyef2K747z8k8l8m|fzdP2p6_7172737h|fA853b6W6Z6_71727b|fJpR2H6L7m7n7o7Z7-7_|fK7V3m6R6Y6-797y|fT5m2M6I7p7q7r7s7t7u7v7w7x85|fT5o2N6I7p7q7r7s7t7u7v7w7x85|fWm91b6L7l7m7n7o|g2hF3g6M6N6O7l8c|g2hG3h6M6N6O7l8c|g3cQ1q787E7F7G7H7I7J7K7L7M7N|g3hH3g6M6N6O7l8c|g4hI3g6M6N6O7l8c|g5hI3g6M6N6O7l8c|g8hL3f7e7f7g7i7j7k7O7Q7R7S7T|giq12B7a7e7Z7-7_|gjq22B7a7e7Z7-7_|gkq22B7a7e7Z7-7_|gn6N3t627c7d7y|gn6O3u627c7d7y|gp6R3w627c7d7y|gp6S3x627c7d7y|gq6U3y627c7d7y|gq6W3z627c7d7y|gr6X3A627c7d7y|gs6-3C627c7d7y|gs6_3C627c7d7y|gC7o3U7b7p7q7r7s7t7u7v7w7x7P|hncQ0H7576777U8e|hu0I0q7B7W|hv0a0t7A7V7W|hveS106U8f8G8K|hx1S0o6I6V6X7X|hza61A7h7H7I7J7K7L7M7N7U|hzab1v7h7H7I7J7K7L7M7N7U|hzac1v7h7H7I7J7K7L7M7N7U|hB8Y2D7h7E7F7G7P|hB8Z2C7h7E7F7G7P|hB912y7h7E7F7G7P|hC8T2I7h7E7F7G7P|hC8V2G7h7E7F7G7P|hC922y7h7E7F7G7P|hD8R2K7h7E7F7G7P|hJhv257l8c8E|hK7G3Q7y7H7I7J7K7L7M7N7Y|hKhu237l8c8E|hLhu237l8c8E|hMht217l8c8E|hNht217l8c8E|hObJ0r7z7E7F7G8a|hU1c0b7B7W|hX1g0c7A7B7V7X878889|hX1Q0r7D7W84|id7H3N7P8182838N8O8R|ierj2U7a7m7n7o8s|ifrk2U7a7m7n7o8s|igrk2U7a7m7n7o8s|ihlD088d8p|ik7C3I7Y8oac|il7B3H7Y8oac|in7A3G7Y8oac|io2i0E7X8586|iC2E0N7c7d848g8y8H8I8J|iI1C04848788898j|iK1b097W868i|iL1b097W868i|iM1b0a7W868i|iRcf0F7U8e8N8O8R|iRlf0g8d8p|iVfc0o7f7g7i7j7k7O7Q7R7S7T8f|iVmk0N808b8h8p8A|iYcC0T7z8a8k8l8m|i-f70u7C8c8G8K|i_2E0E858H8I8J|j0ml0P8d8p8D|ja150s8788898j8n|jc1f0n868i8H8I8J|jdd1117576778e8W|jed2117576778e8W|jfd3127576778e8W|jg0L0z8i8V|jj6U358182838M8P8Qaqaras|jllp0C808b8d8h8q8r|jtli0C8p8F9g9h9i|julh0C8p8F9g9h9i|jysr3B7Z7-7_8t8u8v8w8x8Z8-8_90919293959798|jBsz3D8s8zaL|jBsA3E8s8zaL|jCsB3D8s8zaL|jCsC3E8s8zaL|jCsD3E8s8zaL|jJ2m0x858H8I8J|jJsV3I8t8u8v8w8x8B9y9z9A|jKnr0Y8d8D9T9Vad|jLti3K8z8Cbm|jLtj3K8Bb1b3|jOn30_8h8A9l|jUg-187O7Q7R7S7T8S8T8U8X8Y9u9O9P|jVkQ0F8q8r8L9m|jWe61q7C8f8W|jX1R0o858g8j8y94|jX1S0p858g8j8y94|jX1T0p858g8j8y94|jXe51r7C8f8W|j_kQ0F8F999g9h9i|k3602v8o9nak|k3a31O7Y8a96|k4a41N7Y8a96|k55Z2u8o9nak|k55-2u8o9nak|k5a51N7Y8a96|k5gN0T8E9C9D9E9F9G9H|k5gO0U8E9C9D9E9F9G9H|k7gL0R8E9C9D9E9F9G9H|k90I0b8n949d|k9dT1G8k8l8m8G8K9j|kagI0N8E9C9D9E9F9G9H|kbgH0M8E9C9D9E9F9G9H|kfrm2t8s9xahaiaj|kfrn2u8s9xahaiaj|khrj2q8s9xahaiaj|kirh2o8s9xahaiaj|kiri2p8s9xahaiaj|kjrf2m8s9xahaiaj|kjrg2n8s9xahaiaj|kk1c0d8H8I8J8V9d|kkre2l8s9xahaiaj|klak1H8N8O8R9a9b9c9e9j|klrd2k8s9xahaiaj|kmrb2i8s9xahaiaj|kokK0D8L9k9w|ksam1B969f9K9L9M|ktan1B969f9K9L9M|kuan1A969f9K9L9M|kv0r0q8V94|kvao1z969f9K9L9M|kAao1v9a9b9c9eacao|kDmc0L8q8r8L9k|kEmd0M8q8r8L9k|kFmd0N8q8r8L9k|kGdX1P8W969I|kJmh0S999g9h9i9l|kLmn0X8D9k9V|kRjC0T8F9w9N9Q9R|kW511-8M8P8Q9o9p9q9r9s9B|kY4T1U9n9t9vam|kY4U1U9n9t9vam|kZ4Q1S9n9t9vam|kZ4R1S9n9t9vam|kZ4S1T9n9t9vam|l93X1g9o9p9q9r9sabaf|l9hr0m8E9P|la3U1f9o9p9q9r9sabaf|lek516999m9Q|lepr138Z8-8_909192939597989Jahaiaj|llsj238z9W9Y9Za1a3aXaYaZ|lnsi218z9W9Y9Za1a3aXaYaZ|lqsh1-8z9W9Y9Za1a3aXaYaZ|ly541u9nagaIaJaK|lyfZ0J8S8T8U8X8Y9D9E9F9G9H9P|lAfO0R8S8T8U8X8Y9C9S|lAfP0Q8S8T8U8X8Y9C9S|lAfQ0Q8S8T8U8X8Y9C9S|lBfM0T8S8T8U8X8Y9C9S|lCfL0U8S8T8U8X8Y9C9S|lOe12a9j9K9L9M9S|lOol0T9x9Tan|lSdQ219a9b9c9e9Ia4|lSdR229a9b9c9e9Ia4|lTdO209a9b9c9e9Ia4|lUiF0f9m9Q|lVh20g8E9P|lXgZ0g8E9u9C9OaD|m5kc1l9m9w9N9R9V|m7iM0m9m9Q|m8es259D9E9F9G9H9Iat|m9nP168A9J9U9Vad|mjnI1d9Tadav|mmmp1u8A9l9Q9Tad|mys20W9y9z9Ab5b6b7b8b9bg|mzv11xb1b3bIbJ|mAs10U9y9z9Ab5b6b7b8b9bg|mBs10T9y9z9Ab5b6b7b8b9bg|mBv01vb1b3bIbJ|mDu_1tb1b3bIbJ|mEu_1sb1b3bIbJ|mFs10P9y9z9Ab5b6b7b8b9bg|mGu-1qb1b3bIbJ|mHs10O9y9z9Ab5b6b7b8b9bg|mJcJ189K9L9MaSaTbF|mJuZ1nb1b3bIbJ|mK280Ba7a8a9aaaba_|mK2s0Ga6aaaQ|mK2t0Ga6aaaQ|mK2u0Ga6aaaQ|mL2y0Ha6a7a8a9abaU|mL2B0H9t9va6aaaf|mM9c0U8182839fae|mMn01E8A9T9U9VaEaFaG|mX980Xacauaz|mY390C9t9vabam|mZ5W0f9BapaM|m-pL0C8Z8-8_909192939597989xan|m_pL0D8Z8-8_909192939597989xan|n1pL0E8Z8-8_909192939597989xan|n46U0D8M8P8Qalap|n5770AakaqarasbO|n73q0B9o9p9q9r9safaIaJaK|n7pK0J9JahaiajaC|n8az0n9fazaSaT|nb6I0CagakaM|nd8q0I8oalau|nd8r0J8oalau|nd8s0J8oalau|nef21V9SaDbS|nh8S0-aeaqarasawaxayaAaBaWblbn|njp40k9UaCaEaFaG|nk8S0Yaublbn|nl8S0Yaublbn|nn8S0Xaublbn|nna00baeaobabcbd|no8S0Waublbn|np8S0Waublbn|nqpO0UanavaL|nsfl1Q9PataV|nsnx18adavaH|nunA15adavaH|nvnA15adavaH|nznD12aEaFaGaNaOaPbr|nF4u0E9BamaM|nF4v0D9BamaM|nF4w0D9BamaM|nHq2138t8u8v8w8xaCaXaYaZ|nK5F0wagapaIaJaK|nLnV0PaHaRbp|nLnW0OaHaRbp|nNnY0MaHaRbp|nV2402a7a8a9aUa_|nWoe0AaNaOaPb2bhbibj|nZbk0Oa4aobabcbd|n-bk0Pa4aobabcbd|o22801aaaQa-a_be|o7fU1uaDb0c5|oc9W0kaublbn|ogqk1n9y9z9AaLb2|ohqk1n9y9z9AaLb2|oiql1p9y9z9AaLb2|ol2503aUa_|on1K02a6aQaUa-be|oogk1daVbGbR|optw0O8C9X9-9_a0a2a5bE|orqn1taRaXaYaZb4|ortv0Q8C9X9-9_a0a2a5bE|otqo1ub2b5b6b7b8b9bb|oyqP1j9W9Y9Za1a3b4bf|ozqQ1j9W9Y9Za1a3b4bf|ozqS1i9W9Y9Za1a3b4bf|oAqU1h9W9Y9Za1a3b4bf|oAqV1h9W9Y9Za1a3b4bf|oCbb1bazaSaTblbn|oCqj1pb4bkbN|oEbb1cazaSaTblbn|oFbb1dazaSaTblbn|oI1T03aUa_|oUrS1fb5b6b7b8b9bgc6|oXsj1n9W9Y9Za1a3bfbm|p3oW0zaRbkbo|p3oX0AaRbkbo|p3oY0AaRbkbo|p4pf0Ibbbhbibjca|p6ba1xauawaxayaAaBaWbabcbdbF|p6sM1A8BbgbqbtbubvbxbybAbB|p7bb1yauawaxayaAaBaWbabcbdbF|p7ov0CbhbibjbpbQ|p8on0DaNaOaPbobs|p8sO1AbmbCbDc6|p9nY0taHbzbV|p9ol0DbpbzbL|p9sP1AbmbCbDc6|p9sQ1BbmbCbDc6|pasR1BbmbCbDc6|pbjg0PbGbZ|pbsS1BbmbCbDc6|pbsT1CbmbCbDc6|pcoc0BbrbsbH|pcsU1CbmbCbDc6|pcsV1DbmbCbDc6|pdsV1DbqbtbubvbxbybAbBbDbXc6|pdsW1DbqbtbubvbxbybAbBbCbEbX|pfta1Db1b3bDbIbJ|pobk1Ha4blbncb|pvhw0Cb0bwbUbWbZ|pxo80nbzbPc0|pEtX1G9X9-9_a0a2a5bEbK|pFtY1G9X9-9_a0a2a5bEbK|pMu71IbIbJcn|pVos02bsbMbQ|p_oq03bLbPc3|q2qa0nbbc1cd|q56E1JalbTdg|q5o707bHbMc0|q6oA02bobLc3|q8gH0sb0bWc5|qbe00Datchcs|qi5J1RbOcgdz|qjkr1YbGbZ|qjmC1cbrb-cc|qmhz0WbGbRcL|qmse0DbCbDc8do|qn1j0Tcg|qnku20bwbGbUb_c2|qnmo1jbVb_cG|qolA1NbZb-ej|qon_03bHbPc9|qppD03bNc4cd|qrkr1_bZcIep|qrol03bMbQc7|qvpj02c1cick|qwfG0PaVbRch|qwqX02bfbqbtbubvbxbybAbBbCce|qxog02c3c9ca|qxrL09bXcjcq|qAo003c0c7cc|qDoi02bkc7cl|qFbh27bFcsdh|qGnX05bVc9cD|qHpv03bNc1cf|qKqT03c6cmcu|qLpu03cdcicp|qO2P1-bTbYcCcEcFcK|qOft10bSc5cO|qOpq03c4cfco|qRrg03c8cmcq|qUp203c4cocr|qWov03cacrcN|qXqY02cecjcx|qXu517bKcvcA|q_pe03cickcz|r0pK04cfcycz|r0ri02c8cjcx|r1oG02ckclcH|r4cn1JbScbct|r9cq1Fcsc-dR|raqo04cecwdbdcddde|rat_11cncBdqdrds|rbqj03cucycS|rdq_02cmcqdSdTd_e0e1|repZ03cpcwcJ|rfpf02cocpcZ|rgvA0bcndqdrds|ritW0ZcvdodE|rj2A1KcgdudvdJ|rjny05cccGcM|rk2z1JcgdudvdJ|rm2y1JcgdudvdJ|rmn90hb-cDcX|rmoA02crcNcW|rniO1Fc2cLeZ|rnpU03cycPcQ|ro2x1IcgdudvdJ|rqif1EbWcIcV|rsnA02cDd7da|rtoe03clcHd7|rufy1ichcRcTcU|rxpG03cJcQd8|rxpZ04cJcPcS|ryfF1fcOc_d0d1d2d3d5d6dV|ryp_05cwcQd4|rEfe19cOc-dA|rFfd19cOc-dA|rFh-1xcLd9dj|rHoD03cHcZdn|rIm-05cGcYdl|rKn103cXdadp|rKoV04czcWd8|rLeg0UctcTcUdy|rLgu11cRd9dG|rLgv11cRd9dG|rMgy11cRd9dG|rMgz11cRd9dG|rMgB11cRd9dG|rMq202cSd8dbdcddde|rNgD11cRd9dG|rNgE12cRd9dG|rNnZ03cMcNdk|rPpm03cPcZd4|rQgV15cVc_d0d1d2d3d5d6dP|rRnw0dcMcYdf|rRqr0icud4dSdTe3e5|rSqs0jcud4dSdTe3e5|rTqs0jcud4dSdTe3e5|rUqs0kcud4dSdTe3e5|rVnv0cdadmdHdIdLdMdN|r-9u2nbOdhdi|r-9x2ocbdgeq|r_9u2ndgdxez|s2hY1bcVef|s3nR03d7dmdn|s4my0acXdpei|s9nJ03dfdke9|s9ot03cWdkewey|s9s91lbXcBdU|semN08cYdldHdIdLdMdN|sfvv0GcvcAdt|sgvw0HcvcAdt|shvx0IcvcAdt|sjvz0LdqdrdsdF|so1O1AcCcEcFcKet|sp1N1AcCcEcFcKet|sG5M0qdCdDdJdQ|sG8o1sdidzebed|sGdV06c-dAdX|sH6w0LbTdxdB|sJex05cTcUdydK|sO6n0KdzdCdDeo|sP6k0JdwdBdZ|sP6l0JdwdBdZ|sPue0DcBdFdO|sPv20FdtdEdO|sQgh04c_d0d1d2d3d5d6dPdV|sSnk0Ddfdpe6|sTnk0Ddfdpe6|sU4a0gcCcEcFcKdwe7|sUex08dAdYd-|sUnk0Edfdpe6|sVnl0Fdfdpe6|sWnl0Gdfdpe6|t6tI0OdEdFe2|tch20dd9dGea|tg5n0bdwdZee|tjcA0IctdWeq|tnrq22cxdbdcddded_e0e1e3e5|tnrr23cxdbdcddded_e0e1e3e5|tnrG28dod_e0e1e2|tog30wcRdGe4|tpcG0BdRdXex|tre40BdydWdY|tre80CdKdXen|ts5M0ddCdDdQek|tsfI0xdKe4ec|tsrw29cxdSdTdUe3e5|ttru29cxdSdTdUe3e5|ttrv2acxdSdTdUe3e5|tts91OdOdUeY|turt29dbdcdddedSdTd_e0e1eX|tvg20CdVd-e8|tvrs2adbdcdddedSdTd_e0e1eX|twnz1cdHdIdLdMdNe9eh|tx4p0ndJeger|tAga0Be4eaeu|tBnW1jdme6ewey|tChb0xdPe8ef|tE7S0IdxemeD|tEfj0od-eneJ|tF7R0IdxemeD|tJ520rdQegel|tJho0Gdjea|tK4M0we7eeeC|tKm81he6ei|tNlF1odlehej|tOlB1ob_eiep|tT5z0jdZeleB|tV5p0leeekeT|tY7t0yebedeoeN|u1eF0cdYeceJ|u26H0odBemev|u2k_1Ac2ejeZ|uaaU1CdhdRes|ub3D0be7eQeS|ucaV1CeqezeA|ue2j0jdudveF|uegh0oe8eGeHeIeL|ug6A0jeoeBe-|ujph2adne9eX|ukcJ06dWeEf2f3f4f5|ukpi2bdne9eX|ul8T0hdieseM|uoa_1yeseE|ur620jekeveP|uw4A0pegeOeS|uw8f0eebedeMeV|uwbG0WexeAf2f3f4f5|ux2p0heteQeR|uzft0oeueKeL|uAfr0peueKeL|uBfq0qeueKeL|uEfj0teceneK|uFfk0ueGeHeIeJ|uFgA0BeueGeHeI|uG8t0gezeDf1|uI7x0nemeVf8|uK540feCeTf0|uK5Z0jeBeTe-|uL340nereFeU|uM2c0deFf7|uM4m0rereCe_|uM5r0ieleOeP|uO350neQf6f7|uT7K0neDeNeW|uX7L0meVf1|uZq22Xe3e5ewey|uZui0Qe2|v1k12dcIep|v56p0kevePf8|v84g0yeSf0f6|v84S0aeOe_|va8806eMeW|vpdf0OexeE|vrdg0QexeE|vsdh0RexeE|vtdh0SexeE|vH3S0XeUe_|vR2m0HeReU|vT6u0SeNe-";


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
	Wheels = Blocks.FindAll(x => x.IsSameConstructAs(Me) && x is IMyMotorSuspension).Select(x => x as IMyMotorSuspension).ToList();
	RemoteControl = Blocks.Find(x => x.IsSameConstructAs(Me) && x is IMyRemoteControl) as IMyRemoteControl;
	//Sensor = Blocks.Find(x => x.IsSameConstructAs(Me) && x is IMySensorBlock) as IMySensorBlock;

	
	theAntenna = Blocks.Find(x => x.IsSameConstructAs(Me) && x is IMyRadioAntenna) as IMyRadioAntenna;

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
	
	if(myTerrainTarget== new Vector3D(0,0,0)){
		
		foreach (IMyMotorSuspension Wheel in Wheels)
		{
			Wheel.SetValue<Single>("Steer override", 0);
			Wheel.SetValue<float>("Propulsion override", 0);
			Wheel.Brake = true;
			
			RemoteControl.HandBrake = true;
			
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

		spriteFrame = _drawingSurface.DrawFrame();
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
		
		Vector2 leftMyPosVector2 = new Vector2((float)pixelPosCalculated.Y - 24, (float)pixelPosCalculated.X)/8;
		Vector2 rightMyPosVector2 = new Vector2((float)pixelPosCalculated.Y + 24, (float)pixelPosCalculated.X)/8;
		DrawLine(ref spriteFrame, leftMyPosVector2, rightMyPosVector2, 6.0f, Color.Green );	
		
		
		// add the direction of the rover on the map
		int faceNumberTipRover = -1;
		Vector3D shipForwardVectorTip = 8*1024*RemoteControl.WorldMatrix.Forward+Me.GetPosition();
		
		Point pointShipForwardVector = new Point(0,0);
		faceAndPointOnPlanetsCalculated( RemoteControl,out faceNumberTipRover,out pointShipForwardVector,true,shipForwardVectorTip);
		Echo("shipForwardVectorTip:"+Vector3D.Round(shipForwardVectorTip,3));
		Echo("pointShipForwardVector:"+pointShipForwardVector);
		
		// Vector2 leftMyRoverTipVector2 = new Vector2((float)pointShipForwardVector.Y - 24, (float)pointShipForwardVector.X)/8;
		// Vector2 rightMyRoverTipVector2 = new Vector2((float)pointShipForwardVector.Y + 24, (float)pointShipForwardVector.X)/8;
		Vector2 leftMyRoverTipVector2 = new Vector2((float)pixelPosCalculated.Y, (float)pixelPosCalculated.X)/8;
		Vector2 rightMyRoverTipVector2 = new Vector2((float)pointShipForwardVector.Y, (float)pointShipForwardVector.X)/8;
		// DrawLine(ref spriteFrame, leftMyRoverTipVector2, rightMyRoverTipVector2, 6.0f, Color.Red   );
		DrawLine(ref spriteFrame, leftMyRoverTipVector2, rightMyRoverTipVector2, 3.0f, Color.Green   );
		
		
		
		// x 0 y 0 w 256 h 256
	
	
		Echo("_viewport:"+_viewport);
		// DrawSprites(ref spriteFrame);
		spriteFrame.Dispose();
		
		
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


