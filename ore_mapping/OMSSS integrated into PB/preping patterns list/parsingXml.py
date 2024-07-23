
linkToXmlOreRules = "https://raw.githubusercontent.com/KeenSoftwareHouse/SpaceEngineers/master/Sources/SpaceEngineers/Content/Data/PlanetGeneratorDefinitions.sbc"
# =============================================
# =============================================
# =============================================

import xml.etree.ElementTree as ET

# file from https://github.com/KeenSoftwareHouse/SpaceEngineers/blob/master/Sources/SpaceEngineers/Content/Data/PlanetGeneratorDefinitions.sbc
mytree = ET.parse("PlanetGeneratorDefinitions.sbc")
myroot = mytree.getroot()
# print(myroot)

i = 0
previousGPScoords = [0,0,0]

FeValuesList = []
NiValuesList = []
SiValuesList = []
CoValuesList = []
AgValuesList = []
MgValuesList = []
UrValuesList = []
AuValuesList = []
PtValuesList = []

# print("myroot:",myroot[1])
# print(myroot)

# for explor in myroot:
#     print("explor:",explor)
#     for y in explor:
#         print(y.tag, y.attrib)

# myroot[0] is EarthLike ?
# for x in myroot[0]:

# myroot[5] is Titan line 3894
for x in myroot[5]:
    print(x.tag, x.attrib)
    for y in x:
        if(y.tag=="Ore"):
            print(y.tag, y.attrib)
    if(x.tag == "OreMappings"):
        pass
        for y in x:
            i += 1
            # print("i:",i)
            # print(y.tag, y.attrib)
            # print(y.attrib["Value"])
            oreValue = int(y.attrib["Value"])
            oreTypeStr = y.attrib["Type"]


            oreAbrStr = ""
            if("Iron" in oreTypeStr):
                oreAbrStr = "Fe"
                # print("yyy")
                if(oreValue not in FeValuesList):
                    FeValuesList.append(oreValue)
                # print("FeValuesList:",FeValuesList)
            if("Nickel" in oreTypeStr):
                oreAbrStr = "Ni"
                if(oreValue not in NiValuesList):
                    NiValuesList.append(oreValue)
            if("Silicon" in oreTypeStr):
                oreAbrStr = "Si"
                if(oreValue not in SiValuesList):
                    SiValuesList.append(oreValue)
            if("Cobalt" in oreTypeStr):
                oreAbrStr = "Co"
                if(oreValue not in CoValuesList):
                    CoValuesList.append(oreValue)
            if("Silver" in oreTypeStr):
                oreAbrStr = "Ag"
                if(oreValue not in AgValuesList):
                    AgValuesList.append(oreValue)
            if("Magnesium" in oreTypeStr):
                oreAbrStr = "Mg"
                if(oreValue not in MgValuesList):
                    MgValuesList.append(oreValue)
            if("Uraninite" in oreTypeStr):
                oreAbrStr = "Ur"
                if(oreValue not in UrValuesList):
                    UrValuesList.append(oreValue)
            if("Gold" in oreTypeStr):
                oreAbrStr = "Au"
                if(oreValue not in AuValuesList):
                    AuValuesList.append(oreValue)
            # print("oreTypeStr:",oreTypeStr)
            if("Platinum" in oreTypeStr):
                # print("Platinum")
                oreAbrStr = "Pt"
                if(oreValue not in PtValuesList):
                    PtValuesList.append(oreValue)
            # print("oreAbrStr:",oreAbrStr)
            # else:
            #     pass
            #     # print("nnn")
        break


def whatOreThatValueIs(valueInt):
    global FeValuesList
    global NiValuesList
    global SiValuesList
    global CoValuesList
    global AgValuesList
    global MgValuesList
    global UrValuesList
    global AuValuesList
    if(valueInt in FeValuesList):
        return "Fe"
    if(valueInt in NiValuesList):
        return "Ni"
    if(valueInt in SiValuesList):
        return "Si"
    if(valueInt in CoValuesList):
        return "Co"
    if(valueInt in AgValuesList):
        return "Ag"
    if(valueInt in MgValuesList):
        return "Mg"
    if(valueInt in UrValuesList):
        return "Ur"
    if(valueInt in AuValuesList):
        return "Au"
    return "$"
print("checking xml lists")
# =============================================
# =============================================
# =============================================
print("FeValuesList:",FeValuesList)
print("NiValuesList:",NiValuesList)
print("SiValuesList:",SiValuesList)
print("CoValuesList:",CoValuesList)
print("AgValuesList:",AgValuesList)
print("MgValuesList:",MgValuesList)
print("UrValuesList:",UrValuesList)
print("AuValuesList:",AuValuesList)
print("PtValuesList:",PtValuesList)
