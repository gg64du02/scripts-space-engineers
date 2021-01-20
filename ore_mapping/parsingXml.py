
linkToXmlOreRules = "https://raw.githubusercontent.com/KeenSoftwareHouse/SpaceEngineers/master/Sources/SpaceEngineers/Content/Data/PlanetGeneratorDefinitions.sbc"

print(linkToXmlOreRules)

import xml.etree.ElementTree as ET

mytree = ET.parse("PlanetGeneratorDefinitions.sbc")
myroot = mytree.getroot()
print(myroot)

i = 0

for x in myroot[0]:
    print(x.tag, x.attrib)
    if(x.tag == "OreMappings"):
        pass
        for y in x:
            i += 1
            # print("i:",i)
            print(y.tag, y.attrib)
            # print(y.attrib["Value"])
            oreTypeStr = y.attrib["Type"]

            oreAbrStr = ""
            if("Iron" in oreTypeStr):
                oreAbrStr = "Fe"
                # print("yyy")
            if("Nickel" in oreTypeStr):
                oreAbrStr = "Ni"
            if("Silicon" in oreTypeStr):
                oreAbrStr = "Si"
            if("Cobalt" in oreTypeStr):
                oreAbrStr = "Co"
            if("Silver" in oreTypeStr):
                oreAbrStr = "Ag"
            if("Magnesium" in oreTypeStr):
                oreAbrStr = "Mg"
            if("Uraninite" in oreTypeStr):
                oreAbrStr = "Ur"
            if("Gold" in oreTypeStr):
                oreAbrStr = "Au"
            print("oreAbrStr:",oreAbrStr)
            # else:
            #     pass
            #     # print("nnn")
        break
