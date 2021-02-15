
# load and display an image with Matplotlib
from matplotlib import image
from matplotlib import pyplot

import numpy as np
import os

import array as arr


folder_planetsfiles = 'planets_files/Titan/'


# filename = os.path.join(folder_planetsfiles, 'back_mat.png')
filename = os.path.join('', 'back_mat.png')

# print("centerFacePosition", centerFacePosition)
print("filename:", filename)

print("Trying to load:", filename)
# load image as pixel array
data = image.imread(filename)
print(filename, "loaded")
# print("Trying to load:", filenameHeightmap)
# dataHM = image.imread(filenameHeightmap)
# print(filenameHeightmap, "loaded")

data_lack_layer = 255 * data[:, :, 0]
data_ore_layer = 255 * data[:, :, 2]
# data_HM = 255 * dataHM[:, :]

data_lack_layer = data_lack_layer.astype('int16')
data_ore_layer = data_ore_layer.astype('int16')



import xml.etree.ElementTree as ET

# file from https://github.com/KeenSoftwareHouse/SpaceEngineers/blob/master/Sources/SpaceEngineers/Content/Data/PlanetGeneratorDefinitions.sbc
mytree = ET.parse("PlanetGeneratorDefinitions.sbc")
myroot = mytree.getroot()
# print(myroot)

FeValuesList = []
NiValuesList = []
SiValuesList = []
CoValuesList = []
AgValuesList = []
MgValuesList = []
UrValuesList = []
AuValuesList = []

for x in myroot[0]:
    # print(x.tag, x.attrib)
    if(x.tag == "OreMappings"):
        pass
        for y in x:
            # i += 1
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



def centeroidnp(arr):
    length = arr.shape[0]
    sum_x = np.sum(arr[:, 0])
    sum_y = np.sum(arr[:, 1])
    return sum_x/length, sum_y/length


converted_to_bool_surface_array = np.zeros_like(data_lack_layer)
converted_to_bool_underground_array = np.zeros_like(data_lack_layer)
for j in range(2048):
    for k in range(2048):
        # if (data_lack_layer[j, k] == (constant_surface_lack)):
        #     converted_to_bool_surface_array[j, k] = 1
        # print("data_ore_layer[j,k]",data_ore_layer[j,k])
        # if(data_ore_layer[j,k] == -1):
        if (data_ore_layer[j, k] == 255):
            converted_to_bool_underground_array[j, k] = 0
        else:
            converted_to_bool_underground_array[j, k] = 1
        # if(data_ore_layer[j,k] != (constant_no_ore)):
        #     # print("lol1")
        #     converted_to_bool_underground_array[j,k] = 1
        # else:
        #     print("nope")

print("lol")

# import module
import traceback

from skimage import measure



labeled = measure.label(data_lack_layer, background=False, connectivity=1)
labeledOre = measure.label(converted_to_bool_underground_array, background=False, connectivity=1)
# print("labeled.shape:",labeled.shape)

patternNUmber = 0

tmp_region_size = 0
for j in range(128):
    for k in range(128):

        if ((converted_to_bool_underground_array[j, k] == 1)):
            # continue
            gpsNameOres = ""

            # if(j<1500):
            #     continue

            label = labeledOre[j, k]  # known pixel location
            # print("label:",label)

            rp = measure.regionprops(labeledOre)
            # todo: debug(crash): check why: props = rp[label - 1]  # background is labeled 0, not in rp IndexError: list index out of range
            props = rp[label - 1]  # background is labeled 0, not in rp
            # props.bbox  # (min_row, min_col, max_row, max_col)
            # props.image  # array matching the bbox sub-image
            # print(len(props.coords))  # list of (row,col) pixel indices
            regionSize = len(props.coords)
            # print("regionSize",regionSize)

            for pt in props.coords:
                # print("data_ore_layer[pt[0],pt[1]]:",data_ore_layer[pt[0],pt[1]]+128)
                currentPointValue = data_ore_layer[pt[0], pt[1]] + 0
                # currentPointValue = data_ore_layer[pt[0],pt[1]]+128
                # print("currentPointValue:",currentPointValue)
                ptOreValueIs = whatOreThatValueIs(currentPointValue)
                # print("ptOreValueIs:",ptOreValueIs)
                if (ptOreValueIs != "$"):
                    # print(gpsNameOres.find(ptOreValueIs))
                    if (gpsNameOres.find(ptOreValueIs) < 0):
                        gpsNameOres += whatOreThatValueIs(currentPointValue)

            pixelWithValues = []

            # removing duplicate
            pointsOfCurrentDetectedLackArray = props.coords
            # print("pointsOfCurrentDetectedLackArray",pointsOfCurrentDetectedLackArray)
            for iPoint in range(len(pointsOfCurrentDetectedLackArray)):
                converted_to_bool_underground_array[
                    pointsOfCurrentDetectedLackArray[iPoint, 0], pointsOfCurrentDetectedLackArray[iPoint, 1]] = 0
                # pixelWithValues.append([[
                #     pointsOfCurrentDetectedLackArray[iPoint, 0], pointsOfCurrentDetectedLackArray[iPoint, 1]],[
                #                        data_ore_layer[pointsOfCurrentDetectedLackArray[iPoint,0],pointsOfCurrentDetectedLackArray[iPoint,1]]]])
                # pixelWithValues.append([[
                #     pointsOfCurrentDetectedLackArray[iPoint, 0]-j, pointsOfCurrentDetectedLackArray[iPoint, 1]-k],[
                #                        data_ore_layer[pointsOfCurrentDetectedLackArray[iPoint,0],pointsOfCurrentDetectedLackArray[iPoint,1]]]])
                pixelWithValues.append([[
                    pointsOfCurrentDetectedLackArray[iPoint, 0], pointsOfCurrentDetectedLackArray[iPoint, 1]],[
                                       data_ore_layer[pointsOfCurrentDetectedLackArray[iPoint,0],pointsOfCurrentDetectedLackArray[iPoint,1]]]])
            # print(pixelWithValues)

            centroid_surface_lack = centeroidnp(pointsOfCurrentDetectedLackArray)
            centroid_surface_lack_array = arr.array('d', [centroid_surface_lack[0], centroid_surface_lack[1]])

            centroid_underground_lack = centeroidnp(pointsOfCurrentDetectedLackArray)
            centroid_underground_lack_array = arr.array('d',
                                                        [centroid_underground_lack[0], centroid_underground_lack[1]])

            # print("centroid_underground_lack_array",centroid_underground_lack_array)

            patternNUmber += 1
            # print("patternNUmber",patternNUmber)

            # print("gpsNameOres",gpsNameOres)

            # for c#
            # // Dynamic ArrayList with no size limit.
            # List<int> numberList = new List<int>();
            # numberList.Add(32);
            # print("List<String> stringList = new List<String>();")
            print("stringList.Add("+"\""+str(centroid_underground_lack_array[0])+","+str(centroid_underground_lack_array[1])+","+gpsNameOres+"\");")


        pass