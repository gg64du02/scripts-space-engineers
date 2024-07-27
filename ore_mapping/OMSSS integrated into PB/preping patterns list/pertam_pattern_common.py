
# load and display an image with Matplotlib
from matplotlib import image
from matplotlib import pyplot

import numpy as np
import os

import array as arr


folder_planetsfiles = 'planets_files/Titan/'


filename = os.path.join('preping_pattern_pertam\original\Pertam', 'back_mat.png')
filename2 = os.path.join('preping_pattern_pertam\original\Pertam', 'front_mat.png')

filename3 = os.path.join('preping_pattern_pertam\original\Pertam', 'back_front_mat_and_ore.png')




# print("centerFacePosition", centerFacePosition)
print("filename:", filename)
print("filename2:", filename2)
print("filename3:", filename3)

print("Trying to load:", filename)
print("Trying to load:", filename2)
print("Trying to load:", filename3)
# load image as pixel array
data = image.imread(filename)
print(filename, "loaded")
data2 = image.imread(filename2)
print(filename2, "loaded")
print(filename3, "to be written")
# print("Trying to load:", filenameHeightmap)
# dataHM = image.imread(filenameHeightmap)
# print(filenameHeightmap, "loaded")

data_lack_layer = 255 * data[:, :, 0]
data_ore_layer = 255 * data[:, :, 2]
# data_HM = 255 * dataHM[:, :]

data_lack_layer = data_lack_layer.astype('int16')
data_ore_layer = data_ore_layer.astype('int16')

data_lack_layer2 = 255 * data2[:, :, 0]
data_ore_layer2 = 255 * data2[:, :, 2]
data_lack_layer2 = data_lack_layer2.astype('int16')
data_ore_layer2 = data_ore_layer2.astype('int16')


import xml.etree.ElementTree as ET

# file from https://github.com/KeenSoftwareHouse/SpaceEngineers/blob/master/Sources/SpaceEngineers/Content/Data/PlanetGeneratorDefinitions.sbc
# mytree = ET.parse("PlanetGeneratorDefinitions.sbc")
mytree = ET.parse("Pertam.sbc")
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
PtValuesList = []


# myroot[5] is Titan line 3894
# for x in myroot[5]:
for x in myroot[0][0]:
    print(x.tag, x.attrib)
    for y in x:
        if(y.tag=="Ore"):
            print(y.tag, y.attrib)
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
    if(valueInt in PtValuesList):
        return "Pt"
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
            # print("hit")
            # print("hi1",j,k)

        if (data_ore_layer2[j, k] == 255):
            converted_to_bool_underground_array[j, k] = 0
        else:
            converted_to_bool_underground_array[j, k] = 1
            # print("hit2")
            # print("hi2",j,k)

        # if(data_ore_layer2[j, k]!=data_ore_layer[j, k]):
        #     print("oi filtered")

print("passed")


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
            # print("hit")
            # print("hi1",j,k)

        if (data_ore_layer2[j, k] == 255):
            converted_to_bool_underground_array[j, k] = 0
        else:
            converted_to_bool_underground_array[j, k] = 1
            # print("hit2")
            # print("hi2",j,k)

        # if(data_ore_layer2[j, k]!=data_ore_layer[j, k]):
        #     print("oi ",j,k, "filtered", data_ore_layer[j, k], data_ore_layer2[j, k])

import png

width = 2048
height = 2048
# width = 2048
# height = 2048
img = []
for y in range(512,1024):
    row = ()
    for x in range(512,1024):
        # pixel = (0,0,0)
        pixel = (0,0,255)
        if(x==0):
            if(y==0):
                pixel= (255, 255, 255)
        #row = row + (x, max(0, 255 - x - y), y)
        # if (data_ore_layer[j, k] == 255):
        #     if (data_ore_layer2[j, k] == 255):
        # if (data_ore_layer[y, x] != data_ore_layer2[y, x]):
        if (data_ore_layer[y, x] == data_ore_layer2[y, x]):
            # pixel= (0, 0, 255)
            # pixel= (255, 255, 255)
            pixel= (0, 0, data_ore_layer2[y, x])
            # print("pixel white", data_ore_layer[y, x], data_ore_layer2[y, x])
            if(data_ore_layer2[y, x] == 168 or data_ore_layer2[y, x] == 172 or data_ore_layer2[y, x] == 176 or data_ore_layer2[y, x] == 180 or data_ore_layer2[y, x] == 184  or data_ore_layer2[y, x] == 188):
                print("gold")
        row = row + pixel

    img.append(row)
with open('prepped_pertam.png', 'wb') as f:
    w = png.Writer(512, 512, greyscale=False)
    w.write(f, img)


exit()
