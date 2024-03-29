
# load and display an image with Matplotlib
from matplotlib import image
from matplotlib import pyplot

import numpy as np
import os

import array as arr


folder_planetsfiles = '2. OSMS_data/star system stock'

whichPlanetIsTheClosestStr = ""



def parseGPS(stringGps):
    # TODO
    # positionPlanetCenter =  np.asarray([13006.69678417,145040.712265219,-115439.960197295])
    positionPlanetCenter = np.asarray([0,0,0])
    strPlanet = ""

    # print(stringGps[0:3])
    if("GPS:" in stringGps):
        # print("if(GPS: in stringGps):")
        # print(stringGps.count(":"))
        if((stringGps.count(":") == 5 ) or (stringGps.count(":") == 6 )):
            # print("if((stringGps.count(:) == 5 ) or (stringGps.count(:) == 6 )):")

            positionPlanetCenter = np.asarray([0, 0, 0])


            substrings = stringGps.split(":")
            # print(substrings)
            # print(substrings[2])
            # print(substrings[3])
            # print(substrings[4])
            intX = float(substrings[2])
            intY = float(substrings[3])
            intZ = float(substrings[4])

            positionPlanetCenter = np.asarray([intX, intY, intZ])

            # print(positionPlanetCenter)

            strPlanet = substrings[1]


    return positionPlanetCenter,strPlanet

# Triton
# myCurrentPosArray =  np.asarray([-283368.449761682,-2400056.0174512,348238.53512947])
# Moon
# myCurrentPosArray =  np.asarray([13006.69678417,145040.712265219,-115439.960197295])
# MyPos
# myCurrentPosArray,_ =  parseGPS("GPS:#31:-36112.76:6750.47:49163.05:#FF75C9F1:")
# myCurrentPosArray,_ =  parseGPS("GPS:/// #1:-2389.6:24834.15:16766.67:#FF75C9F1:")
myCurrentPosArray,_ =  parseGPS("GPS:we buy ores itdr:-4802.35:29730.55:-2251.3:#FF75C9F1:")
# myCurrentPosArray,_ =  parseGPS("GPS:/// #29:-27015.78:13238.14:1261.6:#FF75C9F1:")




# Every stock planets center
listOfPlanetsGPSString = []

listOfPlanetsGPSString.append("GPS:EarthLike:0.50:0.50:0.50:")
listOfPlanetsGPSString.append("GPS:Moon:16384.50:136384.50:-113615.50:")

listOfPlanetsGPSString.append("GPS:Mars:1031072.50:131072.50:1631072.50:")
listOfPlanetsGPSString.append("GPS:Europa:916384.50:16384.50:1616384.50:")

listOfPlanetsGPSString.append("GPS:Triton:-284463.50:-2434463.50:365536.50:")

listOfPlanetsGPSString.append("GPS:Pertam:-3967231.50:-32231.50:-767231.50:")

listOfPlanetsGPSString.append("GPS:Alien:131072.50:131072.50:5731072.50:")
listOfPlanetsGPSString.append("GPS:Titan:36384.50:226384.50:5796384.50:")

print()

for planetsGPSString in listOfPlanetsGPSString:
    print("planetsGPSString:",planetsGPSString.replace('\n',''))
    planetCenterArray,planetCenterString = parseGPS(planetsGPSString)
    diffmyCurrentPosArrayPlanetCenter = np.subtract(planetCenterArray,myCurrentPosArray)
    if(np.linalg.norm(diffmyCurrentPosArrayPlanetCenter, ord=3)<100000):
        print(planetCenterString + " should the detected planet")
        break
    print()

print("planetsGPSString",planetsGPSString)


print(folder_planetsfiles)
if(planetCenterString == ""):
    print("not near enough a planet")
    exit()


listOfOresOnThePlanet = []

# print(os.path.join(folder_planetsfiles,planetCenterString,'Triton_ores.png'))
# filename = os.path.join(folder_planetsfiles,planetCenterString,'Triton_ores.txt')
# filename = os.path.join(folder_planetsfiles,planetCenterString,planetCenterString+'_ores.txt')
filename = os.path.join(folder_planetsfiles,planetCenterString,planetCenterString+'_ores2.txt')
# filename = os.path.join(folder_planetsfiles,planetCenterString,planetCenterString+'_up_under_21.txt')
print("filename",filename)

# f = open(filename, "r")
# print(f.readline())
# f.close()

# populate the GPS String list
f = open(filename, "r")
# print(f.readline())
for x in f:
    # print(x)
    tmpParsingArray,tmpStr = parseGPS(x)
    if(tmpStr !=""):
        listOfOresOnThePlanet.append(x)
        pass
    # else:
    #     print(x)
    pass
f.close()


rangeToCheckAt = 800
# range to check ORES
listOfOresOnThePlanetNearMyPos = []
for GPSString in listOfOresOnThePlanet:
    tmpArray, tmpStr = parseGPS(GPSString)
    diffBetweenMyPosAndOreGPS = np.subtract(tmpArray,myCurrentPosArray)
    tmpDiffRangeChecking = np.linalg.norm(diffBetweenMyPosAndOreGPS, ord=3)
    if(tmpDiffRangeChecking>-rangeToCheckAt):
        if(tmpDiffRangeChecking<rangeToCheckAt):
            # print("tmpDiffRangeChecking",tmpDiffRangeChecking)
            listOfOresOnThePlanetNearMyPos.append(GPSString)
    pass

for GPSStringNearMyPos in listOfOresOnThePlanetNearMyPos:
    # print("GPSStringNearMyPos",GPSStringNearMyPos)
    # print(GPSStringNearMyPos)
    print(GPSStringNearMyPos.replace('\n',''))
    # pass

