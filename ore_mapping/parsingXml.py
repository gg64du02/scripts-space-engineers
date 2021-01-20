
linkToXmlOreRules = "https://raw.githubusercontent.com/KeenSoftwareHouse/SpaceEngineers/master/Sources/SpaceEngineers/Content/Data/PlanetGeneratorDefinitions.sbc"

print(linkToXmlOreRules)

import xml.etree.ElementTree as ET

mytree = ET.parse("PlanetGeneratorDefinitions.sbc")
myroot = mytree.getroot()
print(myroot)

i = 0

FeValuesList = []
NiValuesList = []
SiValuesList = []
CoValuesList = []
AgValuesList = []
MgValuesList = []
UrValuesList = []
AuValuesList = []

for x in myroot[0]:
    print(x.tag, x.attrib)
    if(x.tag == "OreMappings"):
        pass
        for y in x:
            i += 1
            # print("i:",i)
            print(y.tag, y.attrib)
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
                if(oreValue not in FeValuesList):
                    NiValuesList.append(oreValue)
            if("Silicon" in oreTypeStr):
                oreAbrStr = "Si"
                if(oreValue not in FeValuesList):
                    SiValuesList.append(oreValue)
            if("Cobalt" in oreTypeStr):
                oreAbrStr = "Co"
                if(oreValue not in FeValuesList):
                    CoValuesList.append(oreValue)
            if("Silver" in oreTypeStr):
                oreAbrStr = "Ag"
                if(oreValue not in FeValuesList):
                    AgValuesList.append(oreValue)
            if("Magnesium" in oreTypeStr):
                oreAbrStr = "Mg"
                if(oreValue not in FeValuesList):
                    MgValuesList.append(oreValue)
            if("Uraninite" in oreTypeStr):
                oreAbrStr = "Ur"
                if(oreValue not in FeValuesList):
                    UrValuesList.append(oreValue)
            if("Gold" in oreTypeStr):
                oreAbrStr = "Au"
                if(oreValue not in FeValuesList):
                    AuValuesList.append(oreValue)
            print("oreAbrStr:",oreAbrStr)
            # else:
            #     pass
            #     # print("nnn")
        break
print("FeValuesList:",FeValuesList)
print("NiValuesList:",NiValuesList)
print("SiValuesList:",SiValuesList)
print("CoValuesList:",CoValuesList)
print("AgValuesList:",AgValuesList)
print("MgValuesList:",MgValuesList)
print("UrValuesList:",UrValuesList)
print("AuValuesList:",AuValuesList)
