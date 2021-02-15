
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
            print("regionSize",regionSize)

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
                pixelWithValues.append([[
                    pointsOfCurrentDetectedLackArray[iPoint, 0]-j, pointsOfCurrentDetectedLackArray[iPoint, 1]-k],[
                                       data_ore_layer[pointsOfCurrentDetectedLackArray[iPoint,0],pointsOfCurrentDetectedLackArray[iPoint,1]]]])
            print(pixelWithValues)

            patternNUmber += 1
            print("patternNUmber",patternNUmber)


        pass