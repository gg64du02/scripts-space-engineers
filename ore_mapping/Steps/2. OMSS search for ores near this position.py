
# load and display an image with Matplotlib
from matplotlib import image
from matplotlib import pyplot

import numpy as np
import os

import array as arr


folder_planetsfiles = '2. OSMS_data/star system stock'

whichPlanetIsTheClosestStr = ""


# Triton
myCurrentPosArray =  np.asarray([-283368.449761682,-2400056.0174512,348238.53512947])
# Moon
# myCurrentPosArray =  np.asarray([13006.69678417,145040.712265219,-115439.960197295])

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

            print(positionPlanetCenter)

            strPlanet = substrings[1]


    return positionPlanetCenter,strPlanet


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
    print("planetsGPSString:",planetsGPSString)
    planetCenterArray,planetCenterString = parseGPS(planetsGPSString)
    diffmyCurrentPosArrayPlanetCenter = np.subtract(planetCenterArray,myCurrentPosArray)
    if(np.linalg.norm(diffmyCurrentPosArrayPlanetCenter, ord=3)<100000):
        print(planetCenterString + " should the detected planet")
        break
    print()